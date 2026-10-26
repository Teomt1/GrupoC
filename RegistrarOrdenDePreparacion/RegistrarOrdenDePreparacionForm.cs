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

            datefecha.MinDate = DateTime.Today;
            datefecha.Value = DateTime.Today;

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
            comboprodretirar.Items.Clear();
            comboprodretirar.SelectedIndex = -1;
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

        // Bloquea al tipear cualquier caracter que no sea un dígito.
        private void textsolonumeros_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Bloquea al tipear cualquier caracter que no sea una letra o un espacio.
        private void textsololetras_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        // Los TextChanged quitan los caracteres inválidos que lleguen al pegar texto.
        private void textcantretirar_TextChanged(object? sender, EventArgs e)
        {
            FiltrarTexto(textcantretirar, char.IsDigit);
        }

        private void textDNI_TextChanged(object? sender, EventArgs e)
        {
            FiltrarTexto(textDNI, char.IsDigit);
        }

        private void textnomtrans_TextChanged(object? sender, EventArgs e)
        {
            FiltrarTexto(textnomtrans, c => char.IsLetter(c) || c == ' ');
        }

        private void textnombre_TextChanged(object? sender, EventArgs e)
        {
            FiltrarTexto(textnombre, c => char.IsLetter(c) || c == ' ');
        }

        private static void FiltrarTexto(TextBox textBox, Func<char, bool> esValido)
        {
            string filtrado = new string(textBox.Text.Where(esValido).ToArray());
            if (filtrado == textBox.Text)
            {
                return;
            }

            int posicion = Math.Max(0, textBox.SelectionStart - (textBox.Text.Length - filtrado.Length));
            textBox.Text = filtrado;
            textBox.SelectionStart = Math.Min(posicion, filtrado.Length);
        }
    }
}
