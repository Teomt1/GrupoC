namespace GrupoC.GrupoC.Entrega_a_Transportista
{
    partial class Entrega_a_Transportista
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
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            listView1 = new ListView();
            Nombre = new ColumnHeader();
            Documento = new ColumnHeader();
            Empresa_de_Transporte = new ColumnHeader();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label7 = new Label();
            listView2 = new ListView();
            Orden_preparación = new ColumnHeader();
            Cliente = new ColumnHeader();
            Paquetes = new ColumnHeader();
            button1 = new Button();
            checkBox1 = new CheckBox();
            label8 = new Label();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Orden 1", "Orden 2", "Orden 3", "Orden..." });
            comboBox1.Location = new Point(219, 69);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(180, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(223, 26);
            label1.Name = "label1";
            label1.Size = new Size(163, 15);
            label1.TabIndex = 1;
            label1.Text = "Seleccionar orden de entrega:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 126);
            label2.Name = "label2";
            label2.Size = new Size(138, 15);
            label2.TabIndex = 2;
            label2.Text = "Transportista Autorizado:";
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Nombre, Documento, Empresa_de_Transporte });
            listView1.Location = new Point(35, 156);
            listView1.Name = "listView1";
            listView1.Size = new Size(244, 60);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // Nombre
            // 
            Nombre.Text = "Nombre";
            Nombre.Width = 80;
            // 
            // Documento
            // 
            Documento.Text = "Documento";
            Documento.Width = 80;
            // 
            // Empresa_de_Transporte
            // 
            Empresa_de_Transporte.Text = "Empresa";
            Empresa_de_Transporte.Width = 80;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 245);
            label3.Name = "label3";
            label3.Size = new Size(150, 15);
            label3.TabIndex = 4;
            label3.Text = "Datos a verificar del chofer:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(35, 277);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 5;
            label4.Text = "Nombre";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 330);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 6;
            label5.Text = "Documento";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(35, 383);
            label6.Name = "label6";
            label6.Size = new Size(47, 15);
            label6.TabIndex = 7;
            label6.Text = "Patente";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(35, 295);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(114, 23);
            textBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(35, 348);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(114, 23);
            textBox2.TabIndex = 9;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(35, 401);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(114, 23);
            textBox3.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(338, 126);
            label7.Name = "label7";
            label7.Size = new Size(122, 15);
            label7.TabIndex = 11;
            label7.Text = "Detalles de la Entrega:";
            // 
            // listView2
            // 
            listView2.Columns.AddRange(new ColumnHeader[] { Orden_preparación, Cliente, Paquetes });
            listView2.Location = new Point(338, 156);
            listView2.Name = "listView2";
            listView2.Size = new Size(244, 60);
            listView2.TabIndex = 12;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            // 
            // Orden_preparación
            // 
            Orden_preparación.Text = "Órden";
            Orden_preparación.Width = 80;
            // 
            // Cliente
            // 
            Cliente.Text = "Cliente";
            Cliente.Width = 80;
            // 
            // Paquetes
            // 
            Paquetes.Text = "Paquetes";
            Paquetes.Width = 80;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlLight;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(286, 282);
            button1.Name = "button1";
            button1.Size = new Size(113, 67);
            button1.TabIndex = 13;
            button1.Text = "Generar Remito";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(458, 330);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(111, 19);
            checkBox1.TabIndex = 14;
            checkBox1.Text = "Remito Firmado";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.Red;
            label8.Location = new Point(439, 277);
            label8.Name = "label8";
            label8.Size = new Size(156, 30);
            label8.TabIndex = 15;
            label8.Text = "Solo confirmar luego que el \r\ntransportista firme el remito";
            // 
            // button2
            // 
            button2.Location = new Point(496, 418);
            button2.Name = "button2";
            button2.Size = new Size(99, 54);
            button2.TabIndex = 16;
            button2.Text = "Confirmar Despacho";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(352, 418);
            button3.Name = "button3";
            button3.Size = new Size(99, 54);
            button3.TabIndex = 17;
            button3.Text = "Cancelar";
            button3.UseVisualStyleBackColor = true;
            // 
            // Entrega_a_Transportista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(627, 493);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label8);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(listView2);
            Controls.Add(label7);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(listView1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Name = "Entrega_a_Transportista";
            Text = "Entrega_a_Transportista";
            Load += Entrega_a_Transportista_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private ListView listView1;
        private ColumnHeader Nombre;
        private ColumnHeader Documento;
        private ColumnHeader Empresa_de_Transporte;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label7;
        private ListView listView2;
        private ColumnHeader Orden_preparación;
        private ColumnHeader Cliente;
        private ColumnHeader Paquetes;
        private Button button1;
        private CheckBox checkBox1;
        private Label label8;
        private Button button2;
        private Button button3;
    }
}