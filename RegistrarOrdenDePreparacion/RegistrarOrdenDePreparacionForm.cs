#nullable enable
namespace GrupoC.RegistrarOrdenDePreparacion
{
    public partial class RegistrarOrdenDePreparacionForm : Form
    {
        private readonly RegistrarOrdenDePreparacionModelo modelo = new();

        public RegistrarOrdenDePreparacionForm()
        {
            InitializeComponent();
        }

        private void RegistrarOrdenDePreparacionForm_Load(object? sender, EventArgs e)
        {
            labtemp.Text = modelo.NombreCliente;

            combodepo.DisplayMember = nameof(Deposito.Nombre);
            combodepo.Items.Clear();
            combodepo.Items.AddRange(modelo.Depositos.ToArray());

            comboprodretirar.DisplayMember = nameof(RegistrarOrdenDePreparacion.Producto.Nombre);
            comboprodretirar.Items.Clear();

            radiomodalidad_CheckedChanged(sender, e);
        }

        private void combodepo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string? mensaje = modelo.SeleccionarDeposito(combodepo.SelectedItem as Deposito);

            comboprodretirar.Items.Clear();
            comboprodretirar.Items.AddRange(modelo.ProductosDisponibles.ToArray());
            listordenact.Items.Clear();

            if (mensaje != null)
            {
                MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radiomodalidad_CheckedChanged(object? sender, EventArgs e)
        {
            if (radiocerrado.Checked)
            {
                modelo.Modalidad = ModalidadEmpaquetado.PaletCerrado;
            }
            else if (radiofullfilment.Checked)
            {
                modelo.Modalidad = ModalidadEmpaquetado.Fullfilment;
            }
            else
            {
                modelo.Modalidad = null;
            }

            labdatosetiq.Visible = modelo.RequiereDatosEtiquetado;
            labnombre.Visible = modelo.RequiereDatosEtiquetado;
            textnombre.Visible = modelo.RequiereDatosEtiquetado;
            labdireccion.Visible = modelo.RequiereDatosEtiquetado;
            textdireccion.Visible = modelo.RequiereDatosEtiquetado;
        }

        private void btnagregar_Click(object? sender, EventArgs e)
        {
            int? cantidad = null;
            if (!string.IsNullOrWhiteSpace(textcantretirar.Text))
            {
                if (!int.TryParse(textcantretirar.Text, out int cantidadIngresada))
                {
                    MessageBox.Show("Error: la cantidad a retirar debe ser un número entero", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cantidad = cantidadIngresada;
            }

            string? error = modelo.AgregarProducto(comboprodretirar.SelectedItem as Producto, cantidad);
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listordenact.Items.Clear();
            foreach (ItemOrden item in modelo.ItemsOrden)
            {
                listordenact.Items.Add(new ListViewItem(new[] { item.Producto.Sku, item.Producto.Nombre, item.Cantidad.ToString() }));
            }

            textcantretirar.Clear();
        }

        private void btnconfirmar_Click(object? sender, EventArgs e)
        {
            int? dni = null;
            if (!string.IsNullOrWhiteSpace(textDNI.Text))
            {
                if (!int.TryParse(textDNI.Text, out int dniIngresado))
                {
                    MessageBox.Show("Error: el DNI debe ser un número entero", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dni = dniIngresado;
            }

            modelo.NombreDestinatario = textnombre.Text;
            modelo.DireccionDestino = textdireccion.Text;
            modelo.NombreTransportista = textnomtrans.Text;
            modelo.DniTransportista = dni;
            modelo.EmpresaTransportista = textempresatrans.Text;

            string? error = modelo.ValidarOrden();
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (string mensaje in modelo.RegistrarOrden())
            {
                MessageBox.Show(mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            combodepo.SelectedIndex = -1;
            radiocerrado.Checked = false;
            radiofullfilment.Checked = false;
            textcantretirar.Clear();
            datefecha.Value = DateTime.Today;
            textnombre.Clear();
            textdireccion.Clear();
            textnomtrans.Clear();
            textDNI.Clear();
            textempresatrans.Clear();
        }

        private void btncancelar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void textcantretirar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
