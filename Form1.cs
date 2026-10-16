using GrupoC.EmpaquetarPedido;
using GrupoC.IngresoStock;
using GrupoC.RecepcionMercaderia;

namespace GrupoC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ordenprep = new RegistrarOrdenDePreparacion.RegistrarOrdenDePreparacionForm();
            ordenprep.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmpaquetarPedidoForm ordenentreg = new EmpaquetarPedidoForm();
            ordenentreg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IngresoStockForm ingstock = new IngresoStockForm();
            ingstock.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //GenerarOrdenSeleccionForm ordselect = new GenerarOrdenSeleccionForm();
            //ordselect.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*Entrega_a_Transportista entrega = new Entrega_a_Transportista();
            entrega.Show();*/
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*
            Completar_Orden_de_Preparación prepcomp = new Completar_Orden_de_Preparación();
            prepcomp.Show();*/
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RecepcionMercaderiaForm recepcion = new RecepcionMercaderiaForm();
            recepcion.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            /*
            RegistroExcedenteForm registroExcedente = new RegistroExcedenteForm();
            registroExcedente.Show();*/
        }
    }
}
