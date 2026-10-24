using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class RegistrarOrdenDePreparacionModelo
    {
        private readonly Form formulario;
        private readonly Label labtemp;
        private readonly ComboBox combodepo;
        private readonly RadioButton radiocerrado;
        private readonly RadioButton radiofullfilment;
        private readonly Label labdatosetiq;
        private readonly Label labnombre;
        private readonly TextBox textnombre;
        private readonly Label labdireccion;
        private readonly TextBox textdireccion;
        private readonly ComboBox comboprodretirar;
        private readonly TextBox textcantretirar;
        private readonly Button btnagregar;
        private readonly ListView listordenact;
        private readonly DateTimePicker datefecha;
        private readonly TextBox textnomtrans;
        private readonly TextBox textDNI;
        private readonly TextBox textempresatrans;
        private readonly Button btnconfirmar;
        private readonly Button btncancelar;

        // Datos del prototipo
        private readonly Cliente clienteLoggeado;
        private readonly List<Deposito> depositos;
        private readonly List<OrdenDePreparacion> ordenesRegistradas;
        private OrdenDePreparacion ordenActual;
        private Deposito? depositoActual;

        public RegistrarOrdenDePreparacionModelo(
            Form formulario,
            Label labtemp,
            ComboBox combodepo,
            RadioButton radiocerrado,
            RadioButton radiofullfilment,
            Label labdatosetiq,
            Label labnombre,
            TextBox textnombre,
            Label labdireccion,
            TextBox textdireccion,
            ComboBox comboprodretirar,
            TextBox textcantretirar,
            Button btnagregar,
            ListView listordenact,
            DateTimePicker datefecha,
            TextBox textnomtrans,
            TextBox textDNI,
            TextBox textempresatrans,
            Button btnconfirmar,
            Button btncancelar)
        {
            this.formulario = formulario;
            this.labtemp = labtemp;
            this.combodepo = combodepo;
            this.radiocerrado = radiocerrado;
            this.radiofullfilment = radiofullfilment;
            this.labdatosetiq = labdatosetiq;
            this.labnombre = labnombre;
            this.textnombre = textnombre;
            this.labdireccion = labdireccion;
            this.textdireccion = textdireccion;
            this.comboprodretirar = comboprodretirar;
            this.textcantretirar = textcantretirar;
            this.btnagregar = btnagregar;
            this.listordenact = listordenact;
            this.datefecha = datefecha;
            this.textnomtrans = textnomtrans;
            this.textDNI = textDNI;
            this.textempresatrans = textempresatrans;
            this.btnconfirmar = btnconfirmar;
            this.btncancelar = btncancelar;
            combodepo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboprodretirar.DropDownStyle = ComboBoxStyle.DropDownList;

            // ---------- Datos hardcodeados ----------
            Producto yerba = new Producto("SKU-1001", "Yerba Mate 1 kg");
            Producto aceite = new Producto("SKU-1002", "Aceite de Girasol 1,5 L");
            Producto arroz = new Producto("SKU-1003", "Arroz Largo Fino 1 kg");
            Producto fideos = new Producto("SKU-1004", "Fideos Spaghetti 500 g");
            Producto azucar = new Producto("SKU-1005", "Azúcar 1 kg");

            // Cliente loggeado con stock mínimo contratado para algunos productos (paso 15.2).
            // Para probar el paso 15.1, dejar la lista de stocks mínimos vacía.
            clienteLoggeado = new Cliente(
                "Distribuidora Los Andes S.A.",
                new List<StockMinimo>
                {
                    new StockMinimo(yerba, 100),
                    new StockMinimo(aceite, 50)
                });

            // Stock que el cliente ingresó previamente en cada depósito (precondición).
            depositos = new List<Deposito>
            {
                new Deposito("Depósito Barracas", new List<StockProducto>
                {
                    new StockProducto(yerba, 500),
                    new StockProducto(aceite, 200),
                    new StockProducto(arroz, 350)
                }),
                new Deposito("Depósito Avellaneda", new List<StockProducto>
                {
                    new StockProducto(fideos, 800),
                    new StockProducto(azucar, 150),
                    new StockProducto(yerba, 120)
                }),
                new Deposito("Depósito Pilar", new List<StockProducto>
                {
                    new StockProducto(arroz, 60),
                    new StockProducto(aceite, 40),
                    new StockProducto(fideos, 300)
                })
            };

            ordenesRegistradas = new List<OrdenDePreparacion>();
            ordenActual = new OrdenDePreparacion(clienteLoggeado);

            SuscribirEventos();
            CargarDepositos();
            LimpiarPantalla();
        }

        // ---------- Inicialización ----------

        private void SuscribirEventos()
        {
            combodepo.SelectedIndexChanged += combodepo_SelectedIndexChanged;
            radiocerrado.CheckedChanged += radiomodalidad_CheckedChanged;
            radiofullfilment.CheckedChanged += radiomodalidad_CheckedChanged;
            textcantretirar.KeyPress += textsolonumeros_KeyPress;
            textDNI.KeyPress += textsolonumeros_KeyPress;
            btnagregar.Click += btnagregar_Click;
            btnconfirmar.Click += btnconfirmar_Click;
            btncancelar.Click += btncancelar_Click;
        }

        private void CargarDepositos()
        {
            combodepo.Items.Clear();
            foreach (Deposito deposito in depositos)
            {
                combodepo.Items.Add(deposito);
            }
        }

        private void LimpiarPantalla()
        {
            ordenActual = new OrdenDePreparacion(clienteLoggeado);

            // Paso 1: se muestra el nombre del cliente loggeado
            labtemp.Text = clienteLoggeado.Nombre;

            combodepo.SelectedIndex = -1;
            combodepo.Text = string.Empty;
            depositoActual = null;
            CargarProductosDelDeposito();

            radiocerrado.Checked = false;
            radiofullfilment.Checked = false;
            MostrarSeccionEtiquetado(false);

            textcantretirar.Clear();
            MostrarItemsDeLaOrden();

            datefecha.Value = DateTime.Today;

            textnomtrans.Clear();
            textDNI.Clear();
            textempresatrans.Clear();
        }

        // ---------- Paso 2: selección de depósito ----------

        private void combodepo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Deposito? seleccionado = combodepo.SelectedItem as Deposito;
            if (seleccionado == depositoActual)
            {
                return;
            }

            // El stock se valida por depósito, por eso al cambiarlo se vacía la orden actual.
            if (ordenActual.Items.Count > 0)
            {
                ordenActual.VaciarProductos();
                MessageBox.Show(
                    "Se cambió el depósito, la orden actual ha sido reiniciada",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            depositoActual = seleccionado;
            CargarProductosDelDeposito();
            MostrarItemsDeLaOrden();
        }

        private void CargarProductosDelDeposito()
        {
            comboprodretirar.Items.Clear();
            comboprodretirar.Text = string.Empty;

            if (depositoActual == null)
            {
                return;
            }

            foreach (Producto producto in depositoActual.ObtenerProductosConStock())
            {
                comboprodretirar.Items.Add(producto);
            }
        }

        // ---------- Paso 3: modalidad de empaquetado (4.1 / 4.2) ----------

        private void radiomodalidad_CheckedChanged(object? sender, EventArgs e)
        {
            MostrarSeccionEtiquetado(radiofullfilment.Checked);
        }

        private void MostrarSeccionEtiquetado(bool mostrar)
        {
            labdatosetiq.Visible = mostrar;
            labnombre.Visible = mostrar;
            textnombre.Visible = mostrar;
            labdireccion.Visible = mostrar;
            textdireccion.Visible = mostrar;

            if (!mostrar)
            {
                textnombre.Clear();
                textdireccion.Clear();
            }
        }

        // ---------- Pasos 5 a 9: agregar productos a la orden ----------

        private void textsolonumeros_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnagregar_Click(object? sender, EventArgs e)
        {
            Deposito? deposito = combodepo.SelectedItem as Deposito;
            if (deposito == null)
            {
                MostrarError("Error: seleccione el depósito del cual quiere retirar mercadería");
                return;
            }

            Producto? producto = comboprodretirar.SelectedItem as Producto;
            if (producto == null)
            {
                MostrarError("Error: seleccione un producto de la lista de productos a retirar");
                return;
            }

            if (!int.TryParse(textcantretirar.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MostrarError("Error: ingrese una cantidad a retirar mayor a 0");
                return;
            }

            // Paso 8 / Excepción 8.1: la cantidad total pedida debe existir en el stock del depósito
            int cantidadTotal = ordenActual.ObtenerCantidad(producto) + cantidad;
            if (cantidadTotal > deposito.ObtenerStock(producto))
            {
                MostrarError("Error: Stock insuficiente");
                textcantretirar.Focus();
                textcantretirar.SelectAll();
                return;
            }

            // Paso 9
            ordenActual.AgregarProducto(producto, cantidad);
            MostrarItemsDeLaOrden();

            comboprodretirar.SelectedIndex = -1;
            comboprodretirar.Text = string.Empty;
            textcantretirar.Clear();
        }

        private void MostrarItemsDeLaOrden()
        {
            listordenact.Items.Clear();
            foreach (ItemOrden item in ordenActual.Items)
            {
                ListViewItem fila = new ListViewItem(item.Producto.Sku);
                fila.SubItems.Add(item.Producto.Nombre);
                fila.SubItems.Add(item.Cantidad.ToString());
                listordenact.Items.Add(fila);
            }
        }

        // ---------- Pasos 14 a 16: confirmar la orden ----------

        private void btnconfirmar_Click(object? sender, EventArgs e)
        {
            Deposito? deposito = combodepo.SelectedItem as Deposito;

            // Excepción 13.1
            if (deposito == null || !CamposObligatoriosCompletos())
            {
                MostrarError("Error: por favor rellene todos los campos antes de completar la orden");
                return;
            }

            // Excepción 13.2
            if (ordenActual.Items.Count == 0)
            {
                MostrarError("Error: la orden de preparación debe contener al menos 1 producto");
                return;
            }

            string dni = textDNI.Text.Trim();
            if (dni.Length < 7 || !dni.All(char.IsDigit))
            {
                MostrarError("Error: el DNI del transportista debe tener 7 u 8 dígitos numéricos");
                return;
            }

            if (datefecha.Value.Date < DateTime.Today)
            {
                MostrarError("Error: la fecha de retiro no puede ser anterior a la fecha actual");
                return;
            }

            ordenActual.Deposito = deposito;
            ordenActual.Modalidad = radiofullfilment.Checked
                ? ModalidadEmpaquetado.Fullfilment
                : ModalidadEmpaquetado.PaletCerrado;
            ordenActual.FechaRetiro = datefecha.Value.Date;

            if (ordenActual.Modalidad == ModalidadEmpaquetado.Fullfilment)
            {
                ordenActual.NombreDestinatario = textnombre.Text.Trim();
                ordenActual.DireccionDestino = textdireccion.Text.Trim();
            }

            ordenActual.Transportista = new Transportista(
                textnomtrans.Text.Trim(),
                dni,
                textempresatrans.Text.Trim());

            // Paso 15 / Excepción 15.2.1
            List<string> advertencias = ValidarStockMinimo(deposito);
            if (advertencias.Count > 0)
            {
                MessageBox.Show(
                    string.Join(Environment.NewLine + Environment.NewLine, advertencias),
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            // Paso 16: se guarda la orden en estado "Pendiente" y se descuenta el stock
            foreach (ItemOrden item in ordenActual.Items)
            {
                deposito.DescontarStock(item.Producto, item.Cantidad);
            }
            ordenesRegistradas.Add(ordenActual);

            MessageBox.Show(
                "Orden de preparación guardada con éxito",
                "Orden registrada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarPantalla();
        }

        private bool CamposObligatoriosCompletos()
        {
            if (!radiocerrado.Checked && !radiofullfilment.Checked)
            {
                return false;
            }

            if (radiofullfilment.Checked
                && (string.IsNullOrWhiteSpace(textnombre.Text) || string.IsNullOrWhiteSpace(textdireccion.Text)))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(textnomtrans.Text)
                && !string.IsNullOrWhiteSpace(textDNI.Text)
                && !string.IsNullOrWhiteSpace(textempresatrans.Text);
        }

        private List<string> ValidarStockMinimo(Deposito deposito)
        {
            List<string> advertencias = new List<string>();

            // 15.1: el cliente no tiene stock mínimo contratado
            if (!clienteLoggeado.TieneStockMinimoContratado())
            {
                return advertencias;
            }

            // 15.2: se valida que la orden no deje ningún producto por debajo de su mínimo
            foreach (ItemOrden item in ordenActual.Items)
            {
                StockMinimo? minimo = clienteLoggeado.BuscarStockMinimo(item.Producto);
                if (minimo == null)
                {
                    continue;
                }

                int stockRestante = deposito.ObtenerStock(item.Producto) - item.Cantidad;
                if (stockRestante < minimo.CantidadMinima)
                {
                    advertencias.Add(
                        $"Advertencia: El producto {item.Producto.Nombre} ({item.Producto.Sku}) ha quedado por debajo del límite establecido, por favor generar un nuevo ingreso de este");
                }
            }

            return advertencias;
        }

        // ---------- Salida: cancelar ----------

        private void btncancelar_Click(object? sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Desea cancelar la orden de preparación? Se perderán los datos cargados.",
                "Cancelar orden",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                formulario.Close();
            }
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
