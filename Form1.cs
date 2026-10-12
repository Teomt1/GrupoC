using GrupoC.Empaquetar_Pedido_y_Generar_Orden_de_Entrega;
using GrupoC.GenerarOrdenSeleccion;
using GrupoC.GrupoC.Registrar_Orden_de_Preparación;

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
            Registrar_orden_de_preparación ordenprep = new Registrar_orden_de_preparación();
            ordenprep.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Empaqu_y_Generar_Orden_de_Entrega_Form ordenentreg = new Empaqu_y_Generar_Orden_de_Entrega_Form();
            ordenentreg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IngresoStockForm ingstock = new IngresoStockForm();
            ingstock.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenerarOrdenSeleccionForm ordselect = new GenerarOrdenSeleccionForm();
            ordselect.Show();
        }
    }
}
