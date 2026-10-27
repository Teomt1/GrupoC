using System;
using System.Windows.Forms;

namespace GrupoC.IngresoStock
{
    public partial class IngresoStockForm : Form
    {
        private readonly IngresoStockModelo modelo = new IngresoStockModelo();

        public IngresoStockForm()
        {
            InitializeComponent();

            modelo.MensajeEmitido += modelo_MensajeEmitido;
            modelo.DatosActualizados += modelo_DatosActualizados;
        }

        private void IngresoStockForm_Load(object sender, EventArgs e)
        {
            cmbPasilloX.Items.Clear();
            cmbPasilloX.Items.AddRange(modelo.Pasillos.ToArray());
            cmbFilaY.Items.Clear();
            cmbFilaY.Items.AddRange(modelo.Filas.ToArray());
            cmbNivelZ.Items.Clear();
            cmbNivelZ.Items.AddRange(modelo.Niveles.ToArray());
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            modelo.NumeroOrden = txtNroOrden.Text;
            modelo.BuscarOrden();
        }

        private void btnAgregarUbicacion_Click(object sender, EventArgs e)
        {
            modelo.SkuSeleccionado = cmbSkuSeleccionado.SelectedItem as ItemOrdenRecepcion;
            modelo.Cantidad = txtCantidadUbicar.Text;
            modelo.Pasillo = cmbPasilloX.Text;
            modelo.Fila = cmbFilaY.Text;
            modelo.Nivel = cmbNivelZ.Text;
            modelo.AgregarUbicacion();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelo.UbicacionSeleccionada = listView1.SelectedItems.Count > 0
                ? listView1.SelectedItems[0].Tag as AsignacionUbicacion
                : null;
        }

        // Botón "Quitar seleccionada"
        private void button1_Click(object sender, EventArgs e)
        {
            modelo.QuitarUbicacion();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            modelo.ConfirmarIngreso();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            modelo.Cancelar();
        }

        private void modelo_MensajeEmitido(object? sender, string mensaje)
        {
            MessageBox.Show(mensaje, Text);
        }

        private void modelo_DatosActualizados(object? sender, EventArgs e)
        {
            txtNroOrden.Text = modelo.NumeroOrden;
            txtClienteOrigen.Text = modelo.ClienteOrigen;

            listViewSkus.Items.Clear();
            foreach (ItemOrdenRecepcion item in modelo.Items)
            {
                listViewSkus.Items.Add(new ListViewItem(new[]
                {
                    item.Sku,
                    item.Descripcion,
                    item.CantidadRecepcionada.ToString(),
                    item.CantidadAUbicar.ToString()
                }));
            }

            cmbSkuSeleccionado.Items.Clear();
            cmbSkuSeleccionado.Items.AddRange(modelo.Items.ToArray());
            cmbSkuSeleccionado.Text = modelo.SkuSeleccionado?.ToString();
            txtCantidadUbicar.Text = modelo.Cantidad;
            cmbPasilloX.Text = modelo.Pasillo;
            cmbFilaY.Text = modelo.Fila;
            cmbNivelZ.Text = modelo.Nivel;

            listView1.Items.Clear();
            foreach (AsignacionUbicacion ubicacion in modelo.Ubicaciones)
            {
                ListViewItem fila = new ListViewItem(new[]
                {
                    ubicacion.Item.Sku,
                    ubicacion.Cantidad.ToString(),
                    ubicacion.Posicion
                });
                fila.Tag = ubicacion;
                listView1.Items.Add(fila);
            }
        }
    }
}
