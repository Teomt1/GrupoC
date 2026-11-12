namespace GrupoC
{
    partial class Form1
    {
        
        private System.ComponentModel.IContainer components = null;

        
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button7 = new Button();
            ComboDepo = new ComboBox();
            ComboEmpleado = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(192, 255, 192);
            button1.Location = new Point(329, 331);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(121, 71);
            button1.TabIndex = 0;
            button1.Text = "Empaquetar Pedido";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 255, 192);
            button2.Location = new Point(548, 185);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(121, 71);
            button2.TabIndex = 1;
            button2.Text = "Registrar Orden de Preparacion";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(192, 255, 192);
            button3.Location = new Point(329, 185);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(121, 71);
            button3.TabIndex = 2;
            button3.Text = "Ingreso Stock";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(192, 255, 192);
            button4.Location = new Point(99, 331);
            button4.Margin = new Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new Size(122, 71);
            button4.TabIndex = 3;
            button4.Text = "Cumplir Orden de Selección";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(192, 255, 192);
            button5.Location = new Point(548, 331);
            button5.Margin = new Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new Size(121, 71);
            button5.TabIndex = 4;
            button5.Text = "Entrega a Transportista";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(192, 255, 192);
            button7.Location = new Point(99, 185);
            button7.Margin = new Padding(4, 3, 4, 3);
            button7.Name = "button7";
            button7.Size = new Size(122, 69);
            button7.TabIndex = 6;
            button7.Text = "Recepción Mercadería";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // ComboDepo
            // 
            ComboDepo.FormattingEnabled = true;
            ComboDepo.Location = new Point(138, 87);
            ComboDepo.Name = "ComboDepo";
            ComboDepo.Size = new Size(159, 23);
            ComboDepo.TabIndex = 7;
            // 
            // ComboEmpleado
            // 
            ComboEmpleado.FormattingEnabled = true;
            ComboEmpleado.Location = new Point(481, 87);
            ComboEmpleado.Name = "ComboEmpleado";
            ComboEmpleado.Size = new Size(159, 23);
            ComboEmpleado.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(138, 58);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 9;
            label1.Text = "Seleccionar Depósito";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(481, 58);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 10;
            label2.Text = "Seleccionar Empleado";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ComboEmpleado);
            Controls.Add(ComboDepo);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button7;
        private ComboBox ComboDepo;
        private ComboBox ComboEmpleado;
        private Label label1;
        private Label label2;
    }
}
