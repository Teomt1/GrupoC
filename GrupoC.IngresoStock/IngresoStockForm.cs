using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GrupoC
{
    public partial class IngresoStockForm : Form

    {
        public IngresoStockForm()
        {
            InitializeComponent();
        }
        private void IngresoStockForm_Load(object sender, EventArgs e)
        {
            if (cmbPasilloX != null)
            {
                cmbPasilloX.Items.Clear();
                cmbPasilloX.Items.AddRange(new object[] { "Pasillo A", "Pasillo B", "Pasillo C", "Pasillo D" });
            }

            if (cmbFilaY != null)
            {
                cmbFilaY.Items.Clear();
                cmbFilaY.Items.AddRange(new object[] { "Fila 1", "Fila 2", "Fila 3", "Fila 4", "Fila 5" });
            }

            if (cmbNivelZ != null)
            {
                cmbNivelZ.Items.Clear();
                cmbNivelZ.Items.AddRange(new object[] { "Nivel 1 (Piso)", "Nivel 2 (Medio)", "Nivel 3 (Alto)" });
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nroOrden = txtNroOrden != null ? txtNroOrden.Text.Trim() : "";

            if (string.IsNullOrEmpty(nroOrden))
            {
                MessageBox.Show("Por favor, ingrese un número de Orden de Recepción.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nroOrden == "1001" || nroOrden == "ORD-1001")
            {
                if (txtCliente != null) txtCliente.Text = "Distribuidora Pampazon S.A.";
                if (txtEstadoOrden != null) txtEstadoOrden.Text = "Pendiente de Ubicación";

                DataTable dt = new DataTable();
                dt.Columns.Add("#");
                dt.Columns.Add("SKU");
                dt.Columns.Add("Descripción");
                dt.Columns.Add("Cantidad");
                dt.Columns.Add("Estado");

                dt.Rows.Add("1", "SKU-PAMP-01", "Caja Termos Acero 1L", "50", "Recepcionado");
                dt.Rows.Add("2", "SKU-PAMP-02", "Bulto Mates de Madera", "120", "Recepcionado");
                dt.Rows.Add("3", "SKU-PAMP-03", "Pack Bombillas Alpaca", "200", "Recepcionado");

                if (dgvSkus != null) dgvSkus.DataSource = dt;

                if (cmbSku != null)
                {
                    cmbSku.Items.Clear();
                    cmbSku.Items.Add("SKU-PAMP-01 - Caja Termos Acero 1L");
                    cmbSku.Items.Add("SKU-PAMP-02 - Bulto Mates de Madera");
                    cmbSku.Items.Add("SKU-PAMP-03 - Pack Bombillas Alpaca");
                    cmbSku.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("No se encontró la Orden de Recepción especificada.", "Orden no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtCliente != null && string.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe buscar una Orden de Recepción válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((cmbPasilloX != null && cmbPasilloX.SelectedIndex == -1) ||
                (cmbFilaY != null && cmbFilaY.SelectedIndex == -1) ||
                (cmbNivelZ != null && cmbNivelZ.SelectedIndex == -1))
            {
                MessageBox.Show("Debe seleccionar las tres coordenadas de ubicación en Racks (Pasillo, Fila y Nivel).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("El ingreso a stock se registró correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbNivelZ_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}