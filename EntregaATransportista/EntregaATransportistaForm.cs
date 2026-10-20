using System.Drawing.Printing;

namespace GrupoC.EntregaATransportista
{
    public partial class EntregaATransportistaForm : Form
    {
        private readonly EntregaATransportistaModel modelo = new();
        private bool cerrandoPorCancelar;
        private int paginaImpresion;

        public EntregaATransportistaForm()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lstDetalle.Items.Clear();

            var error = modelo.Buscar(txtTransportista.Text, txtDni.Text);
            if (error != null)
            {
                MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ActualizarControles();
                return;
            }

            foreach (var orden in modelo.Ordenes)
            {
                var item = new ListViewItem(orden.OrdenPreparacion.Numero.ToString());
                item.SubItems.Add(orden.OrdenEntrega.CantidadPaquetes.ToString());
                lstDetalle.Items.Add(item);
            }
            ActualizarControles();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            // Si cambian los datos del transportista, la grilla deja de corresponder.
            if (modelo.Remito == null && lstDetalle.Items.Count > 0)
            {
                modelo.Reiniciar();
                lstDetalle.Items.Clear();
                ActualizarControles();
            }
        }

        private void btnGenerarRemito_Click(object sender, EventArgs e)
        {
            var remito = modelo.GenerarRemito();
            ImprimirRemito();
            MessageBox.Show($"Remito N° {remito.Numero} generado. Entregue el original y solicite la firma de la copia.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarControles();
        }

        private void chkRemitoFirmado_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarControles();
        }

        private void btnConfirmarDespacho_Click(object sender, EventArgs e)
        {
            var remito = modelo.Remito!;
            var respuesta = MessageBox.Show(
                $"¿Confirma el despacho de {modelo.Ordenes.Count} órdenes al transportista {modelo.TransportistaNombre} con el remito N° {remito.Numero}?",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            modelo.ConfirmarDespacho();
            MessageBox.Show($"Despacho confirmado. Remito N° {remito.Numero}", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            modelo.Reiniciar();
            LimpiarPantalla();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!ConfirmarCancelacion())
                return;

            cerrandoPorCancelar = true;
            Close();
        }

        private void EntregaATransportistaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cerrar la pantalla con un remito emitido equivale a cancelar la entrega.
            if (cerrandoPorCancelar || modelo.Remito?.Estado != EstadoRemito.Emitido)
                return;

            if (!ConfirmarCancelacion())
                e.Cancel = true;
        }

        private bool ConfirmarCancelacion()
        {
            var respuesta = MessageBox.Show("¿Desea cancelar la entrega? No se registrará el despacho",
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return false;

            modelo.Cancelar();
            return true;
        }

        private void LimpiarPantalla()
        {
            txtTransportista.Clear();
            txtDni.Clear();
            lstDetalle.Items.Clear();
            chkRemitoFirmado.Checked = false;
            ActualizarControles();
            txtTransportista.Focus();
        }

        private void ActualizarControles()
        {
            bool remitoGenerado = modelo.Remito != null;

            txtTransportista.Enabled = !remitoGenerado;
            txtDni.Enabled = !remitoGenerado;
            btnBuscar.Enabled = !remitoGenerado;
            btnGenerarRemito.Enabled = !remitoGenerado && modelo.Ordenes.Count > 0;

            chkRemitoFirmado.Enabled = remitoGenerado;
            if (!remitoGenerado)
                chkRemitoFirmado.Checked = false;

            btnConfirmarDespacho.Enabled = remitoGenerado && chkRemitoFirmado.Checked;
        }

        private void ImprimirRemito()
        {
            using var documento = new PrintDocument { DocumentName = $"Remito {modelo.Remito!.Numero}" };
            paginaImpresion = 0;
            documento.PrintPage += Documento_PrintPage;

            using var vistaPrevia = new PrintPreviewDialog { Document = documento, Width = 800, Height = 900 };
            vistaPrevia.ShowDialog(this);
        }

        // Página 1: original, página 2: copia para firmar.
        private void Documento_PrintPage(object sender, PrintPageEventArgs e)
        {
            var remito = modelo.Remito!;
            var g = e.Graphics!;
            using var fuenteTitulo = new Font("Segoe UI", 16, FontStyle.Bold);
            using var fuente = new Font("Segoe UI", 11);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float renglon = fuente.GetHeight(g) + 4;

            g.DrawString($"REMITO DE ENTREGA N° {remito.Numero} - {(paginaImpresion == 0 ? "ORIGINAL" : "COPIA")}", fuenteTitulo, Brushes.Black, x, y);
            y += fuenteTitulo.GetHeight(g) + 12;

            g.DrawString($"Fecha y hora: {remito.FechaHora:dd/MM/yyyy HH:mm}", fuente, Brushes.Black, x, y); y += renglon;
            g.DrawString($"Depósito: {remito.CodigoDeposito}", fuente, Brushes.Black, x, y); y += renglon;
            g.DrawString($"Transportista: {remito.TransportistaNombre} - DNI {remito.TransportistaDni}", fuente, Brushes.Black, x, y); y += renglon;
            g.DrawString($"Usuario: {remito.CodigoUsuario}", fuente, Brushes.Black, x, y); y += renglon * 2;

            g.DrawString("Orden de preparación", fuente, Brushes.Black, x, y);
            g.DrawString("Paquetes", fuente, Brushes.Black, x + 250, y);
            y += renglon;
            foreach (var orden in modelo.Ordenes)
            {
                g.DrawString(orden.OrdenPreparacion.Numero.ToString(), fuente, Brushes.Black, x, y);
                g.DrawString(orden.OrdenEntrega.CantidadPaquetes.ToString(), fuente, Brushes.Black, x + 250, y);
                y += renglon;
            }
            y += renglon;
            g.DrawString($"Total de paquetes: {modelo.Ordenes.Sum(o => o.OrdenEntrega.CantidadPaquetes)}", fuente, Brushes.Black, x, y);

            if (paginaImpresion == 1)
            {
                y += renglon * 4;
                g.DrawString("Firma del transportista: ______________________________", fuente, Brushes.Black, x, y);
            }

            paginaImpresion++;
            e.HasMorePages = paginaImpresion < 2;
            if (!e.HasMorePages)
                paginaImpresion = 0;
        }
    }
}
