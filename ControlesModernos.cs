// Controles redondeados para Windows Forms. No requiere paquetes NuGet.
// Sustituye el archivo anterior; no conserves copias de las mismas clases.
// No incluye DataGridViewRedondeado.
// EspesorSombra mantiene su significado anterior: intensidad de 0 a 100.
// El contorno usa cobertura por pixel; no se desplaza hacia el interior.
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    internal static class GeometriaRedondeada
    {
        internal static GraphicsPath CrearRuta(RectangleF rect, float radio)
        {
            GraphicsPath ruta = new GraphicsPath();
            if (rect.Width <= 0 || rect.Height <= 0) return ruta;

            float r = Math.Max(0, Math.Min(radio,
                Math.Min(rect.Width, rect.Height) / 2f));
            if (r == 0)
            {
                ruta.AddRectangle(rect);
                return ruta;
            }

            float d = r * 2f;
            ruta.AddArc(rect.Left, rect.Top, d, d, 180, 90);
            ruta.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
            ruta.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            ruta.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90);
            ruta.CloseFigure();
            return ruta;
        }

        // Distancia firmada al contorno: negativa dentro, positiva fuera.
        // El borde ocupa exactamente desde distancia 0 hasta -grosor.
        internal static double Distancia(
            double x, double y, double ancho, double alto, double radio)
        {
            double qx = Math.Abs(x - ancho / 2) - (ancho / 2 - radio);
            double qy = Math.Abs(y - alto / 2) - (alto / 2 - radio);
            double dx = Math.Max(qx, 0);
            double dy = Math.Max(qy, 0);
            return Math.Sqrt(dx * dx + dy * dy)
                + Math.Min(Math.Max(qx, qy), 0) - radio;
        }
    }

    /// <summary>
    /// Acabado compartido: borde, esquinas suavizadas y sombra exterior.
    /// No sustituye el pintado de texto, imagen, foco ni celdas del control.
    /// </summary>
    internal sealed class AcabadoRedondeado : IDisposable
    {
        private readonly Control control;
        private readonly Func<int> radio;
        private readonly Func<int> grosor;
        private readonly Func<Color> colorBorde;
        private readonly Func<bool> mostrarSombra;
        private readonly Func<int> tamañoSombra;
        private readonly Func<int> espesorSombra;
        private readonly Func<Color> colorSombra;
        private readonly Action<Graphics, Rectangle> pintarPadre;

        private Control padre;
        private Rectangle zonaAnterior;
        private Bitmap borde;
        private Bitmap fondoEsquinas;
        private byte[] datosEsquinas;
        private readonly List<PixelExterior> exterior = new List<PixelExterior>();
        private Size tamañoRegion;
        private int radioRegion = -1;
        private bool disposed;

        private struct PixelExterior
        {
            internal int X, Y, Alfa;
            internal PixelExterior(int x, int y, int alfa)
            { X = x; Y = y; Alfa = alfa; }
        }

        internal AcabadoRedondeado(
            Control control, Func<int> radio, Func<int> grosor,
            Func<Color> colorBorde, Func<bool> mostrarSombra,
            Func<int> tamañoSombra, Func<int> espesorSombra,
            Func<Color> colorSombra,
            Action<Graphics, Rectangle> pintarPadre)
        {
            this.control = control;
            this.radio = radio;
            this.grosor = grosor;
            this.colorBorde = colorBorde;
            this.mostrarSombra = mostrarSombra;
            this.tamañoSombra = tamañoSombra;
            this.espesorSombra = espesorSombra;
            this.colorSombra = colorSombra;
            this.pintarPadre = pintarPadre;

            control.ParentChanged += CambiarPadre;
            control.SizeChanged += CambiarGeometria;
            control.HandleCreated += CambiarGeometria;
            control.LocationChanged += CambiarPosicion;
            control.VisibleChanged += CambiarPosicion;
            CambiarPadre(control, EventArgs.Empty);
            ActualizarContorno();
        }

        internal void ActualizarContorno()
        {
            if (disposed || control.IsDisposed) return;
            LiberarImagenes();
            ActualizarRegion();
            ActualizarSombra();
            control.Invalidate();
        }

        internal void ActualizarSombra()
        {
            if (disposed) return;
            if (padre != null && !padre.IsDisposed)
            {
                Rectangle zona = control.Bounds;
                int alcance = mostrarSombra() ? tamañoSombra() + 4 : 2;
                zona.Inflate(alcance, alcance);
                padre.Invalidate(Rectangle.Union(zonaAnterior, zona));
                zonaAnterior = zona;
            }
            if (!control.IsDisposed) control.Invalidate();
        }

        private void CambiarGeometria(object sender, EventArgs e)
        { ActualizarContorno(); }

        private void CambiarPosicion(object sender, EventArgs e)
        { ActualizarSombra(); }

        private void CambiarFondoPadre(object sender, EventArgs e)
        { if (!control.IsDisposed) control.Invalidate(); }

        private void CambiarPadre(object sender, EventArgs e)
        {
            DesconectarPadre();
            padre = control.Parent;
            zonaAnterior = control.Bounds;
            if (padre != null)
            {
                padre.Paint += PintarSombra;
                padre.BackColorChanged += CambiarFondoPadre;
                padre.BackgroundImageChanged += CambiarFondoPadre;
            }
            ActualizarSombra();
        }

        private void DesconectarPadre()
        {
            if (padre == null) return;
            padre.Paint -= PintarSombra;
            padre.BackColorChanged -= CambiarFondoPadre;
            padre.BackgroundImageChanged -= CambiarFondoPadre;
            if (!padre.IsDisposed) padre.Invalidate(zonaAnterior);
            padre = null;
        }

        private void ActualizarRegion()
        {
            Size s = control.ClientSize;
            if (s.Width < 1 || s.Height < 1) return;
            if (s == tamañoRegion && radio() == radioRegion) return;
            tamañoRegion = s;
            radioRegion = radio();

            float r = Math.Min(radio(), Math.Min(s.Width, s.Height) / 2f);
            Region anterior = control.Region;
            if (r <= 0)
            {
                control.Region = null;
            }
            else
            {
                // Incluye cada pixel que intersecta la curva, para que Windows
                // no elimine las muestras suavizadas del borde. Las esquinas
                // completamente exteriores quedan realmente fuera del control.
                using (GraphicsPath ruta = new GraphicsPath(FillMode.Winding))
                {
                    int inicio = 0;
                    int izquierdaAnterior = -1;
                    for (int y = 0; y <= s.Height; y++)
                    {
                        int izquierda = -1;
                        if (y < s.Height)
                        {
                            double cercano = Math.Min(y + 1.0, s.Height - y);
                            double dy = Math.Max(0, r - cercano);
                            double limite = r - Math.Sqrt(Math.Max(0, r * r - dy * dy));
                            izquierda = Math.Max(0, (int)Math.Floor(limite));
                        }
                        if (izquierda == izquierdaAnterior) continue;
                        if (izquierdaAnterior >= 0)
                            ruta.AddRectangle(new Rectangle(izquierdaAnterior, inicio,
                                s.Width - izquierdaAnterior * 2, y - inicio));
                        inicio = y;
                        izquierdaAnterior = izquierda;
                    }
                    control.Region = new Region(ruta);
                }
            }
            if (anterior != null) anterior.Dispose();
        }

        internal void Pintar(Graphics graphics)
        {
            if (disposed || control.ClientSize.Width < 1 || control.ClientSize.Height < 1)
                return;
            if (borde == null) CrearAcabado();

            GraphicsState estado = graphics.Save();
            try
            {
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.DrawImageUnscaled(borde, 0, 0);
                if (exterior.Count == 0) return;

                // Recupera el fondo real del padre, incluida la sombra.
                // Nunca se tapa la esquina con un color plano supuesto.
                using (Graphics g = Graphics.FromImage(fondoEsquinas))
                {
                    g.Clear(SystemColors.Control);
                    pintarPadre(g, control.ClientRectangle);
                }

                BitmapData datos = fondoEsquinas.LockBits(
                    new Rectangle(Point.Empty, fondoEsquinas.Size),
                    ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
                try
                {
                    foreach (PixelExterior p in exterior)
                    {
                        int valor = Marshal.ReadInt32(
                            datos.Scan0, p.Y * datos.Stride + p.X * 4);
                        int destino = (p.Y * fondoEsquinas.Width + p.X) * 4;
                        datosEsquinas[destino] = (byte)(((valor & 255) * p.Alfa + 127) / 255);
                        datosEsquinas[destino + 1] = (byte)((((valor >> 8) & 255) * p.Alfa + 127) / 255);
                        datosEsquinas[destino + 2] = (byte)((((valor >> 16) & 255) * p.Alfa + 127) / 255);
                        datosEsquinas[destino + 3] = (byte)p.Alfa;
                    }
                    CopiarFilas(datosEsquinas, datos, fondoEsquinas.Width, fondoEsquinas.Height);
                }
                finally { fondoEsquinas.UnlockBits(datos); }

                graphics.DrawImageUnscaled(fondoEsquinas, 0, 0);
            }
            finally { graphics.Restore(estado); }
        }

        private void CrearAcabado()
        {
            int w = control.ClientSize.Width;
            int h = control.ClientSize.Height;
            double r = Math.Min(radio(), Math.Min(w, h) / 2.0);
            double t = Math.Min(grosor(), Math.Min(w, h) / 2.0);
            Color c = colorBorde();
            byte[] pixeles = new byte[checked(w * h * 4)];
            exterior.Clear();

            // 8 x 8 muestras solo cerca de los contornos. Se calcula al
            // cambiar tamaño/estilo, nunca en cada movimiento del raton.
            const int muestras = 8;
            const int total = muestras * muestras;
            int banda = (int)Math.Ceiling(Math.Max(r, t)) + 1;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (x >= banda && x < w - banda && y >= banda && y < h - banda)
                    { x = w - banda - 1; continue; }

                    int fuera = 0;
                    int linea = 0;
                    double d = GeometriaRedondeada.Distancia(x + 0.5, y + 0.5, w, h, r);
                    if (d > 0.71) fuera = total;
                    else if (d < -0.71 && d > -t + 0.71) linea = total;
                    else if (d >= -t - 0.71)
                    {
                        for (int sy = 0; sy < muestras; sy++)
                            for (int sx = 0; sx < muestras; sx++)
                            {
                                double distancia = GeometriaRedondeada.Distancia(
                                    x + (sx + 0.5) / muestras,
                                    y + (sy + 0.5) / muestras, w, h, r);
                                if (distancia > 0) fuera++;
                                else if (t > 0 && distancia >= -t) linea++;
                            }
                    }

                    if (fuera > 0)
                        exterior.Add(new PixelExterior(x, y, (fuera * 255 + total / 2) / total));

                    // Compensa la composicion posterior del fondo exterior.
                    // La suma final es: fondo*fuera + borde*linea + contenido*interior.
                    int dentro = total - fuera;
                    int alfa = dentro == 0 ? 0 : (linea * c.A + dentro / 2) / dentro;
                    int i = (y * w + x) * 4;
                    pixeles[i] = (byte)((c.B * alfa + 127) / 255);
                    pixeles[i + 1] = (byte)((c.G * alfa + 127) / 255);
                    pixeles[i + 2] = (byte)((c.R * alfa + 127) / 255);
                    pixeles[i + 3] = (byte)alfa;
                }
            }

            borde = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            BitmapData datos = borde.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            try { CopiarFilas(pixeles, datos, w, h); }
            finally { borde.UnlockBits(datos); }

            if (exterior.Count > 0)
            {
                fondoEsquinas = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
                datosEsquinas = new byte[pixeles.Length];
            }
        }

        private static void CopiarFilas(byte[] origen, BitmapData destino, int ancho, int alto)
        {
            int fila = ancho * 4;
            for (int y = 0; y < alto; y++)
                Marshal.Copy(origen, y * fila,
                    IntPtr.Add(destino.Scan0, y * destino.Stride), fila);
        }

        private void PintarSombra(object sender, PaintEventArgs e)
        {
            if (disposed || !mostrarSombra() || !control.Visible ||
                tamañoSombra() <= 0 || espesorSombra() <= 0 ||
                control.Width < 1 || control.Height < 1) return;

            int alcance = tamañoSombra();
            Color c = colorSombra();
            double opacidad = c.A / 255.0 * espesorSombra() / 100.0;
            if (opacidad <= 0) return;
            // Reparte la opacidad entre capas sin multiplicar su intensidad por el tamaño.
            int alfa = (int)Math.Round(255 *
                (1 - Math.Pow(1 - Math.Min(opacidad, 0.995), 1.0 / alcance)));
            if (alfa <= 0) return;

            float r = Math.Min(radio(), Math.Min(control.Width, control.Height) / 2f);
            GraphicsState estado = e.Graphics.Save();
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (SolidBrush brocha = new SolidBrush(Color.FromArgb(alfa, c.R, c.G, c.B)))
                    for (int i = alcance; i >= 1; i--)
                    {
                        RectangleF rect = control.Bounds;
                        rect.Offset(0, 2);
                        rect.Inflate(i, i);
                        using (GraphicsPath ruta = GeometriaRedondeada.CrearRuta(rect, r + i))
                            e.Graphics.FillPath(brocha, ruta);
                    }
            }
            finally { e.Graphics.Restore(estado); }
        }

        private void LiberarImagenes()
        {
            if (borde != null) { borde.Dispose(); borde = null; }
            if (fondoEsquinas != null) { fondoEsquinas.Dispose(); fondoEsquinas = null; }
            datosEsquinas = null;
            exterior.Clear();
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            control.ParentChanged -= CambiarPadre;
            control.SizeChanged -= CambiarGeometria;
            control.HandleCreated -= CambiarGeometria;
            control.LocationChanged -= CambiarPosicion;
            control.VisibleChanged -= CambiarPosicion;
            DesconectarPadre();
            LiberarImagenes();
        }
    }

    [ToolboxItem(true)]
    [DesignerCategory("Code")]
    public class PanelRedondeado : Panel
    {
        private int radioBorde = 14;
        private int grosorBorde = 1;
        private Color colorBorde = Color.FromArgb(218, 229, 241);
        private bool mostrarSombra;
        private int tamañoSombra = 8;
        private int espesorSombra = 35;
        private Color colorSombra = Color.Black;
        private readonly AcabadoRedondeado acabado;

        public PanelRedondeado()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BorderStyle = BorderStyle.None;
            BackColor = Color.White;
            acabado = new AcabadoRedondeado(this,
                () => RadioBorde, () => GrosorBorde, () => ColorBorde,
                () => MostrarSombra, () => TamañoSombra,
                () => EspesorSombra, () => ColorSombra, PintarFondoPadre);
        }

        [Category("Borde")]
        [Description("Radio exterior de las esquinas, en pixeles.")]
        [DefaultValue(14)]
        public int RadioBorde
        {
            get => radioBorde;
            set
            {
                int nuevo = Math.Max(0, value);
                if (radioBorde == nuevo) return;
                radioBorde = nuevo;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Borde")]
        [Description("Grosor del borde, en pixeles. Cero lo oculta.")]
        [DefaultValue(1)]
        public int GrosorBorde
        {
            get => grosorBorde;
            set
            {
                int nuevo = Math.Max(0, value);
                if (grosorBorde == nuevo) return;
                grosorBorde = nuevo;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Borde")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBorde
        {
            get => colorBorde;
            set
            {
                if (colorBorde == value) return;
                colorBorde = value;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Sombra")]
        [DefaultValue(false)]
        public bool MostrarSombra
        {
            get => mostrarSombra;
            set
            {
                if (mostrarSombra == value) return;
                mostrarSombra = value;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [Description("Extension exterior de la sombra, en pixeles (0 a 128).")]
        [DefaultValue(8)]
        public int TamañoSombra
        {
            get => tamañoSombra;
            set
            {
                int nuevo = Math.Max(0, Math.Min(128, value));
                if (tamañoSombra == nuevo) return;
                tamañoSombra = nuevo;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [Description("Intensidad de la sombra, de 0 a 100.")]
        [DefaultValue(35)]
        public int EspesorSombra
        {
            get => espesorSombra;
            set
            {
                int nuevo = Math.Max(0, Math.Min(100, value));
                if (espesorSombra == nuevo) return;
                espesorSombra = nuevo;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorSombra
        {
            get => colorSombra;
            set
            {
                if (colorSombra == value) return;
                colorSombra = value;
                acabado?.ActualizarSombra();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            acabado?.Pintar(e.Graphics);
        }

        private void PintarFondoPadre(Graphics g, Rectangle area)
        {
            Control padre = Parent;
            if (padre == null || padre.IsDisposed) return;
            GraphicsState estado = g.Save();
            try
            {
                g.TranslateTransform(-Left, -Top);
                area.Offset(Left, Top);
                using (PaintEventArgs args = new PaintEventArgs(g, area))
                {
                    InvokePaintBackground(padre, args);
                    InvokePaint(padre, args);
                }
            }
            finally { g.Restore(estado); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) acabado?.Dispose();
            base.Dispose(disposing);
        }

        // Conserva el helper del codigo original por compatibilidad.
        internal static GraphicsPath CrearRuta(Rectangle rectangulo, int radio)
        {
            return GeometriaRedondeada.CrearRuta(rectangulo, radio);
        }
    }

    [ToolboxItem(true)]
    [DesignerCategory("Code")]
    public class BotonRedondeado : Button
    {
        private int radioBorde = 10;
        private int grosorBorde = 1;
        private Color colorBorde = Color.FromArgb(13, 110, 238);
        private bool mostrarSombra;
        private int tamañoSombra = 6;
        private int espesorSombra = 35;
        private Color colorSombra = Color.Black;
        private readonly AcabadoRedondeado acabado;

        public BotonRedondeado()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            Cursor = Cursors.Hand;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseVisualStyleBackColor = false;
            acabado = new AcabadoRedondeado(this,
                () => RadioBorde, () => GrosorBorde, () => ColorBorde,
                () => MostrarSombra, () => TamañoSombra,
                () => EspesorSombra, () => ColorSombra, PintarFondoPadre);
        }

        [Category("Borde")]
        [Description("Radio exterior de las esquinas, en pixeles.")]
        [DefaultValue(10)]
        public int RadioBorde
        {
            get => radioBorde;
            set
            {
                int nuevo = Math.Max(0, value);
                if (radioBorde == nuevo) return;
                radioBorde = nuevo;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Borde")]
        [Description("Grosor del borde, en pixeles. Cero lo oculta.")]
        [DefaultValue(1)]
        public int GrosorBorde
        {
            get => grosorBorde;
            set
            {
                int nuevo = Math.Max(0, value);
                if (grosorBorde == nuevo) return;
                grosorBorde = nuevo;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Borde")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBorde
        {
            get => colorBorde;
            set
            {
                if (colorBorde == value) return;
                colorBorde = value;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Sombra")]
        [DefaultValue(false)]
        public bool MostrarSombra
        {
            get => mostrarSombra;
            set
            {
                if (mostrarSombra == value) return;
                mostrarSombra = value;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [Description("Extension exterior de la sombra, en pixeles (0 a 128).")]
        [DefaultValue(6)]
        public int TamañoSombra
        {
            get => tamañoSombra;
            set
            {
                int nuevo = Math.Max(0, Math.Min(128, value));
                if (tamañoSombra == nuevo) return;
                tamañoSombra = nuevo;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [Description("Intensidad de la sombra, de 0 a 100.")]
        [DefaultValue(35)]
        public int EspesorSombra
        {
            get => espesorSombra;
            set
            {
                int nuevo = Math.Max(0, Math.Min(100, value));
                if (espesorSombra == nuevo) return;
                espesorSombra = nuevo;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorSombra
        {
            get => colorSombra;
            set
            {
                if (colorSombra == value) return;
                colorSombra = value;
                acabado?.ActualizarSombra();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            acabado?.Pintar(e.Graphics);
        }

        private void PintarFondoPadre(Graphics g, Rectangle area)
        {
            Control padre = Parent;
            if (padre == null || padre.IsDisposed) return;
            GraphicsState estado = g.Save();
            try
            {
                g.TranslateTransform(-Left, -Top);
                area.Offset(Left, Top);
                using (PaintEventArgs args = new PaintEventArgs(g, area))
                {
                    InvokePaintBackground(padre, args);
                    InvokePaint(padre, args);
                }
            }
            finally { g.Restore(estado); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) acabado?.Dispose();
            base.Dispose(disposing);
        }
    }

    [ToolboxItem(true)]
    [DesignerCategory("Code")]
    public class TableLayoutRedondeado : TableLayoutPanel
    {
        private int radioBorde = 14;
        private int grosorBorde = 1;
        private Color colorBorde = Color.FromArgb(218, 229, 241);
        private bool mostrarSombra;
        private int tamañoSombra = 8;
        private int espesorSombra = 35;
        private Color colorSombra = Color.Black;
        private readonly AcabadoRedondeado acabado;

        public TableLayoutRedondeado()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BorderStyle = BorderStyle.None;
            BackColor = Color.White;
            acabado = new AcabadoRedondeado(this,
                () => RadioBorde, () => GrosorBorde, () => ColorBorde,
                () => MostrarSombra, () => TamañoSombra,
                () => EspesorSombra, () => ColorSombra, PintarFondoPadre);
        }

        [Category("Borde")]
        [Description("Radio exterior de las esquinas, en pixeles.")]
        [DefaultValue(14)]
        public int RadioBorde
        {
            get => radioBorde;
            set
            {
                int nuevo = Math.Max(0, value);
                if (radioBorde == nuevo) return;
                radioBorde = nuevo;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Borde")]
        [Description("Grosor del borde, en pixeles. Cero lo oculta.")]
        [DefaultValue(1)]
        public int GrosorBorde
        {
            get => grosorBorde;
            set
            {
                int nuevo = Math.Max(0, value);
                if (grosorBorde == nuevo) return;
                grosorBorde = nuevo;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Borde")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorBorde
        {
            get => colorBorde;
            set
            {
                if (colorBorde == value) return;
                colorBorde = value;
                acabado?.ActualizarContorno();
            }
        }

        [Category("Sombra")]
        [DefaultValue(false)]
        public bool MostrarSombra
        {
            get => mostrarSombra;
            set
            {
                if (mostrarSombra == value) return;
                mostrarSombra = value;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [Description("Extension exterior de la sombra, en pixeles (0 a 128).")]
        [DefaultValue(8)]
        public int TamañoSombra
        {
            get => tamañoSombra;
            set
            {
                int nuevo = Math.Max(0, Math.Min(128, value));
                if (tamañoSombra == nuevo) return;
                tamañoSombra = nuevo;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [Description("Intensidad de la sombra, de 0 a 100.")]
        [DefaultValue(35)]
        public int EspesorSombra
        {
            get => espesorSombra;
            set
            {
                int nuevo = Math.Max(0, Math.Min(100, value));
                if (espesorSombra == nuevo) return;
                espesorSombra = nuevo;
                acabado?.ActualizarSombra();
            }
        }

        [Category("Sombra")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorSombra
        {
            get => colorSombra;
            set
            {
                if (colorSombra == value) return;
                colorSombra = value;
                acabado?.ActualizarSombra();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            acabado?.Pintar(e.Graphics);
        }

        private void PintarFondoPadre(Graphics g, Rectangle area)
        {
            Control padre = Parent;
            if (padre == null || padre.IsDisposed) return;
            GraphicsState estado = g.Save();
            try
            {
                g.TranslateTransform(-Left, -Top);
                area.Offset(Left, Top);
                using (PaintEventArgs args = new PaintEventArgs(g, area))
                {
                    InvokePaintBackground(padre, args);
                    InvokePaint(padre, args);
                }
            }
            finally { g.Restore(estado); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) acabado?.Dispose();
            base.Dispose(disposing);
        }
    }
}