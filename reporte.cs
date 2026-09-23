using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    public partial class reporte : Form
    {
        private bool preparandoPeriodo = true;

        public reporte()
        {
            InitializeComponent();
            typeof(DataGridView)
                .GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(tablaReporte, true, null);

            Load += reporte_Load;
            dtpFechaInicio.ValueChanged += FiltroPeriodo_Changed;
            dtpFechaFin.ValueChanged += FiltroPeriodo_Changed;
            nudHorasOperacionDia.ValueChanged += FiltroPeriodo_Changed;
            btnGenerar.Click += btnGenerar_Click;
            btnExportar.Click += btnExportar_Click;
            tablaReporte.CellFormatting += tablaReporte_CellFormatting;
        }

        private void reporte_Load(object? sender, EventArgs e)
        {
            PrepararFiltros();
            ConfigurarTabla();
            preparandoPeriodo = false;
            ActualizarResumenPeriodo();
            GenerarReporte();
        }

        private void PrepararFiltros()
        {
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            dtpFechaFin.Format = DateTimePickerFormat.Custom;
            dtpFechaFin.CustomFormat = "dd/MM/yyyy";

            // Mantiene como valor inicial el mes anterior completo,
            // pero ambas fechas se pueden elegir o escribir manualmente.
            DateTime periodoInicial = DateTime.Today.AddMonths(-1);
            DateTime primerDia = new DateTime(
                periodoInicial.Year,
                periodoInicial.Month,
                1
            );

            dtpFechaInicio.Value = primerDia;
            dtpFechaFin.Value = primerDia.AddMonths(1).AddDays(-1);

            nudHorasOperacionDia.Minimum = 1;
            nudHorasOperacionDia.Maximum = 24;
            nudHorasOperacionDia.DecimalPlaces = 1;
            nudHorasOperacionDia.Increment = 0.5m;
            nudHorasOperacionDia.Value = 21;
        }

        private void ConfigurarTabla()
        {
        
            AgregarColumna("Equipo", "Equipo", 125, null, true);
            AgregarColumna("Modelo", "Modelo", 120, null, true);
            AgregarColumna("Serie", "N° de Serie", 155, null, true);
            AgregarColumna("Eco", "ECO", 65, "N0", true);
            AgregarColumna("Proyecto", "Proyecto", 135);
            AgregarColumna("Propiedad", "Propiedad", 95);
            AgregarColumna("Cliente", "Cliente", 135);
            AgregarColumna("SMRInicial", "SMR INICIAL", 95, "N1");
            AgregarColumna("SMRFinal", "SMR FINAL", 95, "N1");
            AgregarColumna(
                "HorasTrabajadas",
                "HRS TRABAJADAS",
                105,
                "N1"
            );
            AgregarColumna(
                "HorasMantenimiento",
                "HRS MANTTO.",
                100,
                "N1"
            );
            AgregarColumna(
                "DisponibilidadFisica",
                "Disponibilidad Física %",
                115,
                "N1"
            );
            AgregarColumna(
                "DisponibilidadMecanica",
                "Disponibilidad Mecánica %",
                115,
                "N1"
            );
            AgregarColumna("Utilizacion", "Utilización %", 100, "N1");
            AgregarColumna("Reserva", "Reserva (hrs)", 95, "N1");
            AgregarColumna(
                "Aprovechamiento",
                "Aprovechamiento %",
                120,
                "N1"
            );
        }

        private void AgregarColumna(
            string propiedad,
            string titulo,
            int ancho,
            string? formato = null,
            bool congelada = false)
        {
            DataGridViewTextBoxColumn columna =
                new DataGridViewTextBoxColumn
                {
                    Name = propiedad,
                    DataPropertyName = propiedad,
                    HeaderText = titulo,
                    Width = ancho,
                    ReadOnly = true,
                    Frozen = congelada,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };

            if (!string.IsNullOrWhiteSpace(formato))
            {
                columna.DefaultCellStyle.Format = formato;
                columna.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            tablaReporte.Columns.Add(columna);
        }

        private void FiltroPeriodo_Changed(object? sender, EventArgs e)
        {
            if (!preparandoPeriodo)
            {
                ActualizarResumenPeriodo();

                // Evita exportar resultados del rango anterior. El usuario
                // confirma el nuevo intervalo con el botón Generar.
                btnExportar.Enabled = false;
            }
        }

        private void btnGenerar_Click(object? sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            DateTime inicio = dtpFechaInicio.Value.Date;
            DateTime fin = dtpFechaFin.Value.Date;

            if (fin < inicio)
            {
                MessageBox.Show(
                    "La fecha final no puede ser anterior a la fecha inicial.",
                    "Rango de fechas no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                dtpFechaFin.Focus();
                return;
            }

            decimal horasOperacionDia = nudHorasOperacionDia.Value;
            ActualizarResumenPeriodo();

            UseWaitCursor = true;
            btnGenerar.Enabled = false;
            btnExportar.Enabled = false;

            try
            {
                DataTable datos = ReporteMensualBD.ObtenerReporte(
                    inicio,
                    fin,
                    horasOperacionDia
                );

                tablaReporte.DataSource = datos;
                btnExportar.Enabled = datos.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                tablaReporte.DataSource = null;

                MessageBox.Show(
                    "No se pudo generar el reporte.\n\n" + ex.Message,
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

        private void ActualizarResumenPeriodo()
        {
            DateTime inicio = dtpFechaInicio.Value.Date;
            DateTime fin = dtpFechaFin.Value.Date;

            lblInicioValor.Text = inicio.ToString("dd/MM/yyyy");
            lblFinValor.Text = fin.ToString("dd/MM/yyyy");

            if (fin < inicio)
            {
                lblDiasValor.Text = "-";
                lblHorasOperacionValor.Text = "-";
                lblHorasCalendarioValor.Text = "-";
                return;
            }

            int dias = (fin - inicio).Days + 1;
            decimal horasOperacionDia = nudHorasOperacionDia.Value;

            lblDiasValor.Text = dias.ToString(CultureInfo.CurrentCulture);
            lblHorasOperacionValor.Text =
                (dias * horasOperacionDia).ToString("N1");
            lblHorasCalendarioValor.Text =
                (dias * 24m).ToString("N0");
        }

        private void tablaReporte_CellFormatting(
            object? sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                tablaReporte.Rows[e.RowIndex].DataBoundItem is not DataRowView vista)
            {
                return;
            }

            

            string nombreColumna =
            tablaReporte.Columns[e.ColumnIndex].Name;

            if (
                nombreColumna != "DisponibilidadFisica" &&
                nombreColumna != "DisponibilidadMecanica"
               )
            {
                return;
            }

            if (e.Value == null || e.Value == DBNull.Value)
            {
                return;
            }

            decimal disponibilidad = Convert.ToDecimal(e.Value);

            if (disponibilidad < 70m)
            {
                e.CellStyle.BackColor = Color.FromArgb(247, 173, 183);
                e.CellStyle.ForeColor = Color.FromArgb(120, 20, 30);
            }
            else
            {
                e.CellStyle.BackColor = Color.FromArgb(64, 139, 195);
                e.CellStyle.ForeColor = Color.White;
            }
        }

        private void btnExportar_Click(object? sender, EventArgs e)
        {
            if (tablaReporte.DataSource is not DataTable tabla ||
                tabla.Rows.Count == 0)
            {
                return;
            }

            DateTime inicio = dtpFechaInicio.Value.Date;
            DateTime fin = dtpFechaFin.Value.Date;

            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV de Excel (*.csv)|*.csv",
                FileName =
                    $"Reporte_{inicio:yyyy-MM-dd}_a_{fin:yyyy-MM-dd}.csv",
                AddExtension = true,
                DefaultExt = "csv"
            };

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                ExportarCsv(tabla, dialogo.FileName);

                MessageBox.Show(
                    "Reporte exportado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo exportar el reporte.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private static void ExportarCsv(DataTable tabla, string ruta)
        {
            string[] columnas =
            {
                "Equipo", "Modelo", "Serie", "Eco",
                "Proyecto", "Propiedad", "Cliente", "SMRInicial",
                "SMRFinal", "HorasTrabajadas", "HorasMantenimiento",
                "DisponibilidadFisica", "DisponibilidadMecanica", "Utilizacion", "Reserva",
                "Aprovechamiento"
            };

            string[] encabezados =
            {
                "Equipo", "Modelo", "N° de Serie", "ECO",
                "Proyecto", "Propiedad", "Cliente", "SMR INICIAL",
                "SMR FINAL", "HRS TRABAJADAS", "HRS MANTTO.",
                "Disponibilidad Física %",  "Disponibilidad Mecánica %", "Utilización %",
                "Reserva (hrs)", "Aprovechamiento %"
            };

            using StreamWriter escritor = new StreamWriter(
                ruta,
                false,
                new UTF8Encoding(true)
            );

            escritor.WriteLine(string.Join(";", encabezados));

            foreach (DataRow fila in tabla.Rows)
            {
                string[] valores = new string[columnas.Length];

                for (int i = 0; i < columnas.Length; i++)
                {
                    object valor = fila[columnas[i]];
                    string texto;

                    if (valor == DBNull.Value)
                    {
                        texto = "";
                    }
                    else if (valor is decimal numero)
                    {
                        texto = numero.ToString(
                            "0.##",
                            CultureInfo.CurrentCulture
                        );
                    }
                    else
                    {
                        texto = Convert.ToString(
                            valor,
                            CultureInfo.CurrentCulture
                        ) ?? "";
                    }

                    valores[i] = EscaparCsv(texto);
                }

                escritor.WriteLine(string.Join(";", valores));
            }
        }

        private static string EscaparCsv(string texto)
        {
            if (texto.Contains(';') ||
                texto.Contains('"') ||
                texto.Contains('\r') ||
                texto.Contains('\n'))
            {
                return '"' + texto.Replace("\"", "\"\"") + '"';
            }

            return texto;
        }
    }
}
