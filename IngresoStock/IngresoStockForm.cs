using System;
using System.Windows.Forms;

namespace GrupoC.IngresoStock
{
    public partial class IngresoStockForm : Form
    {
        private readonly IngresoStockModelo modelo;

        public IngresoStockForm()
        {
            InitializeComponent();

            modelo = new IngresoStockModelo(
                txtNroOrden,
                btnBuscar,
                txtClienteOrigen,
                listViewSkus,
                cmbSkuSeleccionado,
                txtCantidadUbicar,
                cmbPasilloX,
                cmbFilaY,
                cmbNivelZ,
                btnAgregarUbicacion,
                listView1,
                button1,
                btnConfirmar,
                btnCancelar);
        }

        // Estos dos métodos los referencia el Designer, por eso tienen que existir para compilar.
        // Toda la lógica de esos controles la maneja IngresoStockModelo.
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}