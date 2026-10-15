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
            label1 = new Label();
            listView1 = new ListView();
            label9 = new Label();
            Producto = new ColumnHeader();
            Cantidad = new ColumnHeader();
            Tipo_de_Pedido = new ColumnHeader();
            label17 = new Label();
            textBox2 = new TextBox();
            button2 = new Button();
            label8 = new Label();
            textBox4 = new TextBox();
            listView2 = new ListView();
            Producto_Ingresado = new ColumnHeader();
            Cantidad_Ingresada = new ColumnHeader();
            SuspendLayout();
            // 
            // Guardar
            // 
            Guardar.BackColor = SystemColors.InactiveCaption;
            Guardar.BackgroundImageLayout = ImageLayout.None;
            Guardar.Cursor = Cursors.SizeAll;
            Guardar.Location = new Point(227, 592);
            Guardar.Name = "Guardar";
            Guardar.Size = new Size(141, 34);
            Guardar.TabIndex = 0;
            Guardar.Text = "Cancelar";
            Guardar.UseVisualStyleBackColor = false;
            Guardar.Click += button1_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.InactiveCaption;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(410, 592);
            button1.Name = "button1";
            button1.Size = new Size(233, 34);
            button1.TabIndex = 1;
            button1.Text = "Generar Orden de Entrega";
            button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(152, 25);
            label1.TabIndex = 3;
            label1.Text = "Orden a Preparar ";
            label1.Click += label1_Click;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Producto, Cantidad, Tipo_de_Pedido });
            listView1.Location = new Point(12, 43);
            listView1.Name = "listView1";
            listView1.Size = new Size(447, 85);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.ButtonFace;
            label9.Location = new Point(12, 320);
            label9.Name = "label9";
            label9.Size = new Size(164, 25);
            label9.TabIndex = 13;
            label9.Text = "Detalle de la Orden";
            label9.Click += label9_Click;
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
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(12, 172);
            label17.Name = "label17";
            label17.Size = new Size(263, 25);
            label17.TabIndex = 22;
            label17.Text = "Ingresar el código del producto";
            label17.Click += label17_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ControlLight;
            textBox2.Location = new Point(12, 212);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(263, 31);
            textBox2.TabIndex = 23;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ControlLight;
            button2.Location = new Point(12, 273);
            button2.Name = "button2";
            button2.Size = new Size(505, 34);
            button2.TabIndex = 24;
            button2.Text = "Ingresar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(365, 172);
            label8.Name = "label8";
            label8.Size = new Size(152, 25);
            label8.TabIndex = 29;
            label8.Text = "Ingresar Cantidad";
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.ControlLight;
            textBox4.Location = new Point(365, 212);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(152, 31);
            textBox4.TabIndex = 30;
            // 
            // listView2
            // 
            listView2.Columns.AddRange(new ColumnHeader[] { Producto_Ingresado, Cantidad_Ingresada });
            listView2.Location = new Point(70, 358);
            listView2.Name = "listView2";
            listView2.Size = new Size(447, 146);
            listView2.TabIndex = 31;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            // 
            // Producto_Ingresado
            // 
            Producto_Ingresado.Text = "Producto_Ingresado";
            Producto_Ingresado.Width = 220;
            // 
            // Cantidad_Ingresada
            // 
            Cantidad_Ingresada.Text = "Cantidad_Ingresada";
            Cantidad_Ingresada.Width = 220;
            // 
            // Empaqu_y_Generar_Orden_de_Entrega_Form
            // 
            AccessibleName = "";
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(667, 656);
            Controls.Add(listView2);
            Controls.Add(textBox4);
            Controls.Add(label8);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(label17);
            Controls.Add(label9);
            Controls.Add(listView1);
            Controls.Add(label1);
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
        private Label label1;
        private ListView listView1;
        private Label label9;
        private ColumnHeader Producto;
        private ColumnHeader Cantidad;
        private ColumnHeader Tipo_de_Pedido;
        private Label label17;
        private TextBox textBox2;
        private Button button2;
        private Label label8;
        private TextBox textBox4;
        private ListView listView2;
        private ColumnHeader Producto_Ingresado;
        private ColumnHeader Cantidad_Ingresada;
    }
}