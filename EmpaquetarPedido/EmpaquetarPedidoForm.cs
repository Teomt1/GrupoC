namespace GrupoC.EmpaquetarPedido;

public partial class EmpaquetarPedidoForm : Form
{
    private readonly EmpaquetarPedidoModelo modelo = new();

    public EmpaquetarPedidoForm()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void label6_Click(object sender, EventArgs e)
    {

    }

    private void label10_Click(object sender, EventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void label13_Click(object sender, EventArgs e)
    {

    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {

    }

    private void label20_Click(object sender, EventArgs e)
    {

    }

    private void label21_Click(object sender, EventArgs e)
    {

    }

    private void label22_Click(object sender, EventArgs e)
    {

    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void label17_Click(object sender, EventArgs e)
    {

    }

    private void listView4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void label9_Click(object sender, EventArgs e)
    {

    }

    // Pasos 2 y 3: toma la orden más vieja libre y muestra sus productos.
    private void EmpaquetarPedidoForm_Load(object sender, EventArgs e)
    {
        listViewProdPrep.Items.Clear();

        OrdenPreparacion? orden = modelo.TomarSiguienteOrden();
        if (orden == null)
        {
            // Excepción paso 2: no hay órdenes para empaquetar.
            OrdenPrepNro.Text = "Sin órdenes para preparar";
            buttonConfIEmp.Enabled = false;
            MessageBox.Show("No hay órdenes para preparar", "Empaquetar pedido",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        OrdenPrepNro.Text = $"Orden de prep. N° {orden.Numero}";
        buttonConfIEmp.Enabled = true;
        foreach (RenglonOrdenPreparacion renglon in orden.Renglones)
        {
            var item = new ListViewItem(renglon.Sku);
            item.SubItems.Add(renglon.Cantidad.ToString());
            item.SubItems.Add(modelo.DescripcionTipoPedido(orden.TipoPedido));
            listViewProdPrep.Items.Add(item);
        }
    }

    // Pasos 5 a 13: confirma el empaquetado y genera la orden de entrega.
    private void buttonConfIEmp_Click(object sender, EventArgs e)
    {
        OrdenPreparacion? orden = modelo.OrdenActual;
        if (orden == null)
        {
            return;
        }

        DialogResult respuesta = MessageBox.Show(
            $"¿Confirma que la orden de preparación N° {orden.Numero} quedó empaquetada completa y desea generar la orden de entrega?",
            "Confirmar ingreso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (respuesta != DialogResult.Yes)
        {
            // Excepción paso 6: con "No" no se guarda nada.
            return;
        }

        OrdenEntrega? ordenEntrega = modelo.ConfirmarEmpaquetado();
        if (ordenEntrega == null)
        {
            return;
        }

        MessageBox.Show($"Orden de entrega N° {ordenEntrega.Numero} generada", "Confirmar ingreso",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Paso 13: limpia la grilla y vuelve al paso 2 con la siguiente orden.
        EmpaquetarPedidoForm_Load(sender, e);
    }

    // Excepción "El operario cierra la pantalla": solo se pregunta si hay una orden en pantalla.
    private void EmpaquetarPedidoForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        OrdenPreparacion? orden = modelo.OrdenActual;
        if (orden == null)
        {
            return;
        }

        DialogResult respuesta = MessageBox.Show(
            $"¿Desea salir? La orden N° {orden.Numero} quedará disponible para otro puesto",
            "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (respuesta != DialogResult.Yes)
        {
            e.Cancel = true;
            return;
        }

        // La orden sigue "En preparación" y queda liberada.
        modelo.LiberarOrdenActual();
    }
}
