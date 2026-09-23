using System.Drawing;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    partial class graficasKPI
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
            tabVistas = new TabControl();
            tabPorEco = new TabPage();
            tabIndicadoresEco = new TabControl();
            tabPorModelo = new TabPage();
            tabIndicadoresModelo = new TabControl();
            tabCadaModelo = new TabPage();
            tabModelosIndividuales = new TabControl();
            panelTitulo.SuspendLayout();
            panelFiltros.SuspendLayout();
            layoutFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHorasOperacionDia).BeginInit();
            panelResumen.SuspendLayout();
            tabVistas.SuspendLayout();
            tabPorEco.SuspendLayout();
            tabPorModelo.SuspendLayout();
            tabCadaModelo.SuspendLayout();
            SuspendLayout();
            //
            // panelTitulo
            //
            panelTitulo.BackColor = Color.White;
            panelTitulo.Controls.Add(lblTitulo);
            panelTitulo.Dock = DockStyle.Top;
            panelTitulo.Location = new Point(0, 0);
            panelTitulo.Name = "panelTitulo";
            panelTitulo.Size = new Size(1424, 50);
            panelTitulo.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1424, 50);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gráficas de indicadores KPI";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // panelFiltros
            //
            panelFiltros.BackColor = Color.White;
            panelFiltros.Controls.Add(layoutFiltros);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(0, 50);
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
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
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
            lblFechaFin.Location = new Point(333, 18);
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
            dtpFechaFin.Location = new Point(408, 14);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(134, 23);
            dtpFechaFin.TabIndex = 4;
            //
            // lblHorasOperacionDia
            //
            lblHorasOperacionDia.Anchor = AnchorStyles.Right;
            lblHorasOperacionDia.AutoSize = true;
            lblHorasOperacionDia.Font = new Font("Arial", 10F);
            lblHorasOperacionDia.Location = new Point(557, 18);
            lblHorasOperacionDia.Name = "lblHorasOperacionDia";
            lblHorasOperacionDia.Size = new Size(145, 16);
            lblHorasOperacionDia.TabIndex = 5;
            lblHorasOperacionDia.Text = "Máx. operación/día:";
            //
            // nudHorasOperacionDia
            //
            nudHorasOperacionDia.Anchor = AnchorStyles.Left;
            nudHorasOperacionDia.DecimalPlaces = 1;
            nudHorasOperacionDia.Font = new Font("Arial", 10F);
            nudHorasOperacionDia.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudHorasOperacionDia.Location = new Point(708, 14);
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
            btnGenerar.Font = new Font("Arial", 9.5F, FontStyle.Bold);
            btnGenerar.Location = new Point(1282, 9);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(115, 34);
            btnGenerar.TabIndex = 7;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = true;
            //
            // panelResumen
            //
            panelResumen.BackColor = Color.White;
            panelResumen.Controls.Add(lblResumen);
            panelResumen.Dock = DockStyle.Top;
            panelResumen.Location = new Point(0, 118);
            panelResumen.Name = "panelResumen";
            panelResumen.Padding = new Padding(12, 0, 12, 6);
            panelResumen.Size = new Size(1424, 38);
            panelResumen.TabIndex = 2;
            //
            // lblResumen
            //
            lblResumen.BackColor = Color.FromArgb(237, 244, 249);
            lblResumen.Dock = DockStyle.Fill;
            lblResumen.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblResumen.Location = new Point(12, 0);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(1400, 32);
            lblResumen.TabIndex = 0;
            lblResumen.Text = "Periodo";
            lblResumen.TextAlign = ContentAlignment.MiddleCenter;
            //
            // tabVistas
            //
            tabVistas.Controls.Add(tabPorEco);
            tabVistas.Controls.Add(tabPorModelo);
            tabVistas.Controls.Add(tabCadaModelo);
            tabVistas.Dock = DockStyle.Fill;
            tabVistas.Font = new Font("Arial", 10F, FontStyle.Bold);
            tabVistas.Location = new Point(0, 156);
            tabVistas.Name = "tabVistas";
            tabVistas.SelectedIndex = 0;
            tabVistas.Size = new Size(1424, 645);
            tabVistas.TabIndex = 3;
            //
            // tabPorEco
            //
            tabPorEco.Controls.Add(tabIndicadoresEco);
            tabPorEco.Location = new Point(4, 25);
            tabPorEco.Name = "tabPorEco";
            tabPorEco.Padding = new Padding(4);
            tabPorEco.Size = new Size(1416, 616);
            tabPorEco.TabIndex = 0;
            tabPorEco.Text = "Todos los ECO";
            tabPorEco.UseVisualStyleBackColor = true;
            //
            // tabIndicadoresEco
            //
            tabIndicadoresEco.Dock = DockStyle.Fill;
            tabIndicadoresEco.Font = new Font("Arial", 9F);
            tabIndicadoresEco.Location = new Point(4, 4);
            tabIndicadoresEco.Multiline = true;
            tabIndicadoresEco.Name = "tabIndicadoresEco";
            tabIndicadoresEco.SelectedIndex = 0;
            tabIndicadoresEco.Size = new Size(1408, 608);
            tabIndicadoresEco.TabIndex = 0;
            //
            // tabPorModelo
            //
            tabPorModelo.Controls.Add(tabIndicadoresModelo);
            tabPorModelo.Location = new Point(4, 25);
            tabPorModelo.Name = "tabPorModelo";
            tabPorModelo.Padding = new Padding(4);
            tabPorModelo.Size = new Size(1416, 616);
            tabPorModelo.TabIndex = 1;
            tabPorModelo.Text = "Todos los modelos";
            tabPorModelo.UseVisualStyleBackColor = true;
            //
            // tabIndicadoresModelo
            //
            tabIndicadoresModelo.Dock = DockStyle.Fill;
            tabIndicadoresModelo.Font = new Font("Arial", 9F);
            tabIndicadoresModelo.Location = new Point(4, 4);
            tabIndicadoresModelo.Multiline = true;
            tabIndicadoresModelo.Name = "tabIndicadoresModelo";
            tabIndicadoresModelo.SelectedIndex = 0;
            tabIndicadoresModelo.Size = new Size(1408, 608);
            tabIndicadoresModelo.TabIndex = 0;
            //
            // tabCadaModelo
            //
            tabCadaModelo.Controls.Add(tabModelosIndividuales);
            tabCadaModelo.Location = new Point(4, 25);
            tabCadaModelo.Name = "tabCadaModelo";
            tabCadaModelo.Padding = new Padding(4);
            tabCadaModelo.Size = new Size(1416, 616);
            tabCadaModelo.TabIndex = 2;
            tabCadaModelo.Text = "Cada modelo";
            tabCadaModelo.UseVisualStyleBackColor = true;
            //
            // tabModelosIndividuales
            //
            tabModelosIndividuales.Dock = DockStyle.Fill;
            tabModelosIndividuales.Font = new Font("Arial", 9F, FontStyle.Bold);
            tabModelosIndividuales.Location = new Point(4, 4);
            tabModelosIndividuales.Multiline = true;
            tabModelosIndividuales.Name = "tabModelosIndividuales";
            tabModelosIndividuales.SelectedIndex = 0;
            tabModelosIndividuales.Size = new Size(1408, 608);
            tabModelosIndividuales.TabIndex = 0;
            //
            // graficasKPI
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1424, 801);
            Controls.Add(tabVistas);
            Controls.Add(panelResumen);
            Controls.Add(panelFiltros);
            Controls.Add(panelTitulo);
            MinimumSize = new Size(1050, 650);
            Name = "graficasKPI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gráficas KPI";
            WindowState = FormWindowState.Maximized;
            panelTitulo.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            layoutFiltros.ResumeLayout(false);
            layoutFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHorasOperacionDia).EndInit();
            panelResumen.ResumeLayout(false);
            tabVistas.ResumeLayout(false);
            tabPorEco.ResumeLayout(false);
            tabPorModelo.ResumeLayout(false);
            tabCadaModelo.ResumeLayout(false);
            ResumeLayout(false);
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
        private TabControl tabVistas;
        private TabPage tabPorEco;
        private TabControl tabIndicadoresEco;
        private TabPage tabPorModelo;
        private TabControl tabIndicadoresModelo;
        private TabPage tabCadaModelo;
        private TabControl tabModelosIndividuales;
    }
}
