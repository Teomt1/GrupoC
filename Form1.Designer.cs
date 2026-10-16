namespace GrupoC
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            button8 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(51, 59);
            button1.Name = "button1";
            button1.Size = new Size(169, 66);
            button1.TabIndex = 0;
            button1.Text = "empaquetar pedido y generar orden...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(277, 57);
            button2.Name = "button2";
            button2.Size = new Size(127, 68);
            button2.TabIndex = 1;
            button2.Text = "Registrar orden preparacion";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(491, 53);
            button3.Name = "button3";
            button3.Size = new Size(107, 77);
            button3.TabIndex = 2;
            button3.Text = "Ingreso Stock";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(51, 304);
            button4.Name = "button4";
            button4.Size = new Size(133, 58);
            button4.TabIndex = 3;
            button4.Text = "Generar orden de Selección";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(277, 297);
            button5.Name = "button5";
            button5.Size = new Size(122, 73);
            button5.TabIndex = 4;
            button5.Text = "Entrega Transportista";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button7
            // 
            button7.Location = new Point(643, 59);
            button7.Name = "button7";
            button7.Size = new Size(119, 69);
            button7.TabIndex = 6;
            button7.Text = "Recepción Mercadería";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(500, 293);
            button8.Name = "button8";
            button8.Size = new Size(119, 69);
            button8.TabIndex = 7;
            button8.Text = "Registro de excedente de almacenamiento";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button7;
        private Button button8;
    }
}
