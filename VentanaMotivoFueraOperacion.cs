using System;
using System.Drawing;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    /// <summary>
    /// Ventana modal para capturar el motivo de un equipo fuera de operación.
    /// Se construye por código para no necesitar otro archivo Designer.
    /// </summary>
    internal sealed class VentanaMotivoFueraOperacion : Form
    {
        private readonly TextBox txtDescripcion;
        private readonly Label lblContador;

        public string Descripcion { get; private set; } = "";

        public VentanaMotivoFueraOperacion(string descripcionActual)
        {
            Text = "Motivo de fuera de operación";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(540, 300);
            BackColor = Color.White;

            Label lblTitulo = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                Location = new Point(24, 22),
                Text = "¿Por qué está fuera de operación?"
            };

            Label lblAyuda = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 9.5F),
                ForeColor = Color.FromArgb(95, 100, 105),
                Location = new Point(25, 55),
                Text = "La descripción es obligatoria (máximo 200 caracteres)."
            };

            txtDescripcion = new TextBox
            {
                Font = new Font("Arial", 11F),
                Location = new Point(28, 86),
                Size = new Size(484, 120),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 200,
                Text = descripcionActual ?? ""
            };

            lblContador = new Label
            {
                AutoSize = false,
                Font = new Font("Arial", 9F),
                ForeColor = Color.FromArgb(105, 110, 115),
                Location = new Point(385, 212),
                Size = new Size(127, 20),
                TextAlign = ContentAlignment.MiddleRight
            };

            Button btnCancelar = new Button
            {
                Text = "Cancelar",
                Font = new Font("Arial", 10F),
                Location = new Point(300, 249),
                Size = new Size(100, 34),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(55, 60, 65),
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel,
                Cursor = Cursors.Hand
            };

            btnCancelar.FlatAppearance.BorderColor =
                Color.FromArgb(190, 195, 200);

            Button btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Arial", 10F, FontStyle.Bold),
                Location = new Point(412, 249),
                Size = new Size(100, 34),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btnGuardar.FlatAppearance.BorderSize = 0;

            txtDescripcion.TextChanged += txtDescripcion_TextChanged;
            btnGuardar.Click += btnGuardar_Click;

            Controls.Add(lblTitulo);
            Controls.Add(lblAyuda);
            Controls.Add(txtDescripcion);
            Controls.Add(lblContador);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;

            ActualizarContador();

            Shown += (_, _) =>
            {
                txtDescripcion.Focus();
                txtDescripcion.SelectionStart = txtDescripcion.TextLength;
            };
        }

        private void txtDescripcion_TextChanged(object? sender, EventArgs e)
        {
            ActualizarContador();
        }

        private void ActualizarContador()
        {
            lblContador.Text = $"{txtDescripcion.TextLength}/200";
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text.Trim();

            if (descripcion == "")
            {
                MessageBox.Show(
                    "Escriba el motivo por el que el equipo está fuera de " +
                    "operación.",
                    "Descripción requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtDescripcion.Focus();
                return;
            }

            Descripcion = descripcion;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
