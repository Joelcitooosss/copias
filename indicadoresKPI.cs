using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    public partial class indicadoresKPI : Form
    {
        private readonly DataGridView[] tablas;

        public indicadoresKPI()
        {
            InitializeComponent();

            tablas = new[]
            {
                tablaDisponibilidadModelo,
                tablaMantenimientoModelo,
                tablaMtbfEco,
                tablaMtbfModelo,
                tablaMttrEco,
                tablaMttrModelo,
                tablaConfiabilidadEco,
                tablaConfiabilidadModelo,
                tablaFiabilidadEco,
                tablaFiabilidadModelo
            };

            Load += indicadoresKPI_Load;
            btnGenerar.Click += btnGenerar_Click;
            dtpFechaInicio.ValueChanged += Filtro_Changed;
            dtpFechaFin.ValueChanged += Filtro_Changed;
            nudHorasOperacionDia.ValueChanged += Filtro_Changed;
        }

        private void indicadoresKPI_Load(object? sender, EventArgs e)
        {
            PrepararFiltros();

            foreach (DataGridView tabla in tablas)
            {
                ConfigurarTabla(tabla);
            }

            ActualizarResumen();
            CargarIndicadores();
        }

        private void PrepararFiltros()
        {
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            dtpFechaFin.Format = DateTimePickerFormat.Custom;
            dtpFechaFin.CustomFormat = "dd/MM/yyyy";

            DateTime mesAnterior = DateTime.Today.AddMonths(-1);
            DateTime primerDia = new DateTime(
                mesAnterior.Year,
                mesAnterior.Month,
                1
            );

            dtpFechaInicio.Value = primerDia;
            dtpFechaFin.Value = primerDia.AddMonths(1).AddDays(-1);

            nudHorasOperacionDia.Minimum = 1m;
            nudHorasOperacionDia.Maximum = 24m;
            nudHorasOperacionDia.DecimalPlaces = 1;
            nudHorasOperacionDia.Increment = 0.5m;
            nudHorasOperacionDia.Value = 21m;
        }

        private static void ConfigurarTabla(DataGridView tabla)
        {
            tabla.AutoGenerateColumns = true;
            tabla.ReadOnly = true;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.AllowUserToOrderColumns = true;
            tabla.AllowUserToResizeRows = false;
            tabla.MultiSelect = false;
            tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.RowHeadersVisible = false;
            tabla.ScrollBars = ScrollBars.Both;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            tabla.BackgroundColor = Color.White;
            tabla.BorderStyle = BorderStyle.Fixed3D;
            tabla.GridColor = Color.FromArgb(210, 220, 230);
            tabla.EnableHeadersVisualStyles = false;
            tabla.ColumnHeadersHeight = 42;
            tabla.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(40, 116, 166);
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tabla.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 9F, FontStyle.Bold);
            tabla.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            tabla.DefaultCellStyle.Font = new Font("Arial", 9F);
            tabla.DefaultCellStyle.SelectionBackColor =
                SystemColors.Highlight;
            tabla.DefaultCellStyle.SelectionForeColor =
                SystemColors.HighlightText;
            tabla.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 249, 252);
        }

        private void Filtro_Changed(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void btnGenerar_Click(object? sender, EventArgs e)
        {
            CargarIndicadores();
        }

        private void ActualizarResumen()
        {
            DateTime inicio = dtpFechaInicio.Value.Date;
            DateTime fin = dtpFechaFin.Value.Date;

            if (fin < inicio)
            {
                lblResumen.Text = "Rango de fechas no válido";
                return;
            }

            int dias = (fin - inicio).Days + 1;
            lblResumen.Text =
                $"Periodo: {inicio:dd/MM/yyyy} al {fin:dd/MM/yyyy}  |  " +
                $"Días: {dias}  |  " +
                $"Operación máxima: {nudHorasOperacionDia.Value:N1} h/día";
        }

        private void CargarIndicadores()
        {
            DateTime inicio = dtpFechaInicio.Value.Date;
            DateTime fin = dtpFechaFin.Value.Date;

            if (fin < inicio)
            {
                MessageBox.Show(
                    "La fecha final no puede ser anterior a la fecha inicial.",
                    "Rango no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                dtpFechaFin.Focus();
                return;
            }

            UseWaitCursor = true;
            btnGenerar.Enabled = false;

            try
            {
                ResultadoIndicadoresKPI resultado =
                    IndicadoresKPIBD.ObtenerIndicadores(
                        inicio,
                        fin,
                        nudHorasOperacionDia.Value
                    );

                tablaDisponibilidadModelo.DataSource =
                    resultado.DisponibilidadPorModelo;
                tablaMantenimientoModelo.DataSource =
                    resultado.MantenimientoPorModelo;
                tablaMtbfEco.DataSource = resultado.MtbfPorEco;
                tablaMtbfModelo.DataSource = resultado.MtbfPorModelo;
                tablaMttrEco.DataSource = resultado.MttrPorEco;
                tablaMttrModelo.DataSource = resultado.MttrPorModelo;
                tablaConfiabilidadEco.DataSource =
                    resultado.ConfiabilidadPorEco;
                tablaConfiabilidadModelo.DataSource =
                    resultado.ConfiabilidadPorModelo;
                tablaFiabilidadEco.DataSource =
                    resultado.FiabilidadPorEco;
                tablaFiabilidadModelo.DataSource =
                    resultado.FiabilidadPorModelo;

                foreach (DataGridView tabla in tablas)
                {
                    PrepararColumnas(tabla);
                }
            }
            catch (Exception ex)
            {
                LimpiarTablas();

                MessageBox.Show(
                    "No se pudieron cargar los indicadores.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnGenerar.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private static void PrepararColumnas(DataGridView tabla)
        {
            Dictionary<string, (string Titulo, int Ancho, string? Formato)>
                configuracion = new()
                {
                    ["Eco"] = ("ECO", 75, "N0"),
                    ["Modelo"] = ("MODELO", 170, null),
                    ["CantidadEquipos"] = ("EQUIPOS", 85, "N0"),
                    ["TipoMantenimiento"] =
                        ("TIPO DE MANTENIMIENTO", 190, null),
                    ["Cantidad"] = ("CANTIDAD", 95, "N0"),
                    ["Horas"] = ("HORAS", 105, "N2"),
                    ["HorasTrabajadas"] =
                        ("HRS. TRABAJADAS", 125, "N2"),
                    ["HorasMantenimiento"] =
                        ("HRS. MANTTO.", 115, "N2"),
                    ["HorasCorrectivas"] =
                        ("HRS. CORRECTIVAS", 135, "N2"),
                    ["HorasCalendario"] =
                        ("HRS. DISPONIBLES", 135, "N2"),
                    ["NumeroFallas"] =
                        ("NÚMERO DE FALLAS", 130, "N0"),
                    ["DisponibilidadFisica"] =
                        ("DISP. FÍSICA %", 120, "N2"),
                    ["DisponibilidadMecanica"] =
                        ("DISP. MECÁNICA %", 135, "N2"),
                    ["Utilizacion"] = ("UTILIZACIÓN %", 115, "N2"),
                    ["MTBF"] = ("MTBF (HRS)", 105, "N2"),
                    ["MTTR"] = ("MTTR (HRS)", 105, "N2"),
                    ["Confiabilidad"] =
                        ("CONFIABILIDAD %", 135, "N2"),
                    ["Fiabilidad"] = ("FIABILIDAD %", 120, "N2")
                };

            foreach (DataGridViewColumn columna in tabla.Columns)
            {
                if (!configuracion.TryGetValue(
                        columna.DataPropertyName,
                        out var datos))
                {
                    continue;
                }

                columna.HeaderText = datos.Titulo;
                columna.Width = datos.Ancho;
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

                if (!string.IsNullOrWhiteSpace(datos.Formato))
                {
                    columna.DefaultCellStyle.Format = datos.Formato;
                    columna.DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                    columna.DefaultCellStyle.NullValue = "N/A";
                }
            }

            if (tabla.Columns.Count > 0)
            {
                tabla.Columns[tabla.Columns.Count - 1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void LimpiarTablas()
        {
            foreach (DataGridView tabla in tablas)
            {
                tabla.DataSource = null;
            }
        }
    }
}
