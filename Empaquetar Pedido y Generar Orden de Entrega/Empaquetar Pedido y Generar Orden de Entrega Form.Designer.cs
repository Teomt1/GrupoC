namespace GrupoC.Empaquetar_Pedido_y_Generar_Orden_de_Entrega
{
    partial class Empaqu_y_Generar_Orden_de_Entrega_Form
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
            Guardar = new Button();
            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            listView1 = new ListView();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            listView2 = new ListView();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            SuspendLayout();
            // 
            // Guardar
            // 
            Guardar.BackColor = SystemColors.InactiveCaption;
            Guardar.BackgroundImageLayout = ImageLayout.None;
            Guardar.Cursor = Cursors.SizeAll;
            Guardar.Location = new Point(907, 610);
            Guardar.Name = "Guardar";
            Guardar.Size = new Size(141, 34);
            Guardar.TabIndex = 0;
            Guardar.Text = "Guardar y salir";
            Guardar.UseVisualStyleBackColor = false;
            Guardar.Click += button1_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.InactiveCaption;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(1070, 610);
            button1.Name = "button1";
            button1.Size = new Size(233, 34);
            button1.TabIndex = 1;
            button1.Text = "Generar Orden de Entrega";
            button1.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.AccessibleDescription = "Ordenes de Preparación ";
            comboBox1.AccessibleRole = AccessibleRole.MenuBar;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 43);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(951, 33);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(182, 25);
            label1.TabIndex = 3;
            label1.Text = "Órdenes de Selección";
            label1.Click += label1_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 129);
            listView1.Name = "listView1";
            listView1.Size = new Size(951, 85);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(29, 140);
            label2.Name = "label2";
            label2.Size = new Size(165, 25);
            label2.TabIndex = 5;
            label2.Text = "Órden de Selección";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(233, 140);
            label3.Name = "label3";
            label3.Size = new Size(65, 25);
            label3.TabIndex = 6;
            label3.Text = "Cliente";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(345, 140);
            label4.Name = "label4";
            label4.Size = new Size(132, 25);
            label4.TabIndex = 7;
            label4.Text = "Tipo de Pedido";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(512, 140);
            label5.Name = "label5";
            label5.Size = new Size(180, 25);
            label5.TabIndex = 8;
            label5.Text = "Fecha de Preparación";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(748, 140);
            label6.Name = "label6";
            label6.Size = new Size(66, 25);
            label6.TabIndex = 9;
            label6.Text = "Estado";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ButtonHighlight;
            label7.Location = new Point(855, 140);
            label7.Name = "label7";
            label7.Size = new Size(98, 25);
            label7.TabIndex = 10;
            label7.Text = "Modalidad";
            // 
            // listView2
            // 
            listView2.Location = new Point(12, 267);
            listView2.Name = "listView2";
            listView2.Size = new Size(951, 158);
            listView2.TabIndex = 11;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.ButtonFace;
            label8.Location = new Point(12, 101);
            label8.Name = "label8";
            label8.Size = new Size(104, 25);
            label8.TabIndex = 12;
            label8.Text = "Descripción";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.ButtonFace;
            label9.Location = new Point(12, 239);
            label9.Name = "label9";
            label9.Size = new Size(157, 25);
            label9.TabIndex = 13;
            label9.Text = "Detalle del pedido";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.ButtonHighlight;
            label10.Location = new Point(72, 276);
            label10.Name = "label10";
            label10.Size = new Size(44, 25);
            label10.TabIndex = 14;
            label10.Text = "SKU";
            label10.Click += label10_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.ButtonHighlight;
            label11.Location = new Point(29, 276);
            label11.Name = "label11";
            label11.Size = new Size(32, 25);
            label11.TabIndex = 15;
            label11.Text = "N°";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = SystemColors.ButtonHighlight;
            label12.Location = new Point(143, 276);
            label12.Name = "label12";
            label12.Size = new Size(104, 25);
            label12.TabIndex = 16;
            label12.Text = "Descripción";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = SystemColors.ButtonHighlight;
            label13.Location = new Point(288, 276);
            label13.Name = "label13";
            label13.Size = new Size(164, 25);
            label13.TabIndex = 17;
            label13.Text = "Cantidad Solicitada";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = SystemColors.ButtonHighlight;
            label14.Location = new Point(489, 276);
            label14.Name = "label14";
            label14.Size = new Size(168, 25);
            label14.TabIndex = 18;
            label14.Text = "Cantidad Preparada";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = SystemColors.ButtonHighlight;
            label15.Location = new Point(714, 276);
            label15.Name = "label15";
            label15.Size = new Size(66, 25);
            label15.TabIndex = 19;
            label15.Text = "Estado";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = SystemColors.ButtonHighlight;
            label16.Location = new Point(855, 276);
            label16.Name = "label16";
            label16.Size = new Size(65, 25);
            label16.TabIndex = 20;
            label16.Text = "Acción";
            // 
            // Empaqu_y_Generar_Orden_de_Entrega_Form
            // 
            AccessibleName = "";
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1315, 656);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(listView2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(listView1);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(Guardar);
            Name = "Empaqu_y_Generar_Orden_de_Entrega_Form";
            Text = "Empaquetar_Pedido_y_Generar_Orden_de_Entrega_Form";
            Load += Empaqu_y_Generar_Orden_de_Entrega_Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Guardar;
        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private ListView listView1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ListView listView2;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
    }
}