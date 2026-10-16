namespace GrupoC.EntregaATransportista
{
    partial class EntregaATransportistaForm
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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label7 = new Label();
            listView2 = new ListView();
            Orden_preparación = new ColumnHeader();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(268, 180);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 68);
            label4.Name = "label4";
            label4.Size = new Size(27, 15);
            label4.TabIndex = 5;
            label4.Text = "DNI";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 15);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 6;
            label5.Text = "Transportista";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 42);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(122, 23);
            textBox2.TabIndex = 9;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 86);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(122, 23);
            textBox3.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 143);
            label7.Name = "label7";
            label7.Size = new Size(122, 15);
            label7.TabIndex = 11;
            label7.Text = "Detalles de la Entrega:";
            // 
            // listView2
            // 
            listView2.Columns.AddRange(new ColumnHeader[] { Orden_preparación });
            listView2.Location = new Point(12, 161);
            listView2.Name = "listView2";
            listView2.Size = new Size(244, 155);
            listView2.TabIndex = 12;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            // 
            // Orden_preparación
            // 
            Orden_preparación.Text = "Órden";
            Orden_preparación.Width = 120;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlLight;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(164, 51);
            button1.Name = "button1";
            button1.Size = new Size(104, 49);
            button1.TabIndex = 13;
            button1.Text = "Buscar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(157, 386);
            button2.Name = "button2";
            button2.Size = new Size(99, 63);
            button2.TabIndex = 16;
            button2.Text = "Confirmar Despacho";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(17, 386);
            button3.Name = "button3";
            button3.Size = new Size(105, 63);
            button3.TabIndex = 17;
            button3.Text = "Cancelar";
            button3.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(17, 335);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(111, 19);
            checkBox1.TabIndex = 19;
            checkBox1.Text = "Remito Firmado";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Entrega_a_Transportista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 486);
            Controls.Add(checkBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listView2);
            Controls.Add(label7);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Name = "Entrega_a_Transportista";
            Text = "Entrega_a_Transportista";
            Load += Entrega_a_Transportista_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label7;
        private ListView listView2;
        private ColumnHeader Orden_preparación;
        private Button button1;
        private Button button2;
        private Button button3;
        private CheckBox checkBox1;
    }
}