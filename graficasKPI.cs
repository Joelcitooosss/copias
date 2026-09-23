using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    public partial class graficasKPI : Form
    {
        private static readonly Color[] Paleta =
        {
            Color.FromArgb(52, 152, 219),
            Color.FromArgb(46, 204, 113),
            Color.FromArgb(241, 196, 15),
            Color.FromArgb(231, 76, 60),
            Color.FromArgb(155, 89, 182),
            Color.FromArgb(26, 188, 156),
            Color.FromArgb(230, 126, 34),
            Color.FromArgb(52, 73, 94),
            Color.FromArgb(127, 140, 141),
            Color.FromArgb(22, 160, 133)
        };

        public graficasKPI()
        {
            InitializeComponent();
            Load += graficasKPI_Load;
            btnGenerar.Click += btnGenerar_Click;
            dtpFechaInicio.ValueChanged += Filtro_ValueChanged;
            dtpFechaFin.ValueChanged += Filtro_ValueChanged;
            nudHorasOperacionDia.ValueChanged += Filtro_ValueChanged;
        }

        private void graficasKPI_Load(object? sender, EventArgs e)
        {
            PrepararFiltros();
            ActualizarResumen();
            CargarGraficas();
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

        private void Filtro_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void btnGenerar_Click(object? sender, EventArgs e)
        {
            CargarGraficas();
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
                $"Utilización: {nudHorasOperacionDia.Value:N1} h/día  |  " +
                "Disponibilidad física y fiabilidad: 24 h/día";
        }

        private void CargarGraficas()
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
                ResultadoGraficasKPI resultado = GraficasKPIBD.Obtener(
                    inicio,
                    fin,
                    nudHorasOperacionDia.Value
                );

                ConstruirTodasLasVistas(resultado);
            }
            catch (Exception ex)
            {
                LimpiarPestanas();
                MessageBox.Show(
                    "No se pudieron generar las gráficas.\n\n" + ex.Message,
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

        private void ConstruirTodasLasVistas(
            ResultadoGraficasKPI resultado)
        {
            List<DataRow> filasEco =
                resultado.PorEco.AsEnumerable().ToList();
            List<DataRow> filasModelo =
                resultado.PorModelo.AsEnumerable().ToList();
            List<DataRow> mantenimientoEco =
                resultado.MantenimientoEcoTipo.AsEnumerable().ToList();
            List<DataRow> mantenimientoModelo =
                resultado.MantenimientoModeloTipo.AsEnumerable().ToList();

            ConstruirIndicadores(
                tabIndicadoresEco,
                filasEco,
                "Eco",
                "Todos los ECO",
                mantenimientoEco,
                "Eco"
            );

            ConstruirIndicadores(
                tabIndicadoresModelo,
                filasModelo,
                "Modelo",
                "Todos los modelos",
                mantenimientoModelo,
                "Modelo"
            );

            ConstruirVistaCadaModelo(filasEco, mantenimientoEco);
        }

        private void ConstruirVistaCadaModelo(
            IReadOnlyList<DataRow> filasEco,
            IReadOnlyList<DataRow> mantenimientoEco)
        {
            tabModelosIndividuales.SuspendLayout();
            tabModelosIndividuales.TabPages.Clear();

            string[] modelos = filasEco
                .Select(fila => Texto(fila, "Modelo", "SIN MODELO"))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(modelo => modelo)
                .ToArray();

            foreach (string modelo in modelos)
            {
                TabPage paginaModelo = new TabPage(modelo)
                {
                    BackColor = Color.White,
                    Padding = new Padding(4)
                };

                TabControl indicadoresDelModelo = new TabControl
                {
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 9F),
                    Multiline = true
                };
                paginaModelo.Controls.Add(indicadoresDelModelo);
                tabModelosIndividuales.TabPages.Add(paginaModelo);

                List<DataRow> equiposDelModelo = filasEco
                    .Where(fila => string.Equals(
                        Texto(fila, "Modelo", "SIN MODELO"),
                        modelo,
                        StringComparison.OrdinalIgnoreCase
                    ))
                    .ToList();

                List<DataRow> mantenimientoDelModelo = mantenimientoEco
                    .Where(fila => string.Equals(
                        Texto(fila, "Modelo", "SIN MODELO"),
                        modelo,
                        StringComparison.OrdinalIgnoreCase
                    ))
                    .ToList();

                ConstruirIndicadores(
                    indicadoresDelModelo,
                    equiposDelModelo,
                    "Eco",
                    modelo,
                    mantenimientoDelModelo,
                    "Eco"
                );
            }

            if (modelos.Length == 0)
            {
                tabModelosIndividuales.TabPages.Add(
                    CrearPaginaSinDatos("No hay modelos para mostrar.")
                );
            }

            tabModelosIndividuales.ResumeLayout();
        }

        private void ConstruirIndicadores(
            TabControl destino,
            IReadOnlyList<DataRow> filas,
            string campoCategoria,
            string contexto,
            IReadOnlyList<DataRow> filasMantenimiento,
            string campoCategoriaMantenimiento)
        {
            destino.SuspendLayout();
            destino.TabPages.Clear();

            string[] categorias = filas
                .Select(fila => Categoria(fila, campoCategoria))
                .ToArray();

            AgregarGraficaSimple(
                destino,
                "Disp. física",
                $"Disponibilidad física (%) — {contexto}",
                categorias,
                filas,
                "DisponibilidadFisica",
                "%",
                true,
                Paleta[0]
            );
            AgregarGraficaSimple(
                destino,
                "Disp. mecánica",
                $"Disponibilidad mecánica (%) — {contexto}",
                categorias,
                filas,
                "DisponibilidadMecanica",
                "%",
                true,
                Paleta[1]
            );
            AgregarGraficaSimple(
                destino,
                "Utilización",
                $"Utilización (%) — {contexto}",
                categorias,
                filas,
                "Utilizacion",
                "%",
                true,
                Paleta[2]
            );
            AgregarGraficaMantenimiento(
                destino,
                "Cant. mantto.",
                $"Cantidad por tipo de mantenimiento — {contexto}",
                categorias,
                filasMantenimiento,
                campoCategoriaMantenimiento,
                "Cantidad",
                ""
            );
            AgregarGraficaMantenimiento(
                destino,
                "Horas mantto.",
                $"Horas por tipo de mantenimiento — {contexto}",
                categorias,
                filasMantenimiento,
                campoCategoriaMantenimiento,
                "Horas",
                " h"
            );
            AgregarGraficaSimple(
                destino,
                "MTBF",
                $"Tiempo medio entre fallas (MTBF) — {contexto}",
                categorias,
                filas,
                "MTBF",
                " h",
                false,
                Paleta[5]
            );
            AgregarGraficaSimple(
                destino,
                "MTTR",
                $"Tiempo medio de reparación (MTTR) — {contexto}",
                categorias,
                filas,
                "MTTR",
                " h",
                false,
                Paleta[6]
            );
            AgregarGraficaSimple(
                destino,
                "Confiabilidad",
                $"Confiabilidad (%) — {contexto}",
                categorias,
                filas,
                "Confiabilidad",
                "%",
                true,
                Paleta[7]
            );
            AgregarGraficaSimple(
                destino,
                "Fiabilidad",
                $"Fiabilidad (%) — {contexto}",
                categorias,
                filas,
                "Fiabilidad",
                "%",
                true,
                Paleta[3]
            );

            destino.ResumeLayout();
        }

        private void AgregarGraficaSimple(
            TabControl destino,
            string nombrePestana,
            string titulo,
            IReadOnlyList<string> categorias,
            IReadOnlyList<DataRow> filas,
            string columna,
            string sufijo,
            bool porcentaje,
            Color color)
        {
            List<decimal?> valores = filas
                .Select(fila => DecimalNullable(fila, columna))
                .ToList();

            SerieGrafica serie = new SerieGrafica
            {
                Nombre = nombrePestana,
                Color = color,
                Valores = valores
            };

            AgregarPaginaGrafica(
                destino,
                nombrePestana,
                titulo,
                categorias,
                new[] { serie },
                sufijo,
                porcentaje,
                false
            );
        }

        private void AgregarGraficaMantenimiento(
            TabControl destino,
            string nombrePestana,
            string titulo,
            IReadOnlyList<string> categorias,
            IReadOnlyList<DataRow> filas,
            string campoCategoria,
            string campoValor,
            string sufijo)
        {
            string[] tipos = filas
                .Select(fila => Texto(
                    fila,
                    "TipoMantenimiento",
                    "SIN TIPO"
                ))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(tipo => tipo)
                .ToArray();

            List<SerieGrafica> series = new List<SerieGrafica>();

            for (int indice = 0; indice < tipos.Length; indice++)
            {
                string tipo = tipos[indice];
                List<decimal?> valores = new List<decimal?>();

                foreach (string categoria in categorias)
                {
                    decimal total = filas
                        .Where(fila =>
                            string.Equals(
                                Categoria(fila, campoCategoria),
                                categoria,
                                StringComparison.OrdinalIgnoreCase
                            ) &&
                            string.Equals(
                                Texto(
                                    fila,
                                    "TipoMantenimiento",
                                    "SIN TIPO"
                                ),
                                tipo,
                                StringComparison.OrdinalIgnoreCase
                            ))
                        .Sum(fila => Decimal(fila, campoValor));
                    valores.Add(total);
                }

                series.Add(new SerieGrafica
                {
                    Nombre = tipo,
                    Color = Paleta[indice % Paleta.Length],
                    Valores = valores
                });
            }

            if (series.Count == 0)
            {
                series.Add(new SerieGrafica
                {
                    Nombre = "SIN REGISTROS",
                    Color = Color.LightGray,
                    Valores = categorias.Select(_ => (decimal?)0m).ToArray()
                });
            }

            AgregarPaginaGrafica(
                destino,
                nombrePestana,
                titulo,
                categorias,
                series,
                sufijo,
                false,
                true
            );
        }

        private static void AgregarPaginaGrafica(
            TabControl destino,
            string nombrePestana,
            string titulo,
            IReadOnlyList<string> categorias,
            IReadOnlyList<SerieGrafica> series,
            string sufijo,
            bool porcentaje,
            bool apilada)
        {
            TabPage pagina = new TabPage(nombrePestana)
            {
                BackColor = Color.White,
                Padding = new Padding(4)
            };

            Panel desplazamiento = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White
            };

            GraficaBarrasControl grafica = new GraficaBarrasControl
            {
                Location = new Point(8, 8)
            };
            grafica.EstablecerDatos(
                titulo,
                categorias,
                series,
                sufijo,
                porcentaje,
                apilada
            );

            desplazamiento.Controls.Add(grafica);
            pagina.Controls.Add(desplazamiento);
            destino.TabPages.Add(pagina);

            void AjustarTamano()
            {
                int ancho = Math.Max(
                    grafica.AnchoRecomendado,
                    desplazamiento.ClientSize.Width - 18
                );
                int alto = Math.Max(
                    grafica.AltoRecomendado,
                    desplazamiento.ClientSize.Height - 18
                );
                grafica.Size = new Size(ancho, alto);
                desplazamiento.AutoScrollMinSize =
                    new Size(ancho + 16, alto + 16);
            }

            desplazamiento.Resize += (_, _) => AjustarTamano();
            AjustarTamano();
        }

        private static TabPage CrearPaginaSinDatos(string mensaje)
        {
            TabPage pagina = new TabPage("Sin datos");
            Label etiqueta = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 11F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Text = mensaje,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pagina.Controls.Add(etiqueta);
            return pagina;
        }

        private void LimpiarPestanas()
        {
            tabIndicadoresEco.TabPages.Clear();
            tabIndicadoresModelo.TabPages.Clear();
            tabModelosIndividuales.TabPages.Clear();
        }

        private static string Categoria(
            DataRow fila,
            string columna)
        {
            if (fila.IsNull(columna))
            {
                return "SIN DATO";
            }

            return columna.Equals("Eco", StringComparison.OrdinalIgnoreCase)
                ? Convert.ToInt32(fila[columna]).ToString()
                : (Convert.ToString(fila[columna]) ?? "SIN DATO").Trim();
        }

        private static string Texto(
            DataRow fila,
            string columna,
            string predeterminado)
        {
            string texto = fila.IsNull(columna)
                ? ""
                : Convert.ToString(fila[columna]) ?? "";

            return string.IsNullOrWhiteSpace(texto)
                ? predeterminado
                : texto.Trim();
        }

        private static decimal Decimal(DataRow fila, string columna)
        {
            return fila.IsNull(columna)
                ? 0m
                : Convert.ToDecimal(fila[columna]);
        }

        private static decimal? DecimalNullable(
            DataRow fila,
            string columna)
        {
            return fila.IsNull(columna)
                ? null
                : Convert.ToDecimal(fila[columna]);
        }
    }
}
