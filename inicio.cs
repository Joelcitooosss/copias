using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Color = System.Drawing.Color;

namespace Indicadores_Escoria
{
    public partial class inicio : Form
    {

        private IconButton currenBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public inicio()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            ActivarButton(btninicio, RGBColors.color1);
        }
        private struct RGBColors
        {

            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        private void ActivarButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                desactivarboton();
                //botonselecciionado
                currenBtn = (IconButton)senderBtn;
                // Cambia el color de fondo del botón seleccionado
                currenBtn.BackColor = Color.FromArgb(37, 36, 81);
                currenBtn.Location = new Point(currenBtn.Location.X, currenBtn.Location.Y);
                currenBtn.TextAlign = ContentAlignment.MiddleCenter;
                currenBtn.IconColor = color;
                currenBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currenBtn.ImageAlign = ContentAlignment.MiddleRight;

            }
        }
        private void desactivarboton()
        {
            if (currenBtn != null)
            {
                currenBtn.BackColor = Color.FromArgb(6, 26, 54);
                currenBtn.ForeColor = Color.White;
                // Cambia la alineación del texto y el color del icono a sus valores predeterminados
                currenBtn.TextAlign = ContentAlignment.MiddleLeft;
                currenBtn.IconColor = Color.Gainsboro;
                currenBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currenBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void abrirsegundopanel(Form childform)
        {
            if (currentChildForm != null)
            {
                //solo se abre un formulario a la vez
                currentChildForm.Close();
            }
            currentChildForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            panelescritorio.Controls.Add(childform);
            panelescritorio.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
        private void VolverAInicio()
        {
            // Cierra el formulario cargado, si existe.
            if (currentChildForm != null && !currentChildForm.IsDisposed)
            {
                currentChildForm.Close();
            }

            currentChildForm = null;
            panelescritorio.Tag = null;

            // Apaga el botón anterior y selecciona Inicio.
            ActivarButton(btninicio, RGBColors.color1);
            abrirsegundopanel(new indicadoresKPI());
        }

        private void nuevapestaña_Click(object sender, EventArgs e)
        {

        }

        private void Inicio_Click(object sender, EventArgs e)
        {
            VolverAInicio();
        }

        private void inicio_Load(object sender, EventArgs e)
        {
            abrirsegundopanel(new indicadoresKPI());
        }

        private void panelescritorio_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void nuevapestaña_Click_1(object sender, EventArgs e)
        {
            // Crea una instancia de la ventana de equipos.
            inicio ventana = new inicio();

            // La muestra como una ventana independiente.
            ventana.Show();
        }

        private void btnequipos_Click(object sender, EventArgs e)
        {
            ActivarButton(sender, RGBColors.color5);
            abrirsegundopanel(new equipos());
        }



        private void btnreporte_Click(object sender, EventArgs e)
        {
            ActivarButton(sender, RGBColors.color3);
            abrirsegundopanel(new reporte());
        }

        private void btnbitacora_Click(object sender, EventArgs e)
        {
            ActivarButton(sender, RGBColors.color2);
        }

        private void btnhorometros_Click_1(object sender, EventArgs e)
        {
            ActivarButton(sender, RGBColors.color4);
            abrirsegundopanel(new horometros());
        }

        private void Mantenimientos_Click(object sender, EventArgs e)
        {
            ActivarButton(sender, RGBColors.color4);
            abrirsegundopanel(new mantenimientos());
        }

        private void graficas_Click(object sender, EventArgs e)
        {
            abrirsegundopanel(new graficasKPI());
        }
    }
}
