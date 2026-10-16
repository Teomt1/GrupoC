namespace GrupoC.IngresoStock
{
    partial class IngresoStockForm
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
            groupBox1 = new GroupBox();
            btnBuscar = new Button();
            txtNroOrden = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            txtClienteOrigen = new TextBox();
            label2 = new Label();
            groupBox3 = new GroupBox();
            listViewSkus = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            groupBox4 = new GroupBox();
            btnAgregarUbicacion = new Button();
            txtCantidadUbicar = new TextBox();
            label8 = new Label();
            cmbNivelZ = new ComboBox();
            label7 = new Label();
            cmbFilaY = new ComboBox();
            label6 = new Label();
            cmbPasilloX = new ComboBox();
            label5 = new Label();
            cmbSkuSeleccionado = new ComboBox();
            label4 = new Label();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            listView1 = new ListView();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            button1 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnBuscar);
            groupBox1.Controls.Add(txtNroOrden);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(620, 60);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Búsqueda de Orden de Recepción";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(430, 21);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(90, 25);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtNroOrden
            // 
            txtNroOrden.Location = new Point(150, 22);
            txtNroOrden.Name = "txtNroOrden";
            txtNroOrden.Size = new Size(260, 23);
            txtNroOrden.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 25);
            label1.Name = "label1";
            label1.Size = new Size(118, 15);
            label1.TabIndex = 0;
            label1.Text = "N° Orden Recepción:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtClienteOrigen);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(12, 78);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(620, 60);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Datos de la Orden de Recepción";
            // 
            // txtClienteOrigen
            // 
            txtClienteOrigen.Location = new Point(115, 25);
            txtClienteOrigen.Name = "txtClienteOrigen";
            txtClienteOrigen.ReadOnly = true;
            txtClienteOrigen.Size = new Size(280, 23);
            txtClienteOrigen.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 25);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 0;
            label2.Text = "Cliente / Origen:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(listViewSkus);
            groupBox3.Location = new Point(12, 144);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(743, 160);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Ítems y SKUs a Ubicar";
            // 
            // listViewSkus
            // 
            listViewSkus.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listViewSkus.Location = new Point(15, 22);
            listViewSkus.Name = "listViewSkus";
            listViewSkus.Size = new Size(722, 125);
            listViewSkus.TabIndex = 0;
            listViewSkus.UseCompatibleStateImageBehavior = false;
            listViewSkus.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "SKU";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Descripción";
            columnHeader2.Width = 240;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Cantidad Recepcionada";
            columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Cantidad a ubicar";
            columnHeader4.Width = 130;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnAgregarUbicacion);
            groupBox4.Controls.Add(txtCantidadUbicar);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(cmbNivelZ);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(cmbFilaY);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(cmbPasilloX);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(cmbSkuSeleccionado);
            groupBox4.Controls.Add(label4);
            groupBox4.Location = new Point(27, 310);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(620, 130);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Ubicación Física en Racks (Coordenadas X - Y - Z)";
            // 
            // btnAgregarUbicacion
            // 
            btnAgregarUbicacion.Location = new Point(460, 92);
            btnAgregarUbicacion.Name = "btnAgregarUbicacion";
            btnAgregarUbicacion.Size = new Size(145, 28);
            btnAgregarUbicacion.TabIndex = 10;
            btnAgregarUbicacion.Text = "Agregar Ubicación";
            btnAgregarUbicacion.UseVisualStyleBackColor = true;
            // 
            // txtCantidadUbicar
            // 
            txtCantidadUbicar.Location = new Point(515, 22);
            txtCantidadUbicar.Name = "txtCantidadUbicar";
            txtCantidadUbicar.Size = new Size(90, 23);
            txtCantidadUbicar.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(450, 25);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 8;
            label8.Text = "Cantidad:";
            // 
            // cmbNivelZ
            // 
            cmbNivelZ.FormattingEnabled = true;
            cmbNivelZ.Items.AddRange(new object[] { "Nivel 1", "Nivel 2", "Nivel 3" });
            cmbNivelZ.Location = new Point(490, 60);
            cmbNivelZ.Name = "cmbNivelZ";
            cmbNivelZ.Size = new Size(115, 23);
            cmbNivelZ.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(385, 62);
            label7.Name = "label7";
            label7.Size = new Size(92, 15);
            label7.TabIndex = 6;
            label7.Text = "Nivel/Altura (Z):";
            label7.Click += label7_Click;
            // 
            // cmbFilaY
            // 
            cmbFilaY.FormattingEnabled = true;
            cmbFilaY.Items.AddRange(new object[] { "Fila 1", "Fila 2", "Fila 3" });
            cmbFilaY.Location = new Point(265, 60);
            cmbFilaY.Name = "cmbFilaY";
            cmbFilaY.Size = new Size(115, 23);
            cmbFilaY.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(215, 63);
            label6.Name = "label6";
            label6.Size = new Size(46, 15);
            label6.TabIndex = 4;
            label6.Text = "Fila (Y):";
            // 
            // cmbPasilloX
            // 
            cmbPasilloX.FormattingEnabled = true;
            cmbPasilloX.Items.AddRange(new object[] { "Pasillo 1", "Pasillo 2", "Pasillo 3" });
            cmbPasilloX.Location = new Point(115, 60);
            cmbPasilloX.Name = "cmbPasilloX";
            cmbPasilloX.Size = new Size(85, 23);
            cmbPasilloX.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 63);
            label5.Name = "label5";
            label5.Size = new Size(92, 15);
            label5.TabIndex = 2;
            label5.Text = "Pasillo/Rack (X):";
            // 
            // cmbSkuSeleccionado
            // 
            cmbSkuSeleccionado.FormattingEnabled = true;
            cmbSkuSeleccionado.Location = new Point(130, 22);
            cmbSkuSeleccionado.Name = "cmbSkuSeleccionado";
            cmbSkuSeleccionado.Size = new Size(300, 23);
            cmbSkuSeleccionado.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 25);
            label4.Name = "label4";
            label4.Size = new Size(104, 15);
            label4.TabIndex = 0;
            label4.Text = "SKU Seleccionado:";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(495, 626);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(152, 35);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar Ingreso";
            btnConfirmar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(380, 626);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 35);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6, columnHeader7 });
            listView1.Location = new Point(27, 446);
            listView1.Name = "listView1";
            listView1.Size = new Size(620, 88);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "SKU";
            columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Cantidad";
            columnHeader6.Width = 240;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Posicion";
            columnHeader7.Width = 180;
            // 
            // button1
            // 
            button1.Location = new Point(502, 540);
            button1.Name = "button1";
            button1.Size = new Size(145, 28);
            button1.TabIndex = 11;
            button1.Text = "Quitar seleccionada";
            button1.UseVisualStyleBackColor = true;
            // 
            // IngresoStockForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(867, 694);
            Controls.Add(button1);
            Controls.Add(listView1);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "IngresoStockForm";
            Text = "Pampazon S.A. - Ingreso a Stock";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnBuscar;
        private TextBox txtNroOrden;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox txtClienteOrigen;
        private Label label2;
        private GroupBox groupBox3;
        private ListView listViewSkus;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private GroupBox groupBox4;
        private ComboBox cmbSkuSeleccionado;
        private Label label4;
        private ComboBox cmbPasilloX;
        private Label label5;
        private ComboBox cmbFilaY;
        private Label label6;
        private ComboBox cmbNivelZ;
        private Label label7;
        private TextBox txtCantidadUbicar;
        private Label label8;
        private Button btnAgregarUbicacion;
        private Button btnConfirmar;
        private Button btnCancelar;
        private ListView listView1;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Button button1;
    }
}