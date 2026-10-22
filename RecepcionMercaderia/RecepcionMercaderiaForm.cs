using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GrupoC.RecepcionMercaderia
{

    public partial class RecepcionMercaderiaForm : Form
    {
        // N° de remito: 13 números con un guion después del 5° (punto de venta - comprobante).
        private const int DigitosRemito = 13;
        private const int DigitosPuntoDeVenta = 5;


        private static readonly Color ColorCampoConError = Color.FromArgb(253, 231, 233);
        private static readonly Color ColorFilaFaltante = Color.FromArgb(255, 244, 206);
        private static readonly Color ColorFilaExcedente = Color.FromArgb(222, 236, 249);

        private static readonly Regex FormatoRemito = new Regex(@"^\d{5}-\d{8}$");
        private static readonly Regex FormatoPatente = new Regex(@"^([A-Z]{3}\d{3}|[A-Z]{2}\d{3}[A-Z]{2})$");
        private static readonly Regex SoloLetrasYEspacios = new Regex(@"^[\p{L} ]+$");
        private static readonly Regex VariosEspacios = new Regex(@"\s+");

        private readonly RecepcionModelo modelo = new RecepcionModelo();
        private readonly HashSet<Control> controlesConError = new HashSet<Control>();
        private readonly ToolTip ayuda = new ToolTip { AutoPopDelay = 15000, InitialDelay = 400, ReshowDelay = 100 };
        private readonly ToolTip aviso = new ToolTip();


        private readonly string ejemploObservaciones;

        private Cliente? clienteActual;
        private Control? ultimoCampoActivo;
        private bool cierreConfirmado;
        private bool procesandoCambioDeCliente;
        private bool formateandoRemito;

        public RecepcionMercaderiaForm()
        {
            InitializeComponent();
            ejemploObservaciones = textBoxObsRem.Text;
            PrepararRecuadroRemito();
            ConfigurarCampos();
            ConectarEventos();
            Disposed += (s, e) =>
            {
                ayuda.Dispose();
                aviso.Dispose();
            };
        }

        #region Inicialización

        private void ConectarEventos()
        {
            Load += RecepcionMercaderiaForm_Load;
            Shown += RecepcionMercaderiaForm_Shown;
            FormClosing += RecepcionMercaderiaForm_FormClosing;

            // Cliente: solo letras; se elige de la lista o se escribe y el sistema lo busca.
            comboBoxClieR.KeyPress += PermitirSolo(EsLetraOEspacio, "Cliente: solo se admiten letras.");
            comboBoxClieR.SelectionChangeCommitted += comboBoxClieR_SelectionChangeCommitted;
            comboBoxClieR.Validating += comboBoxClieR_Validating;

            // N° Remito: solo números; el guion se agrega solo después del 5° número.
            textBoxNRem.KeyPress += PermitirSolo(EsDigito, "N° Remito: solo números. El guion se agrega solo después del 5° número.");
            textBoxNRem.TextChanged += (s, e) => FormatearNumeroDeRemito();
            textBoxNRem.Validating += textBoxNRem_Validating;

            // Transportista y Nombre y Apellido: solo letras (y espacios entre palabras).
            textBoxTransRem.KeyPress += PermitirSolo(EsLetraOEspacio, "Transportista: solo se admiten letras.");
            textBoxTransRem.TextChanged += (s, e) => QuitarCaracteresNoPermitidos(textBoxTransRem, EsLetraOEspacio);
            textBoxNARem.KeyPress += PermitirSolo(EsLetraOEspacio, "Nombre y Apellido: solo se admiten letras.");
            textBoxNARem.TextChanged += (s, e) => QuitarCaracteresNoPermitidos(textBoxNARem, EsLetraOEspacio);

            // Patente: letras y números (AAA123 o AA123AA).
            As.KeyPress += PermitirSolo(EsLetraODigitoAscii, "Patente: solo letras y números, sin espacios ni guiones.");
            As.TextChanged += (s, e) => QuitarCaracteresNoPermitidos(As, EsLetraODigitoAscii);

            // SKU: letras, números y guion.
            textBoxSKURem.KeyPress += PermitirSolo(EsCaracterDeSku, "SKU/ Código: solo letras, números y guion.");
            textBoxSKURem.TextChanged += (s, e) => QuitarCaracteresNoPermitidos(textBoxSKURem, EsCaracterDeSku);

            // Cantidades: solo números.
            textBoxCatnCRem.KeyPress += PermitirSolo(EsDigito, "Cant. Contada: solo se admiten números.");
            textBoxCatnCRem.TextChanged += (s, e) => QuitarCaracteresNoPermitidos(textBoxCatnCRem, EsDigito);
            textBoxCanRem.KeyPress += PermitirSolo(EsDigito, "Cant. Remito: solo se admiten números.");
            textBoxCanRem.TextChanged += (s, e) => QuitarCaracteresNoPermitidos(textBoxCanRem, EsDigito);

            // Enter avanza: SKU -> Cant. Contada -> Cant. Remito -> agrega el ítem.
            textBoxSKURem.KeyDown += (s, e) => AvanzarConEnter(e, textBoxCatnCRem);
            textBoxCatnCRem.KeyDown += (s, e) => AvanzarConEnter(e, textBoxCanRem);
            textBoxCanRem.KeyDown += textBoxCanRem_KeyDown;

            textBoxObsRem.Enter += textBoxObsRem_Enter;


            foreach (TextBox campo in CamposDeTexto())
            {
                campo.TextChanged += (s, e) => QuitarMarcaDeError(campo);
            }


            foreach (Control campo in CamposDeEntrada())
            {
                campo.Enter += (s, e) => ultimoCampoActivo = campo;
            }
            Remito.Enter += Remito_Enter;


            buttonAgregIRem.Click += buttonAgregIRem_Click;
            buttonQuitIRem.Click += buttonQuitIRem_Click;
            CancRem.Click += CancRem_Click;
        }

        private void RecepcionMercaderiaForm_Load(object? sender, EventArgs e)
        {

            MinimumSize = Size;

            ReiniciarFechaDelRemito();

            // Cliente: solo los que figuran en el sistema con contrato vigente.
            comboBoxClieR.Items.Clear();
            foreach (Cliente cliente in modelo.ObtenerClientesConContratoVigente())
            {
                comboBoxClieR.Items.Add(cliente);
            }
            comboBoxClieR.SelectedIndex = -1;
            comboBoxClieR.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxClieR.AutoCompleteSource = AutoCompleteSource.ListItems;

            var transportistas = new AutoCompleteStringCollection();
            transportistas.AddRange(modelo.TransportistasFrecuentes.ToArray());
            textBoxTransRem.AutoCompleteCustomSource = transportistas;
            textBoxTransRem.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxTransRem.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBoxSKURem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxSKURem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ActualizarAyudaDeSku(null);

            CargarAyudas();
        }

        private void RecepcionMercaderiaForm_Shown(object? sender, EventArgs e)
        {
            // Excepción paso 3: no hay clientes con contrato vigente.
            if (comboBoxClieR.Items.Count == 0)
            {
                Cartel.Informar(this, "Recepción de mercadería",
                    "No hay clientes con contrato vigente para registrar recepciones.",
                    TipoMensaje.Advertencia);
                cierreConfirmado = true;
                Close();
                return;
            }

            comboBoxClieR.Focus();
        }


        private void PrepararRecuadroRemito()
        {
            Remito.Items.Clear();
            Remito.Items.Add("Remito");
            Remito.SelectionMode = SelectionMode.None;
            Remito.TabStop = false;
        }


        private void ConfigurarCampos()
        {
            textBoxNRem.MaxLength = DigitosRemito + 1; // 13 números + el guion
            textBoxTransRem.MaxLength = 60;
            textBoxNARem.MaxLength = 60;
            As.MaxLength = 7;
            As.CharacterCasing = CharacterCasing.Upper;
            textBoxSKURem.MaxLength = 20;
            textBoxSKURem.CharacterCasing = CharacterCasing.Upper;
            textBoxCatnCRem.MaxLength = 6;
            textBoxCanRem.MaxLength = 6;
            textBoxObsRem.MaxLength = 200;


            listViewRem.ShowItemToolTips = true;

            // Cancelar no valida campos: siempre debe poder salir.
            CancRem.CausesValidation = false;
        }

        private void CargarAyudas()
        {
            ayuda.SetToolTip(comboBoxClieR, "Elija el cliente que envía la mercadería (solo clientes con contrato vigente). Solo letras.");
            ayuda.SetToolTip(textBoxNRem, "13 números: 5 del punto de venta y 8 del comprobante. El guion se agrega solo. Ej.: 00001-00012345");
            ayuda.SetToolTip(textBoxTransRem, "Empresa de transporte que entrega la mercadería. Solo letras.");
            ayuda.SetToolTip(dateTimePickerFRem, "Fecha que figura en el remito. No puede ser posterior a hoy.");
            ayuda.SetToolTip(textBoxNARem, "Nombre y apellido del chofer. Solo letras.");
            ayuda.SetToolTip(As, "Patente del vehículo. Formato AAA123 o AA123AA.");
            ayuda.SetToolTip(textBoxCatnCRem, "Unidades contadas físicamente en el área de Descarga (0 o más). Solo números.");
            ayuda.SetToolTip(textBoxCanRem, "Unidades que indica el remito para ese SKU (mayor a 0). Solo números. Enter agrega el ítem.");
            ayuda.SetToolTip(buttonAgregIRem, "Agrega el ítem al detalle de mercadería recibida.");
            ayuda.SetToolTip(buttonQuitIRem, "Quita el último ítem agregado al detalle.");
            ayuda.SetToolTip(textBoxObsRem, "Opcional. Por ejemplo: cajas golpeadas, embalaje abierto, faltantes visibles.");
            ayuda.SetToolTip(CancRem, "Descarta los datos cargados y cierra la pantalla.");
            ayuda.SetToolTip(ConfRem, "Registra la recepción y genera la orden de recepción.");
        }

        #endregion

        #region Handlers conectados en el Designer (no cambiar los nombres)

        // Remito (ListBox de fondo del recuadro): no se usa para seleccionar.
        private void listBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (Remito.SelectionMode != SelectionMode.None && Remito.SelectedIndex >= 0)
            {
                Remito.ClearSelected();
            }
        }

        // CantRem ("Cant. Remito"): clic en la etiqueta lleva al campo.
        private void label10_Click(object? sender, EventArgs e)
        {
            textBoxCanRem.Focus();
        }

        // CantContRem ("Cant. Contada"): clic en la etiqueta lleva al campo.
        private void label11_Click(object? sender, EventArgs e)
        {
            textBoxCatnCRem.Focus();
        }

        // SKUREM ("SKU/ Código"): clic en la etiqueta lleva al campo.
        private void label7_Click_1(object? sender, EventArgs e)
        {
            textBoxSKURem.Focus();
        }

        // DetMercRRem ("Detalle de Mercaderia Recibida"): clic en la etiqueta lleva al detalle.
        private void label8_Click(object? sender, EventArgs e)
        {
            listViewRem.Focus();
        }

        // listViewRem: la selección no se usa; "- Quitar Ítem" siempre quita el último agregado.
        private void listView1_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }

        // ConfRem (Confirmar).
        private void button5_Click(object? sender, EventArgs e)
        {
            ConfirmarRecepcion();
        }

        #endregion

        #region Cliente

        private void comboBoxClieR_SelectionChangeCommitted(object? sender, EventArgs e)
        {

            BeginInvoke(new MethodInvoker(() =>
            {
                if (comboBoxClieR.SelectedItem is Cliente elegido)
                {
                    ProcesarCambioDeCliente(elegido);
                }
            }));
        }

        private void comboBoxClieR_Validating(object? sender, CancelEventArgs e)
        {
            if (cierreConfirmado)
            {
                return;
            }

            string texto = comboBoxClieR.Text.Trim();
            if (texto.Length == 0)
            {
                ProcesarCambioDeCliente(null);
                return;
            }

            // El cliente debe ser uno de los que figuran en el sistema.
            Cliente? encontrado = BuscarClienteEnCombo();
            if (encontrado == null)
            {
                Cartel.Informar(this, "Cliente inexistente",
                    $"El cliente \"{texto}\" no figura en el sistema con contrato vigente." +
                    Environment.NewLine + Environment.NewLine +
                    "Elija uno de los clientes de la lista.",
                    TipoMensaje.Error);
                RestaurarCliente(clienteActual);
                e.Cancel = true;
                return;
            }

            if (!ReferenceEquals(comboBoxClieR.SelectedItem, encontrado))
            {
                comboBoxClieR.SelectedItem = encontrado;
            }
            ProcesarCambioDeCliente(encontrado);
        }

        private Cliente? BuscarClienteEnCombo()
        {
            string texto = comboBoxClieR.Text.Trim();
            if (texto.Length == 0)
            {
                return null;
            }

            if (comboBoxClieR.SelectedItem is Cliente seleccionado &&
                string.Equals(seleccionado.RazonSocial, texto, StringComparison.OrdinalIgnoreCase))
            {
                return seleccionado;
            }

            return comboBoxClieR.Items.OfType<Cliente>().FirstOrDefault(
                c => string.Equals(c.RazonSocial, texto, StringComparison.OrdinalIgnoreCase));
        }

        private void RestaurarCliente(Cliente? cliente)
        {
            if (cliente == null)
            {
                comboBoxClieR.SelectedIndex = -1;
                comboBoxClieR.Text = string.Empty;
            }
            else
            {
                comboBoxClieR.SelectedItem = cliente;
            }
        }

        private void ProcesarCambioDeCliente(Cliente? nuevo)
        {
            if (procesandoCambioDeCliente || ReferenceEquals(nuevo, clienteActual))
            {
                return;
            }

            procesandoCambioDeCliente = true;
            try
            {
                // Los SKU se validan contra el cliente: si cambia, los ítems cargados dejan de valer.
                if (modelo.Items.Count > 0 && clienteActual != null)
                {
                    bool cambiar = Cartel.Preguntar(this, "Cambiar cliente",
                        $"El detalle tiene {Plural(modelo.Items.Count, "ítem cargado", "ítems cargados")} para {clienteActual.RazonSocial}. " +
                        "Si cambia el cliente, se quitarán del detalle." + Environment.NewLine + Environment.NewLine +
                        "¿Desea cambiar el cliente?",
                        "Sí, cambiar cliente", "No");

                    if (!cambiar)
                    {
                        RestaurarCliente(clienteActual);
                        return;
                    }

                    modelo.DescartarItems();
                    listViewRem.Items.Clear();
                }

                clienteActual = nuevo;
                ActualizarAyudaDeSku(nuevo);

                if (!ValidarRemitoNoRegistrado())
                {
                    textBoxNRem.Focus();
                }
            }
            finally
            {
                procesandoCambioDeCliente = false;
            }
        }

        #endregion

        #region N° de remito


        /// Deja solo números (máximo 13) y agrega el guion después del 5°: 00001-00012345.

        private void FormatearNumeroDeRemito()
        {
            if (formateandoRemito)
            {
                return;
            }

            string texto = textBoxNRem.Text;
            int cursor = textBoxNRem.SelectionStart;
            int digitosAntesDelCursor = texto.Take(cursor).Count(EsDigito);

            string formateado = FormatearRemito(texto);
            if (formateado == texto)
            {
                return;
            }

            formateandoRemito = true;
            try
            {
                textBoxNRem.Text = formateado;
                int digitos = Math.Min(digitosAntesDelCursor, DigitosRemito);
                int nuevaPosicion = digitos > DigitosPuntoDeVenta ? digitos + 1 : digitos;
                textBoxNRem.SelectionStart = Math.Min(nuevaPosicion, formateado.Length);
            }
            finally
            {
                formateandoRemito = false;
            }
        }

        private static string FormatearRemito(string texto)
        {
            string digitos = new string(texto.Where(EsDigito).Take(DigitosRemito).ToArray());
            return digitos.Length > DigitosPuntoDeVenta
                ? digitos.Substring(0, DigitosPuntoDeVenta) + "-" + digitos.Substring(DigitosPuntoDeVenta)
                : digitos;
        }

        private void textBoxNRem_Validating(object? sender, CancelEventArgs e)
        {
            if (cierreConfirmado)
            {
                return;
            }

            string numero = textBoxNRem.Text.Trim();
            if (numero.Length == 0)
            {
                return;
            }


            if (!FormatoRemito.IsMatch(numero))
            {
                int faltan = DigitosRemito - numero.Count(EsDigito);
                MarcarConError(textBoxNRem);
                MostrarAviso(textBoxNRem, $"El N° de remito lleva 13 números: faltan {faltan}.");
                return;
            }

            // Si el remito ya estaba registrado, el foco queda en el campo para cargar otro número.
            if (!ValidarRemitoNoRegistrado())
            {
                e.Cancel = true;
            }
        }


        private bool ValidarRemitoNoRegistrado()
        {
            string numero = textBoxNRem.Text.Trim();
            if (clienteActual == null || !FormatoRemito.IsMatch(numero) ||
                !modelo.RemitoYaRegistrado(clienteActual, numero))
            {
                return true;
            }

            Cartel.Informar(this, "Remito ya registrado",
                $"El remito N° {numero} ya fue registrado para el cliente {clienteActual.RazonSocial}." +
                Environment.NewLine + Environment.NewLine +
                "Verifique el número en el remito que entrega el transportista.",
                TipoMensaje.Error);

            textBoxNRem.Clear();
            MarcarConError(textBoxNRem);
            return false;
        }

        private void Remito_Enter(object? sender, EventArgs e)
        {

            Control destino = ultimoCampoActivo ?? comboBoxClieR;
            BeginInvoke(new MethodInvoker(() => destino.Focus()));
        }

        #endregion

        #region Detalle de mercadería recibida

        private void buttonAgregIRem_Click(object? sender, EventArgs e)
        {
            AgregarItem();
        }

        private void textBoxCanRem_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AgregarItem();
            }
        }

        private void AgregarItem()
        {
            Cliente? cliente = clienteActual;
            if (cliente == null)
            {
                Cartel.Informar(this, "Seleccione el cliente",
                    "Seleccione el cliente antes de cargar la mercadería: cada SKU se valida contra los dados de alta para ese cliente.",
                    TipoMensaje.Advertencia);
                comboBoxClieR.Focus();
                return;
            }

            string sku = textBoxSKURem.Text.Trim().ToUpperInvariant();
            string textoContada = textBoxCatnCRem.Text.Trim();
            string textoRemito = textBoxCanRem.Text.Trim();

            // Los tres campos del ítem son obligatorios.
            var vacios = new List<(string Nombre, Control Campo)>();
            if (sku.Length == 0) vacios.Add(("SKU/ Código", textBoxSKURem));
            if (textoContada.Length == 0) vacios.Add(("Cant. Contada", textBoxCatnCRem));
            if (textoRemito.Length == 0) vacios.Add(("Cant. Remito", textBoxCanRem));

            if (vacios.Count > 0)
            {
                MostrarFaltantes("Faltan datos del ítem",
                    "Complete SKU/ Código, Cant. Contada y Cant. Remito para agregar el ítem.", vacios);
                return;
            }

            // Excepción 10.1: el SKU no está dado de alta para el cliente -> no se agrega.
            Producto? producto = modelo.BuscarProducto(cliente, sku);
            if (producto == null)
            {
                Cartel.Informar(this, "SKU no dado de alta",
                    $"El SKU {sku} no está dado de alta para el cliente {cliente.RazonSocial}, por eso no se puede agregar." +
                    Environment.NewLine + Environment.NewLine +
                    "SKU del cliente: " + string.Join(", ", cliente.Productos.Select(p => p.Sku)) + ".",
                    TipoMensaje.Error);
                textBoxSKURem.Clear();
                MarcarConError(textBoxSKURem);
                textBoxSKURem.Focus();
                return;
            }

            // Excepción 10.2: cantidades enteras, remito > 0 y contada >= 0.
            bool contadaValida = int.TryParse(textoContada, NumberStyles.None, CultureInfo.InvariantCulture, out int contada);
            bool remitoValido = int.TryParse(textoRemito, NumberStyles.None, CultureInfo.InvariantCulture, out int remito);
            if (!contadaValida || !remitoValido || remito <= 0 || contada < 0)
            {
                Cartel.Informar(this, "Cantidades inválidas",
                    "Ingrese cantidades enteras: la del remito mayor a cero y la contada mayor o igual a cero.",
                    TipoMensaje.Error);
                textBoxCatnCRem.Clear();
                textBoxCanRem.Clear();
                MarcarConError(textBoxCatnCRem);
                MarcarConError(textBoxCanRem);
                textBoxCatnCRem.Focus();
                return;
            }

            // Excepción 10.3: el SKU ya está en la lista.
            if (modelo.SkuYaAgregado(producto.Sku))
            {
                Cartel.Informar(this, "SKU repetido",
                    $"El SKU {producto.Sku} ya fue agregado. Quítelo para volver a cargarlo.",
                    TipoMensaje.Advertencia);
                MarcarConError(textBoxSKURem);
                textBoxSKURem.Focus();
                textBoxSKURem.SelectAll();
                return;
            }

            ItemRecepcion? item = modelo.AgregarItem(producto, contada, remito);
            if (item == null)
            {
                Cartel.Informar(this, "Sin posiciones libres",
                    "No hay posiciones libres en el depósito para ubicar esta mercadería. Avise al jefe de Almacenamiento.",
                    TipoMensaje.Error);
                return;
            }

            AgregarFila(item);
            QuitarMarcaDeError(listViewRem);

            textBoxSKURem.Clear();
            textBoxCatnCRem.Clear();
            textBoxCanRem.Clear();
            textBoxSKURem.Focus();
        }

        private void AgregarFila(ItemRecepcion item)
        {
            // Columnas del Designer: SkuCodigo, Descripción, Cantidad contada, Cantidad remito.
            var fila = new ListViewItem(item.Sku);
            fila.SubItems.Add(item.Descripcion);
            fila.SubItems.Add(item.CantidadContada.ToString(CultureInfo.CurrentCulture));
            fila.SubItems.Add(item.CantidadRemito.ToString(CultureInfo.CurrentCulture));
            if (listViewRem.Columns.Count > 4)
            {
                // Si en el Designer se agrega la columna "Posición", se completa sola.
                fila.SubItems.Add(item.Posicion);
            }


            string posicion = item.Posicion == RecepcionModelo.SinPosicion
                ? "sin posición (no se recibieron unidades)"
                : "posición propuesta " + item.Posicion;
            if (item.Diferencia < 0)
            {
                fila.BackColor = ColorFilaFaltante;
                fila.ToolTipText = $"{item.Descripcion} · {posicion} · faltan {-item.Diferencia} u. respecto del remito (se emitirá nota de diferencias).";
            }
            else if (item.Diferencia > 0)
            {
                fila.BackColor = ColorFilaExcedente;
                fila.ToolTipText = $"{item.Descripcion} · {posicion} · se recibieron {item.Diferencia} u. más que las del remito (excedente).";
            }
            else
            {
                fila.ToolTipText = $"{item.Descripcion} · {posicion} · la cantidad contada coincide con el remito.";
            }

            listViewRem.Items.Add(fila);
            fila.EnsureVisible();
        }

        private void buttonQuitIRem_Click(object? sender, EventArgs e)
        {
            // Quita el último ítem agregado; se puede usar las veces que haga falta.
            ItemRecepcion? quitado = modelo.QuitarUltimoItem();
            if (quitado == null)
            {
                Cartel.Informar(this, "Quitar ítem",
                    "No hay mercadería disponible para quitar.",
                    TipoMensaje.Informacion, "Okey");
                return;
            }

            listViewRem.Items.RemoveAt(listViewRem.Items.Count - 1);
        }

        private void textBoxObsRem_Enter(object? sender, EventArgs e)
        {
            // Si todavía tiene el texto de ejemplo, queda seleccionado para que al escribir se reemplace.
            if (textBoxObsRem.Text == ejemploObservaciones)
            {
                BeginInvoke(new MethodInvoker(textBoxObsRem.SelectAll));
            }
        }

        #endregion

        #region Cancelar y Confirmar

        private void CancRem_Click(object? sender, EventArgs e)
        {
            if (!ConfirmarDescarte())
            {
                return;
            }

            modelo.DescartarItems();
            cierreConfirmado = true;
            Close();
        }

        private bool ConfirmarDescarte()
        {
            return Cartel.Preguntar(this, "Cancelar recepción",
                "Si selecciona cancelar, los cambios no se guardarán." + Environment.NewLine + "¿Desea cancelar?",
                "Sí, descartar cambios", "No",
                TipoMensaje.Advertencia, enfocarNo: true);
        }

        private void RecepcionMercaderiaForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Ya confirmado con "Sí, descartar cambios": se cierra aunque un campo no haya validado.
            if (cierreConfirmado)
            {
                e.Cancel = false;
                return;
            }

            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            // Cerrar con la X equivale a Cancelar cuando hay datos cargados.
            if (!HayCambios())
            {
                e.Cancel = false;
                return;
            }

            e.Cancel = !ConfirmarDescarte();
            if (!e.Cancel)
            {
                cierreConfirmado = true;
                modelo.DescartarItems();
            }
        }

        private void ConfirmarRecepcion()
        {
            // 1) Datos obligatorios (excepción paso 15).
            Cliente? cliente = BuscarClienteEnCombo();
            var faltantes = new List<(string Nombre, Control Campo)>();
            if (cliente == null) faltantes.Add(("Cliente (elija uno de la lista)", comboBoxClieR));
            if (textBoxNRem.Text.Trim().Length == 0) faltantes.Add(("N° Remito", textBoxNRem));
            if (textBoxTransRem.Text.Trim().Length == 0) faltantes.Add(("Transportista", textBoxTransRem));
            if (textBoxNARem.Text.Trim().Length == 0) faltantes.Add(("Nombre y Apellido", textBoxNARem));
            if (As.Text.Trim().Length == 0) faltantes.Add(("Patente", As));
            if (modelo.Items.Count == 0) faltantes.Add(("Detalle de mercadería recibida (agregue al menos un ítem)", listViewRem));

            if (faltantes.Count > 0)
            {
                MostrarFaltantes("Faltan datos obligatorios",
                    "Complete los datos del remito y del transportista y cargue al menos un ítem.", faltantes);
                return;
            }

            // Si el cliente se escribió y todavía no se procesó, se aplica ahora.
            if (!ReferenceEquals(cliente, clienteActual))
            {
                ProcesarCambioDeCliente(cliente);
                if (!ReferenceEquals(cliente, clienteActual) || modelo.Items.Count == 0)
                {
                    return;
                }
            }

            Cliente clienteRemito = clienteActual!;
            string numeroRemito = textBoxNRem.Text.Trim();
            string transportista = VariosEspacios.Replace(textBoxTransRem.Text.Trim(), " ");
            string chofer = VariosEspacios.Replace(textBoxNARem.Text.Trim(), " ");
            string patente = As.Text.Trim().ToUpperInvariant();
            DateTime fechaRemito = dateTimePickerFRem.Value.Date;
            string observaciones = textBoxObsRem.Text.Trim() == ejemploObservaciones.Trim()
                ? string.Empty
                : textBoxObsRem.Text.Trim();

            // 2) Formatos y reglas de negocio.
            if (!FormatoRemito.IsMatch(numeroRemito))
            {
                MostrarErrorDeCampo(textBoxNRem, "N° de remito incompleto",
                    "El N° de remito debe tener 13 números: 5 del punto de venta, un guion y 8 del comprobante." +
                    Environment.NewLine + "Ejemplo: 00001-00012345.");
                return;
            }

            if (!ValidarRemitoNoRegistrado())
            {
                textBoxNRem.Focus();
                return;
            }

            if (fechaRemito > DateTime.Today)
            {
                Cartel.Informar(this, "Fecha inválida",
                    "La fecha del remito no puede ser posterior a la fecha actual.", TipoMensaje.Error);
                dateTimePickerFRem.Focus();
                return;
            }

            if (!SoloLetrasYEspacios.IsMatch(transportista))
            {
                MostrarErrorDeCampo(textBoxTransRem, "Transportista inválido",
                    "El transportista solo admite letras.");
                return;
            }

            if (!SoloLetrasYEspacios.IsMatch(chofer) || !chofer.Contains(' '))
            {
                MostrarErrorDeCampo(textBoxNARem, "Nombre y apellido incompleto",
                    "Ingrese el nombre y el apellido del chofer, solo con letras. Por ejemplo: Juan Pérez.");
                return;
            }

            if (!FormatoPatente.IsMatch(patente))
            {
                MostrarErrorDeCampo(As, "Patente inválida",
                    "La patente debe tener el formato AAA123 o AA123AA, por ejemplo AB123CD.");
                return;
            }

            // 3) Ítem escrito pero no agregado al detalle.
            bool hayItemSinAgregar = textBoxSKURem.Text.Trim().Length > 0
                                     || textBoxCatnCRem.Text.Trim().Length > 0
                                     || textBoxCanRem.Text.Trim().Length > 0;
            if (hayItemSinAgregar)
            {
                bool seguir = Cartel.Preguntar(this, "Ítem sin agregar",
                    "Hay datos en SKU/ Código o en las cantidades que todavía no se agregaron al detalle." +
                    Environment.NewLine + Environment.NewLine +
                    "¿Desea confirmar la recepción sin ese ítem?",
                    "Confirmar sin ese ítem", "Volver");
                if (!seguir)
                {
                    textBoxSKURem.Focus();
                    return;
                }
            }

            // 4) Confirmación antes de guardar (paso 16 / excepción 17).
            int itemsConFaltante = modelo.Items.Count(i => i.Diferencia < 0);
            int itemsConExcedente = modelo.Items.Count(i => i.Diferencia > 0);
            string resumen =
                $"¿Confirma el registro de la recepción del remito N° {numeroRemito} del cliente {clienteRemito.RazonSocial}?" +
                Environment.NewLine + Environment.NewLine +
                $"{Plural(modelo.Items.Count, "ítem", "ítems")}  ·  " +
                $"{modelo.Items.Sum(i => i.CantidadContada)} u. contadas  ·  " +
                $"{modelo.Items.Sum(i => i.CantidadRemito)} u. según remito";
            if (itemsConFaltante > 0)
            {
                resumen += Environment.NewLine + $"•  {Plural(itemsConFaltante, "ítem", "ítems")} con faltante: se emitirá la nota de diferencias.";
            }
            if (itemsConExcedente > 0)
            {
                resumen += Environment.NewLine + $"•  {Plural(itemsConExcedente, "ítem", "ítems")} con excedente: se emitirá la orden de espacio insuficiente.";
            }

            bool confirmar = Cartel.Preguntar(this, "Confirmar recepción", resumen,
                "Sí, registrar", "No", TipoMensaje.Informacion, enfocarNo: false);
            if (!confirmar)
            {
                return; // No se guarda nada y se vuelve a la pantalla.
            }

            // 5) Registro (simulado en memoria) de la orden de recepción.
            OrdenRecepcion orden = modelo.RegistrarRecepcion(clienteRemito, numeroRemito, fechaRemito,
                transportista, chofer, patente, observaciones);

            // Excepción paso 19: más mercadería que la del remito.
            if (orden.HayExcedente)
            {
                Cartel.Informar(this, "Orden de espacio insuficiente",
                    "Se recibió más mercadería que la indicada en el remito. " +
                    "Haga firmar la orden de espacio insuficiente al transportista.",
                    TipoMensaje.Advertencia, "Entendido");
            }

            Cartel.Informar(this, "Recepción registrada", ArmarComprobante(orden), TipoMensaje.Exito,
                "Aceptar", anchoTexto: 470);

            // La pantalla queda lista para la próxima recepción.
            LimpiarPantalla();
        }


        private static string ArmarComprobante(OrdenRecepcion orden)
        {
            var texto = new StringBuilder();
            texto.AppendLine($"Orden de recepción N° {orden.Numero}  ·  Estado: {orden.Estado}");
            texto.AppendLine();
            texto.AppendLine($"Cliente: {orden.Cliente.RazonSocial}");
            texto.AppendLine($"Remito N° {orden.NumeroRemito} del {orden.FechaRemito.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
            texto.AppendLine($"Transportista: {orden.Transportista}  ·  Chofer: {orden.Chofer}  ·  Patente: {orden.Patente}");
            texto.AppendLine();
            texto.AppendLine("Ítems registrados:");

            const int maximoVisible = 8;
            foreach (ItemRecepcion item in orden.Items.Take(maximoVisible))
            {
                string ubicacion = item.Posicion == RecepcionModelo.SinPosicion ? "" : $"  →  {item.Posicion}";
                texto.AppendLine($"•  {item.Sku}  {item.Descripcion}: {item.CantidadContada} contadas / {item.CantidadRemito} remito{ubicacion}");
            }
            if (orden.Items.Count > maximoVisible)
            {
                texto.AppendLine($"   … y {orden.Items.Count - maximoVisible} más");
            }

            texto.AppendLine();
            texto.AppendLine("Observaciones: " + (orden.Observaciones.Length == 0 ? "sin observaciones" : orden.Observaciones));
            if (orden.HayFaltantes)
            {
                texto.AppendLine("Se emitió la nota de diferencias para el transportista.");
            }
            texto.Append("Entregue al transportista la copia conformada del remito.");
            return texto.ToString();
        }

        #endregion

        #region Filtros de teclado y feedback

        private KeyPressEventHandler PermitirSolo(Func<char, bool> permitido, string mensaje)
        {
            return (sender, e) =>
            {
                if (char.IsControl(e.KeyChar) || permitido(e.KeyChar))
                {
                    return;
                }

                e.Handled = true;
                if (sender is Control campo)
                {
                    MostrarAviso(campo, mensaje);
                }
            };
        }


        private static void QuitarCaracteresNoPermitidos(TextBox campo, Func<char, bool> permitido)
        {
            string texto = campo.Text;
            if (texto.All(permitido))
            {
                return;
            }

            int validosAntesDelCursor = texto.Take(campo.SelectionStart).Count(permitido);
            campo.Text = new string(texto.Where(permitido).ToArray());
            campo.SelectionStart = Math.Min(validosAntesDelCursor, campo.Text.Length);
        }

        private void MostrarAviso(Control campo, string mensaje)
        {
            aviso.Show(mensaje, campo, 0, campo.Height + 2, 2200);
        }

        private static bool EsDigito(char c) => c >= '0' && c <= '9';

        private static bool EsLetraOEspacio(char c) => char.IsLetter(c) || c == ' ';

        private static bool EsLetraODigitoAscii(char c) =>
            (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || EsDigito(c);

        private static bool EsCaracterDeSku(char c) => EsLetraODigitoAscii(c) || c == '-';

        private static void AvanzarConEnter(KeyEventArgs e, Control siguiente)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                siguiente.Focus();
            }
        }

        #endregion

        #region Auxiliares

        private TextBox[] CamposDeTexto()
        {
            return new[]
            {
                textBoxNRem, textBoxTransRem, textBoxNARem, As,
                textBoxSKURem, textBoxCatnCRem, textBoxCanRem, textBoxObsRem
            };
        }

        private Control[] CamposDeEntrada()
        {
            return new Control[]
            {
                comboBoxClieR, textBoxNRem, textBoxTransRem, dateTimePickerFRem, textBoxNARem, As,
                textBoxSKURem, textBoxCatnCRem, textBoxCanRem, listViewRem, textBoxObsRem
            };
        }

        private void ActualizarAyudaDeSku(Cliente? cliente)
        {
            var codigos = new AutoCompleteStringCollection();
            if (cliente == null)
            {
                textBoxSKURem.AutoCompleteCustomSource = codigos;
                ayuda.SetToolTip(textBoxSKURem, "Seleccione primero el cliente: el SKU se valida contra los dados de alta para ese cliente.");
                return;
            }

            codigos.AddRange(cliente.Productos.Select(p => p.Sku).ToArray());
            textBoxSKURem.AutoCompleteCustomSource = codigos;
            ayuda.SetToolTip(textBoxSKURem,
                "SKU dados de alta para " + cliente.RazonSocial + ":" + Environment.NewLine +
                string.Join(Environment.NewLine, cliente.Productos.Select(p => p.Sku + "   " + p.Descripcion)));
        }

        private void ReiniciarFechaDelRemito()
        {

            dateTimePickerFRem.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dateTimePickerFRem.Value = DateTime.Today;
        }

        private bool HayCambios()
        {
            return comboBoxClieR.Text.Trim().Length > 0
                   || modelo.Items.Count > 0
                   || dateTimePickerFRem.Value.Date != DateTime.Today
                   || textBoxObsRem.Text != ejemploObservaciones
                   || CamposDeTexto().Where(t => t != textBoxObsRem).Any(t => t.Text.Trim().Length > 0);
        }

        private void LimpiarPantalla()
        {
            clienteActual = null;
            comboBoxClieR.SelectedIndex = -1;
            comboBoxClieR.Text = string.Empty;
            ActualizarAyudaDeSku(null);

            foreach (TextBox campo in CamposDeTexto())
            {
                campo.Clear();
            }
            textBoxObsRem.Text = ejemploObservaciones; // vuelve a verse como en el diseño

            ReiniciarFechaDelRemito();
            modelo.DescartarItems();
            listViewRem.Items.Clear();

            foreach (Control control in controlesConError.ToList())
            {
                QuitarMarcaDeError(control);
            }

            ultimoCampoActivo = null;
            comboBoxClieR.Focus();
        }

        private void MostrarFaltantes(string titulo, string mensaje, List<(string Nombre, Control Campo)> faltantes)
        {
            foreach (var faltante in faltantes)
            {
                MarcarConError(faltante.Campo);
            }

            Cartel.Informar(this, titulo,
                mensaje + Environment.NewLine + Environment.NewLine +
                "Falta completar:" + Environment.NewLine +
                string.Join(Environment.NewLine, faltantes.Select(f => "•  " + f.Nombre)),
                TipoMensaje.Error);

            EnfocarCampo(faltantes[0].Campo);
        }

        private void MostrarErrorDeCampo(Control campo, string titulo, string mensaje)
        {
            MarcarConError(campo);
            Cartel.Informar(this, titulo, mensaje, TipoMensaje.Error);
            EnfocarCampo(campo);
        }

        private void EnfocarCampo(Control campo)
        {
            // Para cargar ítems al detalle se empieza por el SKU.
            Control destino = campo == listViewRem ? textBoxSKURem : campo;
            destino.Focus();
            if (destino is TextBox texto)
            {
                texto.SelectAll();
            }
        }

        private void MarcarConError(Control campo)
        {
            // ComboBox y DateTimePicker se señalan con el foco y en el mensaje.
            if (campo is TextBox || campo is ListView)
            {
                campo.BackColor = ColorCampoConError;
                controlesConError.Add(campo);
            }
        }

        private void QuitarMarcaDeError(Control campo)
        {
            if (controlesConError.Remove(campo))
            {
                campo.ResetBackColor();
            }
        }

        private static string Plural(int cantidad, string singular, string plural)
        {
            return cantidad + " " + (cantidad == 1 ? singular : plural);
        }

        #endregion

        // =====================================================================================
        //  MODELO Y DATOS DE PRUEBA (en memoria). En el sistema real saldrían de las tablas
        //  Clientes, SKU, Posiciones y Órdenes de recepción.
        // =====================================================================================

        
        private sealed class Producto
        {
            public Producto(string sku, string descripcion, decimal m3PorUnidad)
            {
                Sku = sku;
                Descripcion = descripcion;
                M3PorUnidad = m3PorUnidad;
            }

            public string Sku { get; }
            public string Descripcion { get; }
            public decimal M3PorUnidad { get; }
        }

        /// <summary>Cliente con sus SKU dados de alta y los remitos que ya se recibieron.</summary>
        private sealed class Cliente
        {
            public Cliente(int codigo, string razonSocial, bool contratoVigente,
                           IEnumerable<Producto> productos, IEnumerable<string> remitosRegistrados)
            {
                Codigo = codigo;
                RazonSocial = razonSocial;
                ContratoVigente = contratoVigente;
                Productos = productos.ToList();
                RemitosRegistrados = new HashSet<string>(remitosRegistrados);
            }

            public int Codigo { get; }
            public string RazonSocial { get; }
            public bool ContratoVigente { get; }
            public IReadOnlyList<Producto> Productos { get; }
            public HashSet<string> RemitosRegistrados { get; }

            // El ComboBox muestra lo que devuelve ToString().
            public override string ToString() => RazonSocial;
        }

        
        private sealed class ItemRecepcion
        {
            public ItemRecepcion(Producto producto, int cantidadContada, int cantidadRemito, string posicion)
            {
                Producto = producto;
                CantidadContada = cantidadContada;
                CantidadRemito = cantidadRemito;
                Posicion = posicion;
            }

            public Producto Producto { get; }
            public string Sku => Producto.Sku;
            public string Descripcion => Producto.Descripcion;
            public int CantidadContada { get; }
            public int CantidadRemito { get; }
            public string Posicion { get; }

            
            public int Diferencia => CantidadContada - CantidadRemito;
        }

        
        private sealed class OrdenRecepcion
        {
            public OrdenRecepcion(int numero, Cliente cliente, string numeroRemito, DateTime fechaRemito,
                                  string transportista, string chofer, string patente, string observaciones,
                                  IEnumerable<ItemRecepcion> items)
            {
                Numero = numero;
                Cliente = cliente;
                NumeroRemito = numeroRemito;
                FechaRemito = fechaRemito;
                FechaHoraRecepcion = DateTime.Now;
                Transportista = transportista;
                Chofer = chofer;
                Patente = patente;
                Observaciones = observaciones;
                Items = items.ToList();

                Estado = HayExcedente ? "Pendiente - Excedente"
                       : HayFaltantes ? "Recibida con diferencias"
                       : "Pendiente de almacenamiento";
            }

            public int Numero { get; }
            public Cliente Cliente { get; }
            public string NumeroRemito { get; }
            public DateTime FechaRemito { get; }
            public DateTime FechaHoraRecepcion { get; }
            public string Transportista { get; }
            public string Chofer { get; }
            public string Patente { get; }
            public string Observaciones { get; }
            public IReadOnlyList<ItemRecepcion> Items { get; }
            public string Estado { get; }

            public bool HayExcedente => Items.Any(i => i.Diferencia > 0);
            public bool HayFaltantes => Items.Any(i => i.Diferencia < 0);
        }

        
        private sealed class RecepcionModelo
        {
            public const string SinPosicion = "—";

            private readonly List<Cliente> clientes = CrearClientesDePrueba();
            private readonly List<string> posiciones = CrearPosicionesDelDeposito();
            private readonly HashSet<string> posicionesOcupadas = new HashSet<string>
            {
                "A-01-1", "A-01-2", "A-01-3", "A-01-4",
                "A-02-1", "A-02-2", "A-02-3", "A-02-4",
                "A-03-2", "A-04-1", "B-01-1", "B-01-2"
            };
            private readonly List<OrdenRecepcion> ordenesRegistradas = new List<OrdenRecepcion>();
            private readonly List<ItemRecepcion> items = new List<ItemRecepcion>();
            private int ultimoNumeroOrden = 1047;

            
            public IReadOnlyList<ItemRecepcion> Items => items;

            public IReadOnlyList<OrdenRecepcion> OrdenesRegistradas => ordenesRegistradas;

            
            public IReadOnlyList<string> TransportistasFrecuentes { get; } = new List<string>
            {
                "Transportes Del Sur",
                "Logística Pampeana",
                "Expreso Litoral",
                "Cargas Rápidas del Plata",
                "TransAndes Logística"
            };

            public List<Cliente> ObtenerClientesConContratoVigente()
            {
                return clientes.Where(c => c.ContratoVigente).OrderBy(c => c.RazonSocial).ToList();
            }

            public bool RemitoYaRegistrado(Cliente cliente, string numeroRemito)
            {
                return cliente.RemitosRegistrados.Contains(numeroRemito);
            }

            public Producto? BuscarProducto(Cliente cliente, string sku)
            {
                return cliente.Productos.FirstOrDefault(
                    p => string.Equals(p.Sku, sku, StringComparison.OrdinalIgnoreCase));
            }

            public bool SkuYaAgregado(string sku)
            {
                return items.Any(i => string.Equals(i.Sku, sku, StringComparison.OrdinalIgnoreCase));
            }

            
            public ItemRecepcion? AgregarItem(Producto producto, int cantidadContada, int cantidadRemito)
            {
                string posicion = SinPosicion;
                if (cantidadContada > 0)
                {
                    string? libre = posiciones.FirstOrDefault(
                        p => !posicionesOcupadas.Contains(p) && !items.Any(i => i.Posicion == p));
                    if (libre == null)
                    {
                        return null;
                    }
                    posicion = libre;
                }

                var item = new ItemRecepcion(producto, cantidadContada, cantidadRemito, posicion);
                items.Add(item);
                return item;
            }

            
            public ItemRecepcion? QuitarUltimoItem()
            {
                if (items.Count == 0)
                {
                    return null;
                }

                ItemRecepcion ultimo = items[items.Count - 1];
                items.RemoveAt(items.Count - 1);
                return ultimo;
            }

            
            public void DescartarItems()
            {
                items.Clear();
            }

            
            public OrdenRecepcion RegistrarRecepcion(Cliente cliente, string numeroRemito, DateTime fechaRemito,
                                                     string transportista, string chofer, string patente,
                                                     string observaciones)
            {
                ultimoNumeroOrden++;
                var orden = new OrdenRecepcion(ultimoNumeroOrden, cliente, numeroRemito, fechaRemito,
                                               transportista, chofer, patente, observaciones, items);

                foreach (ItemRecepcion item in items.Where(i => i.Posicion != SinPosicion))
                {
                    posicionesOcupadas.Add(item.Posicion);
                }

                cliente.RemitosRegistrados.Add(numeroRemito);
                ordenesRegistradas.Add(orden);
                items.Clear();
                return orden;
            }

            private static List<string> CrearPosicionesDelDeposito()
            {
                // Sector - Posición - Fila (ej.: A-03-1), en el orden en que se recorre el depósito.
                var lista = new List<string>();
                foreach (char sector in new[] { 'A', 'B', 'C' })
                {
                    for (int posicion = 1; posicion <= 12; posicion++)
                    {
                        for (int fila = 1; fila <= 4; fila++)
                        {
                            lista.Add($"{sector}-{posicion:00}-{fila}");
                        }
                    }
                }
                return lista;
            }

            private static List<Cliente> CrearClientesDePrueba()
            {
                // Razones sociales solo con letras (el campo Cliente solo admite letras).
                return new List<Cliente>
                {
                    new Cliente(101, "Distribuidora Andina", true,
                        new[]
                        {
                            new Producto("AND-1001", "Yerba mate 1 kg (caja x10)", 0.012m),
                            new Producto("AND-1002", "Aceite girasol 1,5 L (caja x12)", 0.021m),
                            new Producto("AND-1003", "Arroz largo fino 1 kg (caja x10)", 0.011m),
                            new Producto("AND-1004", "Fideos secos 500 g (caja x20)", 0.015m)
                        },
                        new[] { "00001-00004521", "00001-00004522" }),

                    new Cliente(102, "ElectroSur", true,
                        new[]
                        {
                            new Producto("ELS-2001", "Licuadora 600 W", 0.018m),
                            new Producto("ELS-2002", "Pava eléctrica 1,7 L", 0.009m),
                            new Producto("ELS-2003", "Ventilador de pie 20\"", 0.065m),
                            new Producto("ELS-2004", "Microondas 20 L", 0.052m)
                        },
                        new[] { "00003-00001187" }),

                    new Cliente(103, "Textil Pampa", true,
                        new[]
                        {
                            new Producto("TXP-3001", "Remera algodón talle M (x24)", 0.030m),
                            new Producto("TXP-3002", "Jean clásico talle 42 (x12)", 0.034m),
                            new Producto("TXP-3003", "Campera rompeviento (x6)", 0.041m)
                        },
                        new[] { "00002-00000915" }),

                    new Cliente(104, "Ferretería Industrial Norte", true,
                        new[]
                        {
                            new Producto("FIN-4001", "Taladro percutor 750 W", 0.014m),
                            new Producto("FIN-4002", "Tornillos 6x40 (caja x500)", 0.003m),
                            new Producto("FIN-4003", "Pintura látex 20 L", 0.024m)
                        },
                        new[] { "00005-00010233" }),

                    // Contrato vencido: no aparece en el ComboBox (precondición del caso de uso).
                    new Cliente(105, "Juguetería Mundo Feliz", false,
                        new[] { new Producto("JMF-5001", "Pelota de fútbol N°5", 0.008m) },
                        Array.Empty<string>())
                };
            }
        }

        

        private enum TipoMensaje
        {
            Informacion,
            Advertencia,
            Error,
            Exito
        }

        private sealed class Cartel : Form
        {
            private readonly PictureBox icono = new PictureBox();
            private readonly Label texto = new Label();
            private readonly FlowLayoutPanel botonera = new FlowLayoutPanel();
            private readonly Button principal = new Button();
            private readonly Button secundario = new Button();
            private readonly Font fuenteTexto = new Font("Segoe UI", 10F);
            private readonly TipoMensaje tipo;
            private readonly bool conSecundario;

            private Cartel(string titulo, string mensaje, TipoMensaje tipo,
                           string textoPrincipal, string? textoSecundario, int anchoTexto)
            {
                this.tipo = tipo;
                conSecundario = textoSecundario != null;

                SuspendLayout();
                botonera.SuspendLayout();

                icono.Location = new Point(24, 24);
                icono.Size = new Size(32, 32);
                icono.SizeMode = PictureBoxSizeMode.Zoom;
                icono.TabStop = false;

                texto.AutoSize = true;
                texto.Font = fuenteTexto;
                texto.ForeColor = Color.FromArgb(32, 32, 32);
                texto.Location = new Point(72, 28);
                texto.MaximumSize = new Size(anchoTexto, 0);

                PrepararBoton(principal, 0);
                PrepararBoton(secundario, 1);
                secundario.Visible = conSecundario;

                botonera.BackColor = Color.FromArgb(243, 243, 243);
                botonera.Dock = DockStyle.Bottom;
                botonera.FlowDirection = FlowDirection.RightToLeft;
                botonera.Padding = new Padding(12, 12, 16, 12);
                botonera.Size = new Size(440, 56);
                botonera.WrapContents = false;
                botonera.Controls.Add(secundario); // queda a la derecha
                botonera.Controls.Add(principal);

                AutoScaleDimensions = new SizeF(7F, 15F);
                AutoScaleMode = AutoScaleMode.Font;
                BackColor = Color.White;
                ClientSize = new Size(440, 150);
                Controls.Add(texto);
                Controls.Add(icono);
                Controls.Add(botonera);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowIcon = false;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.CenterParent;
                Text = titulo;

                botonera.ResumeLayout(false);
                botonera.PerformLayout();
                ResumeLayout(false);
                PerformLayout();

                // Los textos se cargan después del escalado por DPI para que el tamaño salga bien.
                texto.Text = mensaje;
                principal.Text = textoPrincipal;
                secundario.Text = textoSecundario ?? string.Empty;
                icono.Image = CrearIcono(tipo, icono.Width);
                AjustarTamanio();
            }

            
            public static bool Preguntar(IWin32Window propietario, string titulo, string mensaje,
                                         string textoSi, string textoNo,
                                         TipoMensaje tipo = TipoMensaje.Advertencia, bool enfocarNo = true)
            {
                using (var cartel = new Cartel(titulo, mensaje, tipo, textoSi, textoNo, 380))
                {
                    cartel.principal.DialogResult = DialogResult.Yes;
                    cartel.secundario.DialogResult = DialogResult.No;
                    cartel.CancelButton = cartel.secundario;
                    // En acciones que descartan datos, el foco queda en la opción segura.
                    cartel.ActiveControl = enfocarNo ? cartel.secundario : cartel.principal;
                    return cartel.ShowDialog(propietario) == DialogResult.Yes;
                }
            }

            
            public static void Informar(IWin32Window propietario, string titulo, string mensaje,
                                        TipoMensaje tipo = TipoMensaje.Informacion, string textoBoton = "Aceptar",
                                        int anchoTexto = 380)
            {
                using (var cartel = new Cartel(titulo, mensaje, tipo, textoBoton, null, anchoTexto))
                {
                    cartel.principal.DialogResult = DialogResult.OK;
                    cartel.AcceptButton = cartel.principal;
                    cartel.CancelButton = cartel.principal;
                    cartel.ActiveControl = cartel.principal;
                    cartel.ShowDialog(propietario);
                }
            }

            private static void PrepararBoton(Button boton, int tabIndex)
            {
                boton.AutoSize = true;
                boton.Margin = new Padding(8, 0, 0, 0);
                boton.MinimumSize = new Size(96, 32);
                boton.Padding = new Padding(10, 0, 10, 0);
                boton.Size = new Size(96, 32);
                boton.TabIndex = tabIndex;
                boton.UseVisualStyleBackColor = true;
            }

            private void AjustarTamanio()
            {
                int margen = LogicalToDeviceUnits(24);

                int anchoBotones = botonera.Padding.Horizontal + AnchoDeBoton(principal);
                if (conSecundario)
                {
                    anchoBotones += AnchoDeBoton(secundario);
                }

                int ancho = Math.Max(LogicalToDeviceUnits(420), Math.Max(texto.Right + margen, anchoBotones));
                int altoContenido = Math.Max(icono.Bottom, texto.Bottom) + margen;
                ClientSize = new Size(ancho, altoContenido + botonera.Height);
            }

            private static int AnchoDeBoton(Button boton)
            {
                return Math.Max(boton.PreferredSize.Width, boton.MinimumSize.Width) + boton.Margin.Horizontal;
            }

            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);
                switch (tipo)
                {
                    case TipoMensaje.Error:
                        SystemSounds.Hand.Play();
                        break;
                    case TipoMensaje.Advertencia:
                        SystemSounds.Exclamation.Play();
                        break;
                    default:
                        SystemSounds.Asterisk.Play();
                        break;
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    icono.Image?.Dispose();
                    fuenteTexto.Dispose();
                }
                base.Dispose(disposing);
            }

            private static Image CrearIcono(TipoMensaje tipo, int lado)
            {
                switch (tipo)
                {
                    case TipoMensaje.Advertencia:
                        return SystemIcons.Warning.ToBitmap();
                    case TipoMensaje.Error:
                        return SystemIcons.Error.ToBitmap();
                    case TipoMensaje.Exito:
                        return CrearIconoExito(lado);
                    default:
                        return SystemIcons.Information.ToBitmap();
                }
            }

            private static Image CrearIconoExito(int lado)
            {
                
                var imagen = new Bitmap(lado, lado);
                using (Graphics g = Graphics.FromImage(imagen))
                using (var fondo = new SolidBrush(Color.FromArgb(16, 124, 16)))
                using (var tilde = new Pen(Color.White, Math.Max(2f, lado / 10f)))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillEllipse(fondo, 1, 1, lado - 2, lado - 2);

                    tilde.StartCap = LineCap.Round;
                    tilde.EndCap = LineCap.Round;
                    tilde.LineJoin = LineJoin.Round;
                    g.DrawLines(tilde, new[]
                    {
                        new PointF(lado * 0.28f, lado * 0.52f),
                        new PointF(lado * 0.44f, lado * 0.68f),
                        new PointF(lado * 0.73f, lado * 0.36f)
                    });
                }
                return imagen;
            }
        }

        private void NARem_Click(object sender, EventArgs e)
        {

        }

        private void TranspRem_Click(object sender, EventArgs e)
        {

        }
    }
}