namespace Indicadores_Escoria
{
    partial class horometros
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            label3 = new Label();
            cmbmes = new ComboBox();
            cmbanio = new ComboBox();
            label4 = new Label();
            panel5 = new Panel();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            tableLayoutPanel3 = new TableLayoutPanel();
            Fuera_operación = new FontAwesome.Sharp.IconButton();
            label5 = new Label();
            Sin_horometro = new FontAwesome.Sharp.IconButton();
            Operando = new FontAwesome.Sharp.IconButton();
            Disponible = new FontAwesome.Sharp.IconButton();
            motivo = new TextBox();
            lblMotivo = new Label();
            tabla_horometros = new DataGridView();
            panel6 = new Panel();
            panelRedondeado4 = new PanelRedondeado();
            tableLayoutRedondeado2 = new TableLayoutRedondeado();
            panelRedondeado3 = new PanelRedondeado();
            tableLayoutRedondeado1 = new TableLayoutRedondeado();
            panelRedondeado2 = new PanelRedondeado();
            panelRedondeado1 = new PanelRedondeado();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel5.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tabla_horometros).BeginInit();
            panel6.SuspendLayout();
            panelRedondeado4.SuspendLayout();
            tableLayoutRedondeado2.SuspendLayout();
            panelRedondeado3.SuspendLayout();
            tableLayoutRedondeado1.SuspendLayout();
            panelRedondeado2.SuspendLayout();
            panelRedondeado1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(12, 12);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1400, 48);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.42857146F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 96.57143F));
            tableLayoutPanel2.Controls.Add(label1, 1, 0);
            tableLayoutPanel2.Controls.Add(iconPictureBox1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1400, 48);
            tableLayoutPanel2.TabIndex = 0;
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 25F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(6, 26, 54);
            label1.Location = new Point(48, 4);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(551, 40);
            label1.TabIndex = 0;
            label1.Text = "Registro mensual de horómetros";
            label1.Click += label1_Click;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = SystemColors.ControlLightLight;
            iconPictureBox1.Dock = DockStyle.Fill;
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 48;
            iconPictureBox1.Location = new Point(0, 0);
            iconPictureBox1.Margin = new Padding(0);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(48, 48);
            iconPictureBox1.TabIndex = 1;
            iconPictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.10876846F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.857143F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.2142849F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.642857F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.4285717F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.67151F));
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 1, 0);
            tableLayoutPanel1.Controls.Add(cmbmes, 2, 0);
            tableLayoutPanel1.Controls.Add(cmbanio, 4, 0);
            tableLayoutPanel1.Controls.Add(label4, 3, 0);
            tableLayoutPanel1.Controls.Add(panel5, 5, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(2, 2);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1396, 55);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            label2.Location = new Point(0, 16);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(76, 22);
            label2.TabIndex = 0;
            label2.Text = "Filtros:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 12.75F);
            label3.Location = new Point(89, 18);
            label3.Name = "label3";
            label3.Size = new Size(44, 19);
            label3.TabIndex = 1;
            label3.Text = "Mes:";
            // 
            // cmbmes
            // 
            cmbmes.Anchor = AnchorStyles.Left;
            cmbmes.Font = new Font("Arial", 12.75F);
            cmbmes.FormattingEnabled = true;
            cmbmes.Location = new Point(141, 14);
            cmbmes.Name = "cmbmes";
            cmbmes.Size = new Size(220, 27);
            cmbmes.TabIndex = 2;
            // 
            // cmbanio
            // 
            cmbanio.Anchor = AnchorStyles.Left;
            cmbanio.Font = new Font("Arial", 12.75F);
            cmbanio.FormattingEnabled = true;
            cmbanio.Location = new Point(417, 14);
            cmbanio.Name = "cmbanio";
            cmbanio.Size = new Size(223, 27);
            cmbanio.TabIndex = 3;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 12.75F);
            label4.Location = new Point(367, 18);
            label4.Name = "label4";
            label4.Size = new Size(43, 19);
            label4.TabIndex = 4;
            label4.Text = "Año:";
            // 
            // panel5
            // 
            panel5.Controls.Add(iconButton1);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(643, 0);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(753, 55);
            panel5.TabIndex = 5;
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.Right;
            iconButton1.BackgroundImageLayout = ImageLayout.None;
            iconButton1.Font = new Font("Arial", 12.75F);
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.FileArrowDown;
            iconButton1.IconColor = Color.Green;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(608, 8);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(128, 40);
            iconButton1.TabIndex = 5;
            iconButton1.Text = "Exportar";
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 6;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.834165F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9092369F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9092369F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9092369F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9092369F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.5288925F));
            tableLayoutPanel3.Controls.Add(Fuera_operación, 4, 0);
            tableLayoutPanel3.Controls.Add(label5, 0, 0);
            tableLayoutPanel3.Controls.Add(Sin_horometro, 3, 0);
            tableLayoutPanel3.Controls.Add(Operando, 1, 0);
            tableLayoutPanel3.Controls.Add(Disponible, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1400, 82);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // Fuera_operación
            // 
            Fuera_operación.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Fuera_operación.Font = new Font("Arial", 12.75F);
            Fuera_operación.ForeColor = Color.Red;
            Fuera_operación.IconChar = FontAwesome.Sharp.IconChar.CircleDot;
            Fuera_operación.IconColor = Color.Red;
            Fuera_operación.IconFont = FontAwesome.Sharp.IconFont.Auto;
            Fuera_operación.Location = new Point(820, 6);
            Fuera_operación.Name = "Fuera_operación";
            Fuera_operación.Size = new Size(230, 70);
            Fuera_operación.TabIndex = 5;
            Fuera_operación.Text = "Fuera de operación";
            Fuera_operación.TextImageRelation = TextImageRelation.ImageBeforeText;
            Fuera_operación.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            label5.Location = new Point(0, 30);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(82, 22);
            label5.TabIndex = 4;
            label5.Text = "Estado:";
            // 
            // Sin_horometro
            // 
            Sin_horometro.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Sin_horometro.Font = new Font("Arial", 12.75F);
            Sin_horometro.ForeColor = Color.LimeGreen;
            Sin_horometro.IconChar = FontAwesome.Sharp.IconChar.CircleDot;
            Sin_horometro.IconColor = Color.LimeGreen;
            Sin_horometro.IconFont = FontAwesome.Sharp.IconFont.Auto;
            Sin_horometro.Location = new Point(584, 6);
            Sin_horometro.Name = "Sin_horometro";
            Sin_horometro.Size = new Size(230, 70);
            Sin_horometro.TabIndex = 2;
            Sin_horometro.Text = "Sin horómetro";
            Sin_horometro.TextImageRelation = TextImageRelation.ImageBeforeText;
            Sin_horometro.UseVisualStyleBackColor = true;
            // 
            // Operando
            // 
            Operando.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Operando.Font = new Font("Arial", 12.75F);
            Operando.IconChar = FontAwesome.Sharp.IconChar.CircleDot;
            Operando.IconColor = Color.Black;
            Operando.IconFont = FontAwesome.Sharp.IconFont.Auto;
            Operando.Location = new Point(112, 6);
            Operando.Name = "Operando";
            Operando.Size = new Size(230, 70);
            Operando.TabIndex = 0;
            Operando.Text = "Operando";
            Operando.TextImageRelation = TextImageRelation.ImageBeforeText;
            Operando.UseVisualStyleBackColor = true;
            // 
            // Disponible
            // 
            Disponible.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Disponible.Font = new Font("Arial", 12.75F);
            Disponible.ForeColor = Color.DeepSkyBlue;
            Disponible.IconChar = FontAwesome.Sharp.IconChar.CircleDot;
            Disponible.IconColor = Color.DeepSkyBlue;
            Disponible.IconFont = FontAwesome.Sharp.IconFont.Auto;
            Disponible.Location = new Point(348, 6);
            Disponible.Name = "Disponible";
            Disponible.Size = new Size(230, 70);
            Disponible.TabIndex = 1;
            Disponible.Text = "Disponible";
            Disponible.TextImageRelation = TextImageRelation.ImageBeforeText;
            Disponible.UseVisualStyleBackColor = true;
            // 
            // motivo
            // 
            motivo.BackColor = Color.White;
            motivo.BorderStyle = BorderStyle.FixedSingle;
            motivo.Dock = DockStyle.Fill;
            motivo.Enabled = false;
            motivo.Font = new Font("Arial", 11F);
            motivo.Location = new Point(125, 2);
            motivo.Margin = new Padding(0, 2, 2, 2);
            motivo.MaxLength = 200;
            motivo.Multiline = true;
            motivo.Name = "motivo";
            motivo.PlaceholderText = "Seleccione una fecha; la observación se guarda automáticamente al salir del cuadro...";
            motivo.ScrollBars = ScrollBars.Vertical;
            motivo.Size = new Size(1273, 76);
            motivo.TabIndex = 1;
            // 
            // lblMotivo
            // 
            lblMotivo.Anchor = AnchorStyles.Left;
            lblMotivo.AutoSize = true;
            lblMotivo.BackColor = Color.Transparent;
            lblMotivo.Font = new Font("Arial", 12.75F, FontStyle.Bold);
            lblMotivo.Location = new Point(0, 30);
            lblMotivo.Margin = new Padding(0);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(114, 19);
            lblMotivo.TabIndex = 0;
            lblMotivo.Text = "Observación:";
            // 
            // tabla_horometros
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(242, 247, 253);
            tabla_horometros.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            tabla_horometros.BackgroundColor = Color.White;
            tabla_horometros.BorderStyle = BorderStyle.None;
            tabla_horometros.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.SteelBlue;
            dataGridViewCellStyle2.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            tabla_horometros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            tabla_horometros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Arial", 12F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.White;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            tabla_horometros.DefaultCellStyle = dataGridViewCellStyle3;
            tabla_horometros.Dock = DockStyle.Fill;
            tabla_horometros.EnableHeadersVisualStyles = false;
            tabla_horometros.GridColor = SystemColors.ScrollBar;
            tabla_horometros.Location = new Point(0, 0);
            tabla_horometros.Margin = new Padding(0);
            tabla_horometros.Name = "tabla_horometros";
            tabla_horometros.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Arial", 12F);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            tabla_horometros.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            tabla_horometros.RowHeadersVisible = false;
            tabla_horometros.Size = new Size(1400, 668);
            tabla_horometros.TabIndex = 0;
            // 
            // panel6
            // 
            panel6.Controls.Add(panelRedondeado4);
            panel6.Controls.Add(panelRedondeado3);
            panel6.Controls.Add(panelRedondeado2);
            panel6.Controls.Add(panelRedondeado1);
            panel6.Controls.Add(panel1);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 0);
            panel6.Margin = new Padding(0);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(12);
            panel6.Size = new Size(1424, 981);
            panel6.TabIndex = 5;
            // 
            // panelRedondeado4
            // 
            panelRedondeado4.BackColor = Color.White;
            panelRedondeado4.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado4.ColorSombra = Color.Black;
            panelRedondeado4.Controls.Add(tableLayoutRedondeado2);
            panelRedondeado4.Dock = DockStyle.Fill;
            panelRedondeado4.Location = new Point(12, 301);
            panelRedondeado4.Margin = new Padding(0);
            panelRedondeado4.Name = "panelRedondeado4";
            panelRedondeado4.RadioBorde = 10;
            panelRedondeado4.Size = new Size(1400, 668);
            panelRedondeado4.TabIndex = 8;
            // 
            // tableLayoutRedondeado2
            // 
            tableLayoutRedondeado2.BackColor = Color.White;
            tableLayoutRedondeado2.ColorBorde = Color.FromArgb(218, 229, 241);
            tableLayoutRedondeado2.ColorSombra = Color.Black;
            tableLayoutRedondeado2.ColumnCount = 1;
            tableLayoutRedondeado2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutRedondeado2.Controls.Add(tabla_horometros, 0, 0);
            tableLayoutRedondeado2.Dock = DockStyle.Fill;
            tableLayoutRedondeado2.GrosorBorde = 0;
            tableLayoutRedondeado2.Location = new Point(0, 0);
            tableLayoutRedondeado2.Margin = new Padding(0);
            tableLayoutRedondeado2.Name = "tableLayoutRedondeado2";
            tableLayoutRedondeado2.RadioBorde = 10;
            tableLayoutRedondeado2.RowCount = 1;
            tableLayoutRedondeado2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutRedondeado2.Size = new Size(1400, 668);
            tableLayoutRedondeado2.TabIndex = 1;
            // 
            // panelRedondeado3
            // 
            panelRedondeado3.BackColor = Color.White;
            panelRedondeado3.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado3.ColorSombra = Color.Black;
            panelRedondeado3.Controls.Add(tableLayoutRedondeado1);
            panelRedondeado3.Dock = DockStyle.Top;
            panelRedondeado3.GrosorBorde = 0;
            panelRedondeado3.Location = new Point(12, 201);
            panelRedondeado3.Margin = new Padding(0);
            panelRedondeado3.Name = "panelRedondeado3";
            panelRedondeado3.Padding = new Padding(0, 10, 0, 10);
            panelRedondeado3.RadioBorde = 10;
            panelRedondeado3.Size = new Size(1400, 100);
            panelRedondeado3.TabIndex = 7;
            // 
            // tableLayoutRedondeado1
            // 
            tableLayoutRedondeado1.BackColor = Color.White;
            tableLayoutRedondeado1.ColorBorde = Color.FromArgb(218, 229, 241);
            tableLayoutRedondeado1.ColorSombra = Color.Black;
            tableLayoutRedondeado1.ColumnCount = 2;
            tableLayoutRedondeado1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.928572F));
            tableLayoutRedondeado1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 91.07143F));
            tableLayoutRedondeado1.Controls.Add(lblMotivo, 0, 0);
            tableLayoutRedondeado1.Controls.Add(motivo, 1, 0);
            tableLayoutRedondeado1.Dock = DockStyle.Fill;
            tableLayoutRedondeado1.Location = new Point(0, 10);
            tableLayoutRedondeado1.Margin = new Padding(0);
            tableLayoutRedondeado1.Name = "tableLayoutRedondeado1";
            tableLayoutRedondeado1.RowCount = 1;
            tableLayoutRedondeado1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutRedondeado1.Size = new Size(1400, 80);
            tableLayoutRedondeado1.TabIndex = 2;
            // 
            // panelRedondeado2
            // 
            panelRedondeado2.BackColor = Color.White;
            panelRedondeado2.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado2.ColorSombra = Color.Black;
            panelRedondeado2.Controls.Add(tableLayoutPanel3);
            panelRedondeado2.Dock = DockStyle.Top;
            panelRedondeado2.Location = new Point(12, 119);
            panelRedondeado2.Margin = new Padding(0);
            panelRedondeado2.Name = "panelRedondeado2";
            panelRedondeado2.Size = new Size(1400, 82);
            panelRedondeado2.TabIndex = 6;
            // 
            // panelRedondeado1
            // 
            panelRedondeado1.BackColor = Color.White;
            panelRedondeado1.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado1.ColorSombra = Color.Black;
            panelRedondeado1.Controls.Add(tableLayoutPanel1);
            panelRedondeado1.Dock = DockStyle.Top;
            panelRedondeado1.Location = new Point(12, 60);
            panelRedondeado1.Margin = new Padding(0);
            panelRedondeado1.Name = "panelRedondeado1";
            panelRedondeado1.Padding = new Padding(2);
            panelRedondeado1.RadioBorde = 10;
            panelRedondeado1.Size = new Size(1400, 59);
            panelRedondeado1.TabIndex = 5;
            // 
            // horometros
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1424, 981);
            Controls.Add(panel6);
            Name = "horometros";
            Text = "Horómetros";
            Load += horometros_Load;
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel5.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tabla_horometros).EndInit();
            panel6.ResumeLayout(false);
            panelRedondeado4.ResumeLayout(false);
            tableLayoutRedondeado2.ResumeLayout(false);
            panelRedondeado3.ResumeLayout(false);
            tableLayoutRedondeado1.ResumeLayout(false);
            tableLayoutRedondeado1.PerformLayout();
            panelRedondeado2.ResumeLayout(false);
            panelRedondeado1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label2;
        private Label label3;
        private ComboBox cmbmes;
        private ComboBox cmbanio;
        private Label label4;
        private FontAwesome.Sharp.IconButton iconButton1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label5;
        private FontAwesome.Sharp.IconButton Sin_horometro;
        private FontAwesome.Sharp.IconButton Disponible;
        private FontAwesome.Sharp.IconButton Operando;
        private TableLayoutPanel tableLayoutPanel3;
        private DataGridView tabla_horometros;
        private Panel panel5;
        private FontAwesome.Sharp.IconButton Fuera_operación;
        private Label lblMotivo;
        private TextBox motivo;
        private Panel panel6;
        private PanelRedondeado panelRedondeado1;
        private PanelRedondeado panelRedondeado3;
        private PanelRedondeado panelRedondeado2;
        private PanelRedondeado panelRedondeado4;
        private TableLayoutRedondeado tableLayoutRedondeado1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private TableLayoutRedondeado tableLayoutRedondeado2;
    }
}
