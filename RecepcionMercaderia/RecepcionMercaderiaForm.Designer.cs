namespace GrupoC.RecepcionMercaderia
{
    partial class RecepcionMercaderiaForm
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


        private void InitializeComponent()
        {
            Remito = new ListBox();
            ClieRem = new Label();
            NRem = new Label();
            TranspRem = new Label();
            comboBoxClieR = new ComboBox();
            textBoxNRem = new TextBox();
            textBoxTransRem = new TextBox();
            dateTimePickerFRem = new DateTimePicker();
            FecRem = new Label();
            NARem = new Label();
            textBoxNARem = new TextBox();
            PatenR = new Label();
            As = new TextBox();
            CantRem = new Label();
            CantContRem = new Label();
            buttonAgregIRem = new Button();
            textBoxSKURem = new TextBox();
            CancRem = new Button();
            ConfRem = new Button();
            ObsRem = new Label();
            textBoxObsRem = new TextBox();
            fileSystemWatcher1 = new FileSystemWatcher();
            SKUREM = new Label();
            textBoxCatnCRem = new TextBox();
            textBoxCanRem = new TextBox();
            DetMercRRem = new Label();
            listViewRem = new ListView();
            SkuCodigo = new ColumnHeader();
            Descripción = new ColumnHeader();
            Tipo = new ColumnHeader();
            CantcR = new ColumnHeader();
            CantR = new ColumnHeader();
            buttonQuitIRem = new Button();
            radiopalet = new RadioButton();
            radiounidad = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // Remito
            // 
            Remito.FormattingEnabled = true;
            Remito.Location = new Point(15, 20);
            Remito.Margin = new Padding(2);
            Remito.Name = "Remito";
            Remito.Size = new Size(693, 109);
            Remito.TabIndex = 0;
            Remito.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // ClieRem
            // 
            ClieRem.AutoSize = true;
            ClieRem.BackColor = SystemColors.ButtonHighlight;
            ClieRem.ForeColor = SystemColors.ActiveCaptionText;
            ClieRem.Location = new Point(22, 50);
            ClieRem.Margin = new Padding(2, 0, 2, 0);
            ClieRem.Name = "ClieRem";
            ClieRem.Size = new Size(44, 15);
            ClieRem.TabIndex = 1;
            ClieRem.Text = "Cliente";
            // 
            // NRem
            // 
            NRem.AutoSize = true;
            NRem.BackColor = SystemColors.ButtonHighlight;
            NRem.ForeColor = SystemColors.ActiveCaptionText;
            NRem.Location = new Point(22, 72);
            NRem.Margin = new Padding(2, 0, 2, 0);
            NRem.Name = "NRem";
            NRem.Size = new Size(62, 15);
            NRem.TabIndex = 2;
            NRem.Text = "N° Remito";
            // 
            // TranspRem
            // 
            TranspRem.AutoSize = true;
            TranspRem.BackColor = SystemColors.ButtonHighlight;
            TranspRem.ForeColor = SystemColors.ActiveCaptionText;
            TranspRem.Location = new Point(22, 92);
            TranspRem.Margin = new Padding(2, 0, 2, 0);
            TranspRem.Name = "TranspRem";
            TranspRem.Size = new Size(74, 15);
            TranspRem.TabIndex = 3;
            TranspRem.Text = "Transportista";
            // 
            // comboBoxClieR
            // 
            comboBoxClieR.FormattingEnabled = true;
            comboBoxClieR.Location = new Point(85, 48);
            comboBoxClieR.Margin = new Padding(2);
            comboBoxClieR.Name = "comboBoxClieR";
            comboBoxClieR.Size = new Size(216, 23);
            comboBoxClieR.TabIndex = 4;
            comboBoxClieR.SelectedIndexChanged += comboBoxClieR_SelectedIndexChanged;
            comboBoxClieR.KeyPress += comboBoxClieR_KeyPress;
            comboBoxClieR.Leave += comboBoxClieR_Leave;
            // 
            // textBoxNRem
            // 
            textBoxNRem.Location = new Point(89, 73);
            textBoxNRem.Margin = new Padding(2);
            textBoxNRem.Name = "textBoxNRem";
            textBoxNRem.Size = new Size(211, 23);
            textBoxNRem.TabIndex = 5;
            textBoxNRem.TextChanged += textBoxNRem_TextChanged;
            textBoxNRem.KeyPress += textBoxNRem_KeyPress;
            textBoxNRem.Leave += textBoxNRem_Leave;
            // 
            // textBoxTransRem
            // 
            textBoxTransRem.Location = new Point(110, 94);
            textBoxTransRem.Margin = new Padding(2);
            textBoxTransRem.Name = "textBoxTransRem";
            textBoxTransRem.Size = new Size(190, 23);
            textBoxTransRem.TabIndex = 6;
            textBoxTransRem.KeyPress += textBoxTransRem_KeyPress;
            // 
            // dateTimePickerFRem
            // 
            dateTimePickerFRem.Location = new Point(441, 46);
            dateTimePickerFRem.Margin = new Padding(2);
            dateTimePickerFRem.Name = "dateTimePickerFRem";
            dateTimePickerFRem.Size = new Size(255, 23);
            dateTimePickerFRem.TabIndex = 7;
            // 
            // FecRem
            // 
            FecRem.AutoSize = true;
            FecRem.BackColor = SystemColors.ButtonHighlight;
            FecRem.ForeColor = SystemColors.ActiveCaptionText;
            FecRem.Location = new Point(318, 50);
            FecRem.Margin = new Padding(2, 0, 2, 0);
            FecRem.Name = "FecRem";
            FecRem.Size = new Size(98, 15);
            FecRem.TabIndex = 8;
            FecRem.Text = "Fecha del Remito";
            // 
            // NARem
            // 
            NARem.AutoSize = true;
            NARem.BackColor = SystemColors.ButtonHighlight;
            NARem.ForeColor = SystemColors.ActiveCaptionText;
            NARem.Location = new Point(318, 72);
            NARem.Margin = new Padding(2, 0, 2, 0);
            NARem.Name = "NARem";
            NARem.Size = new Size(107, 15);
            NARem.TabIndex = 9;
            NARem.Text = "Nombre y Apellido";
            // 
            // textBoxNARem
            // 
            textBoxNARem.Location = new Point(441, 70);
            textBoxNARem.Margin = new Padding(2);
            textBoxNARem.Name = "textBoxNARem";
            textBoxNARem.Size = new Size(255, 23);
            textBoxNARem.TabIndex = 10;
            textBoxNARem.KeyPress += textBoxNARem_KeyPress;
            // 
            // PatenR
            // 
            PatenR.AutoSize = true;
            PatenR.BackColor = SystemColors.ButtonHighlight;
            PatenR.ForeColor = SystemColors.ActiveCaptionText;
            PatenR.Location = new Point(318, 98);
            PatenR.Margin = new Padding(2, 0, 2, 0);
            PatenR.Name = "PatenR";
            PatenR.Size = new Size(47, 15);
            PatenR.TabIndex = 11;
            PatenR.Text = "Patente";
            // 
            // As
            // 
            As.Location = new Point(441, 96);
            As.Margin = new Padding(2);
            As.Name = "As";
            As.Size = new Size(255, 23);
            As.TabIndex = 12;
            // 
            // CantRem
            // 
            CantRem.AutoSize = true;
            CantRem.BackColor = SystemColors.ControlLight;
            CantRem.ForeColor = SystemColors.ActiveCaptionText;
            CantRem.Location = new Point(277, 141);
            CantRem.Margin = new Padding(2, 0, 2, 0);
            CantRem.Name = "CantRem";
            CantRem.Size = new Size(76, 15);
            CantRem.TabIndex = 17;
            CantRem.Text = "Cant. Remito";
            CantRem.Click += label10_Click;
            // 
            // CantContRem
            // 
            CantContRem.AutoSize = true;
            CantContRem.BackColor = SystemColors.ControlLight;
            CantContRem.ForeColor = SystemColors.ActiveCaptionText;
            CantContRem.Location = new Point(149, 141);
            CantContRem.Margin = new Padding(2, 0, 2, 0);
            CantContRem.Name = "CantContRem";
            CantContRem.Size = new Size(83, 15);
            CantContRem.TabIndex = 18;
            CantContRem.Text = "Cant. Contada";
            CantContRem.Click += label11_Click;
            // 
            // buttonAgregIRem
            // 
            buttonAgregIRem.BackColor = SystemColors.ButtonFace;
            buttonAgregIRem.Location = new Point(506, 168);
            buttonAgregIRem.Margin = new Padding(2);
            buttonAgregIRem.Name = "buttonAgregIRem";
            buttonAgregIRem.Size = new Size(113, 29);
            buttonAgregIRem.TabIndex = 20;
            buttonAgregIRem.Text = "+ Agregar Ítem";
            buttonAgregIRem.UseVisualStyleBackColor = false;
            buttonAgregIRem.Click += buttonAgregIRem_Click;
            // 
            // textBoxSKURem
            // 
            textBoxSKURem.Location = new Point(15, 168);
            textBoxSKURem.Margin = new Padding(2);
            textBoxSKURem.Name = "textBoxSKURem";
            textBoxSKURem.Size = new Size(121, 23);
            textBoxSKURem.TabIndex = 24;
            // 
            // CancRem
            // 
            CancRem.BackColor = SystemColors.ButtonFace;
            CancRem.Location = new Point(494, 411);
            CancRem.Margin = new Padding(2);
            CancRem.Name = "CancRem";
            CancRem.Size = new Size(105, 20);
            CancRem.TabIndex = 33;
            CancRem.Text = "Cancelar";
            CancRem.UseVisualStyleBackColor = false;
            CancRem.Click += CancRem_Click;
            // 
            // ConfRem
            // 
            ConfRem.BackColor = SystemColors.ButtonFace;
            ConfRem.Location = new Point(603, 411);
            ConfRem.Margin = new Padding(2);
            ConfRem.Name = "ConfRem";
            ConfRem.Size = new Size(105, 20);
            ConfRem.TabIndex = 34;
            ConfRem.Text = "Confirmar Recepeción";
            ConfRem.UseVisualStyleBackColor = false;
            ConfRem.Click += button5_Click;
            // 
            // ObsRem
            // 
            ObsRem.AutoSize = true;
            ObsRem.BackColor = SystemColors.ControlLight;
            ObsRem.ForeColor = SystemColors.ActiveCaptionText;
            ObsRem.Location = new Point(15, 362);
            ObsRem.Margin = new Padding(2, 0, 2, 0);
            ObsRem.Name = "ObsRem";
            ObsRem.Size = new Size(87, 15);
            ObsRem.TabIndex = 41;
            ObsRem.Text = "Observaciones:";
            // 
            // textBoxObsRem
            // 
            textBoxObsRem.Location = new Point(16, 379);
            textBoxObsRem.Margin = new Padding(2);
            textBoxObsRem.Name = "textBoxObsRem";
            textBoxObsRem.Size = new Size(234, 23);
            textBoxObsRem.TabIndex = 42;
            textBoxObsRem.Text = "Caja con golpes visibles...";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // SKUREM
            // 
            SKUREM.AutoSize = true;
            SKUREM.BackColor = SystemColors.ControlLight;
            SKUREM.ForeColor = SystemColors.ActiveCaptionText;
            SKUREM.Location = new Point(15, 141);
            SKUREM.Margin = new Padding(2, 0, 2, 0);
            SKUREM.Name = "SKUREM";
            SKUREM.Size = new Size(75, 15);
            SKUREM.TabIndex = 43;
            SKUREM.Text = "SKU/ Código";
            SKUREM.Click += label7_Click_1;
            // 
            // textBoxCatnCRem
            // 
            textBoxCatnCRem.Location = new Point(149, 168);
            textBoxCatnCRem.Margin = new Padding(2);
            textBoxCatnCRem.Name = "textBoxCatnCRem";
            textBoxCatnCRem.Size = new Size(121, 23);
            textBoxCatnCRem.TabIndex = 44;
            textBoxCatnCRem.KeyPress += textBoxCatnCRem_KeyPress;
            // 
            // textBoxCanRem
            // 
            textBoxCanRem.Location = new Point(277, 168);
            textBoxCanRem.Margin = new Padding(2);
            textBoxCanRem.Name = "textBoxCanRem";
            textBoxCanRem.Size = new Size(121, 23);
            textBoxCanRem.TabIndex = 45;
            textBoxCanRem.KeyPress += textBoxCanRem_KeyPress;
            // 
            // DetMercRRem
            // 
            DetMercRRem.AutoSize = true;
            DetMercRRem.BackColor = SystemColors.ControlLight;
            DetMercRRem.ForeColor = SystemColors.ActiveCaptionText;
            DetMercRRem.Location = new Point(15, 204);
            DetMercRRem.Margin = new Padding(2, 0, 2, 0);
            DetMercRRem.Name = "DetMercRRem";
            DetMercRRem.Size = new Size(169, 15);
            DetMercRRem.TabIndex = 46;
            DetMercRRem.Text = "Detalle de Mercaderia Recibida";
            DetMercRRem.Click += label8_Click;
            // 
            // listViewRem
            // 
            listViewRem.Columns.AddRange(new ColumnHeader[] { SkuCodigo, Descripción, Tipo, CantcR, CantR });
            listViewRem.Location = new Point(16, 232);
            listViewRem.Margin = new Padding(3, 2, 3, 2);
            listViewRem.Name = "listViewRem";
            listViewRem.Size = new Size(603, 90);
            listViewRem.TabIndex = 47;
            listViewRem.UseCompatibleStateImageBehavior = false;
            listViewRem.View = View.Details;
            listViewRem.SelectedIndexChanged += listView1_SelectedIndexChanged;
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
            // Tipo
            // 
            Tipo.Text = "Tipo";
            Tipo.Width = 120;
            // 
            // CantcR
            // 
            CantcR.Text = "Cantidad contada";
            CantcR.Width = 120;
            // 
            // CantR
            // 
            CantR.Text = "Cantidad remito";
            CantR.Width = 100;
            // 
            // buttonQuitIRem
            // 
            buttonQuitIRem.BackColor = SystemColors.ButtonFace;
            buttonQuitIRem.Location = new Point(16, 324);
            buttonQuitIRem.Margin = new Padding(2);
            buttonQuitIRem.Name = "buttonQuitIRem";
            buttonQuitIRem.Size = new Size(603, 20);
            buttonQuitIRem.TabIndex = 48;
            buttonQuitIRem.Text = "- Quitar Ítem";
            buttonQuitIRem.UseVisualStyleBackColor = false;
            buttonQuitIRem.Click += buttonQuitIRem_Click;
            // 
            // radiopalet
            // 
            radiopalet.AutoSize = true;
            radiopalet.Location = new Point(425, 162);
            radiopalet.Name = "radiopalet";
            radiopalet.Size = new Size(51, 19);
            radiopalet.TabIndex = 49;
            radiopalet.TabStop = true;
            radiopalet.Text = "Palet";
            radiopalet.UseVisualStyleBackColor = true;
            // 
            // radiounidad
            // 
            radiounidad.AutoSize = true;
            radiounidad.Location = new Point(425, 187);
            radiounidad.Name = "radiounidad";
            radiounidad.Size = new Size(63, 19);
            radiounidad.TabIndex = 50;
            radiounidad.TabStop = true;
            radiounidad.Text = "Unidad";
            radiounidad.UseVisualStyleBackColor = true;
            // 
            // RecepcionMercaderiaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(722, 540);
            Controls.Add(radiounidad);
            Controls.Add(radiopalet);
            Controls.Add(buttonQuitIRem);
            Controls.Add(listViewRem);
            Controls.Add(DetMercRRem);
            Controls.Add(textBoxCanRem);
            Controls.Add(textBoxCatnCRem);
            Controls.Add(SKUREM);
            Controls.Add(textBoxObsRem);
            Controls.Add(ObsRem);
            Controls.Add(ConfRem);
            Controls.Add(CancRem);
            Controls.Add(textBoxSKURem);
            Controls.Add(buttonAgregIRem);
            Controls.Add(CantContRem);
            Controls.Add(CantRem);
            Controls.Add(As);
            Controls.Add(PatenR);
            Controls.Add(textBoxNARem);
            Controls.Add(NARem);
            Controls.Add(FecRem);
            Controls.Add(dateTimePickerFRem);
            Controls.Add(textBoxTransRem);
            Controls.Add(textBoxNRem);
            Controls.Add(comboBoxClieR);
            Controls.Add(TranspRem);
            Controls.Add(NRem);
            Controls.Add(ClieRem);
            Controls.Add(Remito);
            Cursor = Cursors.AppStarting;
            Margin = new Padding(2);
            Name = "RecepcionMercaderiaForm";
            Text = "RecepcionMercaderia";
            FormClosing += RecepcionMercaderiaForm_FormClosing;
            Load += RecepcionMercaderiaForm_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox Remito;
        private Label ClieRem;
        private Label NRem;
        private Label TranspRem;
        private ComboBox comboBoxClieR;
        private TextBox textBoxNRem;
        private TextBox textBoxTransRem;
        private DateTimePicker dateTimePickerFRem;
        private Label FecRem;
        private Label NARem;
        private TextBox textBoxNARem;
        private Label PatenR;
        private TextBox As;
        private Label CantRem;
        private Label CantContRem;
        private Button buttonAgregIRem;
        private TextBox textBoxSKURem;
        private Button CancRem;
        private Button ConfRem;
        private Label ObsRem;
        private TextBox textBoxObsRem;
        private FileSystemWatcher fileSystemWatcher1;
        private Label SKUREM;
        private TextBox textBoxCanRem;
        private TextBox textBoxCatnCRem;
        private Label DetMercRRem;
        private ListView listViewRem;
        private ColumnHeader SkuCodigo;
        private ColumnHeader Descripción;
        private Button buttonQuitIRem;
        private ColumnHeader CantcR;
        private ColumnHeader CantR;
        private ColumnHeader Tipo;
        private RadioButton radiounidad;
        private RadioButton radiopalet;
    }
}