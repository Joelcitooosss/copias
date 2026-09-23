namespace Indicadores_Escoria
{
    partial class reporte
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblTitulo = new Label();
            tablaFiltros = new TableLayoutPanel();
            lblFiltros = new Label();
            btnExportar = new Button();
            lblFechaInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFechaFin = new Label();
            dtpFechaFin = new DateTimePicker();
            lblHorasOperacionDia = new Label();
            nudHorasOperacionDia = new NumericUpDown();
            btnGenerar = new Button();
            lblInicio = new Label();
            lblInicioValor = new Label();
            lblFin = new Label();
            lblFinValor = new Label();
            lblDias = new Label();
            lblDiasValor = new Label();
            lblHorasOperacion = new Label();
            lblHorasOperacionValor = new Label();
            lblHorasCalendario = new Label();
            lblHorasCalendarioValor = new Label();
            tablaReporte = new DataGridView();
            panel1 = new Panel();
            panelRedondeado4 = new PanelRedondeado();
            panelRedondeado3 = new PanelRedondeado();
            tableLayoutRedondeado3 = new TableLayoutRedondeado();
            panelRedondeado2 = new PanelRedondeado();
            panelRedondeado1 = new PanelRedondeado();
            tableLayoutRedondeado1 = new TableLayoutRedondeado();
            tablaFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHorasOperacionDia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablaReporte).BeginInit();
            panel1.SuspendLayout();
            panelRedondeado4.SuspendLayout();
            panelRedondeado3.SuspendLayout();
            tableLayoutRedondeado3.SuspendLayout();
            panelRedondeado2.SuspendLayout();
            panelRedondeado1.SuspendLayout();
            tableLayoutRedondeado1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.Left;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Arial", 25F, FontStyle.Bold);
            lblTitulo.Location = new Point(84, 3);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(393, 43);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reporte de equipos por rango de fechas";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tablaFiltros
            // 
            tablaFiltros.ColumnCount = 10;
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 73F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 152F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 144F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tablaFiltros.Controls.Add(lblFiltros, 0, 0);
            tablaFiltros.Controls.Add(btnExportar, 9, 0);
            tablaFiltros.Controls.Add(lblFechaInicio, 1, 0);
            tablaFiltros.Controls.Add(dtpFechaInicio, 2, 0);
            tablaFiltros.Controls.Add(lblFechaFin, 3, 0);
            tablaFiltros.Controls.Add(dtpFechaFin, 4, 0);
            tablaFiltros.Controls.Add(lblHorasOperacionDia, 5, 0);
            tablaFiltros.Controls.Add(nudHorasOperacionDia, 6, 0);
            tablaFiltros.Controls.Add(btnGenerar, 8, 0);
            tablaFiltros.Dock = DockStyle.Fill;
            tablaFiltros.Location = new Point(10, 10);
            tablaFiltros.Margin = new Padding(0);
            tablaFiltros.Name = "tablaFiltros";
            tablaFiltros.RowCount = 1;
            tablaFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablaFiltros.Size = new Size(1380, 46);
            tablaFiltros.TabIndex = 0;
            // 
            // lblFiltros
            // 
            lblFiltros.Anchor = AnchorStyles.Left;
            lblFiltros.AutoSize = true;
            lblFiltros.Font = new Font("Arial", 12F);
            lblFiltros.Location = new Point(0, 14);
            lblFiltros.Margin = new Padding(0);
            lblFiltros.Name = "lblFiltros";
            lblFiltros.Size = new Size(55, 18);
            lblFiltros.TabIndex = 0;
            lblFiltros.Text = "Filtros:";
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Right;
            btnExportar.Font = new Font("Arial", 12F);
            btnExportar.Location = new Point(1272, 7);
            btnExportar.Margin = new Padding(0);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(108, 32);
            btnExportar.TabIndex = 8;
            btnExportar.Text = "Exportar CSV";
            btnExportar.UseVisualStyleBackColor = true;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.Anchor = AnchorStyles.Left;
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Arial", 12F);
            lblFechaInicio.Location = new Point(73, 14);
            lblFechaInicio.Margin = new Padding(0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(100, 18);
            lblFechaInicio.TabIndex = 1;
            lblFechaInicio.Text = "Fecha inicial:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Anchor = AnchorStyles.Left;
            dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            dtpFechaInicio.Font = new Font("Arial", 12F);
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaInicio.Location = new Point(178, 10);
            dtpFechaInicio.Margin = new Padding(0);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(152, 26);
            dtpFechaInicio.TabIndex = 2;
            // 
            // lblFechaFin
            // 
            lblFechaFin.Anchor = AnchorStyles.Left;
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font = new Font("Arial", 12F);
            lblFechaFin.Location = new Point(330, 14);
            lblFechaFin.Margin = new Padding(0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(88, 18);
            lblFechaFin.TabIndex = 3;
            lblFechaFin.Text = "Fecha final:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Anchor = AnchorStyles.Left;
            dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            dtpFechaFin.Font = new Font("Arial", 12F);
            dtpFechaFin.Format = DateTimePickerFormat.Custom;
            dtpFechaFin.Location = new Point(418, 10);
            dtpFechaFin.Margin = new Padding(0);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(132, 26);
            dtpFechaFin.TabIndex = 4;
            // 
            // lblHorasOperacionDia
            // 
            lblHorasOperacionDia.Anchor = AnchorStyles.Left;
            lblHorasOperacionDia.AutoSize = true;
            lblHorasOperacionDia.Font = new Font("Arial", 12F);
            lblHorasOperacionDia.Location = new Point(550, 14);
            lblHorasOperacionDia.Margin = new Padding(0);
            lblHorasOperacionDia.Name = "lblHorasOperacionDia";
            lblHorasOperacionDia.Size = new Size(144, 18);
            lblHorasOperacionDia.TabIndex = 5;
            lblHorasOperacionDia.Text = "Máx. operación/día:";
            // 
            // nudHorasOperacionDia
            // 
            nudHorasOperacionDia.Anchor = AnchorStyles.Left;
            nudHorasOperacionDia.DecimalPlaces = 1;
            nudHorasOperacionDia.Font = new Font("Arial", 12F);
            nudHorasOperacionDia.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudHorasOperacionDia.Location = new Point(694, 10);
            nudHorasOperacionDia.Margin = new Padding(0);
            nudHorasOperacionDia.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            nudHorasOperacionDia.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudHorasOperacionDia.Name = "nudHorasOperacionDia";
            nudHorasOperacionDia.Size = new Size(70, 26);
            nudHorasOperacionDia.TabIndex = 6;
            nudHorasOperacionDia.Value = new decimal(new int[] { 21, 0, 0, 0 });
            // 
            // btnGenerar
            // 
            btnGenerar.Anchor = AnchorStyles.Right;
            btnGenerar.Font = new Font("Arial", 12F);
            btnGenerar.Location = new Point(1152, 7);
            btnGenerar.Margin = new Padding(0);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(108, 32);
            btnGenerar.TabIndex = 7;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = true;
            // 
            // lblInicio
            // 
            lblInicio.Anchor = AnchorStyles.Right;
            lblInicio.BackColor = Color.Transparent;
            lblInicio.Font = new Font("Arial", 12F);
            lblInicio.Location = new Point(12, 27);
            lblInicio.Margin = new Padding(0);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(61, 26);
            lblInicio.TabIndex = 0;
            lblInicio.Text = "INICIO";
            lblInicio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblInicioValor
            // 
            lblInicioValor.Anchor = AnchorStyles.Left;
            lblInicioValor.BackColor = Color.Transparent;
            lblInicioValor.Font = new Font("Arial", 12F);
            lblInicioValor.Location = new Point(73, 27);
            lblInicioValor.Margin = new Padding(0);
            lblInicioValor.Name = "lblInicioValor";
            lblInicioValor.Size = new Size(89, 26);
            lblInicioValor.TabIndex = 1;
            lblInicioValor.Text = "01/08/2026";
            lblInicioValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFin
            // 
            lblFin.Anchor = AnchorStyles.Left;
            lblFin.BackColor = Color.Transparent;
            lblFin.Font = new Font("Arial", 12F);
            lblFin.Location = new Point(162, 27);
            lblFin.Margin = new Padding(0);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(51, 26);
            lblFin.TabIndex = 2;
            lblFin.Text = "FIN";
            lblFin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFinValor
            // 
            lblFinValor.Anchor = AnchorStyles.Left;
            lblFinValor.BackColor = Color.Transparent;
            lblFinValor.Font = new Font("Arial", 12F);
            lblFinValor.Location = new Point(214, 27);
            lblFinValor.Margin = new Padding(0);
            lblFinValor.Name = "lblFinValor";
            lblFinValor.Size = new Size(116, 26);
            lblFinValor.TabIndex = 3;
            lblFinValor.Text = "31/08/2026";
            lblFinValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDias
            // 
            lblDias.Anchor = AnchorStyles.Left;
            lblDias.BackColor = Color.Transparent;
            lblDias.Font = new Font("Arial", 12F);
            lblDias.Location = new Point(332, 27);
            lblDias.Margin = new Padding(0);
            lblDias.Name = "lblDias";
            lblDias.Size = new Size(47, 26);
            lblDias.TabIndex = 4;
            lblDias.Text = "DÍAS";
            lblDias.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDiasValor
            // 
            lblDiasValor.Anchor = AnchorStyles.Left;
            lblDiasValor.BackColor = Color.Transparent;
            lblDiasValor.Font = new Font("Arial", 12F);
            lblDiasValor.Location = new Point(380, 27);
            lblDiasValor.Margin = new Padding(0);
            lblDiasValor.Name = "lblDiasValor";
            lblDiasValor.Size = new Size(66, 26);
            lblDiasValor.TabIndex = 5;
            lblDiasValor.Text = "31";
            lblDiasValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHorasOperacion
            // 
            lblHorasOperacion.Anchor = AnchorStyles.Left;
            lblHorasOperacion.BackColor = Color.Transparent;
            lblHorasOperacion.Font = new Font("Arial", 12F);
            lblHorasOperacion.Location = new Point(447, 27);
            lblHorasOperacion.Margin = new Padding(0);
            lblHorasOperacion.Name = "lblHorasOperacion";
            lblHorasOperacion.Size = new Size(145, 26);
            lblHorasOperacion.TabIndex = 6;
            lblHorasOperacion.Text = "HRS. OPERACIÓN";
            lblHorasOperacion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHorasOperacionValor
            // 
            lblHorasOperacionValor.Anchor = AnchorStyles.Left;
            lblHorasOperacionValor.BackColor = Color.Transparent;
            lblHorasOperacionValor.Font = new Font("Arial", 12F);
            lblHorasOperacionValor.Location = new Point(595, 27);
            lblHorasOperacionValor.Margin = new Padding(0);
            lblHorasOperacionValor.Name = "lblHorasOperacionValor";
            lblHorasOperacionValor.Size = new Size(55, 26);
            lblHorasOperacionValor.TabIndex = 7;
            lblHorasOperacionValor.Text = "651";
            lblHorasOperacionValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHorasCalendario
            // 
            lblHorasCalendario.Anchor = AnchorStyles.Left;
            lblHorasCalendario.BackColor = Color.Transparent;
            lblHorasCalendario.Font = new Font("Arial", 12F);
            lblHorasCalendario.Location = new Point(651, 27);
            lblHorasCalendario.Margin = new Padding(0);
            lblHorasCalendario.Name = "lblHorasCalendario";
            lblHorasCalendario.Size = new Size(155, 26);
            lblHorasCalendario.TabIndex = 8;
            lblHorasCalendario.Text = "HRS. CALENDARIO";
            lblHorasCalendario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHorasCalendarioValor
            // 
            lblHorasCalendarioValor.Anchor = AnchorStyles.Left;
            lblHorasCalendarioValor.BackColor = Color.Transparent;
            lblHorasCalendarioValor.Font = new Font("Arial", 12F);
            lblHorasCalendarioValor.Location = new Point(809, 27);
            lblHorasCalendarioValor.Margin = new Padding(0);
            lblHorasCalendarioValor.Name = "lblHorasCalendarioValor";
            lblHorasCalendarioValor.Size = new Size(579, 26);
            lblHorasCalendarioValor.TabIndex = 9;
            lblHorasCalendarioValor.Text = "744";
            lblHorasCalendarioValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tablaReporte
            // 
            tablaReporte.BackgroundColor = Color.White;
            tablaReporte.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.SteelBlue;
            dataGridViewCellStyle1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            tablaReporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            tablaReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Arial", 12F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            tablaReporte.DefaultCellStyle = dataGridViewCellStyle2;
            tablaReporte.Dock = DockStyle.Fill;
            tablaReporte.EnableHeadersVisualStyles = false;
            tablaReporte.GridColor = SystemColors.ScrollBar;
            tablaReporte.Location = new Point(0, 0);
            tablaReporte.Margin = new Padding(0);
            tablaReporte.Name = "tablaReporte";
            tablaReporte.RowHeadersVisible = false;
            tablaReporte.Size = new Size(1400, 562);
            tablaReporte.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(panelRedondeado4);
            panel1.Controls.Add(panelRedondeado3);
            panel1.Controls.Add(panelRedondeado2);
            panel1.Controls.Add(panelRedondeado1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(12);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(12);
            panel1.Size = new Size(1424, 801);
            panel1.TabIndex = 9;
            // 
            // panelRedondeado4
            // 
            panelRedondeado4.BackColor = Color.White;
            panelRedondeado4.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado4.ColorSombra = Color.Black;
            panelRedondeado4.Controls.Add(tablaReporte);
            panelRedondeado4.Dock = DockStyle.Fill;
            panelRedondeado4.GrosorBorde = 0;
            panelRedondeado4.Location = new Point(12, 227);
            panelRedondeado4.Margin = new Padding(0);
            panelRedondeado4.Name = "panelRedondeado4";
            panelRedondeado4.RadioBorde = 10;
            panelRedondeado4.Size = new Size(1400, 562);
            panelRedondeado4.TabIndex = 12;
            // 
            // panelRedondeado3
            // 
            panelRedondeado3.BackColor = Color.Transparent;
            panelRedondeado3.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado3.ColorSombra = Color.Black;
            panelRedondeado3.Controls.Add(tableLayoutRedondeado3);
            panelRedondeado3.Dock = DockStyle.Top;
            panelRedondeado3.GrosorBorde = 0;
            panelRedondeado3.Location = new Point(12, 127);
            panelRedondeado3.Margin = new Padding(0);
            panelRedondeado3.Name = "panelRedondeado3";
            panelRedondeado3.Padding = new Padding(0, 10, 0, 10);
            panelRedondeado3.Size = new Size(1400, 100);
            panelRedondeado3.TabIndex = 11;
            // 
            // tableLayoutRedondeado3
            // 
            tableLayoutRedondeado3.BackColor = Color.White;
            tableLayoutRedondeado3.ColorBorde = Color.FromArgb(218, 229, 241);
            tableLayoutRedondeado3.ColorSombra = Color.Black;
            tableLayoutRedondeado3.ColumnCount = 10;
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.214286F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.357143F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.768116F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.478261F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.47826076F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.8550725F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.57971F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.057971F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.304348F));
            tableLayoutRedondeado3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.8840561F));
            tableLayoutRedondeado3.Controls.Add(lblHorasCalendarioValor, 9, 0);
            tableLayoutRedondeado3.Controls.Add(lblHorasCalendario, 8, 0);
            tableLayoutRedondeado3.Controls.Add(lblHorasOperacionValor, 7, 0);
            tableLayoutRedondeado3.Controls.Add(lblHorasOperacion, 6, 0);
            tableLayoutRedondeado3.Controls.Add(lblDiasValor, 5, 0);
            tableLayoutRedondeado3.Controls.Add(lblDias, 4, 0);
            tableLayoutRedondeado3.Controls.Add(lblFinValor, 3, 0);
            tableLayoutRedondeado3.Controls.Add(lblFin, 2, 0);
            tableLayoutRedondeado3.Controls.Add(lblInicioValor, 1, 0);
            tableLayoutRedondeado3.Controls.Add(lblInicio, 0, 0);
            tableLayoutRedondeado3.Dock = DockStyle.Fill;
            tableLayoutRedondeado3.Location = new Point(0, 10);
            tableLayoutRedondeado3.Margin = new Padding(0);
            tableLayoutRedondeado3.Name = "tableLayoutRedondeado3";
            tableLayoutRedondeado3.RowCount = 1;
            tableLayoutRedondeado3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutRedondeado3.Size = new Size(1400, 80);
            tableLayoutRedondeado3.TabIndex = 0;
            // 
            // panelRedondeado2
            // 
            panelRedondeado2.BackColor = Color.White;
            panelRedondeado2.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado2.ColorSombra = Color.Black;
            panelRedondeado2.Controls.Add(tablaFiltros);
            panelRedondeado2.Dock = DockStyle.Top;
            panelRedondeado2.Location = new Point(12, 61);
            panelRedondeado2.Margin = new Padding(0);
            panelRedondeado2.Name = "panelRedondeado2";
            panelRedondeado2.Padding = new Padding(10);
            panelRedondeado2.RadioBorde = 10;
            panelRedondeado2.Size = new Size(1400, 66);
            panelRedondeado2.TabIndex = 10;
            // 
            // panelRedondeado1
            // 
            panelRedondeado1.BackColor = Color.Transparent;
            panelRedondeado1.ColorBorde = Color.FromArgb(218, 229, 241);
            panelRedondeado1.ColorSombra = Color.Black;
            panelRedondeado1.Controls.Add(tableLayoutRedondeado1);
            panelRedondeado1.Dock = DockStyle.Top;
            panelRedondeado1.GrosorBorde = 0;
            panelRedondeado1.Location = new Point(12, 12);
            panelRedondeado1.Margin = new Padding(0);
            panelRedondeado1.Name = "panelRedondeado1";
            panelRedondeado1.Size = new Size(1400, 49);
            panelRedondeado1.TabIndex = 9;
            // 
            // tableLayoutRedondeado1
            // 
            tableLayoutRedondeado1.BackColor = Color.Transparent;
            tableLayoutRedondeado1.ColorBorde = Color.FromArgb(218, 229, 241);
            tableLayoutRedondeado1.ColorSombra = Color.Black;
            tableLayoutRedondeado1.ColumnCount = 2;
            tableLayoutRedondeado1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.785714F));
            tableLayoutRedondeado1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 94.21429F));
            tableLayoutRedondeado1.Controls.Add(lblTitulo, 1, 0);
            tableLayoutRedondeado1.Dock = DockStyle.Fill;
            tableLayoutRedondeado1.GrosorBorde = 0;
            tableLayoutRedondeado1.Location = new Point(0, 0);
            tableLayoutRedondeado1.Margin = new Padding(0);
            tableLayoutRedondeado1.Name = "tableLayoutRedondeado1";
            tableLayoutRedondeado1.RowCount = 1;
            tableLayoutRedondeado1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutRedondeado1.Size = new Size(1400, 49);
            tableLayoutRedondeado1.TabIndex = 0;
            // 
            // reporte
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1424, 801);
            Controls.Add(panel1);
            Name = "reporte";
            Text = "Reporte por rango de fechas";
            tablaFiltros.ResumeLayout(false);
            tablaFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHorasOperacionDia).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablaReporte).EndInit();
            panel1.ResumeLayout(false);
            panelRedondeado4.ResumeLayout(false);
            panelRedondeado3.ResumeLayout(false);
            tableLayoutRedondeado3.ResumeLayout(false);
            panelRedondeado2.ResumeLayout(false);
            panelRedondeado1.ResumeLayout(false);
            tableLayoutRedondeado1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label lblTitulo;
        private TableLayoutPanel tablaFiltros;
        private Label lblFiltros;
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaFin;
        private Label lblHorasOperacionDia;
        private NumericUpDown nudHorasOperacionDia;
        private Button btnGenerar;
        private Button btnExportar;
        private Label lblInicio;
        private Label lblInicioValor;
        private Label lblFin;
        private Label lblFinValor;
        private Label lblDias;
        private Label lblDiasValor;
        private Label lblHorasOperacion;
        private Label lblHorasOperacionValor;
        private Label lblHorasCalendario;
        private Label lblHorasCalendarioValor;
        private DataGridView tablaReporte;
        private Panel panel1;
        private PanelRedondeado panelRedondeado4;
        private PanelRedondeado panelRedondeado3;
        private TableLayoutRedondeado tableLayoutRedondeado3;
        private PanelRedondeado panelRedondeado2;
        private PanelRedondeado panelRedondeado1;
        private TableLayoutRedondeado tableLayoutRedondeado1;
    }
}
