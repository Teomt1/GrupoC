namespace GrupoC.CumplirOrdenSeleccion
{
    public partial class CumplirOrdenSeleccionForm : Form
    {
        #region Datos de prueba del prototipo (en memoria)

        // Usuario identificado en el sistema: operario de Almacenamiento del depósito DEP01.
        private const string UsuarioActual = "OPA01";
        private const string DepositoActual = "DEP01";

        // Tabla "Órdenes de selección".
        private class OrdenSeleccion
        {
            public int Numero;
            public string Deposito = "";
            public string Estado = "";              // "Pendiente" o "Cumplida"
            public DateTime? FechaHoraCumplida;
            public string? UsuarioCumplida;
        }

        // Tabla "Detalle de orden de selección": un renglón por cada mercadería a bajar.
        private class RenglonOrdenSeleccion
        {
            public int NumeroOrden;
            public string Cliente = "";
            public string Posicion = "";            // X-Y-Z: sector, posición y fila
            public string Sku = "";
            public string Descripcion = "";
            public int Cantidad;
        }

        // Tabla "Inventario": stock de cada cliente por SKU y posición.
        private class ItemInventario
        {
            public string Cliente = "";
            public string Sku = "";
            public string Posicion = "";
            public int Cantidad;
        }

        private readonly List<OrdenSeleccion> ordenesSeleccion = new()
        {
            // Ya cumplida: no se muestra.
            new OrdenSeleccion { Numero = 1000, Deposito = "DEP01", Estado = "Cumplida", FechaHoraCumplida = new DateTime(2026, 10, 21, 9, 30, 0), UsuarioCumplida = "OPA01" },
            // Pendientes de DEP01. Están cargadas desordenadas a propósito: se muestra primero la 1001 (la de número más bajo).
            new OrdenSeleccion { Numero = 1002, Deposito = "DEP01", Estado = "Pendiente" },
            new OrdenSeleccion { Numero = 1001, Deposito = "DEP01", Estado = "Pendiente" },
            // Pendiente pero de otro depósito: no se muestra.
            new OrdenSeleccion { Numero = 1003, Deposito = "DEP02", Estado = "Pendiente" },
        };

        private readonly List<RenglonOrdenSeleccion> detalleOrdenes = new()
        {
            // Orden 1001: las posiciones están desordenadas a propósito para probar el orden del paso 4.
            new RenglonOrdenSeleccion { NumeroOrden = 1001, Cliente = "CLI001", Posicion = "12-43-2", Sku = "AUR-200", Descripcion = "Auriculares inalámbricos BT-200", Cantidad = 4 },
            new RenglonOrdenSeleccion { NumeroOrden = 1001, Cliente = "CLI002", Posicion = "3-10-1",  Sku = "CAR-020", Descripcion = "Cargador USB-C 20W", Cantidad = 10 },
            new RenglonOrdenSeleccion { NumeroOrden = 1001, Cliente = "CLI001", Posicion = "12-5-3",  Sku = "FUN-015", Descripcion = "Funda de silicona para celular", Cantidad = 6 },
            new RenglonOrdenSeleccion { NumeroOrden = 1001, Cliente = "CLI002", Posicion = "3-10-4",  Sku = "MOU-100", Descripcion = "Mouse óptico M100", Cantidad = 2 },
            // Orden 1002
            new RenglonOrdenSeleccion { NumeroOrden = 1002, Cliente = "CLI003", Posicion = "7-2-1",   Sku = "TEC-300", Descripcion = "Teclado inalámbrico K300", Cantidad = 3 },
            new RenglonOrdenSeleccion { NumeroOrden = 1002, Cliente = "CLI003", Posicion = "7-15-2",  Sku = "CAB-001", Descripcion = "Cable HDMI 2 m", Cantidad = 8 },
            // Orden 1003 (otro depósito)
            new RenglonOrdenSeleccion { NumeroOrden = 1003, Cliente = "CLI001", Posicion = "1-1-1",   Sku = "AUR-200", Descripcion = "Auriculares inalámbricos BT-200", Cantidad = 1 },
        };

        private readonly List<ItemInventario> inventario = new()
        {
            new ItemInventario { Cliente = "CLI001", Sku = "AUR-200", Posicion = "12-43-2", Cantidad = 20 },
            new ItemInventario { Cliente = "CLI002", Sku = "CAR-020", Posicion = "3-10-1",  Cantidad = 50 },
            new ItemInventario { Cliente = "CLI001", Sku = "FUN-015", Posicion = "12-5-3",  Cantidad = 30 },
            new ItemInventario { Cliente = "CLI002", Sku = "MOU-100", Posicion = "3-10-4",  Cantidad = 12 },
            new ItemInventario { Cliente = "CLI003", Sku = "TEC-300", Posicion = "7-2-1",   Cantidad = 9 },
            new ItemInventario { Cliente = "CLI003", Sku = "CAB-001", Posicion = "7-15-2",  Cantidad = 40 },
        };

        // Orden de selección que se está mostrando en pantalla.
        private OrdenSeleccion? ordenActual;

        #endregion

        #region Paso 1: el operario abre la pantalla

        public CumplirOrdenSeleccionForm()
        {
            InitializeComponent();

            // Los eventos se conectan acá para no modificar el Designer.
            Shown += CumplirOrdenSeleccionForm_Shown;
            btnConfirmar.Click += btnConfirmar_Click;
        }

        private void CumplirOrdenSeleccionForm_Shown(object? sender, EventArgs e)
        {
            MostrarSiguienteOrden();
        }

        #endregion

        #region Pasos 2 a 4: mostrar la orden de selección pendiente

        private void MostrarSiguienteOrden()
        {
            lvMercaderia.Items.Clear();
            lblFecha.Text = DateTime.Today.ToString("dd/MM/yy");

            // Paso 2: busca las órdenes del depósito del usuario en estado "Pendiente" y toma la de número más bajo.
            ordenActual = ordenesSeleccion
                .Where(o => o.Deposito == DepositoActual && o.Estado == "Pendiente")
                .OrderBy(o => o.Numero)
                .FirstOrDefault();

            // Excepción 2.1: no hay órdenes pendientes. La lista queda vacía y el botón deshabilitado.
            if (ordenActual == null)
            {
                lblOrden.Text = "-";
                btnConfirmar.Enabled = false;
                MessageBox.Show("No hay órdenes de selección pendientes", "Cumplir orden de selección",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Paso 3: muestra el número de la orden (la fecha actual ya se mostró arriba).
            lblOrden.Text = ordenActual.Numero.ToString();

            // Paso 4: muestra la mercadería a bajar, ordenada por posición de menor a mayor (sector, posición y fila).
            int numero = ordenActual.Numero;
            var renglones = detalleOrdenes
                .Where(r => r.NumeroOrden == numero)
                .OrderBy(r => ClaveDePosicion(r.Posicion))
                .ToList();

            foreach (var renglon in renglones)
            {
                var item = new ListViewItem(renglon.Posicion);
                item.SubItems.Add(renglon.Sku);
                item.SubItems.Add(renglon.Descripcion);
                item.SubItems.Add(renglon.Cantidad.ToString());
                lvMercaderia.Items.Add(item);
            }

            btnConfirmar.Enabled = true;
        }

        // Convierte "12-43-2" en (12, 43, 2) para ordenar por número y no como texto
        // (como texto, "12-43-2" quedaría antes que "12-5-3").
        private static (int Sector, int Posicion, int Fila) ClaveDePosicion(string posicion)
        {
            var partes = posicion.Split('-');
            return (int.Parse(partes[0]), int.Parse(partes[1]), int.Parse(partes[2]));
        }

        #endregion

        // Paso 5: el operario baja la mercadería y la lleva a Preparación. Pasa fuera del sistema.
        // Excepción 5.1: si falta mercadería o está dañada, el operario no presiona "Confirmar" y avisa al jefe.
        // La orden sigue "Pendiente" y el caso de uso termina (puede cerrar la pantalla sin guardar nada).

        #region Pasos 6 a 11: confirmar la entrega a Preparación

        private void btnConfirmar_Click(object? sender, EventArgs e)
        {
            // Paso 6: el operario presionó "Confirmar entrega a Preparación".
            if (ordenActual == null) return;

            // Paso 7: el sistema pide confirmación.
            var respuesta = MessageBox.Show(
                $"¿Confirma que entregó a Preparación toda la mercadería de la orden de selección N° {ordenActual.Numero}?",
                "Confirmar entrega a Preparación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Excepción 8.1: presiona "No". No se guarda nada y vuelve al paso 5.
            if (respuesta == DialogResult.No) return;

            // Paso 8: presionó "Sí".
            // Paso 9: la orden queda "Cumplida", con fecha, hora y usuario.
            ordenActual.Estado = "Cumplida";
            ordenActual.FechaHoraCumplida = DateTime.Now;
            ordenActual.UsuarioCumplida = UsuarioActual;

            // Paso 10: por cada renglón, resta la cantidad del inventario (clave: cliente, SKU y posición).
            int numero = ordenActual.Numero;
            foreach (var renglon in detalleOrdenes.Where(r => r.NumeroOrden == numero))
            {
                var stock = inventario.FirstOrDefault(i => i.Cliente == renglon.Cliente
                                                        && i.Sku == renglon.Sku
                                                        && i.Posicion == renglon.Posicion);
                if (stock != null)
                    stock.Cantidad -= renglon.Cantidad;
            }

            // Paso 11: mensaje de éxito y vuelve al paso 2 con la siguiente orden pendiente.
            MessageBox.Show($"Orden de selección N° {numero} cumplida", "Cumplir orden de selección",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            MostrarSiguienteOrden();
        }

        #endregion
    }
}

