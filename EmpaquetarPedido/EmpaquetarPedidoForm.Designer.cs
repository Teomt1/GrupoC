namespace GrupoC.EmpaquetarPedido
{
    partial class EmpaquetarPedidoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            Prodaprep = new Label();
            listViewProdPrep = new ListView();
            Producto = new ColumnHeader();
            Cantidad = new ColumnHeader();
            Tipo_de_Pedido = new ColumnHeader();
            buttonConfIEmp = new Button();
            OrdenPrepNro = new Label();
            lblpaquetes = new Label();
            textpaquetes = new TextBox();
            btnpaquetes = new Button();
            SuspendLayout();
            // 
            // Prodaprep
            // 
            Prodaprep.AutoSize = true;
            Prodaprep.Location = new Point(11, 67);
            Prodaprep.Margin = new Padding(1, 0, 1, 0);
            Prodaprep.Name = "Prodaprep";
            Prodaprep.Size = new Size(117, 15);
            Prodaprep.TabIndex = 3;
            Prodaprep.Text = "Productos a preparar";
            // 
            // listViewProdPrep
            // 
            listViewProdPrep.Columns.AddRange(new ColumnHeader[] { Producto, Cantidad, Tipo_de_Pedido });
            listViewProdPrep.Location = new Point(11, 113);
            listViewProdPrep.Margin = new Padding(1, 2, 1, 2);
            listViewProdPrep.Name = "listViewProdPrep";
            listViewProdPrep.BackColor = Color.White;
            listViewProdPrep.ForeColor = Color.FromArgb(64, 64, 64);
            listViewProdPrep.Size = new Size(504, 301);
            listViewProdPrep.TabIndex = 4;
            listViewProdPrep.UseCompatibleStateImageBehavior = false;
            listViewProdPrep.View = View.Details;
            // 
            // Producto
            // 
            Producto.Text = "Producto";
            Producto.Width = 140;
            // 
            // Cantidad
            // 
            Cantidad.Text = "Cantidad";
            Cantidad.Width = 140;
            // 
            // Tipo_de_Pedido
            // 
            Tipo_de_Pedido.Text = "Tipo_de_pedido";
            Tipo_de_Pedido.Width = 140;
            // 
            // buttonConfIEmp
            // 
            buttonConfIEmp.Location = new Point(11, 589);
            buttonConfIEmp.Margin = new Padding(1, 2, 1, 2);
            buttonConfIEmp.Name = "buttonConfIEmp";
            buttonConfIEmp.BackColor = Color.FromArgb(160, 108, 245);
            buttonConfIEmp.FlatAppearance.BorderSize = 0;
            buttonConfIEmp.FlatStyle = FlatStyle.Flat;
            buttonConfIEmp.ForeColor = Color.White;
            buttonConfIEmp.UseVisualStyleBackColor = false;
            buttonConfIEmp.Size = new Size(504, 38);
            buttonConfIEmp.TabIndex = 24;
            buttonConfIEmp.Text = "Confirmar ingreso";
            buttonConfIEmp.Click += buttonConfIEmp_Click;
            // 
            // OrdenPrepNro
            // 
            OrdenPrepNro.AutoSize = true;
            OrdenPrepNro.Font = new Font("Segoe UI", 15F);
            OrdenPrepNro.Location = new Point(279, 20);
            OrdenPrepNro.Name = "OrdenPrepNro";
            OrdenPrepNro.Size = new Size(207, 28);
            OrdenPrepNro.TabIndex = 25;
            OrdenPrepNro.Text = "[Orden de prep. Nro...]";
            // 
            // lblpaquetes
            // 
            lblpaquetes.AutoSize = true;
            lblpaquetes.Location = new Point(124, 444);
            lblpaquetes.Name = "lblpaquetes";
            lblpaquetes.Size = new Size(257, 15);
            lblpaquetes.TabIndex = 26;
            lblpaquetes.Text = "Ingrese la cantidad final de aquetes preparados:";
            // 
            // textpaquetes
            // 
            textpaquetes.Location = new Point(112, 493);
            textpaquetes.Name = "textpaquetes";
            textpaquetes.Size = new Size(116, 23);
            textpaquetes.TabIndex = 27;
            // 
            // btnpaquetes
            // 
            btnpaquetes.Location = new Point(279, 474);
            btnpaquetes.Name = "btnpaquetes";
            btnpaquetes.BackColor = Color.FromArgb(0, 163, 180);
            btnpaquetes.FlatAppearance.BorderSize = 0;
            btnpaquetes.FlatStyle = FlatStyle.Flat;
            btnpaquetes.ForeColor = Color.White;
            btnpaquetes.UseVisualStyleBackColor = false;
            btnpaquetes.Size = new Size(125, 58);
            btnpaquetes.TabIndex = 28;
            btnpaquetes.Text = "Confirmar Cantidad de Paquetes";
            // 
            // EmpaquetarPedidoForm
            // 
            AccessibleName = "";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 650);
            Controls.Add(btnpaquetes);
            Controls.Add(textpaquetes);
            Controls.Add(lblpaquetes);
            Controls.Add(OrdenPrepNro);
            Controls.Add(buttonConfIEmp);
            Controls.Add(listViewProdPrep);
            Controls.Add(Prodaprep);
            Margin = new Padding(1, 2, 1, 2);
            Name = "EmpaquetarPedidoForm";
            BackColor = Color.FromArgb(246, 242, 253);
            ForeColor = Color.FromArgb(46, 26, 71);
            Text = "Empaquetar_Pedido_y_Generar_Orden_de_Entrega_Form";
            FormClosing += EmpaquetarPedidoForm_FormClosing;
            Load += EmpaquetarPedidoForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label Prodaprep;
        private ListView listViewProdPrep;
        private ColumnHeader Producto;
        private ColumnHeader Cantidad;
        private ColumnHeader Tipo_de_Pedido;
        private Button buttonConfIEmp;
        private Label OrdenPrepNro;
        private Label lblpaquetes;
        private TextBox textpaquetes;
        private Button btnpaquetes;
    }
}
