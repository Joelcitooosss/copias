using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using OfficeOpenXml;
using System.IO;

namespace Indicadores_Escoria
{
    public partial class horometros : Form
    {
        private bool preparando = true;

        // Conserva la última celda de fecha elegida aunque después se presione
        // uno de los botones de estado.
        private DataGridViewCell? celdaHorometroSeleccionada;

        // Guarda en memoria las observaciones del mes para mostrarlas sin
        // consultar SQL Server cada vez que se cambia de celda.
        private readonly Dictionary<(int Eco, DateTime Fecha), string>
            observacionesPorCelda = new();

        // Evita que cargar texto desde la base de datos dispare un guardado.
        private bool cargandoMotivo;

        public horometros()
        {
            InitializeComponent();

            // Elimina el parpadeo al hacer scroll
            typeof(DataGridView)
                .GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(tabla_horometros, true, null);



            ConfigurarEventosTabla();
            ConfigurarBotonesEstado();
            ConfigurarEventosMotivo();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void horometros_Load(object sender, EventArgs e)
        {
            preparando = true;

            cmbmes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbanio.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbmes.Sorted = false;
            cmbanio.Sorted = false;

            cmbmes.DataSource = null;
            cmbanio.DataSource = null;

            cmbmes.Items.Clear();
            cmbanio.Items.Clear();

            cmbmes.Items.AddRange(new object[]
            {
                "Enero", "Febrero", "Marzo", "Abril",
                "Mayo", "Junio", "Julio", "Agosto",
                "Septiembre", "Octubre", "Noviembre", "Diciembre"
            });

            for (int anio = 2026;
                 anio <= Math.Max(2026, DateTime.Today.Year + 1);
                 anio++)
            {
                cmbanio.Items.Add(anio);
            }

            // Inicia con el período de los datos de tu captura.
            cmbmes.SelectedIndex = 8;
            cmbanio.SelectedItem = 2026;

            cmbmes.SelectedIndexChanged -= CambiarPeriodo;
            cmbanio.SelectedIndexChanged -= CambiarPeriodo;

            cmbmes.SelectedIndexChanged += CambiarPeriodo;
            cmbanio.SelectedIndexChanged += CambiarPeriodo;

            preparando = false;

            CargarTabla();

        }
        private void CambiarPeriodo(object sender, EventArgs e)
        {
            if (!preparando)
            {
                CargarTabla();
            }
        }

        private void CargarTabla()
        {
            if (cmbmes.SelectedIndex < 0 ||
                cmbanio.SelectedItem == null)
            {
                return;
            }

            try
            {
                int mes = cmbmes.SelectedIndex + 1;
                int anio = Convert.ToInt32(cmbanio.SelectedItem);

                DataTable datos =
                    horometrosBD.ObtenerHorometros(anio, mes);

                observacionesPorCelda.Clear();

                foreach (KeyValuePair<(int Eco, DateTime Fecha), string> dato
                         in HorometrosGuardadoBD.ObtenerObservacionesMes(anio, mes))
                {
                    observacionesPorCelda[dato.Key] = dato.Value;
                }

                PrepararTabla(anio, mes);

                Dictionary<int, DataGridViewRow> filas =
                    new Dictionary<int, DataGridViewRow>();

                foreach (DataRow registro in datos.Rows)
                {
                    int eco = Convert.ToInt32(registro["Eco"]);

                    if (!filas.ContainsKey(eco))
                    {
                        int indice = tabla_horometros.Rows.Add();

                        DataGridViewRow fila =
                            tabla_horometros.Rows[indice];

                        fila.Cells["Serie"].Value = registro["serie"];
                        fila.Cells["Equipo"].Value = registro["tipo_equipo"];
                        fila.Cells["Modelo"].Value = registro["modelo"];
                        fila.Cells["Eco"].Value = eco;

                        filas.Add(eco, fila);
                    }

                    if (registro.IsNull("fecha_lectura"))
                    {
                        continue;
                    }

                    DateTime fecha =
                        Convert.ToDateTime(registro["fecha_lectura"]);

                    DataGridViewCell celda =
                        filas[eco].Cells["Dia" + fecha.Day];

                    celda.Value =
                        Convert.ToDecimal(registro["valor"]);

                    // Acepta tanto el alias usado por tu consulta actual
                    // como el nombre real de la columna de SQL Server.
                    int estatus = ObtenerEstatus(registro);

                    // El Tag guarda el estado de esta celda para iluminar el
                    // botón correcto cuando posteriormente sea seleccionada.
                    celda.Tag = estatus;
                    AplicarColorEstado(celda, estatus);

                    observacionesPorCelda.TryGetValue(
                        (eco, fecha.Date),
                        out string? observacion
                    );

                    celda.ToolTipText =
                        $"Eco: {eco}\n" +
                        $"Fecha: {fecha:dd/MM/yyyy}\n" +
                        $"Estado: {ObtenerNombreEstado(estatus)}" +
                        (string.IsNullOrWhiteSpace(observacion)
                            ? ""
                            : $"\nObservación: {observacion}");
                }

                tabla_horometros.ClearSelection();
                tabla_horometros.CurrentCell = null;
                celdaHorometroSeleccionada = null;
                MostrarBotonActivo(0);
                LimpiarMotivo();
            }
            catch (Exception ex)
            {
                tabla_horometros.Rows.Clear();
                observacionesPorCelda.Clear();
                celdaHorometroSeleccionada = null;
                MostrarBotonActivo(0);
                LimpiarMotivo();

                MessageBox.Show(
                    "No se pudo cargar la tabla:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void PrepararTabla(int anio, int mes)
        {
            tabla_horometros.DataSource = null;
            tabla_horometros.Rows.Clear();
            tabla_horometros.Columns.Clear();

            tabla_horometros.AutoGenerateColumns = false;
            tabla_horometros.Rows.Clear();
            tabla_horometros.Columns.Clear();

            AgregarColumna("Serie", "Serie", 195);
            AgregarColumna("Equipo", "Equipo", 180);
            AgregarColumna("Modelo", "Modelo", 150);
            AgregarColumna("Eco", "No Eco.", 85);

            tabla_horometros.Columns["Eco"].ValueType = typeof(int);

            int dias = DateTime.DaysInMonth(anio, mes);

            for (int dia = 1; dia <= dias; dia++)
            {
                DateTime fecha = new DateTime(anio, mes, dia);

                DataGridViewTextBoxColumn columna =
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Dia" + dia,
                        HeaderText = fecha.ToString("dd/MM/yyyy"),
                        Width = 115,
                        ValueType = typeof(string),
                        ReadOnly = false,
                        Tag = fecha,
                        MaxInputLength = 11,
                        SortMode = DataGridViewColumnSortMode.NotSortable
                    };

                tabla_horometros.Columns.Add(columna);
            }
        }
        private void AgregarColumna(
            string nombre,
            string titulo,
            int ancho)
        {
            tabla_horometros.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = nombre,
                    HeaderText = titulo,
                    Width = ancho,
                    Frozen = true,
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                }
            );
        }

        // ---------------------------------------------------------------
        // EDICIÓN Y SELECCIÓN DE CELDAS
        // ---------------------------------------------------------------

        private void ConfigurarEventosTabla()
        {
            tabla_horometros.CellEnter -= tabla_horometros_CellEnter;
            tabla_horometros.CellEnter += tabla_horometros_CellEnter;

            tabla_horometros.CellValidating -= tabla_horometros_CellValidating;
            tabla_horometros.CellValidating += tabla_horometros_CellValidating;

            tabla_horometros.DataError -= tabla_horometros_DataError;
            tabla_horometros.DataError += tabla_horometros_DataError;
        }

        private void tabla_horometros_CellEnter(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !EsColumnaDeFecha(e.ColumnIndex))
            {
                celdaHorometroSeleccionada = null;
                MostrarBotonActivo(0);
                LimpiarMotivo();
                return;
            }

            celdaHorometroSeleccionada =
                tabla_horometros.Rows[e.RowIndex].Cells[e.ColumnIndex];

            int estatus = celdaHorometroSeleccionada.Tag is int id
                ? id
                : 0;

            MostrarBotonActivo(estatus);
            MostrarMotivoDeCelda(celdaHorometroSeleccionada);
        }

        private void tabla_horometros_CellValidating(
            object? sender,
            DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || !EsColumnaDeFecha(e.ColumnIndex))
            {
                return;
            }

            string texto = e.FormattedValue?.ToString()?.Trim() ?? "";

            // Se permite dejar la celda vacía mientras se está capturando.
            // El botón de estado no permitirá guardarla así.
            if (texto == "")
            {
                return;
            }

            if (!IntentarConvertirValor(texto, out decimal valor) ||
                valor < 0 ||
                valor > 99999999.99m ||
                decimal.Round(valor, 2) != valor)
            {
                e.Cancel = true;

                MessageBox.Show(
                    "Escriba un horómetro válido, positivo y con máximo " +
                    "dos decimales.",
                    "Valor no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void tabla_horometros_DataError(
            object? sender,
            DataGridViewDataErrorEventArgs e)
        {
            // Evita la excepción visual del DataGridView cuando el texto no
            // puede convertirse a decimal. CellValidating muestra el aviso.
            e.ThrowException = false;
        }

        private bool EsColumnaDeFecha(int indiceColumna)
        {
            return indiceColumna >= 0 &&
                   indiceColumna < tabla_horometros.Columns.Count &&
                   tabla_horometros.Columns[indiceColumna].Tag is DateTime;
        }

        private static bool IntentarConvertirValor(
            object? contenido,
            out decimal valor)
        {
            if (contenido is decimal valorDecimal)
            {
                valor = valorDecimal;
                return true;
            }

            string texto = contenido?.ToString()?.Trim() ?? "";

            // Permite escribir el decimal con punto o con coma, pero evita
            // interpretar accidentalmente la coma como separador de miles.
            texto = texto.Replace(',', '.');

            return decimal.TryParse(
                texto,
                NumberStyles.AllowLeadingSign |
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out valor
            );
        }

        // ---------------------------------------------------------------
        // CUADRO DE OBSERVACIÓN
        // ---------------------------------------------------------------

        private void ConfigurarEventosMotivo()
        {
            motivo.Leave -= motivo_Leave;
            motivo.Leave += motivo_Leave;

            motivo.TextChanged -= motivo_TextChanged;
            motivo.TextChanged += motivo_TextChanged;
        }

        private void motivo_TextChanged(object? sender, EventArgs e)
        {
            if (!cargandoMotivo)
            {
                motivo.BackColor = Color.White;
            }
        }

        private void MostrarMotivoDeCelda(DataGridViewCell celda)
        {
            if (!IntentarObtenerClaveCelda(celda, out int eco, out DateTime fecha))
            {
                LimpiarMotivo();
                return;
            }

            observacionesPorCelda.TryGetValue(
                (eco, fecha.Date),
                out string? observacion
            );

            cargandoMotivo = true;

            try
            {
                motivo.Enabled = true;
                motivo.BackColor = Color.White;
                motivo.Text = observacion ?? "";
            }
            finally
            {
                cargandoMotivo = false;
            }
        }

        private void LimpiarMotivo()
        {
            cargandoMotivo = true;

            try
            {
                motivo.Clear();
                motivo.Enabled = false;
                motivo.BackColor = Color.FromArgb(242, 244, 246);
            }
            finally
            {
                cargandoMotivo = false;
            }
        }

        private void motivo_Leave(object? sender, EventArgs e)
        {
            if (cargandoMotivo || IsDisposed || Disposing)
            {
                return;
            }

            GuardarMotivoDeCelda(celdaHorometroSeleccionada);
        }

        private void GuardarMotivoDeCelda(DataGridViewCell? celda)
        {
            if (celda == null ||
                celda.Tag is not int idEstatus ||
                idEstatus < 1 ||
                !IntentarObtenerClaveCelda(celda, out int eco, out DateTime fecha))
            {
                // Una celda nueva todavía no tiene registro. En ese caso la
                // observación se guardará al presionar uno de los estados.
                return;
            }

            string observacion = motivo.Text.Trim();

            observacionesPorCelda.TryGetValue(
                (eco, fecha.Date),
                out string? observacionGuardada
            );

            if (string.Equals(
                    observacionGuardada ?? "",
                    observacion,
                    StringComparison.Ordinal))
            {
                return;
            }

            // Un registro fuera de operación nunca puede quedar sin motivo.
            // Si se borra todo, no se altera lo que ya estaba guardado.
            if (idEstatus == 4 && observacion == "")
            {
                motivo.BackColor = Color.FromArgb(255, 235, 235);
                return;
            }

            try
            {
                HorometrosGuardadoBD.ActualizarObservacion(
                    eco,
                    fecha,
                    observacion
                );

                observacionesPorCelda[(eco, fecha.Date)] = observacion;
                ActualizarToolTipCelda(celda, eco, fecha, idEstatus, observacion);
                motivo.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                motivo.BackColor = Color.FromArgb(255, 235, 235);

                MessageBox.Show(
                    "No se pudo guardar la observación:\n\n" + ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool IntentarObtenerClaveCelda(
            DataGridViewCell celda,
            out int eco,
            out DateTime fecha)
        {
            eco = 0;
            fecha = DateTime.MinValue;

            if (!EsColumnaDeFecha(celda.ColumnIndex) ||
                celda.OwningColumn.Tag is not DateTime fechaColumna ||
                !tabla_horometros.Columns.Contains("Eco") ||
                !int.TryParse(
                    celda.OwningRow.Cells["Eco"].Value?.ToString(),
                    out eco))
            {
                return false;
            }

            fecha = fechaColumna.Date;
            return true;
        }

        private static void ActualizarToolTipCelda(
            DataGridViewCell celda,
            int eco,
            DateTime fecha,
            int idEstatus,
            string? observacion)
        {
            celda.ToolTipText =
                $"Eco: {eco}\n" +
                $"Fecha: {fecha:dd/MM/yyyy}\n" +
                $"Estado: {ObtenerNombreEstado(idEstatus)}" +
                (string.IsNullOrWhiteSpace(observacion)
                    ? ""
                    : $"\nObservación: {observacion}");
        }

        // ---------------------------------------------------------------
        // BOTONES DE ESTADO
        // ---------------------------------------------------------------

        private void ConfigurarBotonesEstado()
        {
            ConfigurarBotonEstado(Operando, 1);
            ConfigurarBotonEstado(Disponible, 2);
            ConfigurarBotonEstado(Sin_horometro, 3);
            ConfigurarBotonEstado(Fuera_operación, 4);
        }

        private void ConfigurarBotonEstado(IconButton boton, int idEstatus)
        {
            // Conserva el diseño definido en el Designer.
            // Aquí únicamente se relaciona cada botón con su estatus.
            boton.Tag = idEstatus;

            boton.Click -= BotonEstado_Click;
            boton.Click += BotonEstado_Click;
        }

        private void BotonEstado_Click(object? sender, EventArgs e)
        {
            if (sender is IconButton boton && boton.Tag is int idEstatus)
            {
                GuardarHorometroConEstado(idEstatus);
            }
        }

        private void MostrarBotonActivo(int idEstatus)
        {
            // Sin diseño dinámico en los botones.
            // Se conserva para no alterar la lógica existente que llama a este método.
        }

        // ---------------------------------------------------------------
        // GUARDADO AUTOMÁTICO AL ELEGIR EL ESTADO
        // ---------------------------------------------------------------

        private void GuardarHorometroConEstado(int idEstatus)
        {
            // Confirma primero cualquier texto que todavía esté en edición.
            if (!Validate())
            {
                return;
            }

            tabla_horometros.EndEdit();

            DataGridViewCell? celda = celdaHorometroSeleccionada;

            if (celda == null || !EsColumnaDeFecha(celda.ColumnIndex))
            {
                MessageBox.Show(
                    "Primero seleccione una celda correspondiente a una fecha.",
                    "Celda no seleccionada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if (!IntentarConvertirValor(celda.Value, out decimal valor))
            {
                MessageBox.Show(
                    "Escriba el valor del horómetro en la celda antes de " +
                    "elegir su estado.",
                    "Horómetro pendiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tabla_horometros.CurrentCell = celda;
                tabla_horometros.BeginEdit(true);
                return;
            }

            if (valor < 0 ||
                valor > 99999999.99m ||
                decimal.Round(valor, 2) != valor)
            {
                MessageBox.Show(
                    "El horómetro debe ser positivo, tener máximo dos " +
                    "decimales y caber en el campo decimal(10,2).",
                    "Horómetro no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tabla_horometros.CurrentCell = celda;
                tabla_horometros.BeginEdit(true);
                return;
            }

            if (!IntentarObtenerClaveCelda(
                    celda,
                    out int eco,
                    out DateTime fecha))
            {
                MessageBox.Show(
                    "No se pudo identificar el Eco o la fecha seleccionada.",
                    "Celda no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            string? observaciones = string.IsNullOrWhiteSpace(motivo.Text)
                ? null
                : motivo.Text.Trim();

            try
            {
                if (idEstatus == 4)
                {
                    string descripcionActual = observaciones ?? "";

                    if (descripcionActual == "")
                    {
                        if (!observacionesPorCelda.TryGetValue(
                                (eco, fecha.Date),
                                out descripcionActual) ||
                            string.IsNullOrWhiteSpace(descripcionActual))
                        {
                            descripcionActual =
                                HorometrosGuardadoBD.ObtenerObservacion(eco, fecha);
                        }
                    }

                    using VentanaMotivoFueraOperacion ventana =
                        new VentanaMotivoFueraOperacion(descripcionActual);

                    if (ventana.ShowDialog(this) != DialogResult.OK)
                    {
                        int estadoAnterior = celda.Tag is int idAnterior
                            ? idAnterior
                            : 0;

                        MostrarBotonActivo(estadoAnterior);
                        return;
                    }

                    observaciones = ventana.Descripcion;
                }

                // Si ya existe Eco + fecha, actualiza. Si todavía no existe,
                // lo inserta. Por eso el mismo flujo sirve para capturar y editar.
                HorometrosGuardadoBD.Guardar(
                    eco,
                    fecha,
                    valor,
                    idEstatus,
                    observaciones
                );

                celda.Value = valor;
                celda.Tag = idEstatus;
                AplicarColorEstado(celda, idEstatus);

                observacionesPorCelda[(eco, fecha.Date)] =
                    observaciones ?? "";

                cargandoMotivo = true;

                try
                {
                    motivo.Text = observaciones ?? "";
                    motivo.BackColor = Color.White;
                }
                finally
                {
                    cargandoMotivo = false;
                }

                ActualizarToolTipCelda(
                    celda,
                    eco,
                    fecha,
                    idEstatus,
                    observaciones
                );

                MostrarBotonActivo(idEstatus);
                tabla_horometros.InvalidateCell(celda);
            }
            catch (Exception ex)
            {
                int estadoAnterior = celda.Tag is int idAnterior
                    ? idAnterior
                    : 0;

                MostrarBotonActivo(estadoAnterior);

                MessageBox.Show(
                    "No se pudo guardar el horómetro:\n\n" + ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private static int ObtenerEstatus(DataRow registro)
        {
            string[] nombresPosibles =
            {
                "Id_estatus_h",
                "id_estatus_h",
                "id_estatus"
            };

            foreach (string nombre in nombresPosibles)
            {
                if (registro.Table.Columns.Contains(nombre) &&
                    !registro.IsNull(nombre))
                {
                    return Convert.ToInt32(registro[nombre]);
                }
            }

            return 0;
        }

        private static void AplicarColorEstado(
            DataGridViewCell celda,
            int idEstatus)
                    {
            celda.Style.ForeColor = idEstatus switch
            {
                1 => SystemColors.ControlText,
                2 => Color.FromArgb(55, 160, 190),
                3 => Color.FromArgb(110, 160, 65),
                4 => Color.FromArgb(210, 55, 55),
                _ => SystemColors.ControlText
            };

            celda.Style.SelectionForeColor = celda.Style.ForeColor;
        }

        private static string ObtenerNombreEstado(int idEstatus)
        {
            return idEstatus switch
            {
                1 => "Operando",
                2 => "Disponible",
                3 => "Sin horómetro",
                4 => "Fuera de operación",
                _ => "Sin estado"
            };
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            using SaveFileDialog guardar = new SaveFileDialog();

            guardar.Filter = "Excel (*.xlsx)|*.xlsx";
            guardar.FileName = $"Horometros_{DateTime.Now:yyyyMMdd}.xlsx";

            if (guardar.ShowDialog() != DialogResult.OK)
                return;

            ExcelPackage.License.SetNonCommercialPersonal("Joel Hernandez");

            using ExcelPackage excel = new ExcelPackage();
            var hoja = excel.Workbook.Worksheets.Add("Horómetros");

            // Encabezados
            for (int c = 0; c < tabla_horometros.Columns.Count; c++)
            {
                hoja.Cells[1, c + 1].Value =
                    tabla_horometros.Columns[c].HeaderText;

                hoja.Cells[1, c + 1].Style.Font.Bold = true;
            }

            // Datos
            for (int f = 0; f < tabla_horometros.Rows.Count; f++)
            {

                for (int c = 0; c < tabla_horometros.Columns.Count; c++)
                {
                    var celdaOrigen = tabla_horometros.Rows[f].Cells[c];
                    var celdaExcel = hoja.Cells[f + 2, c + 1];

                    celdaExcel.Value = celdaOrigen.Value;

                    if (celdaOrigen.Style.ForeColor != Color.Empty)
                    {
                        celdaExcel.Style.Font.Color.SetColor(
                            celdaOrigen.Style.ForeColor);
                    }

                    hoja.Cells[f + 2, c + 1].Value =
                        tabla_horometros.Rows[f].Cells[c].Value?.ToString();
                }
            }

            hoja.Cells.AutoFitColumns();

            File.WriteAllBytes(
                guardar.FileName,
                excel.GetAsByteArray());

            MessageBox.Show(
                "Exportación completada.",
                "Excel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

    }
}