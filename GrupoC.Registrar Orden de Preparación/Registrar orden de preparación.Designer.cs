namespace GrupoC.GrupoC.Registrar_Orden_de_Preparación
{
    partial class Registrar_orden_de_preparación
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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox2 = new TextBox();
            label6 = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            dateTimePicker1 = new DateTimePicker();
            label7 = new Label();
            label8 = new Label();
            comboBox1 = new ComboBox();
            label9 = new Label();
            button1 = new Button();
            textBox3 = new TextBox();
            label10 = new Label();
            listView1 = new ListView();
            SKU = new ColumnHeader();
            Depósito = new ColumnHeader();
            Producto = new ColumnHeader();
            Cantidad = new ColumnHeader();
            label11 = new Label();
            label12 = new Label();
            textBox4 = new TextBox();
            label13 = new Label();
            label14 = new Label();
            textBox5 = new TextBox();
            label15 = new Label();
            textBox6 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            label16 = new Label();
            comboBox2 = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 42);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Usuario:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(117, 42);
            label2.Name = "label2";
            label2.Size = new Size(238, 15);
            label2.TabIndex = 1;
            label2.Text = "(Nombre del cliente actualmente loggeado)";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(532, 446);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(122, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(532, 362);
            label3.Name = "label3";
            label3.Size = new Size(230, 15);
            label3.TabIndex = 3;
            label3.Text = "Datos del transportista autorizado a reitrar:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(532, 416);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 4;
            label4.Text = "Nombre";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(700, 416);
            label5.Name = "label5";
            label5.Size = new Size(27, 15);
            label5.TabIndex = 5;
            label5.Text = "DNI";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(700, 446);
            textBox2.MaxLength = 8;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(79, 23);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(247, 123);
            label6.Name = "label6";
            label6.Size = new Size(160, 15);
            label6.TabIndex = 8;
            label6.Text = "Modalidad de Empaquetado:";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(247, 163);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(96, 19);
            radioButton1.TabIndex = 9;
            radioButton1.TabStop = true;
            radioButton1.Text = "Palet Cerrado";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(393, 163);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(82, 19);
            radioButton2.TabIndex = 10;
            radioButton2.TabStop = true;
            radioButton2.Text = "Fullfilment";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(572, 292);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(228, 23);
            dateTimePicker1.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(572, 258);
            label7.Name = "label7";
            label7.Size = new Size(91, 15);
            label7.TabIndex = 12;
            label7.Text = "Fecha de Retiro:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(255, 128, 128);
            label8.Location = new Point(632, 42);
            label8.Name = "label8";
            label8.Size = new Size(293, 30);
            label8.TabIndex = 13;
            label8.Text = "Aviso: Los pedidos generados luego de las 18:00 serán \r\nprocesados en el primer turno del siguiente día hábil";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Prod 1", "Prod 2", "Prod 3" });
            comboBox1.Location = new Point(38, 163);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(157, 23);
            comboBox1.TabIndex = 14;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(38, 123);
            label9.Name = "label9";
            label9.Size = new Size(127, 15);
            label9.TabIndex = 15;
            label9.Text = "Productos disponibles:";
            // 
            // button1
            // 
            button1.Location = new Point(36, 325);
            button1.Name = "button1";
            button1.Size = new Size(127, 41);
            button1.TabIndex = 16;
            button1.Text = "Agregar a la Orden";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(38, 270);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(90, 23);
            textBox3.TabIndex = 17;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(38, 226);
            label10.Name = "label10";
            label10.Size = new Size(104, 15);
            label10.TabIndex = 18;
            label10.Text = "Cantidad a Retirar:";
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { SKU, Depósito, Producto, Cantidad });
            listView1.Location = new Point(36, 416);
            listView1.Name = "listView1";
            listView1.Size = new Size(403, 134);
            listView1.TabIndex = 19;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // SKU
            // 
            SKU.DisplayIndex = 2;
            SKU.Text = "SKU";
            SKU.Width = 100;
            // 
            // Depósito
            // 
            Depósito.DisplayIndex = 3;
            Depósito.Text = "Depósito";
            Depósito.Width = 100;
            // 
            // Producto
            // 
            Producto.DisplayIndex = 0;
            Producto.Text = "Producto";
            Producto.Width = 100;
            // 
            // Cantidad
            // 
            Cantidad.DisplayIndex = 1;
            Cantidad.Text = "Cantidad";
            Cantidad.Width = 100;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(36, 389);
            label11.Name = "label11";
            label11.Size = new Size(141, 15);
            label11.TabIndex = 20;
            label11.Text = "Estado actual de la Orden";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(573, 123);
            label12.Name = "label12";
            label12.Size = new Size(138, 15);
            label12.TabIndex = 21;
            label12.Text = "Datos para el Etiquetado:";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(573, 203);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 22;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(572, 166);
            label13.Name = "label13";
            label13.Size = new Size(139, 15);
            label13.TabIndex = 23;
            label13.Text = "Nombre del Destinatario:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(780, 166);
            label14.Name = "label14";
            label14.Size = new Size(119, 15);
            label14.TabIndex = 24;
            label14.Text = "Dirección de Destino:";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(780, 203);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 25;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(841, 416);
            label15.Name = "label15";
            label15.Size = new Size(126, 15);
            label15.TabIndex = 26;
            label15.Text = "Empresa de Transporte";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(841, 446);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 27;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Control;
            button2.Location = new Point(864, 505);
            button2.Name = "button2";
            button2.Size = new Size(103, 45);
            button2.TabIndex = 28;
            button2.Text = "Confirmar Orden";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.Location = new Point(718, 505);
            button3.Name = "button3";
            button3.Size = new Size(103, 45);
            button3.TabIndex = 29;
            button3.Text = "Cancelar";
            button3.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(247, 226);
            label16.Name = "label16";
            label16.Size = new Size(57, 15);
            label16.TabIndex = 30;
            label16.Text = "Depósito:";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Depo 1", "Depo 2", "Depo 3" });
            comboBox2.Location = new Point(247, 270);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(136, 23);
            comboBox2.TabIndex = 31;
            // 
            // Registrar_orden_de_preparación
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(995, 562);
            Controls.Add(comboBox2);
            Controls.Add(label16);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox6);
            Controls.Add(label15);
            Controls.Add(textBox5);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(textBox4);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(listView1);
            Controls.Add(label10);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(label9);
            Controls.Add(comboBox1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(dateTimePicker1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(label6);
            Controls.Add(textBox2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Registrar_orden_de_preparación";
            Text = "Registrar_orden_de_preparación";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox2;
        private Label label6;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private DateTimePicker dateTimePicker1;
        private Label label7;
        private Label label8;
        private ComboBox comboBox1;
        private Label label9;
        private Button button1;
        private TextBox textBox3;
        private Label label10;
        private ListView listView1;
        private ColumnHeader Producto;
        private ColumnHeader Cantidad;
        private Label label11;
        private ColumnHeader SKU;
        private Label label12;
        private TextBox textBox4;
        private Label label13;
        private Label label14;
        private TextBox textBox5;
        private Label label15;
        private TextBox textBox6;
        private Button button2;
        private Button button3;
        private ColumnHeader Depósito;
        private Label label16;
        private ComboBox comboBox2;
    }
}