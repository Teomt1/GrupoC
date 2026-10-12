using System.Data;

namespace GrupoC
{
    partial class IngresoStockForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            gbxBusqueda = new GroupBox();
            btnBuscar = new Button();
            txtNroOrden = new TextBox();
            lblNroOrden = new Label();
            gbxDatosOrden = new GroupBox();
            txtEstadoOrden = new TextBox();
            lblEstadoOrden = new Label();
            txtCliente = new TextBox();
            lblCliente = new Label();
            gbxDetalle = new GroupBox();
            dgvSkus = new DataGridView();
            gbxUbicacion = new GroupBox();
            cmbNivelZ = new ComboBox();
            lblNivel = new Label();
            cmbFilaY = new ComboBox();
            lblFila = new Label();
            cmbPasilloX = new ComboBox();
            lblPasillo = new Label();
            cmbSku = new ComboBox();
            lblSkuSeleccionado = new Label();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            gbxBusqueda.SuspendLayout();
            gbxDatosOrden.SuspendLayout();
            gbxDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSkus).BeginInit();
            gbxUbicacion.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(188, 32);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Ingreso a stock";
            // 
            // gbxBusqueda
            // 
            gbxBusqueda.Controls.Add(btnBuscar);
            gbxBusqueda.Controls.Add(txtNroOrden);
            gbxBusqueda.Controls.Add(lblNroOrden);
            gbxBusqueda.Location = new Point(20, 50);
            gbxBusqueda.Name = "gbxBusqueda";
            gbxBusqueda.Size = new Size(740, 70);
            gbxBusqueda.TabIndex = 5;
            gbxBusqueda.TabStop = false;
            gbxBusqueda.Text = "Búsqueda de Orden de Recepción";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(339, 26);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 26);
            btnBuscar.TabIndex = 0;
            btnBuscar.Text = "Buscar";
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtNroOrden
            // 
            txtNroOrden.Location = new Point(168, 23);
            txtNroOrden.Name = "txtNroOrden";
            txtNroOrden.Size = new Size(150, 27);
            txtNroOrden.TabIndex = 1;
            // 
            // lblNroOrden
            // 
            lblNroOrden.AutoSize = true;
            lblNroOrden.Location = new Point(15, 28);
            lblNroOrden.Name = "lblNroOrden";
            lblNroOrden.Size = new Size(147, 20);
            lblNroOrden.TabIndex = 2;
            lblNroOrden.Text = "N° Orden Recepción:";
            // 
            // gbxDatosOrden
            // 
            gbxDatosOrden.Controls.Add(txtEstadoOrden);
            gbxDatosOrden.Controls.Add(lblEstadoOrden);
            gbxDatosOrden.Controls.Add(txtCliente);
            gbxDatosOrden.Controls.Add(lblCliente);
            gbxDatosOrden.Location = new Point(20, 130);
            gbxDatosOrden.Name = "gbxDatosOrden";
            gbxDatosOrden.Size = new Size(740, 70);
            gbxDatosOrden.TabIndex = 4;
            gbxDatosOrden.TabStop = false;
            gbxDatosOrden.Text = "Datos de la Orden de Recepción";
            // 
            // txtEstadoOrden
            // 
            txtEstadoOrden.Location = new Point(488, 25);
            txtEstadoOrden.Name = "txtEstadoOrden";
            txtEstadoOrden.ReadOnly = true;
            txtEstadoOrden.Size = new Size(200, 27);
            txtEstadoOrden.TabIndex = 0;
            // 
            // lblEstadoOrden
            // 
            lblEstadoOrden.AutoSize = true;
            lblEstadoOrden.Location = new Point(380, 28);
            lblEstadoOrden.Name = "lblEstadoOrden";
            lblEstadoOrden.Size = new Size(102, 20);
            lblEstadoOrden.TabIndex = 1;
            lblEstadoOrden.Text = "Estado Orden:";
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(130, 26);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(230, 27);
            txtCliente.TabIndex = 2;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(15, 28);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(117, 20);
            lblCliente.TabIndex = 3;
            lblCliente.Text = "Cliente / Origen:";
            // 
            // gbxDetalle
            // 
            gbxDetalle.Controls.Add(dgvSkus);
            gbxDetalle.Location = new Point(20, 210);
            gbxDetalle.Name = "gbxDetalle";
            gbxDetalle.Size = new Size(740, 180);
            gbxDetalle.TabIndex = 3;
            gbxDetalle.TabStop = false;
            gbxDetalle.Text = "Ítems y SKUs a Ubicar";
            // 
            // dgvSkus
            // 
            dgvSkus.AllowUserToAddRows = false;
            dgvSkus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSkus.ColumnHeadersHeight = 29;
            dgvSkus.Location = new Point(15, 25);
            dgvSkus.Name = "dgvSkus";
            dgvSkus.ReadOnly = true;
            dgvSkus.RowHeadersWidth = 51;
            dgvSkus.Size = new Size(710, 140);
            dgvSkus.TabIndex = 0;
            // 
            // gbxUbicacion
            // 
            gbxUbicacion.Controls.Add(cmbNivelZ);
            gbxUbicacion.Controls.Add(lblNivel);
            gbxUbicacion.Controls.Add(cmbFilaY);
            gbxUbicacion.Controls.Add(lblFila);
            gbxUbicacion.Controls.Add(cmbPasilloX);
            gbxUbicacion.Controls.Add(lblPasillo);
            gbxUbicacion.Controls.Add(cmbSku);
            gbxUbicacion.Controls.Add(lblSkuSeleccionado);
            gbxUbicacion.Location = new Point(20, 400);
            gbxUbicacion.Name = "gbxUbicacion";
            gbxUbicacion.Size = new Size(740, 110);
            gbxUbicacion.TabIndex = 2;
            gbxUbicacion.TabStop = false;
            gbxUbicacion.Text = "Ubicación Física en Racks (Coordenadas X - Y - Z)";
            // 
            // cmbNivelZ
            // 
            cmbNivelZ.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNivelZ.Location = new Point(521, 67);
            cmbNivelZ.Name = "cmbNivelZ";
            cmbNivelZ.Size = new Size(119, 28);
            cmbNivelZ.TabIndex = 0;
            cmbNivelZ.SelectedIndexChanged += cmbNivelZ_SelectedIndexChanged;
            // 
            // lblNivel
            // 
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(400, 70);
            lblNivel.Name = "lblNivel";
            lblNivel.Size = new Size(115, 20);
            lblNivel.TabIndex = 1;
            lblNivel.Text = "Nivel/Altura (Z):";
            // 
            // cmbFilaY
            // 
            cmbFilaY.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilaY.Location = new Point(300, 67);
            cmbFilaY.Name = "cmbFilaY";
            cmbFilaY.Size = new Size(90, 28);
            cmbFilaY.TabIndex = 2;
            // 
            // lblFila
            // 
            lblFila.AutoSize = true;
            lblFila.Location = new Point(240, 70);
            lblFila.Name = "lblFila";
            lblFila.Size = new Size(57, 20);
            lblFila.TabIndex = 3;
            lblFila.Text = "Fila (Y):";
            // 
            // cmbPasilloX
            // 
            cmbPasilloX.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPasilloX.Location = new Point(130, 67);
            cmbPasilloX.Name = "cmbPasilloX";
            cmbPasilloX.Size = new Size(90, 28);
            cmbPasilloX.TabIndex = 4;
            // 
            // lblPasillo
            // 
            lblPasillo.AutoSize = true;
            lblPasillo.Location = new Point(15, 70);
            lblPasillo.Name = "lblPasillo";
            lblPasillo.Size = new Size(114, 20);
            lblPasillo.TabIndex = 5;
            lblPasillo.Text = "Pasillo/Rack (X):";
            // 
            // cmbSku
            // 
            cmbSku.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSku.Location = new Point(153, 26);
            cmbSku.Name = "cmbSku";
            cmbSku.Size = new Size(200, 28);
            cmbSku.TabIndex = 6;
            // 
            // lblSkuSeleccionado
            // 
            lblSkuSeleccionado.AutoSize = true;
            lblSkuSeleccionado.Location = new Point(15, 30);
            lblSkuSeleccionado.Name = "lblSkuSeleccionado";
            lblSkuSeleccionado.Size = new Size(132, 20);
            lblSkuSeleccionado.TabIndex = 7;
            lblSkuSeleccionado.Text = "SKU Seleccionado:";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(469, 530);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(141, 35);
            btnConfirmar.TabIndex = 1;
            btnConfirmar.Text = "Confirmar ingreso";
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(630, 530);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 35);
            btnCancelar.TabIndex = 0;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // IngresoStockForm
            // 
            ClientSize = new Size(784, 591);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(gbxUbicacion);
            Controls.Add(gbxDetalle);
            Controls.Add(gbxDatosOrden);
            Controls.Add(gbxBusqueda);
            Controls.Add(lblTitulo);
            Name = "IngresoStockForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pampazon S.A. - Ingreso a stock";
            Load += IngresoStockForm_Load;
            gbxBusqueda.ResumeLayout(false);
            gbxBusqueda.PerformLayout();
            gbxDatosOrden.ResumeLayout(false);
            gbxDatosOrden.PerformLayout();
            gbxDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSkus).EndInit();
            gbxUbicacion.ResumeLayout(false);
            gbxUbicacion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private GroupBox gbxBusqueda;
        private Label lblNroOrden;
        private TextBox txtNroOrden;
        private Button btnBuscar;
        private GroupBox gbxDatosOrden;
        private Label lblCliente;
        private TextBox txtCliente;
        private Label lblEstadoOrden;
        private TextBox txtEstadoOrden;
        private GroupBox gbxDetalle;
        private DataGridView dgvSkus;
        private GroupBox gbxUbicacion;
        private Label lblSkuSeleccionado;
        private ComboBox cmbSku;
        private Label lblPasillo;
        private ComboBox cmbPasilloX;
        private Label lblFila;
        private ComboBox cmbFilaY;
        private Label lblNivel;
        private ComboBox cmbNivelZ;
        private Button btnConfirmar;
        private Button btnCancelar;
    }
}