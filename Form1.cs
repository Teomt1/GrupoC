using GrupoC.Almacenes;
using GrupoC.EmpaquetarPedido;
using GrupoC.EntregaATransportista;
using GrupoC.IngresoStock;
using GrupoC.RecepcionMercaderia;
using GrupoC.RegistrarOrdenDePreparacion;

namespace GrupoC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CargarCombos();
        }

        private void CargarCombos()
        {
            ComboDepo.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboDepo.DisplayMember = "Nombre";
            ComboDepo.ValueMember = "Codigo";
            ComboDepo.DataSource = DepositoAlmacen.Deposito;
            ComboDepo.SelectedIndex = -1;
            ComboDepo.SelectedIndexChanged += ComboDepo_SelectedIndexChanged;

            ComboEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboEmpleado.DisplayMember = "Nombre";
            ComboEmpleado.ValueMember = "Legajo";
            ComboEmpleado.DataSource = EmpleadoAlmacen.Empleado;
            ComboEmpleado.SelectedIndex = -1;
            ComboEmpleado.SelectedIndexChanged += ComboEmpleado_SelectedIndexChanged;
        }

        private void ComboDepo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ComboDepo.SelectedItem is DepositoEntidad deposito)
            {
                DepositoAlmacen.DepositoActual = deposito.Codigo;
            }
        }

        private void ComboEmpleado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ComboEmpleado.SelectedItem is EmpleadoEntidad empleado)
            {
                EmpleadoAlmacen.EmpleadoActual = empleado.Legajo.ToString();
            }
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
            var cumplir = new GrupoC.CumplirOrdenSeleccion.CumplirOrdenSeleccionForm();
            cumplir.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EntregaATransportistaForm entrega = new EntregaATransportistaForm();
            entrega.Show();
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
           
        }
    }
}
