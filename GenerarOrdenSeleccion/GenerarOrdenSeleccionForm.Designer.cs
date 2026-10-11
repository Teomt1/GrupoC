namespace GrupoC.GenerarOrdenSeleccion
{
    partial class GenerarOrdenSeleccionForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblDepositoTitulo = new Label();
            lblDeposito = new Label();
            lblNroOrdenTitulo = new Label();
            lblNroOrden = new Label();
            lblFechaTitulo = new Label();
            lblFecha = new Label();
            lblPendientes = new Label();
            lvOrdenesPendientes = new ListView();
            colNroOP = new ColumnHeader();
            colCliente = new ColumnHeader();
            colRecibida = new ColumnHeader();
            colTransportista = new ColumnHeader();
            lblMercaderia = new Label();
            lvMercaderia = new ListView();
            colSKU = new ColumnHeader();
            colDescripcion = new ColumnHeader();
            colPosicion = new ColumnHeader();
            colCantidad = new ColumnHeader();
            btnGenerar = new Button();
            SuspendLayout();
            // 
            // lblDepositoTitulo
            // 
            lblDepositoTitulo.AutoSize = true;
            lblDepositoTitulo.Location = new Point(20, 20);
            lblDepositoTitulo.Name = "lblDepositoTitulo";
            lblDepositoTitulo.Text = "Depósito";
            // 
            // lblDeposito
            // 
            lblDeposito.AutoSize = true;
            lblDeposito.Location = new Point(20, 42);
            lblDeposito.Name = "lblDeposito";
            lblDeposito.Text = "[depósito]";
            // 
            // lblNroOrdenTitulo
            // 
            lblNroOrdenTitulo.AutoSize = true;
            lblNroOrdenTitulo.Location = new Point(240, 20);
            lblNroOrdenTitulo.Name = "lblNroOrdenTitulo";
            lblNroOrdenTitulo.Text = "Nro de orden";
            // 
            // lblNroOrden
            // 
            lblNroOrden.AutoSize = true;
            lblNroOrden.Location = new Point(240, 42);
            lblNroOrden.Name = "lblNroOrden";
            lblNroOrden.Text = "[próximo nro]";
            // 
            // lblFechaTitulo
            // 
            lblFechaTitulo.AutoSize = true;
            lblFechaTitulo.Location = new Point(460, 20);
            lblFechaTitulo.Name = "lblFechaTitulo";
            lblFechaTitulo.Text = "Fecha";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(460, 42);
            lblFecha.Name = "lblFecha";
            lblFecha.Text = "dd/MM/yy [hoy]";
            // 
            // lblPendientes
            // 
            lblPendientes.AutoSize = true;
            lblPendientes.Location = new Point(20, 80);
            lblPendientes.Name = "lblPendientes";
            lblPendientes.Text = "Órdenes de preparación pendientes";
            // 
            // lvOrdenesPendientes
            // 
            lvOrdenesPendientes.Columns.AddRange(new ColumnHeader[] { colNroOP, colCliente, colRecibida, colTransportista });
            lvOrdenesPendientes.FullRowSelect = true;
            lvOrdenesPendientes.GridLines = true;
            lvOrdenesPendientes.Location = new Point(20, 102);
            lvOrdenesPendientes.MultiSelect = false;
            lvOrdenesPendientes.Name = "lvOrdenesPendientes";
            lvOrdenesPendientes.Size = new Size(645, 170);
            lvOrdenesPendientes.UseCompatibleStateImageBehavior = false;
            lvOrdenesPendientes.View = View.Details;
            // 
            // colNroOP
            // 
            colNroOP.Text = "Nro OP";
            colNroOP.Width = 100;
            // 
            // colCliente
            // 
            colCliente.Text = "Cliente";
            colCliente.Width = 200;
            // 
            // colRecibida
            // 
            colRecibida.Text = "Recibida";
            colRecibida.Width = 150;
            // 
            // colTransportista
            // 
            colTransportista.Text = "Transportista";
            colTransportista.Width = 180;
            // 
            // lblMercaderia
            // 
            lblMercaderia.AutoSize = true;
            lblMercaderia.Location = new Point(20, 290);
            lblMercaderia.Name = "lblMercaderia";
            lblMercaderia.Text = "Mercadería a seleccionar";
            // 
            // lvMercaderia
            // 
            lvMercaderia.Columns.AddRange(new ColumnHeader[] { colSKU, colDescripcion, colPosicion, colCantidad });
            lvMercaderia.FullRowSelect = true;
            lvMercaderia.GridLines = true;
            lvMercaderia.Location = new Point(20, 312);
            lvMercaderia.Name = "lvMercaderia";
            lvMercaderia.Size = new Size(645, 170);
            lvMercaderia.UseCompatibleStateImageBehavior = false;
            lvMercaderia.View = View.Details;
            // 
            // colSKU
            // 
            colSKU.Text = "SKU";
            colSKU.Width = 120;
            // 
            // colDescripcion
            // 
            colDescripcion.Text = "Descripción";
            colDescripcion.Width = 260;
            // 
            // colPosicion
            // 
            colPosicion.Text = "Posición";
            colPosicion.Width = 120;
            // 
            // colCantidad
            // 
            colCantidad.Text = "Cantidad";
            colCantidad.Width = 120;
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(20, 500);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(645, 35);
            btnGenerar.Text = "Generar orden de selección";
            btnGenerar.UseVisualStyleBackColor = true;
            // 
            // GenerarOrdenSeleccionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 555);
            Controls.Add(btnGenerar);
            Controls.Add(lvMercaderia);
            Controls.Add(lblMercaderia);
            Controls.Add(lvOrdenesPendientes);
            Controls.Add(lblPendientes);
            Controls.Add(lblFecha);
            Controls.Add(lblFechaTitulo);
            Controls.Add(lblNroOrden);
            Controls.Add(lblNroOrdenTitulo);
            Controls.Add(lblDeposito);
            Controls.Add(lblDepositoTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "GenerarOrdenSeleccionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Generar orden de selección";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDepositoTitulo;
        private Label lblDeposito;
        private Label lblNroOrdenTitulo;
        private Label lblNroOrden;
        private Label lblFechaTitulo;
        private Label lblFecha;
        private Label lblPendientes;
        private ListView lvOrdenesPendientes;
        private ColumnHeader colNroOP;
        private ColumnHeader colCliente;
        private ColumnHeader colRecibida;
        private ColumnHeader colTransportista;
        private Label lblMercaderia;
        private ListView lvMercaderia;
        private ColumnHeader colSKU;
        private ColumnHeader colDescripcion;
        private ColumnHeader colPosicion;
        private ColumnHeader colCantidad;
        private Button btnGenerar;
    }
}