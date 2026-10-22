using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GrupoC.EmpaquetarPedido
{
    /// <summary>
    /// Pantalla "Empaquetar pedido y generar orden de entrega" (Empaquetar_Pedido_y_Generar_Orden_de_Entrega_Form).
    ///
    /// ARCHIVO ÚNICO: reemplaza a EmpaquetarPedidoForm.cs y no necesita ningún otro archivo.
    /// El Designer (EmpaquetarPedidoForm.Designer.cs) NO se modifica:
    ///  - Se conservan, con el mismo nombre, todos los eventos que tenía este archivo (button1_Click,
    ///    listView1_SelectedIndexChanged, label1_Click, Empaqu_y_Generar_Orden_de_Entrega_Form_Load, etc.),
    ///    así el Designer compila igual. Quedan vacíos para que nada se ejecute dos veces.
    ///  - La lista, el botón y la etiqueta del número de orden se ubican por lo que muestran
    ///    (columna "Producto", texto "Confirmar", texto "Orden"), así funciona con el Designer tal cual está.
    ///    Ver UbicarControlesDelDesigner().
    ///
    /// Funcionamiento (caso de uso v2):
    ///  - Al abrir, el sistema toma la orden de preparación más vieja, muestra "Orden de prep. N° [nro]"
    ///    y carga en la grilla sus productos, cantidades y tipo de pedido.
    ///  - Productos y cantidades NO se editan: quedan como los da la orden de preparación.
    ///    El operario prepara la orden, controla contra la grilla y presiona "Confirmar ingreso".
    ///  - "Confirmar ingreso" exige al menos un producto, pide confirmación y genera la orden de entrega.
    ///  - La X de la ventana pregunta antes de salir.
    /// Los datos de prueba y el cartel con botones personalizados están al final de este archivo.
    /// </summary>
    public partial class EmpaquetarPedidoForm : Form
    {
        // Reglas de los datos que llegan en la orden: producto solo letras, cantidad solo números.
        private static readonly Regex SoloLetrasYEspacios = new Regex(@"^[\p{L} ]+$");

        private readonly EmpaquetadoModelo modelo = new EmpaquetadoModelo();
        private readonly ToolTip ayuda = new ToolTip { AutoPopDelay = 15000, InitialDelay = 400, ReshowDelay = 100 };
        private readonly ToolTip aviso = new ToolTip();

        // Controles del Designer que usa la pantalla (se asignan en UbicarControlesDelDesigner).
        private ListView listaProductos = null!;
        private Button botonConfirmar = null!;
        private Label? etiquetaOrden;
        private int columnaProducto;
        private int columnaCantidad;
        private int columnaTipo;
        private int bordeDerechoEtiquetaOrden;

        private OrdenPreparacion? ordenActual;
        private bool cierreConfirmado;

        public EmpaquetarPedidoForm()
        {
            InitializeComponent();
            UbicarControlesDelDesigner();
            PrepararGrilla();
            ConectarEventos();
            Disposed += (s, e) =>
            {
                ayuda.Dispose();
                aviso.Dispose();
            };
        }

        #region Controles del Designer

        /// <summary>
        /// Ubica en el Designer la lista de productos, el botón "Confirmar ingreso" y la etiqueta
        /// "[Orden de prep. Nro...]" por lo que muestran, sin depender del nombre de cada control.
        /// Si preferís usar los nombres directamente, reemplazá estas asignaciones por los del Designer,
        /// por ejemplo: listaProductos = listView1; botonConfirmar = button1; etiquetaOrden = label1;
        /// </summary>
        private void UbicarControlesDelDesigner()
        {
            List<ListView> listas = BuscarControles<ListView>(this).ToList();
            listaProductos = listas
                .Where(l => l.Columns.Cast<ColumnHeader>().Any(c => Contiene(c.Text, "Producto")))
                .OrderByDescending(l => l.Width * l.Height)
                .FirstOrDefault()
                ?? listas.OrderByDescending(l => l.Width * l.Height).FirstOrDefault()
                ?? throw new InvalidOperationException("La pantalla necesita la lista \"Productos a preparar\" (ListView) del Designer.");

            List<Button> botones = BuscarControles<Button>(this).ToList();
            botonConfirmar = botones.FirstOrDefault(b => Contiene(b.Text, "Confirmar"))
                ?? botones.FirstOrDefault()
                ?? throw new InvalidOperationException("La pantalla necesita el botón \"Confirmar ingreso\" del Designer.");

            etiquetaOrden = BuscarControles<Label>(this).FirstOrDefault(l => Contiene(l.Text, "Orden"));

            columnaProducto = IndiceDeColumna("Producto", 0);
            columnaCantidad = IndiceDeColumna("Cantidad", 1);
            columnaTipo = IndiceDeColumna("Tipo", 2);
        }

        private int IndiceDeColumna(string encabezado, int porDefecto)
        {
            for (int i = 0; i < listaProductos.Columns.Count; i++)
            {
                if (Contiene(listaProductos.Columns[i].Text, encabezado))
                {
                    return i;
                }
            }
            return porDefecto;
        }

        private static IEnumerable<T> BuscarControles<T>(Control contenedor) where T : Control
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control is T buscado)
                {
                    yield return buscado;
                }
                foreach (T hijo in BuscarControles<T>(control))
                {
                    yield return hijo;
                }
            }
        }

        private static bool Contiene(string texto, string buscado)
        {
            return texto.IndexOf(buscado, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion

        #region Inicialización

        /// <summary>Grilla de solo lectura (no cambia su aspecto).</summary>
        private void PrepararGrilla()
        {
            listaProductos.LabelEdit = false;      // productos y cantidades no se editan
            listaProductos.FullRowSelect = true;   // se marca el renglón completo para controlarlo
            listaProductos.MultiSelect = false;
            listaProductos.ShowItemToolTips = true;
        }

        private void ConectarEventos()
        {
            Load += EmpaquetarPedidoForm_Load;
            Shown += EmpaquetarPedidoForm_Shown;
            FormClosing += EmpaquetarPedidoForm_FormClosing;

            // Si el operario intenta cambiar un renglón, se le recuerda que la orden no se modifica.
            listaProductos.MouseDoubleClick += (s, e) => AvisarQueNoSeEdita();
            listaProductos.KeyDown += listaProductos_KeyDown;

            botonConfirmar.Click += botonConfirmar_Click;
        }

        private void EmpaquetarPedidoForm_Load(object? sender, EventArgs e)
        {
            // Evita que se achique la ventana y se corten controles del diseño.
            MinimumSize = Size;

            if (etiquetaOrden != null)
            {
                bordeDerechoEtiquetaOrden = etiquetaOrden.Right;
            }

            ayuda.SetToolTip(listaProductos,
                "Productos, cantidades y tipo de pedido de la orden de preparación." + Environment.NewLine +
                "No se modifican: prepare la orden, controle contra esta lista y confirme.");
            ayuda.SetToolTip(botonConfirmar, "Genera la orden de entrega de esta orden de preparación.");

            MostrarSiguienteOrden();
        }

        private void EmpaquetarPedidoForm_Shown(object? sender, EventArgs e)
        {
            if (ordenActual == null)
            {
                // Excepción paso 2: no hay órdenes listas para empaquetar.
                Cartel.Informar(this, "Empaquetar pedido", "No hay órdenes para preparar.", TipoMensaje.Informacion);
                return;
            }

            botonConfirmar.Focus();
        }

        #endregion

        #region Eventos que ya tenía el formulario (se conservan para que el Designer compile)

        // Se dejan vacíos a propósito: la funcionalidad se conecta en ConectarEventos(),
        // así funciona igual esté o no conectado cada uno en el Designer, y nada se ejecuta dos veces.
        private void button1_Click(object? sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object? sender, EventArgs e) { }
        private void label2_Click(object? sender, EventArgs e) { }
        private void label1_Click(object? sender, EventArgs e) { }
        private void Empaqu_y_Generar_Orden_de_Entrega_Form_Load(object? sender, EventArgs e) { }
        private void label5_Click(object? sender, EventArgs e) { }
        private void label3_Click(object? sender, EventArgs e) { }
        private void label4_Click(object? sender, EventArgs e) { }
        private void label6_Click(object? sender, EventArgs e) { }
        private void label10_Click(object? sender, EventArgs e) { }
        private void textBox1_TextChanged(object? sender, EventArgs e) { }
        private void label13_Click(object? sender, EventArgs e) { }
        private void groupBox1_Enter(object? sender, EventArgs e) { }
        private void label20_Click(object? sender, EventArgs e) { }
        private void label21_Click(object? sender, EventArgs e) { }
        private void label22_Click(object? sender, EventArgs e) { }
        private void listView1_SelectedIndexChanged(object? sender, EventArgs e) { }
        private void label17_Click(object? sender, EventArgs e) { }
        private void button2_Click(object? sender, EventArgs e) { }
        private void listView4_SelectedIndexChanged(object? sender, EventArgs e) { }
        private void label9_Click(object? sender, EventArgs e) { }

        #endregion

        #region Orden de preparación

        /// <summary>Pasos 2 y 3: toma la orden más vieja libre y muestra sus productos tal como vienen.</summary>
        private void MostrarSiguienteOrden()
        {
            listaProductos.Items.Clear();

            ordenActual = modelo.TomarSiguienteOrden();
            if (ordenActual == null)
            {
                MostrarTextoDeOrden("Sin órdenes para preparar");
                return;
            }

            MostrarTextoDeOrden($"Orden de prep. N° {ordenActual.Numero}");
            foreach (RenglonOrden renglon in ordenActual.Renglones)
            {
                AgregarFila(renglon);
            }
        }

        private void MostrarTextoDeOrden(string texto)
        {
            if (etiquetaOrden == null)
            {
                return;
            }

            etiquetaOrden.Text = texto;
            // Se mantiene alineada a la derecha, como en el diseño.
            if (etiquetaOrden.AutoSize && bordeDerechoEtiquetaOrden > 0)
            {
                etiquetaOrden.Left = Math.Max(0, bordeDerechoEtiquetaOrden - etiquetaOrden.Width);
            }
        }

        private void AgregarFila(RenglonOrden renglon)
        {
            int columnas = Math.Max(listaProductos.Columns.Count,
                                    Math.Max(columnaTipo, Math.Max(columnaProducto, columnaCantidad)) + 1);
            var valores = new string[columnas];
            for (int i = 0; i < columnas; i++)
            {
                valores[i] = string.Empty;
            }
            valores[columnaProducto] = renglon.Producto;
            valores[columnaCantidad] = renglon.Cantidad.ToString(CultureInfo.InvariantCulture);
            valores[columnaTipo] = ordenActual?.TipoDePedido ?? string.Empty;

            var fila = new ListViewItem(valores)
            {
                ToolTipText = ordenActual?.TipoDePedido == EmpaquetadoModelo.PaleCerrado
                    ? $"{renglon.Producto}: {renglon.Cantidad} u. Revise que la identificación del palé coincida (no se desarma)."
                    : $"{renglon.Producto}: {renglon.Cantidad} u. a empaquetar."
            };
            listaProductos.Items.Add(fila);
        }

        private void listaProductos_KeyDown(object? sender, KeyEventArgs e)
        {
            // Teclas que en otras grillas editan o borran: acá no hacen nada y se avisa por qué.
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Insert || e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
                AvisarQueNoSeEdita();
            }
        }

        private void AvisarQueNoSeEdita()
        {
            if (ordenActual == null)
            {
                return;
            }

            aviso.Show("Los productos y las cantidades los define la orden de preparación: no se modifican.",
                       listaProductos, 0, -26, 2500);
        }

        #endregion

        #region Confirmar ingreso y salir

        private void botonConfirmar_Click(object? sender, EventArgs e)
        {
            // Obligatorio: al menos un producto en la lista.
            if (ordenActual == null || listaProductos.Items.Count == 0)
            {
                Cartel.Informar(this, "Confirmar ingreso",
                    "Debe cargar como mínimo un producto para confirmar.",
                    TipoMensaje.Advertencia, "OK");
                return;
            }

            // Control de los datos que vinieron en la orden: producto solo letras y cantidad solo números.
            List<RenglonOrden> invalidos = ordenActual.Renglones.Where(r => !RenglonValido(r)).ToList();
            if (invalidos.Count > 0)
            {
                Cartel.Informar(this, "Orden con datos inválidos",
                    $"La orden de preparación N° {ordenActual.Numero} tiene productos con datos inválidos " +
                    "(el producto solo admite letras y la cantidad solo números mayores a cero)." +
                    Environment.NewLine + Environment.NewLine +
                    "No se puede confirmar. Avise al Jefe de Preparación.",
                    TipoMensaje.Error, "OK");
                return;
            }

            // Paso 6: confirmación. Excepción 7: con "No" no se guarda nada.
            string mensaje =
                $"¿Confirma que la orden de preparación N° {ordenActual.Numero} quedó empaquetada completa y desea generar la orden de entrega?" +
                Environment.NewLine + Environment.NewLine +
                $"{Plural(ordenActual.Renglones.Count, "producto", "productos")}  ·  " +
                $"{ordenActual.Renglones.Sum(r => r.Cantidad)} u.  ·  {ordenActual.TipoDePedido}";

            if (!Cartel.Preguntar(this, "Confirmar ingreso", mensaje, "Sí", "No",
                                  TipoMensaje.Informacion, enfocarNo: false))
            {
                return;
            }

            // Pasos 8 a 13 (simulados en memoria).
            OrdenEntrega entrega = modelo.GenerarOrdenDeEntrega(ordenActual);
            Cartel.Informar(this, "Orden de entrega generada", ArmarComprobante(entrega),
                            TipoMensaje.Exito, "Aceptar", anchoTexto: 440);

            MostrarSiguienteOrden();
            if (ordenActual == null)
            {
                Cartel.Informar(this, "Empaquetar pedido", "No hay órdenes para preparar.", TipoMensaje.Informacion);
            }
        }

        private static bool RenglonValido(RenglonOrden renglon)
        {
            return SoloLetrasYEspacios.IsMatch(renglon.Producto) && renglon.Cantidad > 0;
        }

        private static string ArmarComprobante(OrdenEntrega entrega)
        {
            OrdenPreparacion orden = entrega.OrdenPreparacion;
            bool esFulfillment = orden.TipoDePedido == EmpaquetadoModelo.Fulfillment;

            var texto = new StringBuilder();
            texto.AppendLine($"Orden de entrega N° {entrega.Numero} generada.");
            texto.AppendLine();
            texto.AppendLine($"Orden de preparación N° {orden.Numero}  →  {orden.Estado}");
            texto.AppendLine($"Cliente: {orden.Cliente}");
            texto.AppendLine($"Tipo de pedido: {orden.TipoDePedido}");
            texto.AppendLine($"Transportista autorizado: {orden.Transportista}");
            if (esFulfillment)
            {
                texto.AppendLine($"Destinatario: {orden.Destinatario}");
            }
            texto.AppendLine($"Fecha y hora: {entrega.FechaHora.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}  ·  Estado: {entrega.Estado}");
            texto.AppendLine();
            texto.AppendLine("Productos:");
            foreach (RenglonOrden renglon in orden.Renglones)
            {
                texto.AppendLine($"•  {renglon.Producto}: {renglon.Cantidad} u.");
            }
            texto.AppendLine();
            texto.Append(esFulfillment
                ? "Se imprimieron la orden de entrega y la etiqueta del paquete. Pegue la etiqueta en el paquete y deje la mercadería en el área de despacho."
                : "Se imprimió la orden de entrega. Deje el palé en el área de despacho junto con la orden.");
            return texto.ToString();
        }

        private void EmpaquetarPedidoForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (cierreConfirmado || e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            // Sin orden en pantalla no hay nada que descartar.
            if (ordenActual == null)
            {
                return;
            }

            bool salir = Cartel.Preguntar(this, "Cancelar",
                "Si selecciona cancelar, los cambios no se guardarán." + Environment.NewLine + "¿Desea cancelar?",
                "Sí, descartar cambios", "No", TipoMensaje.Advertencia, enfocarNo: true);
            if (!salir)
            {
                e.Cancel = true;
                return;
            }

            // La orden queda "En preparación" y liberada para otro puesto.
            modelo.LiberarOrden(ordenActual);
            cierreConfirmado = true;
        }

        #endregion

        #region Auxiliares

        private static string Plural(int cantidad, string singular, string plural)
        {
            return cantidad + " " + (cantidad == 1 ? singular : plural);
        }

        #endregion

        // =====================================================================================
        //  MODELO Y DATOS DE PRUEBA (en memoria). En el sistema real saldrían de las tablas
        //  Órdenes de preparación, Detalle de orden de preparación, Órdenes de entrega y
        //  Movimientos de mercadería.
        // =====================================================================================

        private sealed class RenglonOrden
        {
            public RenglonOrden(string producto, int cantidad)
            {
                Producto = producto;
                Cantidad = cantidad;
            }

            public string Producto { get; }
            public int Cantidad { get; }
        }

        private sealed class OrdenPreparacion
        {
            public OrdenPreparacion(int numero, string cliente, string tipoDePedido, string transportista,
                                    string destinatario, IEnumerable<RenglonOrden> renglones)
            {
                Numero = numero;
                Cliente = cliente;
                TipoDePedido = tipoDePedido;
                Transportista = transportista;
                Destinatario = destinatario;
                Renglones = renglones.ToList();
            }

            public int Numero { get; }
            public string Cliente { get; }
            public string TipoDePedido { get; }
            public string Transportista { get; }
            public string Destinatario { get; }
            public IReadOnlyList<RenglonOrden> Renglones { get; }
            public string Estado { get; set; } = "En preparación";
            public string? TomadaPor { get; set; }
            public DateTime? FechaPreparada { get; set; }
        }

        private sealed class OrdenEntrega
        {
            public OrdenEntrega(int numero, OrdenPreparacion ordenPreparacion)
            {
                Numero = numero;
                OrdenPreparacion = ordenPreparacion;
                FechaHora = DateTime.Now;
            }

            public int Numero { get; }
            public OrdenPreparacion OrdenPreparacion { get; }
            public DateTime FechaHora { get; }
            public string Estado { get; } = "Pendiente";
        }

        private sealed class EmpaquetadoModelo
        {
            public const string Fulfillment = "Fulfillment";
            public const string PaleCerrado = "Palé cerrado";
            private const string UsuarioActual = "OP-PREP-01";

            private readonly List<OrdenPreparacion> ordenes = CrearOrdenesDePrueba();
            private readonly List<OrdenEntrega> ordenesDeEntrega = new List<OrdenEntrega>();
            private readonly List<string> movimientos = new List<string>();
            private int ultimaOrdenDeEntrega = 7310;

            public IReadOnlyList<OrdenEntrega> OrdenesDeEntrega => ordenesDeEntrega;
            public IReadOnlyList<string> Movimientos => movimientos;

            /// <summary>Paso 2: la orden "En preparación" de número más bajo que no tomó otro puesto.</summary>
            public OrdenPreparacion? TomarSiguienteOrden()
            {
                OrdenPreparacion? orden = ordenes
                    .Where(o => o.Estado == "En preparación" && (o.TomadaPor == null || o.TomadaPor == UsuarioActual))
                    .OrderBy(o => o.Numero)
                    .FirstOrDefault();
                if (orden != null)
                {
                    orden.TomadaPor = UsuarioActual;
                }
                return orden;
            }

            /// <summary>Salida sin confirmar: la orden sigue "En preparación" y queda libre.</summary>
            public void LiberarOrden(OrdenPreparacion orden)
            {
                orden.TomadaPor = null;
            }

            /// <summary>Pasos 8 a 10: orden de entrega "Pendiente", orden "Preparada" y movimiento para facturar.</summary>
            public OrdenEntrega GenerarOrdenDeEntrega(OrdenPreparacion orden)
            {
                ultimaOrdenDeEntrega++;
                var entrega = new OrdenEntrega(ultimaOrdenDeEntrega, orden);
                ordenesDeEntrega.Add(entrega);

                orden.Estado = "Preparada";
                orden.FechaPreparada = entrega.FechaHora;
                orden.TomadaPor = null;

                movimientos.Add($"{orden.Cliente};{orden.Numero};{orden.TipoDePedido};{entrega.FechaHora:yyyy-MM-dd}");
                return entrega;
            }

            private static List<OrdenPreparacion> CrearOrdenesDePrueba()
            {
                // Productos solo con letras y cantidades numéricas, tal como los da la orden de preparación.
                return new List<OrdenPreparacion>
                {
                    new OrdenPreparacion(2045, "Distribuidora Andina", Fulfillment, "Expreso Litoral", "Almacén Don Pedro",
                        new[]
                        {
                            new RenglonOrden("Yerba mate", 10),
                            new RenglonOrden("Aceite de girasol", 6),
                            new RenglonOrden("Arroz largo fino", 8)
                        }),

                    new OrdenPreparacion(2046, "ElectroSur", PaleCerrado, "Transportes Del Sur", "Depósito ElectroSur Rosario",
                        new[]
                        {
                            new RenglonOrden("Licuadora", 24),
                            new RenglonOrden("Pava eléctrica", 36)
                        })
                };
            }
        }

        // =====================================================================================
        //  CARTEL MODAL con textos de botones personalizados (MessageBox no permite
        //  "Sí, descartar cambios" ni "OK" en español). Se arma por código: no necesita Designer.
        // =====================================================================================

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

            /// <summary>Pregunta con dos botones. True si se eligió el botón principal.</summary>
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

            /// <summary>Aviso con un solo botón.</summary>
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
                // Círculo verde con tilde blanca, dibujado para no depender de archivos de recursos.
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
    }
}