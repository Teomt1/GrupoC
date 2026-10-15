namespace GrupoC.Recepción_de_Mercadería
{
    partial class Recepción_de_mercaderia_Form
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
            Remito = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
            textBox4 = new TextBox();
            label10 = new Label();
            label11 = new Label();
            button1 = new Button();
            textBox5 = new TextBox();
            button4 = new Button();
            button5 = new Button();
            label22 = new Label();
            textBox11 = new TextBox();
            fileSystemWatcher1 = new FileSystemWatcher();
            label7 = new Label();
            textBox12 = new TextBox();
            textBox13 = new TextBox();
            label8 = new Label();
            listView1 = new ListView();
            SkuCodigo = new ColumnHeader();
            Descripción = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            Posición = new ColumnHeader();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // Remito
            // 
            Remito.FormattingEnabled = true;
            Remito.Location = new Point(17, 27);
            Remito.Margin = new Padding(2);
            Remito.Name = "Remito";
            Remito.Size = new Size(791, 144);
            Remito.TabIndex = 0;
            Remito.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(25, 66);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 1;
            label1.Text = "Cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(25, 96);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(78, 20);
            label2.TabIndex = 2;
            label2.Text = "N° Remito";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(25, 123);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(94, 20);
            label3.TabIndex = 3;
            label3.Text = "Transportista";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(97, 64);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(246, 28);
            comboBox1.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(102, 97);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(241, 27);
            textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(126, 126);
            textBox2.Margin = new Padding(2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(217, 27);
            textBox2.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(504, 61);
            dateTimePicker1.Margin = new Padding(2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(291, 27);
            dateTimePicker1.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonHighlight;
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(364, 66);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(124, 20);
            label4.TabIndex = 8;
            label4.Text = "Fecha del Remito";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonHighlight;
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(364, 96);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(136, 20);
            label5.TabIndex = 9;
            label5.Text = "Nombre y Apellido";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(504, 93);
            textBox3.Margin = new Padding(2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(291, 27);
            textBox3.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonHighlight;
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(364, 131);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(58, 20);
            label6.TabIndex = 11;
            label6.Text = "Patente";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(504, 128);
            textBox4.Margin = new Padding(2);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(291, 27);
            textBox4.TabIndex = 12;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.ControlLight;
            label10.ForeColor = SystemColors.ActiveCaptionText;
            label10.Location = new Point(317, 188);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(94, 20);
            label10.TabIndex = 17;
            label10.Text = "Cant. Remito";
            label10.Click += label10_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.ControlLight;
            label11.ForeColor = SystemColors.ActiveCaptionText;
            label11.Location = new Point(170, 188);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(102, 20);
            label11.TabIndex = 18;
            label11.Text = "Cant. Contada";
            label11.Click += label11_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonFace;
            button1.Location = new Point(480, 225);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(120, 26);
            button1.TabIndex = 20;
            button1.Text = "+ Agregar Ítem";
            button1.UseVisualStyleBackColor = false;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(17, 224);
            textBox5.Margin = new Padding(2);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(138, 27);
            textBox5.TabIndex = 24;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ButtonFace;
            button4.Location = new Point(562, 533);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(120, 26);
            button4.TabIndex = 33;
            button4.Text = "Cancelar";
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ButtonFace;
            button5.Location = new Point(688, 533);
            button5.Margin = new Padding(2);
            button5.Name = "button5";
            button5.Size = new Size(120, 26);
            button5.TabIndex = 34;
            button5.Text = "Confirmar Recepeción";
            button5.UseVisualStyleBackColor = false;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.BackColor = SystemColors.ControlLight;
            label22.ForeColor = SystemColors.ActiveCaptionText;
            label22.Location = new Point(18, 471);
            label22.Margin = new Padding(2, 0, 2, 0);
            label22.Name = "label22";
            label22.Size = new Size(108, 20);
            label22.TabIndex = 41;
            label22.Text = "Observaciones:";
            // 
            // textBox11
            // 
            textBox11.Location = new Point(18, 493);
            textBox11.Margin = new Padding(2);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(267, 27);
            textBox11.TabIndex = 42;
            textBox11.Text = "Caja con golpes visibles...";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ControlLight;
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.Location = new Point(17, 188);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(95, 20);
            label7.TabIndex = 43;
            label7.Text = "SKU/ Código";
            label7.Click += label7_Click_1;
            // 
            // textBox12
            // 
            textBox12.Location = new Point(170, 224);
            textBox12.Margin = new Padding(2);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(138, 27);
            textBox12.TabIndex = 44;
            // 
            // textBox13
            // 
            textBox13.Location = new Point(317, 224);
            textBox13.Margin = new Padding(2);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(138, 27);
            textBox13.TabIndex = 45;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.ControlLight;
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.Location = new Point(17, 272);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(219, 20);
            label8.TabIndex = 46;
            label8.Text = "Detalle de Mercaderia Recibida";
            label8.Click += label8_Click;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { SkuCodigo, Descripción, columnHeader1, columnHeader2, Posición });
            listView1.Location = new Point(18, 309);
            listView1.Name = "listView1";
            listView1.Size = new Size(566, 118);
            listView1.TabIndex = 47;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // SkuCodigo
            // 
            SkuCodigo.Text = "SkuCodigo";
            SkuCodigo.Width = 100;
            // 
            // Descripción
            // 
            Descripción.Text = "Descripción";
            Descripción.Width = 100;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Cantidad contada";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Cantidad remito";
            columnHeader2.Width = 100;
            // 
            // Posición
            // 
            Posición.Text = "Posición";
            Posición.Width = 100;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ButtonFace;
            button2.Location = new Point(18, 432);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(566, 26);
            button2.TabIndex = 48;
            button2.Text = "- Quitar Ítem";
            button2.UseVisualStyleBackColor = false;
            // 
            // Recepción_de_mercaderia_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(892, 626);
            Controls.Add(button2);
            Controls.Add(listView1);
            Controls.Add(label8);
            Controls.Add(textBox13);
            Controls.Add(textBox12);
            Controls.Add(label7);
            Controls.Add(textBox11);
            Controls.Add(label22);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(textBox5);
            Controls.Add(button1);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(textBox4);
            Controls.Add(label6);
            Controls.Add(textBox3);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Remito);
            Margin = new Padding(2);
            Name = "Recepción_de_mercaderia_Form";
            Text = "Vlidar";
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox Remito;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private DateTimePicker dateTimePicker1;
        private Label label4;
        private Label label5;
        private TextBox textBox3;
        private Label label6;
        private TextBox textBox4;
        private Label label10;
        private Label label11;
        private Button button1;
        private TextBox textBox5;
        private Button button4;
        private Button button5;
        private Label label22;
        private TextBox textBox11;
        private FileSystemWatcher fileSystemWatcher1;
        private Label label7;
        private TextBox textBox13;
        private TextBox textBox12;
        private Label label8;
        private ListView listView1;
        private ColumnHeader SkuCodigo;
        private ColumnHeader Descripción;
        private Button button2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader Posición;
    }
}