using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    internal sealed class SerieGrafica
    {
        public string Nombre { get; init; } = "";
        public Color Color { get; init; } = Color.SteelBlue;
        public IReadOnlyList<decimal?> Valores { get; init; } =
            Array.Empty<decimal?>();
    }

    internal sealed class GraficaBarrasControl : Control
    {
        private IReadOnlyList<string> categorias = Array.Empty<string>();
        private IReadOnlyList<SerieGrafica> series =
            Array.Empty<SerieGrafica>();
        private string titulo = "";
        private string sufijo = "";
        private bool porcentaje;
        private bool apilada;

        public GraficaBarrasControl()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
            Font = new Font("Arial", 9F);
            MinimumSize = new Size(760, 470);
        }

        public int AnchoRecomendado
        {
            get
            {
                int anchoPorCategoria = apilada ? 86 : 74;
                return Math.Max(
                    900,
                    135 + categorias.Count * anchoPorCategoria
                );
            }
        }

        public int AltoRecomendado => 560;

        public void EstablecerDatos(
            string nuevoTitulo,
            IReadOnlyList<string> nuevasCategorias,
            IReadOnlyList<SerieGrafica> nuevasSeries,
            string nuevoSufijo = "",
            bool esPorcentaje = false,
            bool esApilada = false)
        {
            titulo = nuevoTitulo;
            categorias = nuevasCategorias.ToArray();
            series = nuevasSeries.ToArray();
            sufijo = nuevoSufijo;
            porcentaje = esPorcentaje;
            apilada = esApilada;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint =
                System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(BackColor);

            using Font fuenteTitulo =
                new Font("Arial", 13F, FontStyle.Bold);
            using Font fuenteEje =
                new Font("Arial", 8.5F, FontStyle.Regular);
            using Font fuenteValor =
                new Font("Arial", 8F, FontStyle.Bold);
            using Brush textoPrincipal =
                new SolidBrush(Color.FromArgb(35, 45, 55));
            using Brush textoSecundario =
                new SolidBrush(Color.FromArgb(75, 85, 95));

            StringFormat centrado = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(
                titulo,
                fuenteTitulo,
                textoPrincipal,
                new RectangleF(20, 14, Width - 40, 28),
                centrado
            );

            if (categorias.Count == 0 || series.Count == 0)
            {
                g.DrawString(
                    "No hay datos en el periodo seleccionado.",
                    Font,
                    textoSecundario,
                    new RectangleF(20, 80, Width - 40, 100),
                    centrado
                );
                return;
            }

            int parteSuperior = DibujarLeyenda(
                g,
                fuenteEje,
                textoSecundario
            );
            RectangleF area = new RectangleF(
                76,
                parteSuperior,
                Math.Max(100, Width - 104),
                Math.Max(120, Height - parteSuperior - 112)
            );

            decimal maximo = ObtenerMaximo();
            DibujarEjes(
                g,
                area,
                maximo,
                fuenteEje,
                textoSecundario
            );

            if (apilada)
            {
                DibujarBarrasApiladas(
                    g,
                    area,
                    maximo,
                    fuenteValor,
                    textoPrincipal
                );
            }
            else
            {
                DibujarBarrasAgrupadas(
                    g,
                    area,
                    maximo,
                    fuenteValor,
                    textoPrincipal
                );
            }

            DibujarCategorias(
                g,
                area,
                fuenteEje,
                textoSecundario
            );
        }

        private int DibujarLeyenda(
            Graphics g,
            Font fuente,
            Brush texto)
        {
            if (series.Count == 1)
            {
                return 58;
            }

            float x = 24;
            float y = 50;
            float altoFila = 23;

            foreach (SerieGrafica serie in series)
            {
                SizeF medida = g.MeasureString(serie.Nombre, fuente);
                float anchoElemento = medida.Width + 34;

                if (x + anchoElemento > Width - 24 && x > 24)
                {
                    x = 24;
                    y += altoFila;
                }

                using Brush color = new SolidBrush(serie.Color);
                g.FillRectangle(color, x, y + 2, 13, 13);
                g.DrawString(serie.Nombre, fuente, texto, x + 19, y);
                x += anchoElemento;
            }

            return (int)(y + altoFila + 8);
        }

        private decimal ObtenerMaximo()
        {
            if (porcentaje)
            {
                return 100m;
            }

            decimal maximo = 0m;

            if (apilada)
            {
                for (int i = 0; i < categorias.Count; i++)
                {
                    decimal total = series.Sum(serie =>
                        ValorEn(serie, i) ?? 0m
                    );
                    maximo = Math.Max(maximo, total);
                }
            }
            else
            {
                foreach (SerieGrafica serie in series)
                {
                    foreach (decimal? valor in serie.Valores)
                    {
                        if (valor.HasValue)
                        {
                            maximo = Math.Max(maximo, valor.Value);
                        }
                    }
                }
            }

            if (maximo <= 0m)
            {
                return 1m;
            }

            return RedondearMaximo(maximo * 1.12m);
        }

        private static decimal RedondearMaximo(decimal valor)
        {
            double numero = (double)valor;
            double magnitud = Math.Pow(10, Math.Floor(Math.Log10(numero)));
            double normalizado = numero / magnitud;
            double agradable;

            if (normalizado <= 1)
            {
                agradable = 1;
            }
            else if (normalizado <= 2)
            {
                agradable = 2;
            }
            else if (normalizado <= 5)
            {
                agradable = 5;
            }
            else
            {
                agradable = 10;
            }

            return (decimal)(agradable * magnitud);
        }

        private void DibujarEjes(
            Graphics g,
            RectangleF area,
            decimal maximo,
            Font fuente,
            Brush texto)
        {
            using Pen cuadricula =
                new Pen(Color.FromArgb(222, 228, 234), 1F);
            using Pen eje =
                new Pen(Color.FromArgb(105, 115, 125), 1.2F);

            const int divisiones = 5;

            for (int i = 0; i <= divisiones; i++)
            {
                float y = area.Bottom - area.Height * i / divisiones;
                decimal valor = maximo * i / divisiones;
                g.DrawLine(cuadricula, area.Left, y, area.Right, y);

                string etiqueta = FormatearEje(valor);
                SizeF medida = g.MeasureString(etiqueta, fuente);
                g.DrawString(
                    etiqueta,
                    fuente,
                    texto,
                    area.Left - medida.Width - 8,
                    y - medida.Height / 2
                );
            }

            g.DrawLine(eje, area.Left, area.Top, area.Left, area.Bottom);
            g.DrawLine(eje, area.Left, area.Bottom, area.Right, area.Bottom);
        }

        private void DibujarBarrasAgrupadas(
            Graphics g,
            RectangleF area,
            decimal maximo,
            Font fuenteValor,
            Brush texto)
        {
            float anchoGrupo = area.Width / categorias.Count;
            float anchoUtil = Math.Min(anchoGrupo * 0.72F, 58F);
            float anchoBarra = Math.Max(3F, anchoUtil / series.Count);

            for (int categoria = 0;
                 categoria < categorias.Count;
                 categoria++)
            {
                float inicio = area.Left + categoria * anchoGrupo +
                    (anchoGrupo - anchoBarra * series.Count) / 2F;
                bool tieneValor = false;

                for (int serieIndice = 0;
                     serieIndice < series.Count;
                     serieIndice++)
                {
                    SerieGrafica serie = series[serieIndice];
                    decimal? valorNullable = ValorEn(serie, categoria);

                    if (!valorNullable.HasValue)
                    {
                        continue;
                    }

                    tieneValor = true;
                    decimal valor = Math.Max(0m, valorNullable.Value);
                    float alto = (float)(valor / maximo) * area.Height;
                    float x = inicio + serieIndice * anchoBarra;
                    RectangleF barra = new RectangleF(
                        x + 1,
                        area.Bottom - alto,
                        Math.Max(2, anchoBarra - 2),
                        alto
                    );

                    using Brush relleno = new SolidBrush(serie.Color);
                    using Pen borde = new Pen(Oscurecer(serie.Color), 1F);
                    g.FillRectangle(relleno, barra);
                    g.DrawRectangle(
                        borde,
                        barra.X,
                        barra.Y,
                        barra.Width,
                        barra.Height
                    );

                    if (series.Count == 1 && categorias.Count <= 36)
                    {
                        string etiqueta = FormatearValor(valor);
                        SizeF medida = g.MeasureString(etiqueta, fuenteValor);
                        g.DrawString(
                            etiqueta,
                            fuenteValor,
                            texto,
                            barra.X + barra.Width / 2F - medida.Width / 2F,
                            Math.Max(area.Top, barra.Y - medida.Height - 2)
                        );
                    }
                }

                if (!tieneValor)
                {
                    DibujarNoDisponible(
                        g,
                        area,
                        categoria,
                        anchoGrupo,
                        fuenteValor,
                        texto
                    );
                }
            }
        }

        private void DibujarBarrasApiladas(
            Graphics g,
            RectangleF area,
            decimal maximo,
            Font fuenteValor,
            Brush texto)
        {
            float anchoGrupo = area.Width / categorias.Count;
            float anchoBarra = Math.Min(anchoGrupo * 0.62F, 52F);

            for (int categoria = 0;
                 categoria < categorias.Count;
                 categoria++)
            {
                float x = area.Left + categoria * anchoGrupo +
                    (anchoGrupo - anchoBarra) / 2F;
                float yActual = area.Bottom;
                decimal total = 0m;

                foreach (SerieGrafica serie in series)
                {
                    decimal valor = Math.Max(
                        0m,
                        ValorEn(serie, categoria) ?? 0m
                    );

                    if (valor == 0m)
                    {
                        continue;
                    }

                    float alto = (float)(valor / maximo) * area.Height;
                    RectangleF barra = new RectangleF(
                        x,
                        yActual - alto,
                        anchoBarra,
                        alto
                    );

                    using Brush relleno = new SolidBrush(serie.Color);
                    using Pen borde = new Pen(Color.White, 0.8F);
                    g.FillRectangle(relleno, barra);
                    g.DrawRectangle(
                        borde,
                        barra.X,
                        barra.Y,
                        barra.Width,
                        barra.Height
                    );
                    yActual -= alto;
                    total += valor;
                }

                if (total > 0m && categorias.Count <= 30)
                {
                    string etiqueta = FormatearValor(total);
                    SizeF medida = g.MeasureString(etiqueta, fuenteValor);
                    g.DrawString(
                        etiqueta,
                        fuenteValor,
                        texto,
                        x + anchoBarra / 2F - medida.Width / 2F,
                        Math.Max(area.Top, yActual - medida.Height - 2)
                    );
                }
            }
        }

        private void DibujarNoDisponible(
            Graphics g,
            RectangleF area,
            int categoria,
            float anchoGrupo,
            Font fuente,
            Brush texto)
        {
            string etiqueta = "N/A";
            SizeF medida = g.MeasureString(etiqueta, fuente);
            float centro = area.Left +
                categoria * anchoGrupo + anchoGrupo / 2F;
            g.DrawString(
                etiqueta,
                fuente,
                texto,
                centro - medida.Width / 2F,
                area.Bottom - medida.Height - 4
            );
        }

        private void DibujarCategorias(
            Graphics g,
            RectangleF area,
            Font fuente,
            Brush texto)
        {
            float anchoGrupo = area.Width / categorias.Count;

            for (int i = 0; i < categorias.Count; i++)
            {
                float centro = area.Left + i * anchoGrupo + anchoGrupo / 2F;
                GraphicsState estado = g.Save();
                g.TranslateTransform(centro - 2, area.Bottom + 9);
                g.RotateTransform(-43F);
                g.DrawString(
                    categorias[i],
                    fuente,
                    texto,
                    new RectangleF(-4, 0, 120, 20)
                );
                g.Restore(estado);
            }
        }

        private string FormatearEje(decimal valor)
        {
            string numero = valor >= 1000m
                ? valor.ToString("N0", CultureInfo.CurrentCulture)
                : valor.ToString("0.##", CultureInfo.CurrentCulture);
            return numero + sufijo;
        }

        private string FormatearValor(decimal valor)
        {
            string numero = valor.ToString(
                valor == decimal.Truncate(valor) ? "N0" : "N2",
                CultureInfo.CurrentCulture
            );
            return numero + sufijo;
        }

        private static decimal? ValorEn(
            SerieGrafica serie,
            int indice)
        {
            return indice >= 0 && indice < serie.Valores.Count
                ? serie.Valores[indice]
                : null;
        }

        private static Color Oscurecer(Color color)
        {
            return Color.FromArgb(
                Math.Max(0, color.R - 35),
                Math.Max(0, color.G - 35),
                Math.Max(0, color.B - 35)
            );
        }
    }
}
