namespace GrupoC.RegistroExcedente
{
    partial class RegistroExcedenteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpFiltros = new GroupBox();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblDeposito = new Label();
            cmbDeposito = new ComboBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnBuscar = new Button();
            lblOrdenes = new Label();
            dgvOrdenes = new DataGridView();
            dgvOrdenesNroOrden = new DataGridViewTextBoxColumn();
            dgvOrdenesFechaRecepcion = new DataGridViewTextBoxColumn();
            dgvOrdenesCliente = new DataGridViewTextBoxColumn();
            dgvOrdenesRemito = new DataGridViewTextBoxColumn();
            dgvOrdenesDeposito = new DataGridViewTextBoxColumn();
            dgvOrdenesCantEsperada = new DataGridViewTextBoxColumn();
            dgvOrdenesCantRecibida = new DataGridViewTextBoxColumn();
            dgvOrdenesExcedente = new DataGridViewTextBoxColumn();
            lblDetalle = new Label();
            dgvDetalle = new DataGridView();
            grpRegistro = new GroupBox();
            lblNroRegistro = new Label();
            txtNroRegistro = new TextBox();
            lblFechaRegistro = new Label();
            dtpFechaRegistro = new DateTimePicker();
            lblLegajo = new Label();
            txtLegajo = new TextBox();
            lblSupervisor = new Label();
            txtSupervisor = new TextBox();
            lblDecision = new Label();
            rbtAceptar = new RadioButton();
            rbtRechazar = new RadioButton();
            lblMotivo = new Label();
            cmbMotivo = new ComboBox();
            lblObservaciones = new Label();
            txtObservaciones = new TextBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            dgvDetalleSku = new DataGridViewTextBoxColumn();
            dgvDetalleProducto = new DataGridViewTextBoxColumn();
            dgvDetalleCantEsperada = new DataGridViewTextBoxColumn();
            dgvDetalleCantRecibida = new DataGridViewTextBoxColumn();
            dgvDetalleExcedente = new DataGridViewTextBoxColumn();
            dgvDetalleUbicacion = new DataGridViewTextBoxColumn();
            grpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            grpRegistro.SuspendLayout();
            SuspendLayout();
            // 
            // grpFiltros
            // 
            grpFiltros.Controls.Add(lblCliente);
            grpFiltros.Controls.Add(cmbCliente);
            grpFiltros.Controls.Add(lblDeposito);
            grpFiltros.Controls.Add(cmbDeposito);
            grpFiltros.Controls.Add(lblDesde);
            grpFiltros.Controls.Add(dtpDesde);
            grpFiltros.Controls.Add(lblHasta);
            grpFiltros.Controls.Add(dtpHasta);
            grpFiltros.Controls.Add(btnBuscar);
            grpFiltros.Location = new Point(15, 12);
            grpFiltros.Name = "grpFiltros";
            grpFiltros.Size = new Size(910, 70);
            grpFiltros.TabIndex = 0;
            grpFiltros.TabStop = false;
            grpFiltros.Text = "Filtros";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(15, 32);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(47, 15);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.Items.AddRange(new object[] { "Todos", "Distribuidora Norte S.A.", "Alimentos del Sur S.R.L.", "TecnoImport S.A." });
            cmbCliente.Location = new Point(75, 29);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(210, 23);
            cmbCliente.TabIndex = 2;
            // 
            // lblDeposito
            // 
            lblDeposito.AutoSize = true;
            lblDeposito.Location = new Point(300, 32);
            lblDeposito.Name = "lblDeposito";
            lblDeposito.Size = new Size(57, 15);
            lblDeposito.TabIndex = 3;
            lblDeposito.Text = "Depósito:";
            // 
            // cmbDeposito
            // 
            cmbDeposito.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeposito.Items.AddRange(new object[] { "Todos", "Depósito Central", "Depósito Norte", "Depósito Sur" });
            cmbDeposito.Location = new Point(370, 29);
            cmbDeposito.Name = "cmbDeposito";
            cmbDeposito.Size = new Size(150, 23);
            cmbDeposito.TabIndex = 4;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(535, 32);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(42, 15);
            lblDesde.TabIndex = 5;
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(585, 29);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(100, 23);
            dtpDesde.TabIndex = 6;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(695, 32);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(40, 15);
            lblHasta.TabIndex = 7;
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(740, 29);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(100, 23);
            dtpHasta.TabIndex = 8;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(850, 26);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(50, 30);
            btnBuscar.TabIndex = 9;
            btnBuscar.Text = "Buscar";
            // 
            // lblOrdenes
            // 
            lblOrdenes.AutoSize = true;
            lblOrdenes.Location = new Point(15, 92);
            lblOrdenes.Name = "lblOrdenes";
            lblOrdenes.Size = new Size(265, 15);
            lblOrdenes.TabIndex = 10;
            lblOrdenes.Text = "Órdenes de recepción pendientes con excedente:";
            // 
            // dgvOrdenes
            // 
            dgvOrdenes.AllowUserToAddRows = false;
            dgvOrdenes.AllowUserToDeleteRows = false;
            dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenes.Columns.AddRange(new DataGridViewColumn[] { dgvOrdenesNroOrden, dgvOrdenesFechaRecepcion, dgvOrdenesCliente, dgvOrdenesRemito, dgvOrdenesDeposito, dgvOrdenesCantEsperada, dgvOrdenesCantRecibida, dgvOrdenesExcedente });
            dgvOrdenes.Location = new Point(15, 112);
            dgvOrdenes.MultiSelect = false;
            dgvOrdenes.Name = "dgvOrdenes";
            dgvOrdenes.ReadOnly = true;
            dgvOrdenes.RowHeadersVisible = false;
            dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenes.Size = new Size(910, 170);
            dgvOrdenes.TabIndex = 11;
            // 
            // dgvOrdenesNroOrden
            // 
            dgvOrdenesNroOrden.HeaderText = "Nº orden recepción";
            dgvOrdenesNroOrden.Name = "dgvOrdenesNroOrden";
            dgvOrdenesNroOrden.ReadOnly = true;
            dgvOrdenesNroOrden.Width = 120;
            // 
            // dgvOrdenesFechaRecepcion
            // 
            dgvOrdenesFechaRecepcion.HeaderText = "Fecha recepción";
            dgvOrdenesFechaRecepcion.Name = "dgvOrdenesFechaRecepcion";
            dgvOrdenesFechaRecepcion.ReadOnly = true;
            dgvOrdenesFechaRecepcion.Width = 110;
            // 
            // dgvOrdenesCliente
            // 
            dgvOrdenesCliente.HeaderText = "Cliente";
            dgvOrdenesCliente.Name = "dgvOrdenesCliente";
            dgvOrdenesCliente.ReadOnly = true;
            dgvOrdenesCliente.Width = 190;
            // 
            // dgvOrdenesRemito
            // 
            dgvOrdenesRemito.HeaderText = "Nº remito";
            dgvOrdenesRemito.Name = "dgvOrdenesRemito";
            dgvOrdenesRemito.ReadOnly = true;
            // 
            // dgvOrdenesDeposito
            // 
            dgvOrdenesDeposito.HeaderText = "Depósito";
            dgvOrdenesDeposito.Name = "dgvOrdenesDeposito";
            dgvOrdenesDeposito.ReadOnly = true;
            dgvOrdenesDeposito.Width = 120;
            // 
            // dgvOrdenesCantEsperada
            // 
            dgvOrdenesCantEsperada.HeaderText = "Cant. esperada";
            dgvOrdenesCantEsperada.Name = "dgvOrdenesCantEsperada";
            dgvOrdenesCantEsperada.ReadOnly = true;
            dgvOrdenesCantEsperada.Width = 90;
            // 
            // dgvOrdenesCantRecibida
            // 
            dgvOrdenesCantRecibida.HeaderText = "Cant. recibida";
            dgvOrdenesCantRecibida.Name = "dgvOrdenesCantRecibida";
            dgvOrdenesCantRecibida.ReadOnly = true;
            dgvOrdenesCantRecibida.Width = 90;
            // 
            // dgvOrdenesExcedente
            // 
            dgvOrdenesExcedente.HeaderText = "Excedente";
            dgvOrdenesExcedente.Name = "dgvOrdenesExcedente";
            dgvOrdenesExcedente.ReadOnly = true;
            dgvOrdenesExcedente.Width = 87;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(15, 292);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(331, 15);
            lblDetalle.TabIndex = 12;
            lblDetalle.Text = "Detalle de productos con excedente de la orden seleccionada:";
            // 
            // dgvDetalle
            // 
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Columns.AddRange(new DataGridViewColumn[] { dgvDetalleSku, dgvDetalleProducto, dgvDetalleCantEsperada, dgvDetalleCantRecibida, dgvDetalleExcedente, dgvDetalleUbicacion });
            dgvDetalle.Location = new Point(15, 312);
            dgvDetalle.MultiSelect = false;
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(910, 150);
            dgvDetalle.TabIndex = 13;
            // 
            // grpRegistro
            // 
            grpRegistro.Controls.Add(lblNroRegistro);
            grpRegistro.Controls.Add(txtNroRegistro);
            grpRegistro.Controls.Add(lblFechaRegistro);
            grpRegistro.Controls.Add(dtpFechaRegistro);
            grpRegistro.Controls.Add(lblLegajo);
            grpRegistro.Controls.Add(txtLegajo);
            grpRegistro.Controls.Add(lblSupervisor);
            grpRegistro.Controls.Add(txtSupervisor);
            grpRegistro.Controls.Add(lblDecision);
            grpRegistro.Controls.Add(rbtAceptar);
            grpRegistro.Controls.Add(rbtRechazar);
            grpRegistro.Controls.Add(lblMotivo);
            grpRegistro.Controls.Add(cmbMotivo);
            grpRegistro.Controls.Add(lblObservaciones);
            grpRegistro.Controls.Add(txtObservaciones);
            grpRegistro.Location = new Point(18, 474);
            grpRegistro.Name = "grpRegistro";
            grpRegistro.Size = new Size(910, 225);
            grpRegistro.TabIndex = 14;
            grpRegistro.TabStop = false;
            grpRegistro.Text = "Datos del registro";
            // 
            // lblNroRegistro
            // 
            lblNroRegistro.AutoSize = true;
            lblNroRegistro.Location = new Point(15, 32);
            lblNroRegistro.Name = "lblNroRegistro";
            lblNroRegistro.Size = new Size(83, 15);
            lblNroRegistro.TabIndex = 15;
            lblNroRegistro.Text = "Nº de registro:";
            // 
            // txtNroRegistro
            // 
            txtNroRegistro.Location = new Point(130, 29);
            txtNroRegistro.Name = "txtNroRegistro";
            txtNroRegistro.ReadOnly = true;
            txtNroRegistro.Size = new Size(120, 23);
            txtNroRegistro.TabIndex = 16;
            // 
            // lblFechaRegistro
            // 
            lblFechaRegistro.AutoSize = true;
            lblFechaRegistro.Location = new Point(275, 32);
            lblFechaRegistro.Name = "lblFechaRegistro";
            lblFechaRegistro.Size = new Size(41, 15);
            lblFechaRegistro.TabIndex = 17;
            lblFechaRegistro.Text = "Fecha:";
            // 
            // dtpFechaRegistro
            // 
            dtpFechaRegistro.Format = DateTimePickerFormat.Short;
            dtpFechaRegistro.Location = new Point(325, 29);
            dtpFechaRegistro.Name = "dtpFechaRegistro";
            dtpFechaRegistro.Size = new Size(120, 23);
            dtpFechaRegistro.TabIndex = 18;
            // 
            // lblLegajo
            // 
            lblLegajo.AutoSize = true;
            lblLegajo.Location = new Point(15, 70);
            lblLegajo.Name = "lblLegajo";
            lblLegajo.Size = new Size(102, 15);
            lblLegajo.TabIndex = 21;
            lblLegajo.Text = "Legajo supervisor:";
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new Point(130, 67);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.Size = new Size(120, 23);
            txtLegajo.TabIndex = 22;
            // 
            // lblSupervisor
            // 
            lblSupervisor.AutoSize = true;
            lblSupervisor.Location = new Point(275, 70);
            lblSupervisor.Name = "lblSupervisor";
            lblSupervisor.Size = new Size(111, 15);
            lblSupervisor.TabIndex = 23;
            lblSupervisor.Text = "Nombre supervisor:";
            // 
            // txtSupervisor
            // 
            txtSupervisor.Location = new Point(400, 67);
            txtSupervisor.Name = "txtSupervisor";
            txtSupervisor.Size = new Size(490, 23);
            txtSupervisor.TabIndex = 24;
            // 
            // lblDecision
            // 
            lblDecision.AutoSize = true;
            lblDecision.Location = new Point(15, 108);
            lblDecision.Name = "lblDecision";
            lblDecision.Size = new Size(55, 15);
            lblDecision.TabIndex = 25;
            lblDecision.Text = "Decisión:";
            // 
            // rbtAceptar
            // 
            rbtAceptar.Checked = true;
            rbtAceptar.Location = new Point(130, 105);
            rbtAceptar.Name = "rbtAceptar";
            rbtAceptar.Size = new Size(230, 24);
            rbtAceptar.TabIndex = 26;
            rbtAceptar.TabStop = true;
            rbtAceptar.Text = "Aceptar excedente y almacenar";
            // 
            // rbtRechazar
            // 
            rbtRechazar.Location = new Point(366, 103);
            rbtRechazar.Name = "rbtRechazar";
            rbtRechazar.Size = new Size(230, 24);
            rbtRechazar.TabIndex = 27;
            rbtRechazar.Text = "Rechazar y devolver al cliente";
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(15, 146);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(48, 15);
            lblMotivo.TabIndex = 29;
            lblMotivo.Text = "Motivo:";
            // 
            // cmbMotivo
            // 
            cmbMotivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMotivo.Items.AddRange(new object[] { "Error de remito del cliente", "Envío adicional autorizado", "Error de conteo en recepción", "Mercadería no solicitada" });
            cmbMotivo.Location = new Point(130, 143);
            cmbMotivo.Name = "cmbMotivo";
            cmbMotivo.Size = new Size(300, 23);
            cmbMotivo.TabIndex = 30;
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.Location = new Point(450, 146);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(87, 15);
            lblObservaciones.TabIndex = 31;
            lblObservaciones.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(550, 143);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            txtObservaciones.Size = new Size(340, 65);
            txtObservaciones.TabIndex = 32;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(707, 716);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(110, 30);
            btnAceptar.TabIndex = 33;
            btnAceptar.Text = "Registrar";
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(825, 716);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.TabIndex = 34;
            btnCancelar.Text = "Cancelar";
            // 
            // dgvDetalleSku
            // 
            dgvDetalleSku.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalleSku.HeaderText = "SKU";
            dgvDetalleSku.Name = "dgvDetalleSku";
            dgvDetalleSku.ReadOnly = true;
            // 
            // dgvDetalleProducto
            // 
            dgvDetalleProducto.HeaderText = "Producto";
            dgvDetalleProducto.Name = "dgvDetalleProducto";
            dgvDetalleProducto.ReadOnly = true;
            dgvDetalleProducto.Width = 250;
            // 
            // dgvDetalleCantEsperada
            // 
            dgvDetalleCantEsperada.HeaderText = "Cant. esperada";
            dgvDetalleCantEsperada.Name = "dgvDetalleCantEsperada";
            dgvDetalleCantEsperada.ReadOnly = true;
            // 
            // dgvDetalleCantRecibida
            // 
            dgvDetalleCantRecibida.HeaderText = "Cant. recibida";
            dgvDetalleCantRecibida.Name = "dgvDetalleCantRecibida";
            dgvDetalleCantRecibida.ReadOnly = true;
            // 
            // dgvDetalleExcedente
            // 
            dgvDetalleExcedente.HeaderText = "Excedente";
            dgvDetalleExcedente.Name = "dgvDetalleExcedente";
            dgvDetalleExcedente.ReadOnly = true;
            dgvDetalleExcedente.Width = 90;
            // 
            // dgvDetalleUbicacion
            // 
            dgvDetalleUbicacion.HeaderText = "Ubicación destino";
            dgvDetalleUbicacion.Name = "dgvDetalleUbicacion";
            dgvDetalleUbicacion.Width = 157;
            // 
            // RegistroExcedenteForm
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(942, 753);
            Controls.Add(grpFiltros);
            Controls.Add(lblOrdenes);
            Controls.Add(dgvOrdenes);
            Controls.Add(lblDetalle);
            Controls.Add(dgvDetalle);
            Controls.Add(grpRegistro);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "RegistroExcedenteForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registro de Excedente de almacenamiento";
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            grpRegistro.ResumeLayout(false);
            grpRegistro.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblDeposito;
        private System.Windows.Forms.ComboBox cmbDeposito;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblOrdenes;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesNroOrden;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesFechaRecepcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesRemito;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesDeposito;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesCantEsperada;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesCantRecibida;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOrdenesExcedente;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.GroupBox grpRegistro;
        private System.Windows.Forms.Label lblNroRegistro;
        private System.Windows.Forms.TextBox txtNroRegistro;
        private System.Windows.Forms.Label lblFechaRegistro;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label lblLegajo;
        private System.Windows.Forms.TextBox txtLegajo;
        private System.Windows.Forms.Label lblSupervisor;
        private System.Windows.Forms.TextBox txtSupervisor;
        private System.Windows.Forms.Label lblDecision;
        private System.Windows.Forms.RadioButton rbtAceptar;
        private System.Windows.Forms.RadioButton rbtRechazar;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.ComboBox cmbMotivo;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private DataGridViewTextBoxColumn dgvDetalleSku;
        private DataGridViewTextBoxColumn dgvDetalleProducto;
        private DataGridViewTextBoxColumn dgvDetalleCantEsperada;
        private DataGridViewTextBoxColumn dgvDetalleCantRecibida;
        private DataGridViewTextBoxColumn dgvDetalleExcedente;
        private DataGridViewTextBoxColumn dgvDetalleUbicacion;
    }
}
