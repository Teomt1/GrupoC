namespace GrupoC.GrupoC.Completar_Orden_de_Preparación
{
    partial class Completar_Orden_de_Preparación
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
            comboBox1 = new ComboBox();
            button1 = new Button();
            listView1 = new ListView();
            Orden = new ColumnHeader();
            Cliente = new ColumnHeader();
            Transportista = new ColumnHeader();
            Fecha_Entrega = new ColumnHeader();
            checkBox1 = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 41);
            label1.Name = "label1";
            label1.Size = new Size(200, 15);
            label1.TabIndex = 0;
            label1.Text = "Ordenes de Preparación Disponibles:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Orden 1", "Orden 2", "Orden 3", "Orden..." });
            comboBox1.Location = new Point(30, 69);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(197, 23);
            comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(275, 41);
            button1.Name = "button1";
            button1.Size = new Size(89, 56);
            button1.TabIndex = 2;
            button1.Text = "Seleccionar Orden";
            button1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Orden, Cliente, Transportista, Fecha_Entrega });
            listView1.Location = new Point(34, 135);
            listView1.Name = "listView1";
            listView1.Size = new Size(366, 108);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // Orden
            // 
            Orden.Text = "Orden";
            Orden.Width = 90;
            // 
            // Cliente
            // 
            Cliente.Text = "Cliente";
            Cliente.Width = 90;
            // 
            // Transportista
            // 
            Transportista.Text = "Transportista";
            Transportista.Width = 90;
            // 
            // Fecha_Entrega
            // 
            Fecha_Entrega.Text = "Fecha";
            Fecha_Entrega.Width = 90;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(30, 301);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(168, 19);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "Se Recibió Remito Firmado";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 274);
            label2.Name = "label2";
            label2.Size = new Size(203, 15);
            label2.TabIndex = 5;
            label2.Text = "¿Fue el paquete entregado con éxito?";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 345);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 6;
            label3.Text = "Observaciones:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(30, 377);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(249, 71);
            textBox1.TabIndex = 7;
            // 
            // button2
            // 
            button2.Location = new Point(275, 486);
            button2.Name = "button2";
            button2.Size = new Size(101, 65);
            button2.TabIndex = 8;
            button2.Text = "Cerrar Orden de Preparación";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(34, 486);
            button3.Name = "button3";
            button3.Size = new Size(101, 65);
            button3.TabIndex = 9;
            button3.Text = "Cancelar";
            button3.UseVisualStyleBackColor = true;
            // 
            // Completar_Orden_de_Preparación
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 563);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(checkBox1);
            Controls.Add(listView1);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Name = "Completar_Orden_de_Preparación";
            Text = "Completar_Orden_de_Preparación";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
        private ListView listView1;
        private ColumnHeader Orden;
        private ColumnHeader Cliente;
        private ColumnHeader Transportista;
        private ColumnHeader Fecha_Entrega;
        private CheckBox checkBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
    }
}