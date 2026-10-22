#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    /*
     * PROTOTIPO - CU 4: Registrar Orden de Preparación
     * ----------------------------------------------------------------------------
     * Toda la funcionalidad está en este archivo. El Designer NO se modifica:
     * los eventos de botones, combos y radios se vinculan por código en el
     * constructor (método VincularEventos).
     *
     * Los datos son de prueba y viven en memoria (no hay base de datos).
     *
     * Cómo probar cada flujo del caso de uso:
     *  - Camino feliz.............: DEP-01, Palet Cerrado, 10 de "Arroz 1 kg",
     *                               completar el transportista y Confirmar Orden.
     *  - 4.1 / 4.2................: elegir Fullfilment / Palet Cerrado y ver cómo
     *                               aparece / desaparece <Datos para el Etiquetado>.
     *  - 8.1  Stock insuficiente..: DEP-03 -> 50 de "Fideos 500 g" (hay 10).
     *  - 13.1 Campos incompletos..: Confirmar Orden dejando algún campo vacío.
     *  - 13.2 Orden sin productos.: completar todo menos los productos y confirmar.
     *  - 15.2.1 Stock mínimo......: DEP-01 -> 30 de "Yerba 1 kg" (hay 120, mínimo 100).
     *  - 15.1 Sin stock mínimo....: dejar vacío el diccionario de stock mínimo
     *                               de "clienteLoggeado" (más abajo).
     *  - Quitar un producto.......: seleccionarlo en la lista y presionar Supr
     *                               (o hacerle doble click).
     */
    public partial class RegistrarOrdenDePreparacionForm : Form
    {
        #region Datos de prueba del prototipo (en memoria)

        // Son "static" para que se conserven mientras la aplicación esté abierta
        // (por ejemplo, el stock que ya descontó una orden registrada antes).

        // Cliente actualmente loggeado y su stock mínimo contratado por producto
        // (SKU -> cantidad mínima que debe quedar en el depósito).
        // Para probar el paso 15.1 (cliente SIN stock mínimo contratado), reemplazar
        // el diccionario por:  new Dictionary<string, int>()
        private static readonly Cliente clienteLoggeado = new Cliente(
            "Distribuidora Andina S.A.",
            new Dictionary<string, int>
            {
                { "ALM-YER-001", 100 },
                { "ALM-ACE-002", 20 },
                { "BEB-GAS-005", 50 },
                { "LIM-JAB-007", 30 },
            });

        // Productos que el cliente tiene almacenados
        private static readonly List<ProductoCliente> productosDelCliente = new List<ProductoCliente>
        {
            new ProductoCliente("ALM-YER-001", "Yerba 1 kg"),
            new ProductoCliente("ALM-ACE-002", "Aceite 900 ml"),
            new ProductoCliente("ALM-ARR-003", "Arroz 1 kg"),
            new ProductoCliente("ALM-FID-004", "Fideos 500 g"),
            new ProductoCliente("BEB-GAS-005", "Gaseosa 2 L"),
            new ProductoCliente("LIM-LAV-006", "Lavandina 1 L"),
            new ProductoCliente("LIM-JAB-007", "Jabón 800 g"),
        };

        // Depósitos y stock que el cliente tiene en cada uno (SKU -> unidades)
        private static readonly List<Deposito> depositos = new List<Deposito>
        {
            new Deposito("DEP-01", "Depósito Central (Barracas)", new Dictionary<string, int>
            {
                { "ALM-YER-001", 120 },
                { "ALM-ACE-002", 80 },
                { "ALM-ARR-003", 200 },
                { "ALM-FID-004", 150 },
                { "BEB-GAS-005", 60 },
            }),
            new Deposito("DEP-02", "Depósito Norte (Gral. Pacheco)", new Dictionary<string, int>
            {
                { "ALM-YER-001", 40 },
                { "BEB-GAS-005", 300 },
                { "LIM-LAV-006", 90 },
                { "LIM-JAB-007", 35 },
            }),
            new Deposito("DEP-03", "Depósito Sur (Avellaneda)", new Dictionary<string, int>
            {
                { "ALM-ACE-002", 25 },
                { "ALM-ARR-003", 50 },
                { "ALM-FID-004", 10 },
            }),
        };

        // Órdenes registradas durante la ejecución del prototipo
        private static readonly List<OrdenPreparacion> ordenesRegistradas = new List<OrdenPreparacion>();
        private static int proximoNumeroOrden = 1;

        #endregion

        #region Estado de la pantalla

        private readonly List<ItemOrden> itemsOrden = new List<ItemOrden>(); // productos de la orden actual
        private Deposito? depositoActual;     // depósito al que pertenecen los productos cargados
        private bool ignorarCambioDeposito;   // evita disparar el evento cuando el combo se cambia por código

        #endregion

        #region Inicialización

        public RegistrarOrdenDePreparacionForm()
        {
            InitializeComponent();
            VincularEventos();

            // Paso 1: el sistema muestra el nombre del cliente loggeado
            labtemp.Text = clienteLoggeado.RazonSocial;

            LimpiarPantalla();
        }

        /// <summary>
        /// Vincula los eventos por código para no tocar el Designer.
        /// Se hace "-=" antes de "+=" para que, si más adelante alguien vincula el mismo
        /// evento desde el Designer (doble click sobre el control), no se ejecute dos veces.
        /// </summary>
        private void VincularEventos()
        {
            combodepo.SelectedIndexChanged -= combodepo_SelectedIndexChanged;
            combodepo.SelectedIndexChanged += combodepo_SelectedIndexChanged;

            radiocerrado.CheckedChanged -= radiocerrado_CheckedChanged;
            radiocerrado.CheckedChanged += radiocerrado_CheckedChanged;
            radiofullfilment.CheckedChanged -= radiofullfilment_CheckedChanged;
            radiofullfilment.CheckedChanged += radiofullfilment_CheckedChanged;

            btnagregar.Click -= btnagregar_Click;
            btnagregar.Click += btnagregar_Click;
            btnconfirmar.Click -= btnconfirmar_Click;
            btnconfirmar.Click += btnconfirmar_Click;
            btncancelar.Click -= btncancelar_Click;
            btncancelar.Click += btncancelar_Click;

            listordenact.KeyDown -= listordenact_KeyDown;
            listordenact.KeyDown += listordenact_KeyDown;
            listordenact.DoubleClick -= listordenact_DoubleClick;
            listordenact.DoubleClick += listordenact_DoubleClick;

            textcantretirar.KeyDown -= textcantretirar_KeyDown;
            textcantretirar.KeyDown += textcantretirar_KeyDown;

            FormClosing -= RegistrarOrdenDePreparacionForm_FormClosing;
            FormClosing += RegistrarOrdenDePreparacionForm_FormClosing;

            // Comportamiento de teclado (no cambia la apariencia de los controles)
            combodepo.KeyPress += ComboSinEscritura_KeyPress;
            combodepo.KeyDown += ComboSinEscritura_KeyDown;
            comboprodretirar.KeyPress += ComboSinEscritura_KeyPress;
            comboprodretirar.KeyDown += ComboSinEscritura_KeyDown;
            textcantretirar.KeyPress += SoloNumeros_KeyPress;
            textDNI.KeyPress += SoloNumeros_KeyPress;

            // Al abrir la pantalla el cursor arranca en "Depósito" (paso 2)
            Shown += (s, e) => combodepo.Focus();
        }

        /// <summary>Deja la pantalla lista para cargar una orden nueva.</summary>
        private void LimpiarPantalla()
        {
            CargarDepositos();
            depositoActual = null;
            itemsOrden.Clear();
            RefrescarListaOrden();
            CargarProductosDelDeposito();

            radiocerrado.Checked = false;
            radiofullfilment.Checked = false;
            ActualizarSeccionEtiquetado();
            textnombre.Clear();
            textdireccion.Clear();

            datefecha.MinDate = DateTime.Today;   // no se puede elegir una fecha de retiro pasada
            datefecha.Value = DateTime.Today;

            textnomtrans.Clear();
            textDNI.Clear();
            textempresatrans.Clear();
        }

        /// <summary>
        /// Carga solo los depósitos donde el cliente tiene mercadería
        /// (precondición: el cliente ingresó previamente mercadería al depósito).
        /// Reemplaza los ítems de ejemplo ("Depo 1", "Depo 2"...) que trae el Designer.
        /// </summary>
        private void CargarDepositos()
        {
            ignorarCambioDeposito = true;
            try
            {
                combodepo.Items.Clear();
                foreach (var deposito in depositos.Where(d => d.TieneStock()))
                    combodepo.Items.Add(deposito);

                combodepo.SelectedIndex = -1;
                combodepo.Text = string.Empty;
            }
            finally
            {
                ignorarCambioDeposito = false;
            }
        }

        /// <summary>
        /// Carga en "Productos a retirar" los productos del cliente con stock en el depósito elegido.
        /// Reemplaza los ítems de ejemplo ("Prod 1", "Prod 2"...) que trae el Designer.
        /// </summary>
        private void CargarProductosDelDeposito()
        {
            comboprodretirar.Items.Clear();
            comboprodretirar.Text = string.Empty;
            textcantretirar.Clear();

            if (depositoActual == null)
                return;

            foreach (var producto in productosDelCliente)
            {
                if (depositoActual.StockDe(producto.Sku) > 0)
                    comboprodretirar.Items.Add(producto);
            }
        }

        #endregion

        #region Paso 2: selección del depósito

        private void combodepo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ignorarCambioDeposito)
                return;

            var depositoSeleccionado = combodepo.SelectedItem as Deposito;
            if (depositoSeleccionado == depositoActual)
                return;

            // Si ya hay productos cargados, pertenecen al depósito anterior
            if (itemsOrden.Count > 0 && depositoActual != null)
            {
                var respuesta = MessageBox.Show(this,
                    $"La orden actual tiene productos cargados del depósito \"{depositoActual.Nombre}\".\n\n" +
                    "Si cambia de depósito se quitarán todos los productos agregados a la orden.\n" +
                    "¿Desea continuar?",
                    "Cambio de depósito",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (respuesta != DialogResult.Yes)
                {
                    // Se vuelve a dejar seleccionado el depósito anterior. Se hace con BeginInvoke
                    // para que el ComboBox termine de procesar la selección antes de revertirla.
                    var depositoAnterior = depositoActual;
                    BeginInvoke(new Action(() =>
                    {
                        ignorarCambioDeposito = true;
                        try
                        {
                            combodepo.SelectedItem = depositoAnterior;
                        }
                        finally
                        {
                            ignorarCambioDeposito = false;
                        }
                    }));
                    return;
                }

                itemsOrden.Clear();
                RefrescarListaOrden();
            }

            depositoActual = depositoSeleccionado;
            CargarProductosDelDeposito();
        }

        #endregion

        #region Pasos 3 y 4: modalidad de empaquetado

        private void radiocerrado_CheckedChanged(object? sender, EventArgs e)
        {
            ActualizarSeccionEtiquetado();
        }

        private void radiofullfilment_CheckedChanged(object? sender, EventArgs e)
        {
            ActualizarSeccionEtiquetado();
        }

        /// <summary>
        /// 4.1: con "Fullfilment" se muestra la sección &lt;Datos para el Etiquetado&gt;.
        /// 4.2: con "Palet Cerrado" (o sin modalidad elegida) no se muestra.
        /// </summary>
        private void ActualizarSeccionEtiquetado()
        {
            bool mostrar = radiofullfilment.Checked;

            labdatosetiq.Visible = mostrar;
            labnombre.Visible = mostrar;
            textnombre.Visible = mostrar;
            labdireccion.Visible = mostrar;
            textdireccion.Visible = mostrar;
        }

        #endregion

        #region Pasos 5 a 9: agregar productos a la orden

        private void btnagregar_Click(object? sender, EventArgs e)
        {
            var deposito = combodepo.SelectedItem as Deposito;
            if (deposito == null)
            {
                MostrarError("Error: seleccione el depósito del cual desea retirar la mercadería antes de agregar productos.",
                             "Error - Depósito no seleccionado");
                combodepo.Focus();
                return;
            }

            // Paso 5
            var producto = comboprodretirar.SelectedItem as ProductoCliente;
            if (producto == null)
            {
                MostrarError("Error: seleccione un producto de la lista \"Productos a retirar\".",
                             "Error - Producto no seleccionado");
                comboprodretirar.Focus();
                return;
            }

            // Paso 6
            if (!int.TryParse(textcantretirar.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MostrarError("Error: ingrese una cantidad a retirar válida (número entero mayor a 0).",
                             "Error - Cantidad inválida");
                textcantretirar.Focus();
                textcantretirar.SelectAll();
                return;
            }

            // Paso 8: el sistema valida que la cantidad pedida exista en el stock del depósito
            // (se suma lo que ya se haya agregado de ese mismo producto a la orden)
            var itemExistente = itemsOrden.FirstOrDefault(i => i.Sku == producto.Sku);
            int cantidadYaEnOrden = itemExistente?.Cantidad ?? 0;
            int stockDisponible = deposito.StockDe(producto.Sku);

            if (cantidadYaEnOrden + cantidad > stockDisponible)
            {
                // Excepción 8.1: no se agrega a la orden y se vuelve al paso 8
                string yaEnOrden = cantidadYaEnOrden > 0 ? $" (ya hay {cantidadYaEnOrden} en la orden)" : string.Empty;

                MostrarError("Error: Stock insuficiente\n\n" +
                             $"Producto: {producto.Sku} - {producto.Nombre}\n" +
                             $"Depósito: {deposito}\n" +
                             $"Cantidad solicitada: {cantidad}{yaEnOrden}\n" +
                             $"Stock disponible: {stockDisponible}",
                             "Error - Stock insuficiente");
                textcantretirar.Focus();
                textcantretirar.SelectAll();
                return;
            }

            // Paso 9: se agrega a la orden actual (si el producto ya estaba, se suma la cantidad)
            if (itemExistente != null)
                itemExistente.Cantidad += cantidad;
            else
                itemsOrden.Add(new ItemOrden(producto.Sku, producto.Nombre, cantidad));

            RefrescarListaOrden();

            comboprodretirar.SelectedIndex = -1;
            comboprodretirar.Text = string.Empty;
            textcantretirar.Clear();
            comboprodretirar.Focus();
        }

        /// <summary>Muestra el estado actual de la orden en la lista (SKU, Producto, Cantidad).</summary>
        private void RefrescarListaOrden()
        {
            listordenact.BeginUpdate();
            listordenact.Items.Clear();

            foreach (var item in itemsOrden)
            {
                var fila = new ListViewItem(item.Sku);
                fila.SubItems.Add(item.Nombre);
                fila.SubItems.Add(item.Cantidad.ToString());
                fila.Tag = item;
                listordenact.Items.Add(fila);
            }

            listordenact.EndUpdate();
        }

        // Enter en "Cantidad a Retirar" equivale a presionar "Agregar a la Orden"
        private void textcantretirar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnagregar.PerformClick();
            }
        }

        // Supr sobre la lista quita los productos seleccionados de la orden
        private void listordenact_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                QuitarProductosSeleccionados();
            }
        }

        // Doble click sobre un producto de la lista lo quita de la orden
        private void listordenact_DoubleClick(object? sender, EventArgs e)
        {
            QuitarProductosSeleccionados();
        }

        private void QuitarProductosSeleccionados()
        {
            var seleccionados = listordenact.SelectedItems
                .Cast<ListViewItem>()
                .Select(fila => fila.Tag)
                .OfType<ItemOrden>()
                .ToList();

            if (seleccionados.Count == 0)
                return;

            string detalle = string.Join("\n", seleccionados.Select(i => $"• {i.Sku} - {i.Nombre} (cantidad: {i.Cantidad})"));

            var respuesta = MessageBox.Show(this,
                "¿Desea quitar de la orden los siguientes productos?\n\n" + detalle,
                "Quitar productos de la orden",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (respuesta != DialogResult.Yes)
                return;

            foreach (var item in seleccionados)
                itemsOrden.Remove(item);

            RefrescarListaOrden();
        }

        #endregion

        #region Pasos 13 a 16: confirmar la orden

        private void btnconfirmar_Click(object? sender, EventArgs e)
        {
            var deposito = combodepo.SelectedItem as Deposito;

            // ---- Excepción 13.1: algún campo obligatorio sin completar ----
            var camposFaltantes = new List<string>();
            Control? primerCampoFaltante = null;

            void RegistrarFaltante(string nombreCampo, Control? control)
            {
                camposFaltantes.Add("• " + nombreCampo);
                if (primerCampoFaltante == null)
                    primerCampoFaltante = control;
            }

            if (deposito == null)
                RegistrarFaltante("Depósito", combodepo);

            // (no se le pasa el foco a los radio buttons porque eso los tilda solos)
            if (!radiocerrado.Checked && !radiofullfilment.Checked)
                RegistrarFaltante("Modalidad de Empaquetado", null);

            // Paso 11: los datos para el etiquetado solo son obligatorios en Fullfilment
            if (radiofullfilment.Checked)
            {
                if (EstaVacio(textnombre))
                    RegistrarFaltante("Nombre del Destinatario", textnombre);
                if (EstaVacio(textdireccion))
                    RegistrarFaltante("Dirección de Destino", textdireccion);
            }

            // Paso 12: datos del transportista autorizado a retirar
            if (EstaVacio(textnomtrans))
                RegistrarFaltante("Nombre del transportista", textnomtrans);
            if (EstaVacio(textDNI))
                RegistrarFaltante("DNI del transportista", textDNI);
            if (EstaVacio(textempresatrans))
                RegistrarFaltante("Empresa de Transporte", textempresatrans);

            if (camposFaltantes.Count > 0 || deposito == null)
            {
                MostrarError("Error: por favor rellene todos los campos antes de completar la orden\n\n" +
                             "Campos sin completar:\n" + string.Join("\n", camposFaltantes),
                             "Error - Campos obligatorios incompletos");
                primerCampoFaltante?.Focus();
                return;
            }

            // ---- Excepción 13.2: la orden no tiene productos ----
            if (itemsOrden.Count == 0)
            {
                MostrarError("Error: la orden de preparación debe contener al menos 1 producto",
                             "Error - Orden sin productos");
                comboprodretirar.Focus();
                return;
            }

            // ---- Validaciones de formato ----
            string dni = textDNI.Text.Trim();
            if (dni.Length < 7 || dni.Length > 8 || !dni.All(c => c >= '0' && c <= '9'))
            {
                MostrarError("Error: el DNI del transportista debe tener 7 u 8 dígitos (solo números).",
                             "Error - DNI inválido");
                textDNI.Focus();
                textDNI.SelectAll();
                return;
            }

            if (datefecha.Value.Date < DateTime.Today)
            {
                MostrarError("Error: la fecha de retiro no puede ser anterior a la fecha actual.",
                             "Error - Fecha de retiro inválida");
                datefecha.Focus();
                return;
            }

            // ---- Pasos 14 y 15: validación del stock mínimo contratado por producto ----
            var advertencias = new List<string>();

            if (clienteLoggeado.TieneStockMinimoContratado)
            {
                // 15.2: el cliente tiene stock mínimo contratado -> se valida que la orden
                // no haga bajar ningún producto de ese límite en el depósito elegido
                foreach (var item in itemsOrden)
                {
                    if (!clienteLoggeado.StockMinimoPorSku.TryGetValue(item.Sku, out int minimo))
                        continue;   // ese producto no tiene mínimo contratado

                    int stockRestante = deposito.StockDe(item.Sku) - item.Cantidad;
                    if (stockRestante < minimo)
                    {
                        advertencias.Add(
                            $"Advertencia: El producto {item.Sku} ({item.Nombre}) ha quedado por debajo del límite " +
                            "establecido, por favor generar un nuevo ingreso de este.\n" +
                            $"     Stock mínimo contratado: {minimo}  |  Stock luego de la orden: {stockRestante}");
                    }
                }
            }
            // 15.1: si el cliente no tiene stock mínimo contratado no hay nada que validar y se continúa.

            // Excepción 15.2.1: se avisa y luego se continúa con el paso 16
            if (advertencias.Count > 0)
            {
                MessageBox.Show(this,
                    string.Join("\n\n", advertencias),
                    "Advertencia - Stock por debajo del mínimo contratado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // ---- Paso 16: se guarda la orden y se informa al cliente ----
            var orden = GuardarOrden(deposito);

            MessageBox.Show(this,
                "Orden de preparación guardada con éxito\n\n" +
                $"N° de orden: {orden.Numero:D5}\n" +
                $"Estado: {orden.Estado}\n" +
                $"Depósito: {orden.NombreDeposito}\n" +
                $"Modalidad: {orden.Modalidad}\n" +
                $"Fecha de retiro: {orden.FechaRetiro:dd/MM/yyyy}\n" +
                $"Productos: {orden.Items.Count} (unidades: {orden.Items.Sum(i => i.Cantidad)})",
                "Orden de preparación registrada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // La pantalla queda lista para otra orden; el cliente puede salir cuando quiera.
            LimpiarPantalla();
            combodepo.Focus();
        }

        /// <summary>
        /// Registra la orden en estado "Pendiente" y descuenta del stock disponible
        /// las unidades pedidas (quedan comprometidas por la orden).
        /// </summary>
        private OrdenPreparacion GuardarOrden(Deposito deposito)
        {
            bool esFullfilment = radiofullfilment.Checked;

            var orden = new OrdenPreparacion
            {
                Numero = proximoNumeroOrden++,
                FechaCreacion = DateTime.Now,
                Estado = "Pendiente",
                RazonSocialCliente = clienteLoggeado.RazonSocial,
                CodigoDeposito = deposito.Codigo,
                NombreDeposito = deposito.Nombre,
                Modalidad = esFullfilment ? "Fullfilment" : "Palet Cerrado",
                Items = itemsOrden.Select(i => new ItemOrden(i.Sku, i.Nombre, i.Cantidad)).ToList(),
                FechaRetiro = datefecha.Value.Date,
                NombreDestinatario = esFullfilment ? textnombre.Text.Trim() : string.Empty,
                DireccionDestino = esFullfilment ? textdireccion.Text.Trim() : string.Empty,
                NombreTransportista = textnomtrans.Text.Trim(),
                DniTransportista = textDNI.Text.Trim(),
                EmpresaTransporte = textempresatrans.Text.Trim(),
            };

            ordenesRegistradas.Add(orden);

            foreach (var item in orden.Items)
                deposito.DescontarStock(item.Sku, item.Cantidad);

            return orden;
        }

        #endregion

        #region Cancelar / salir de la pantalla

        // El cliente puede cancelar en cualquier momento con el botón "Cancelar"
        private void btncancelar_Click(object? sender, EventArgs e)
        {
            Close();   // la confirmación se pide en FormClosing (también cubre la "X" de la ventana)
        }

        private void RegistrarOrdenDePreparacionForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing || !HayDatosCargados())
                return;

            var respuesta = MessageBox.Show(this,
                "¿Desea cancelar el registro de la orden de preparación?\n\n" +
                "Se perderán todos los datos cargados.",
                "Cancelar orden de preparación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (respuesta != DialogResult.Yes)
                e.Cancel = true;
        }

        private bool HayDatosCargados()
        {
            return itemsOrden.Count > 0
                || combodepo.SelectedItem != null
                || radiocerrado.Checked
                || radiofullfilment.Checked
                || !EstaVacio(textcantretirar)
                || !EstaVacio(textnombre)
                || !EstaVacio(textdireccion)
                || !EstaVacio(textnomtrans)
                || !EstaVacio(textDNI)
                || !EstaVacio(textempresatrans);
        }

        #endregion

        #region Métodos auxiliares

        private void MostrarError(string mensaje, string titulo)
        {
            MessageBox.Show(this, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static bool EstaVacio(TextBox textBox)
        {
            return string.IsNullOrWhiteSpace(textBox.Text);
        }

        // Solo permite números (y teclas de control como Backspace, Ctrl+C, Ctrl+V)
        private void SoloNumeros_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9'))
                e.Handled = true;
        }

        // Los combos solo permiten elegir de la lista: se bloquea la escritura manual
        private void ComboSinEscritura_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Escape)
                e.Handled = true;
        }

        private void ComboSinEscritura_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                e.Handled = true;
        }

        #endregion

        #region Métodos referenciados por el Designer (NO BORRAR)

        // El Designer tiene vinculados estos métodos; si se borran, el proyecto no compila.

        private void label1_Click(object? sender, EventArgs e)            // labusuario.Click
        {
        }

        private void label4_Click(object? sender, EventArgs e)            // labnomtrans.Click
        {
        }

        private void textBox1_TextChanged(object? sender, EventArgs e)    // textnomtrans.TextChanged
        {
        }

        private void textBox2_TextChanged(object? sender, EventArgs e)    // textDNI.TextChanged
        {
            // Si se pega texto con puntos, letras o espacios, se dejan solo los números
            string soloNumeros = new string(textDNI.Text.Where(c => c >= '0' && c <= '9').ToArray());
            if (soloNumeros == textDNI.Text)
                return;

            textDNI.Text = soloNumeros;
            textDNI.SelectionStart = textDNI.Text.Length;
        }

        private void comboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Venía en el Form original. No está vinculado a ningún control; se deja por compatibilidad.
        }

        #endregion

        #region Clases del prototipo (modelo en memoria)

        private sealed class Cliente
        {
            public string RazonSocial { get; }
            public Dictionary<string, int> StockMinimoPorSku { get; }
            public bool TieneStockMinimoContratado => StockMinimoPorSku.Count > 0;

            public Cliente(string razonSocial, Dictionary<string, int> stockMinimoPorSku)
            {
                RazonSocial = razonSocial;
                StockMinimoPorSku = stockMinimoPorSku;
            }
        }

        private sealed class ProductoCliente
        {
            public string Sku { get; }
            public string Nombre { get; }

            public ProductoCliente(string sku, string nombre)
            {
                Sku = sku;
                Nombre = nombre;
            }

            public override string ToString() => Nombre;   // texto que se ve en el ComboBox
        }

        private sealed class Deposito
        {
            private readonly Dictionary<string, int> stockPorSku;

            public string Codigo { get; }
            public string Nombre { get; }

            public Deposito(string codigo, string nombre, Dictionary<string, int> stockPorSku)
            {
                Codigo = codigo;
                Nombre = nombre;
                this.stockPorSku = stockPorSku;
            }

            public int StockDe(string sku) => stockPorSku.TryGetValue(sku, out int stock) ? stock : 0;

            public bool TieneStock() => stockPorSku.Values.Any(stock => stock > 0);

            public void DescontarStock(string sku, int cantidad) => stockPorSku[sku] = StockDe(sku) - cantidad;

            public override string ToString() => $"{Codigo} - {Nombre}";   // texto que se ve en el ComboBox
        }

        private sealed class ItemOrden
        {
            public string Sku { get; }
            public string Nombre { get; }
            public int Cantidad { get; set; }

            public ItemOrden(string sku, string nombre, int cantidad)
            {
                Sku = sku;
                Nombre = nombre;
                Cantidad = cantidad;
            }
        }

        private sealed class OrdenPreparacion
        {
            public int Numero { get; set; }
            public DateTime FechaCreacion { get; set; }
            public string Estado { get; set; } = "Pendiente";
            public string RazonSocialCliente { get; set; } = string.Empty;
            public string CodigoDeposito { get; set; } = string.Empty;
            public string NombreDeposito { get; set; } = string.Empty;
            public string Modalidad { get; set; } = string.Empty;
            public List<ItemOrden> Items { get; set; } = new List<ItemOrden>();
            public DateTime FechaRetiro { get; set; }
            public string NombreDestinatario { get; set; } = string.Empty;
            public string DireccionDestino { get; set; } = string.Empty;
            public string NombreTransportista { get; set; } = string.Empty;
            public string DniTransportista { get; set; } = string.Empty;
            public string EmpresaTransporte { get; set; } = string.Empty;
        }

        #endregion
    }
}