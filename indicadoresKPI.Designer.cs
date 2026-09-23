namespace Indicadores_Escoria
{
    partial class indicadoresKPI
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
            panelTitulo = new Panel();
            lblTitulo = new Label();
            panelFiltros = new Panel();
            layoutFiltros = new TableLayoutPanel();
            lblFiltros = new Label();
            lblFechaInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFechaFin = new Label();
            dtpFechaFin = new DateTimePicker();
            lblHorasOperacionDia = new Label();
            nudHorasOperacionDia = new NumericUpDown();
            btnGenerar = new Button();
            panelResumen = new Panel();
            lblResumen = new Label();
            tabIndicadores = new TabControl();
            tabDisponibilidad = new TabPage();
            layoutDisponibilidad = new TableLayoutPanel();
            lblDisponibilidadModelo = new Label();
            tablaDisponibilidadModelo = new DataGridView();
            tabMantenimiento = new TabPage();
            layoutMantenimiento = new TableLayoutPanel();
            lblMantenimientoModelo = new Label();
            tablaMantenimientoModelo = new DataGridView();
            tabMtbf = new TabPage();
            layoutMtbf = new TableLayoutPanel();
            lblMtbfEco = new Label();
            tablaMtbfEco = new DataGridView();
            lblMtbfModelo = new Label();
            tablaMtbfModelo = new DataGridView();
            tabMttr = new TabPage();
            layoutMttr = new TableLayoutPanel();
            lblMttrEco = new Label();
            tablaMttrEco = new DataGridView();
            lblMttrModelo = new Label();
            tablaMttrModelo = new DataGridView();
            tabConfiabilidad = new TabPage();
            layoutConfiabilidad = new TableLayoutPanel();
            lblConfiabilidadEco = new Label();
            tablaConfiabilidadEco = new DataGridView();
            lblConfiabilidadModelo = new Label();
            tablaConfiabilidadModelo = new DataGridView();
            tabFiabilidad = new TabPage();
            layoutFiabilidad = new TableLayoutPanel();
            lblFiabilidadEco = new Label();
            tablaFiabilidadEco = new DataGridView();
            lblFiabilidadModelo = new Label();
            tablaFiabilidadModelo = new DataGridView();
            panelTitulo.SuspendLayout();
            panelFiltros.SuspendLayout();
            layoutFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHorasOperacionDia).BeginInit();
            panelResumen.SuspendLayout();
            tabIndicadores.SuspendLayout();
            tabDisponibilidad.SuspendLayout();
            layoutDisponibilidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablaDisponibilidadModelo).BeginInit();
            tabMantenimiento.SuspendLayout();
            layoutMantenimiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablaMantenimientoModelo).BeginInit();
            tabMtbf.SuspendLayout();
            layoutMtbf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablaMtbfEco).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablaMtbfModelo).BeginInit();
            tabMttr.SuspendLayout();
            layoutMttr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablaMttrEco).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablaMttrModelo).BeginInit();
            tabConfiabilidad.SuspendLayout();
            layoutConfiabilidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablaConfiabilidadEco).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablaConfiabilidadModelo).BeginInit();
            tabFiabilidad.SuspendLayout();
            layoutFiabilidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablaFiabilidadEco).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablaFiabilidadModelo).BeginInit();
            SuspendLayout();
            //
            // panelTitulo
            //
            panelTitulo.Controls.Add(lblTitulo);
            panelTitulo.Dock = DockStyle.Top;
            panelTitulo.Location = new Point(0, 0);
            panelTitulo.Name = "panelTitulo";
            panelTitulo.Size = new Size(1424, 48);
            panelTitulo.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1424, 48);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Análisis de indicadores KPI";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // panelFiltros
            //
            panelFiltros.Controls.Add(layoutFiltros);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(0, 48);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Padding = new Padding(12, 8, 12, 8);
            panelFiltros.Size = new Size(1424, 68);
            panelFiltros.TabIndex = 1;
            //
            // layoutFiltros
            //
            layoutFiltros.ColumnCount = 9;
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutFiltros.Controls.Add(lblFiltros, 0, 0);
            layoutFiltros.Controls.Add(lblFechaInicio, 1, 0);
            layoutFiltros.Controls.Add(dtpFechaInicio, 2, 0);
            layoutFiltros.Controls.Add(lblFechaFin, 3, 0);
            layoutFiltros.Controls.Add(dtpFechaFin, 4, 0);
            layoutFiltros.Controls.Add(lblHorasOperacionDia, 5, 0);
            layoutFiltros.Controls.Add(nudHorasOperacionDia, 6, 0);
            layoutFiltros.Controls.Add(btnGenerar, 8, 0);
            layoutFiltros.Dock = DockStyle.Fill;
            layoutFiltros.Location = new Point(12, 8);
            layoutFiltros.Name = "layoutFiltros";
            layoutFiltros.RowCount = 1;
            layoutFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutFiltros.Size = new Size(1400, 52);
            layoutFiltros.TabIndex = 0;
            //
            // lblFiltros
            //
            lblFiltros.Anchor = AnchorStyles.Left;
            lblFiltros.AutoSize = true;
            lblFiltros.Font = new Font("Arial", 11F, FontStyle.Bold);
            lblFiltros.Location = new Point(3, 17);
            lblFiltros.Name = "lblFiltros";
            lblFiltros.Size = new Size(59, 18);
            lblFiltros.TabIndex = 0;
            lblFiltros.Text = "Filtros:";
            //
            // lblFechaInicio
            //
            lblFechaInicio.Anchor = AnchorStyles.Right;
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Arial", 10F);
            lblFechaInicio.Location = new Point(88, 18);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(89, 16);
            lblFechaInicio.TabIndex = 1;
            lblFechaInicio.Text = "Fecha inicial:";
            //
            // dtpFechaInicio
            //
            dtpFechaInicio.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            dtpFechaInicio.Font = new Font("Arial", 10F);
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaInicio.Location = new Point(183, 14);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(134, 23);
            dtpFechaInicio.TabIndex = 2;
            //
            // lblFechaFin
            //
            lblFechaFin.Anchor = AnchorStyles.Right;
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font = new Font("Arial", 10F);
            lblFechaFin.Location = new Point(328, 18);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(69, 16);
            lblFechaFin.TabIndex = 3;
            lblFechaFin.Text = "Fecha final:";
            //
            // dtpFechaFin
            //
            dtpFechaFin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            dtpFechaFin.Font = new Font("Arial", 10F);
            dtpFechaFin.Format = DateTimePickerFormat.Custom;
            dtpFechaFin.Location = new Point(403, 14);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(134, 23);
            dtpFechaFin.TabIndex = 4;
            //
            // lblHorasOperacionDia
            //
            lblHorasOperacionDia.Anchor = AnchorStyles.Right;
            lblHorasOperacionDia.AutoSize = true;
            lblHorasOperacionDia.Font = new Font("Arial", 10F);
            lblHorasOperacionDia.Location = new Point(552, 18);
            lblHorasOperacionDia.Name = "lblHorasOperacionDia";
            lblHorasOperacionDia.Size = new Size(135, 16);
            lblHorasOperacionDia.TabIndex = 5;
            lblHorasOperacionDia.Text = "Máx. operación/día:";
            //
            // nudHorasOperacionDia
            //
            nudHorasOperacionDia.Anchor = AnchorStyles.Left;
            nudHorasOperacionDia.DecimalPlaces = 1;
            nudHorasOperacionDia.Font = new Font("Arial", 10F);
            nudHorasOperacionDia.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudHorasOperacionDia.Location = new Point(693, 14);
            nudHorasOperacionDia.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            nudHorasOperacionDia.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudHorasOperacionDia.Name = "nudHorasOperacionDia";
            nudHorasOperacionDia.Size = new Size(70, 23);
            nudHorasOperacionDia.TabIndex = 6;
            nudHorasOperacionDia.Value = new decimal(new int[] { 21, 0, 0, 0 });
            //
            // btnGenerar
            //
            btnGenerar.Anchor = AnchorStyles.Right;
            btnGenerar.Location = new Point(1289, 10);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(108, 32);
            btnGenerar.TabIndex = 7;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = true;
            //
            // panelResumen
            //
            panelResumen.Controls.Add(lblResumen);
            panelResumen.Dock = DockStyle.Top;
            panelResumen.Location = new Point(0, 116);
            panelResumen.Name = "panelResumen";
            panelResumen.Padding = new Padding(12, 0, 12, 4);
            panelResumen.Size = new Size(1424, 34);
            panelResumen.TabIndex = 2;
            //
            // lblResumen
            //
            lblResumen.BackColor = Color.FromArgb(237, 244, 249);
            lblResumen.Dock = DockStyle.Fill;
            lblResumen.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblResumen.Location = new Point(12, 0);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(1400, 30);
            lblResumen.TabIndex = 0;
            lblResumen.Text = "Periodo";
            lblResumen.TextAlign = ContentAlignment.MiddleCenter;
            //
            // tabIndicadores
            //
            tabIndicadores.Controls.Add(tabDisponibilidad);
            tabIndicadores.Controls.Add(tabMantenimiento);
            tabIndicadores.Controls.Add(tabMtbf);
            tabIndicadores.Controls.Add(tabMttr);
            tabIndicadores.Controls.Add(tabConfiabilidad);
            tabIndicadores.Controls.Add(tabFiabilidad);
            tabIndicadores.Dock = DockStyle.Fill;
            tabIndicadores.Font = new Font("Arial", 9.5F);
            tabIndicadores.Location = new Point(0, 150);
            tabIndicadores.Name = "tabIndicadores";
            tabIndicadores.SelectedIndex = 0;
            tabIndicadores.Size = new Size(1424, 651);
            tabIndicadores.TabIndex = 3;
            //
            // tabDisponibilidad
            //
            tabDisponibilidad.Controls.Add(layoutDisponibilidad);
            tabDisponibilidad.Location = new Point(4, 25);
            tabDisponibilidad.Name = "tabDisponibilidad";
            tabDisponibilidad.Padding = new Padding(3);
            tabDisponibilidad.Size = new Size(1416, 622);
            tabDisponibilidad.TabIndex = 0;
            tabDisponibilidad.Text = "Disponibilidad y utilización";
            tabDisponibilidad.UseVisualStyleBackColor = true;
            //
            // layoutDisponibilidad
            //
            layoutDisponibilidad.ColumnCount = 1;
            layoutDisponibilidad.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDisponibilidad.Controls.Add(lblDisponibilidadModelo, 0, 0);
            layoutDisponibilidad.Controls.Add(tablaDisponibilidadModelo, 0, 1);
            layoutDisponibilidad.Dock = DockStyle.Fill;
            layoutDisponibilidad.Location = new Point(3, 3);
            layoutDisponibilidad.Name = "layoutDisponibilidad";
            layoutDisponibilidad.Padding = new Padding(8);
            layoutDisponibilidad.RowCount = 2;
            layoutDisponibilidad.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            layoutDisponibilidad.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDisponibilidad.Size = new Size(1410, 616);
            layoutDisponibilidad.TabIndex = 0;
            //
            // lblDisponibilidadModelo
            //
            ConfigurarTitulo(lblDisponibilidadModelo, "Indicadores ponderados por modelo");
            //
            // tablaDisponibilidadModelo
            //
            ConfigurarTablaDesigner(tablaDisponibilidadModelo, "tablaDisponibilidadModelo");
            //
            // tabMantenimiento
            //
            tabMantenimiento.Controls.Add(layoutMantenimiento);
            tabMantenimiento.Location = new Point(4, 25);
            tabMantenimiento.Name = "tabMantenimiento";
            tabMantenimiento.Padding = new Padding(3);
            tabMantenimiento.Size = new Size(1416, 622);
            tabMantenimiento.TabIndex = 1;
            tabMantenimiento.Text = "Mantenimiento por modelo";
            tabMantenimiento.UseVisualStyleBackColor = true;
            //
            // layoutMantenimiento
            //
            layoutMantenimiento.ColumnCount = 1;
            layoutMantenimiento.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMantenimiento.Controls.Add(lblMantenimientoModelo, 0, 0);
            layoutMantenimiento.Controls.Add(tablaMantenimientoModelo, 0, 1);
            layoutMantenimiento.Dock = DockStyle.Fill;
            layoutMantenimiento.Location = new Point(3, 3);
            layoutMantenimiento.Name = "layoutMantenimiento";
            layoutMantenimiento.Padding = new Padding(8);
            layoutMantenimiento.RowCount = 2;
            layoutMantenimiento.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            layoutMantenimiento.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMantenimiento.Size = new Size(1410, 616);
            layoutMantenimiento.TabIndex = 0;
            ConfigurarTitulo(lblMantenimientoModelo, "Cantidad y horas por tipo de mantenimiento y modelo");
            ConfigurarTablaDesigner(tablaMantenimientoModelo, "tablaMantenimientoModelo");
            //
            // tabMtbf
            //
            tabMtbf.Controls.Add(layoutMtbf);
            tabMtbf.Location = new Point(4, 25);
            tabMtbf.Name = "tabMtbf";
            tabMtbf.Padding = new Padding(3);
            tabMtbf.Size = new Size(1416, 622);
            tabMtbf.TabIndex = 2;
            tabMtbf.Text = "MTBF";
            tabMtbf.UseVisualStyleBackColor = true;
            ConfigurarLayoutDoble(layoutMtbf, "layoutMtbf", lblMtbfEco,
                "MTBF por ECO", tablaMtbfEco, "tablaMtbfEco",
                lblMtbfModelo, "MTBF ponderado por modelo",
                tablaMtbfModelo, "tablaMtbfModelo");
            //
            // tabMttr
            //
            tabMttr.Controls.Add(layoutMttr);
            tabMttr.Location = new Point(4, 25);
            tabMttr.Name = "tabMttr";
            tabMttr.Padding = new Padding(3);
            tabMttr.Size = new Size(1416, 622);
            tabMttr.TabIndex = 3;
            tabMttr.Text = "MTTR";
            tabMttr.UseVisualStyleBackColor = true;
            ConfigurarLayoutDoble(layoutMttr, "layoutMttr", lblMttrEco,
                "MTTR por ECO", tablaMttrEco, "tablaMttrEco",
                lblMttrModelo, "MTTR ponderado por modelo",
                tablaMttrModelo, "tablaMttrModelo");
            //
            // tabConfiabilidad
            //
            tabConfiabilidad.Controls.Add(layoutConfiabilidad);
            tabConfiabilidad.Location = new Point(4, 25);
            tabConfiabilidad.Name = "tabConfiabilidad";
            tabConfiabilidad.Padding = new Padding(3);
            tabConfiabilidad.Size = new Size(1416, 622);
            tabConfiabilidad.TabIndex = 4;
            tabConfiabilidad.Text = "Confiabilidad";
            tabConfiabilidad.UseVisualStyleBackColor = true;
            ConfigurarLayoutDoble(layoutConfiabilidad, "layoutConfiabilidad",
                lblConfiabilidadEco, "Confiabilidad por ECO",
                tablaConfiabilidadEco, "tablaConfiabilidadEco",
                lblConfiabilidadModelo, "Confiabilidad ponderada por modelo",
                tablaConfiabilidadModelo, "tablaConfiabilidadModelo");
            //
            // tabFiabilidad
            //
            tabFiabilidad.Controls.Add(layoutFiabilidad);
            tabFiabilidad.Location = new Point(4, 25);
            tabFiabilidad.Name = "tabFiabilidad";
            tabFiabilidad.Padding = new Padding(3);
            tabFiabilidad.Size = new Size(1416, 622);
            tabFiabilidad.TabIndex = 5;
            tabFiabilidad.Text = "Fiabilidad";
            tabFiabilidad.UseVisualStyleBackColor = true;
            ConfigurarLayoutDoble(layoutFiabilidad, "layoutFiabilidad",
                lblFiabilidadEco, "Fiabilidad por ECO",
                tablaFiabilidadEco, "tablaFiabilidadEco",
                lblFiabilidadModelo, "Fiabilidad ponderada por modelo",
                tablaFiabilidadModelo, "tablaFiabilidadModelo");
            //
            // indicadoresKPI
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1424, 801);
            Controls.Add(tabIndicadores);
            Controls.Add(panelResumen);
            Controls.Add(panelFiltros);
            Controls.Add(panelTitulo);
            MinimumSize = new Size(1100, 650);
            Name = "indicadoresKPI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Análisis de indicadores KPI";
            WindowState = FormWindowState.Maximized;
            panelTitulo.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            layoutFiltros.ResumeLayout(false);
            layoutFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHorasOperacionDia).EndInit();
            panelResumen.ResumeLayout(false);
            tabIndicadores.ResumeLayout(false);
            tabDisponibilidad.ResumeLayout(false);
            layoutDisponibilidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablaDisponibilidadModelo).EndInit();
            tabMantenimiento.ResumeLayout(false);
            layoutMantenimiento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablaMantenimientoModelo).EndInit();
            tabMtbf.ResumeLayout(false);
            layoutMtbf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablaMtbfEco).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablaMtbfModelo).EndInit();
            tabMttr.ResumeLayout(false);
            layoutMttr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablaMttrEco).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablaMttrModelo).EndInit();
            tabConfiabilidad.ResumeLayout(false);
            layoutConfiabilidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablaConfiabilidadEco).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablaConfiabilidadModelo).EndInit();
            tabFiabilidad.ResumeLayout(false);
            layoutFiabilidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablaFiabilidadEco).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablaFiabilidadModelo).EndInit();
            ResumeLayout(false);
        }

        private static void ConfigurarTitulo(Label etiqueta, string texto)
        {
            etiqueta.Dock = DockStyle.Fill;
            etiqueta.Font = new Font("Arial", 10F, FontStyle.Bold);
            etiqueta.Text = texto;
            etiqueta.TextAlign = ContentAlignment.MiddleLeft;
        }

        private static void ConfigurarTablaDesigner(
            DataGridView tabla,
            string nombre)
        {
            tabla.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tabla.Dock = DockStyle.Fill;
            tabla.Name = nombre;
            tabla.ScrollBars = ScrollBars.Both;
            tabla.TabIndex = 0;
        }

        private static void ConfigurarLayoutDoble(
            TableLayoutPanel layout,
            string nombreLayout,
            Label tituloEco,
            string textoEco,
            DataGridView tablaEco,
            string nombreTablaEco,
            Label tituloModelo,
            string textoModelo,
            DataGridView tablaModelo,
            string nombreTablaModelo)
        {
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(tituloEco, 0, 0);
            layout.Controls.Add(tablaEco, 0, 1);
            layout.Controls.Add(tituloModelo, 0, 2);
            layout.Controls.Add(tablaModelo, 0, 3);
            layout.Dock = DockStyle.Fill;
            layout.Name = nombreLayout;
            layout.Padding = new Padding(8);
            layout.RowCount = 4;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ConfigurarTitulo(tituloEco, textoEco);
            ConfigurarTablaDesigner(tablaEco, nombreTablaEco);
            ConfigurarTitulo(tituloModelo, textoModelo);
            ConfigurarTablaDesigner(tablaModelo, nombreTablaModelo);
        }

        #endregion

        private Panel panelTitulo;
        private Label lblTitulo;
        private Panel panelFiltros;
        private TableLayoutPanel layoutFiltros;
        private Label lblFiltros;
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaFin;
        private Label lblHorasOperacionDia;
        private NumericUpDown nudHorasOperacionDia;
        private Button btnGenerar;
        private Panel panelResumen;
        private Label lblResumen;
        private TabControl tabIndicadores;
        private TabPage tabDisponibilidad;
        private TableLayoutPanel layoutDisponibilidad;
        private Label lblDisponibilidadModelo;
        private DataGridView tablaDisponibilidadModelo;
        private TabPage tabMantenimiento;
        private TableLayoutPanel layoutMantenimiento;
        private Label lblMantenimientoModelo;
        private DataGridView tablaMantenimientoModelo;
        private TabPage tabMtbf;
        private TableLayoutPanel layoutMtbf;
        private Label lblMtbfEco;
        private DataGridView tablaMtbfEco;
        private Label lblMtbfModelo;
        private DataGridView tablaMtbfModelo;
        private TabPage tabMttr;
        private TableLayoutPanel layoutMttr;
        private Label lblMttrEco;
        private DataGridView tablaMttrEco;
        private Label lblMttrModelo;
        private DataGridView tablaMttrModelo;
        private TabPage tabConfiabilidad;
        private TableLayoutPanel layoutConfiabilidad;
        private Label lblConfiabilidadEco;
        private DataGridView tablaConfiabilidadEco;
        private Label lblConfiabilidadModelo;
        private DataGridView tablaConfiabilidadModelo;
        private TabPage tabFiabilidad;
        private TableLayoutPanel layoutFiabilidad;
        private Label lblFiabilidadEco;
        private DataGridView tablaFiabilidadEco;
        private Label lblFiabilidadModelo;
        private DataGridView tablaFiabilidadModelo;
    }
}
