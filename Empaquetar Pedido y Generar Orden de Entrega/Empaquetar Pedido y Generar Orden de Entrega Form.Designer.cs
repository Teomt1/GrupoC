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
            label1 = new Label();
            listView1 = new ListView();
            Producto = new ColumnHeader();
            Cantidad = new ColumnHeader();
            Tipo_de_Pedido = new ColumnHeader();
            button2 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 67);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 3;
            label1.Text = "Productos a preparar";
            label1.Click += label1_Click;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Producto, Cantidad, Tipo_de_Pedido });
            listView1.Location = new Point(11, 113);
            listView1.Margin = new Padding(2, 2, 2, 2);
            listView1.Name = "listView1";
            listView1.Size = new Size(474, 301);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
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
            // button2
            // 
            button2.BackColor = SystemColors.ControlLight;
            button2.Location = new Point(11, 418);
            button2.Margin = new Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new Size(483, 38);
            button2.TabIndex = 24;
            button2.Text = "Confirmar ingreso";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(278, 20);
            label2.Name = "label2";
            label2.Size = new Size(207, 28);
            label2.TabIndex = 25;
            label2.Text = "[Orden de prep. Nro...]";
            // 
            // Empaqu_y_Generar_Orden_de_Entrega_Form
            // 
            AccessibleName = "";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 489);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(listView1);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Empaqu_y_Generar_Orden_de_Entrega_Form";
            Text = "Empaquetar_Pedido_y_Generar_Orden_de_Entrega_Form";
            Load += Empaqu_y_Generar_Orden_de_Entrega_Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ListView listView1;
        private ColumnHeader Producto;
        private ColumnHeader Cantidad;
        private ColumnHeader Tipo_de_Pedido;
        private Button button2;
        private Label label2;
    }
}