using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace Indicadores_Escoria
{
    /// <summary>
    /// Lógica de la pantalla Equipos.
    /// La posición y apariencia de los controles permanece en
    /// equipos.Designer.cs para que pueda editarse desde la vista Diseño.
    /// </summary>
    public partial class equipos : Form
    {
        // Referencia a la tabla cargada por equiposBD. Se utiliza para buscar
        // por ECO sin volver a consultar SQL Server después de cada tecla.
        private DataTable? datosEquipos;

        public equipos()
        {
            InitializeComponent();

            // Reduce el parpadeo de la tabla al cargar o filtrar registros.
            typeof(DataGridView)
                .GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(tablaEquipos, true, null);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();


            // El ECO del formulario y el buscador sólo aceptan números.
            txtEco.KeyPress += SoloNumeros_KeyPress;
        }



        /// <summary>
        /// Carga la información cuando se abre el formulario.
        /// </summary>
        private void equipos_Load(object? sender, EventArgs e)
        {
            txtEco.MaxLength = 10;
            actualizarTabla();
            limpiarCampos();
            buscarEco.Focus();
            CargarKPIs();
        }

        /// <summary>
        /// Solicita a equiposBD los registros actuales y prepara el origen
        /// para que el buscador pueda filtrar la tabla localmente.
        /// </summary>
        private void actualizarTabla()
        {
            try
            {
                equiposBD datos = new equiposBD();
                datos.cargarequipos(tablaEquipos);

                PrepararOrigenBusqueda();
                AplicarFiltroEco();
                tablaEquipos.ClearSelection();
            }
            catch (Exception ex)
            {
                datosEquipos = null;
                tablaEquipos.DataSource = null;

                MessageBox.Show(
                    "No se pudieron cargar los equipos.\n\n" + ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Localiza el DataTable colocado por equiposBD como DataSource.
        /// También acepta un DataView o un BindingSource.
        /// </summary>
        private void PrepararOrigenBusqueda()
        {
            datosEquipos = tablaEquipos.DataSource switch
            {
                DataTable tabla => tabla,
                DataView vista => vista.Table,
                BindingSource origen when origen.DataSource is DataTable tabla => tabla,
                BindingSource origen when origen.DataSource is DataView vista => vista.Table,
                BindingSource origen when origen.List is DataView vista => vista.Table,
                _ => null
            };
        }

        /// <summary>
        /// Guarda un equipo nuevo después de validar todos los campos.
        /// </summary>
        private void Guardar_Click(object? sender, EventArgs e)
        {
            if (txtEco.ReadOnly)
            {
                MessageBox.Show(
                    "Presiona Limpiar antes de registrar un equipo nuevo.",
                    "Equipo seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!ValidarCampos(out _))
            {
                return;
            }

            try
            {
                equiposBD equipo = CrearEquipoDesdeFormulario();
                int filas = equipo.agregarequipo();

                if (filas <= 0)
                {
                    MessageBox.Show(
                        "Este equipo ya existe o no pudo guardarse.",
                        "Equipo no guardado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    "El equipo se guardó correctamente.",
                    "Equipo guardado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                actualizarTabla();
                limpiarCampos();
                CargarKPIs();
            }
            catch (Exception ex)
            {
                MostrarErrorOperacion("guardar", ex);
            }
        }

        /// <summary>
        /// Carga la fila seleccionada dentro del formulario de la derecha.
        /// El ECO queda bloqueado porque es la llave primaria.
        /// </summary>
        private void tablaEquipos_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.RowIndex >= tablaEquipos.Rows.Count)
            {
                return;
            }

            DataGridViewRow fila = tablaEquipos.Rows[e.RowIndex];

            txtEco.Text = ObtenerTexto(fila, "Eco");
            txtserie.Text = ObtenerTexto(fila, "Serie");
            txttipoequipo.Text = ObtenerTexto(fila, "Equipo");
            txtmodelo.Text = ObtenerTexto(fila, "Modelo");
            txtproyecto.Text = ObtenerTexto(fila, "Proyecto");
            txtpropiedad.Text = ObtenerTexto(fila, "Propiedad");
            txtcliente.Text = ObtenerTexto(fila, "Cliente");

            txtEco.ReadOnly = true;
            txtEco.BackColor = System.Drawing.Color.FromArgb(240, 245, 250);
            btneditar.Enabled = true;
            btneliminar.Enabled = true;
            Guardar.Enabled = false;
        }

        /// <summary>
        /// Actualiza el equipo seleccionado. El ECO no se modifica porque es
        /// la llave primaria utilizada también por horómetros.
        /// </summary>
        private void btneditar_Click(object? sender, EventArgs e)
        {
            if (!txtEco.ReadOnly)
            {
                MessageBox.Show(
                    "Selecciona primero un equipo de la tabla.",
                    "Sin selección",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!ValidarCampos(out _))
            {
                return;
            }

            try
            {
                equiposBD equipo = CrearEquipoDesdeFormulario();
                int filas = equipo.editarEquipo();

                if (filas <= 0)
                {
                    MessageBox.Show(
                        "No se encontró el equipo o no hubo cambios para guardar.",
                        "Equipo no actualizado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    "El equipo se actualizó correctamente.",
                    "Equipo actualizado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                actualizarTabla();
                limpiarCampos();
                CargarKPIs();
            }
            catch (Exception ex)
            {
                MostrarErrorOperacion("editar", ex);
            }
        }

        /// <summary>
        /// Elimina el equipo seleccionado después de solicitar confirmación.
        /// </summary>
        private void btneliminar_Click(object? sender, EventArgs e)
        {
            if (!txtEco.ReadOnly ||
                !int.TryParse(txtEco.Text.Trim(), out int eco))
            {
                MessageBox.Show(
                    "Selecciona primero un equipo de la tabla.",
                    "Sin selección",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"¿Deseas eliminar el equipo con ECO {eco}?\n\n" +
                "La operación no puede deshacerse.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                equiposBD equipo = new equiposBD(eco);
                int filas = equipo.eliminarEquipo();

                if (filas <= 0)
                {
                    MessageBox.Show(
                        "No se encontró el equipo que intentas eliminar.",
                        "Equipo no eliminado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    "El equipo se eliminó correctamente.",
                    "Equipo eliminado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                actualizarTabla();
                limpiarCampos();
                CargarKPIs();
            }
            catch (Exception ex)
            {
                MostrarErrorOperacion("eliminar", ex);
            }
        }

        /// <summary>
        /// Limpia la captura y regresa la pantalla al modo de alta.
        /// No borra el buscador para que el usuario no pierda su filtro.
        /// </summary>

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            txtEco.Focus();
        }
        private void limpiarCampos()
        {
            txtEco.Clear();
            txtserie.Clear();
            txtmodelo.Clear();
            txtproyecto.Clear();
            txtcliente.Clear();
            txttipoequipo.Clear();
            txtpropiedad.Clear();
            txtEco.ReadOnly = false;
            txtEco.BackColor = System.Drawing.Color.White;
            Guardar.Enabled = true;
            btneditar.Enabled = false;
            btneliminar.Enabled = false;
            tablaEquipos.ClearSelection();
        }

        /// <summary>
        /// Filtra mientras el usuario escribe el ECO.
        /// </summary>
        private void buscarEco_TextChanged(object? sender, EventArgs e)
        {
            string texto = buscarEco.Text.Trim();

            // Filtra la tabla
            if (datosEquipos != null)
            {
                if (string.IsNullOrEmpty(texto))
                {
                    datosEquipos.DefaultView.RowFilter = "";
                    limpiarCampos();
                    return;
                }

                if (int.TryParse(texto, out int ecoBuscado))
                {
                    datosEquipos.DefaultView.RowFilter = $"Eco = {ecoBuscado}";
                }
            }

            // Llena los campos
            foreach (DataGridViewRow fila in tablaEquipos.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string eco = fila.Cells["Eco"].Value?.ToString() ?? "";

                if (eco == texto)
                {
                    txtEco.Text = fila.Cells["Eco"].Value?.ToString();
                    txtserie.Text = fila.Cells["Serie"].Value?.ToString();
                    txttipoequipo.Text = fila.Cells["Equipo"].Value?.ToString();
                    txtmodelo.Text = fila.Cells["Modelo"].Value?.ToString();
                    txtproyecto.Text = fila.Cells["Proyecto"].Value?.ToString();
                    txtpropiedad.Text = fila.Cells["Propiedad"].Value?.ToString();
                    txtcliente.Text = fila.Cells["Cliente"].Value?.ToString();

                    txtEco.ReadOnly = true;
                    txtEco.BackColor = Color.FromArgb(240, 245, 250);

                    btneditar.Enabled = true;
                    btneliminar.Enabled = true;
                    Guardar.Enabled = false;

                    fila.Selected = true;
                    tablaEquipos.CurrentCell = fila.Cells[0];

                    break;
                }
            }
        }

        private void AplicarFiltroEco()
        {
            string texto = buscarEco.Text.Trim();
            string filtro = texto.Length == 0
                ? string.Empty
                : $"CONVERT([Eco], 'System.String') LIKE '%{EscaparFiltro(texto)}%'";

            try
            {
                if (tablaEquipos.DataSource is BindingSource origen &&
                    origen.SupportsFiltering)
                {
                    origen.Filter = filtro;
                }
                else if (datosEquipos != null)
                {
                    datosEquipos.DefaultView.RowFilter = filtro;
                }
                else
                {
                    FiltrarFilasSinDataTable(texto);
                }

                tablaEquipos.ClearSelection();
            }
            catch (EvaluateException)
            {
                // Si el proveedor usa una columna con otro tipo, no bloquea la
                // pantalla; se aplica el filtro directamente sobre las filas.
                FiltrarFilasSinDataTable(texto);
            }
        }

        private void FiltrarFilasSinDataTable(string texto)
        {
            foreach (DataGridViewRow fila in tablaEquipos.Rows)
            {
                if (fila.IsNewRow)
                {
                    continue;
                }

                string eco = ObtenerTexto(fila, "colEco");
                bool coincide = texto.Length == 0 ||
                    eco.Contains(texto, StringComparison.OrdinalIgnoreCase);

                // Esta ruta se usa sólo cuando la tabla no tiene DataTable.
                // En una tabla enlazada, DataView.RowFilter es la ruta normal.
                try
                {
                    fila.Visible = coincide;
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
        }

        private void buscarEco_KeyPress(object? sender, KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private static void SoloNumeros_KeyPress(
            object? sender,
            KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool ValidarCampos(out int eco)
        {
            eco = 0;

            if (string.IsNullOrWhiteSpace(txtEco.Text))
            {
                MostrarCampoObligatorio(txtEco, "ECO");
                return false;
            }

            if (!int.TryParse(txtEco.Text.Trim(), out eco) || eco <= 0)
            {
                MessageBox.Show(
                    "El ECO debe ser un número entero mayor que cero.",
                    "ECO inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtEco.Focus();
                txtEco.SelectAll();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtserie.Text))
            {
                MostrarCampoObligatorio(txtserie, "Serie");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txttipoequipo.Text))
            {
                MostrarCampoObligatorio(txttipoequipo, "Equipo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtmodelo.Text))
            {
                MostrarCampoObligatorio(txtmodelo, "Modelo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtproyecto.Text))
            {
                MostrarCampoObligatorio(txtproyecto, "Proyecto");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtpropiedad.Text))
            {
                MostrarCampoObligatorio(txtpropiedad, "Propiedad");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtcliente.Text))
            {
                MostrarCampoObligatorio(txtcliente, "Cliente");
                return false;
            }

            return true;
        }

        private equiposBD CrearEquipoDesdeFormulario()
        {
            return new equiposBD(
                txtEco.Text.Trim(),
                txtserie.Text.Trim(),
                txttipoequipo.Text.Trim(),
                txtmodelo.Text.Trim(),
                txtproyecto.Text.Trim(),
                txtpropiedad.Text.Trim(),
                txtcliente.Text.Trim());
        }

        private static string ObtenerTexto(
            DataGridViewRow fila,
            string nombreColumna)
        {
            object? valor = fila.Cells[nombreColumna].Value;
            return Convert.ToString(valor)?.Trim() ?? string.Empty;
        }

        private static string EscaparFiltro(string valor)
        {
            return valor
                .Replace("'", "''")
                .Replace("[", "[[]")
                .Replace("]", "[]]")
                .Replace("%", "[%]")
                .Replace("*", "[*]");
        }

        private static void MostrarCampoObligatorio(
            Control control,
            string nombre)
        {
            MessageBox.Show(
                $"El campo {nombre} es obligatorio.",
                "Datos incompletos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            control.Focus();
        }

        private static void MostrarErrorOperacion(
            string operacion,
            Exception ex)
        {
            string mensaje = EsErrorDuplicado(ex)
                ? "Este equipo ya existe. Verifica el ECO y la serie."
                : $"No se pudo {operacion} el equipo.\n\n{ex.Message}";

            MessageBox.Show(
                mensaje,
                "Error de base de datos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static bool EsErrorDuplicado(Exception ex)
        {
            string mensaje = ex.ToString();

            return mensaje.Contains("PRIMARY KEY", StringComparison.OrdinalIgnoreCase) ||
                   mensaje.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase) ||
                   mensaje.Contains("duplicate", StringComparison.OrdinalIgnoreCase) ||
                   mensaje.Contains("duplicad", StringComparison.OrdinalIgnoreCase) ||
                   mensaje.Contains("ya existe", StringComparison.OrdinalIgnoreCase);
        }
        private void CargarKPIs()
        {
            equiposBD bd = new equiposBD();

            var kpis = bd.ObtenerKPIs();

            lblTotalEquipos.Text = kpis.totalEquipos.ToString();
            lblTotalModelos.Text = kpis.totalModelos.ToString();

            lblActivos.Text = bd.ObtenerEquiposActivos().ToString();
            lblInactivos.Text = bd.ObtenerEquiposInactivos().ToString();
        }

        private void panelRedondeado5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void tablaEquipos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
