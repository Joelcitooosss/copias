using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Indicadores_Escoria
{
    public partial class mantenimientos : Form
    {
        private readonly MantenimientosBD datos = new();
        private readonly BindingSource origen = new();

        private DataTable equipos = new();
        private DataTable tiposMantenimiento = new();
        private bool preparando;

        public mantenimientos()
        {
            InitializeComponent();

            typeof(DataGridView)
                .GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(tablaMantenimientos, true, null);

            Load += mantenimientos_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnEditar.Click += btnEditar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            tablaMantenimientos.CellValueChanged +=
                tablaMantenimientos_CellValueChanged;
            tablaMantenimientos.CurrentCellDirtyStateChanged +=
                tablaMantenimientos_CurrentCellDirtyStateChanged;
            tablaMantenimientos.DefaultValuesNeeded +=
                tablaMantenimientos_DefaultValuesNeeded;
            tablaMantenimientos.CellParsing +=
                tablaMantenimientos_CellParsing;
            tablaMantenimientos.CellEndEdit +=
                tablaMantenimientos_CellEndEdit;
            tablaMantenimientos.DataError +=
                tablaMantenimientos_DataError;
            tablaMantenimientos.SelectionChanged +=
                tablaMantenimientos_SelectionChanged;
            tablaMantenimientos.KeyDown +=
                tablaMantenimientos_KeyDown;
        }

        private void mantenimientos_Load(object? sender, EventArgs e)
        {
            ConfigurarTabla();

            try
            {
                equipos = datos.ObtenerEquipos();
                tiposMantenimiento = datos.ObtenerTipos();
                ActualizarTabla();
            }
            catch (Exception ex)
            {
                MostrarError(
                    "No se pudieron cargar los mantenimientos.",
                    ex
                );
            }
        }

        private void ConfigurarTabla()
        {
            tablaMantenimientos.AutoGenerateColumns = true;
            tablaMantenimientos.ReadOnly = false;
            tablaMantenimientos.MultiSelect = false;
            tablaMantenimientos.SelectionMode =
                DataGridViewSelectionMode.CellSelect;
            tablaMantenimientos.EditMode =
                DataGridViewEditMode.EditOnEnter;

            tablaMantenimientos.AllowUserToAddRows = true;
            tablaMantenimientos.AllowUserToDeleteRows = false;
            tablaMantenimientos.AllowUserToOrderColumns = true;
            tablaMantenimientos.AllowUserToResizeRows = false;
            tablaMantenimientos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            tablaMantenimientos.ScrollBars = ScrollBars.Both;
            tablaMantenimientos.ClipboardCopyMode =
                DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
        }

        private void ActualizarTabla()
        {
            preparando = true;

            try
            {
                origen.DataSource = datos.ObtenerMantenimientos();

                tablaMantenimientos.Columns.Clear();
                tablaMantenimientos.DataSource = null;
                tablaMantenimientos.DataSource = origen;
                ConfigurarColumnas();
                tablaMantenimientos.ClearSelection();
            }
            finally
            {
                preparando = false;
            }

            ActualizarBotones();
        }

        private void ConfigurarColumnas()
        {
            foreach (DataGridViewColumn col in tablaMantenimientos.Columns)
                            
            {
                          
            col.Frozen = false;
                            
            }
            ReemplazarColumnaTipoMantenimiento();

            Ocultar("Id_mantenimiento");
            Ocultar("Eco");
            Ocultar("es_taller");
            Ocultar("tipo_mantenimiento");

            Encabezado("registro", "ECO");
            Encabezado("equipo", "EQUIPO");
            Encabezado("modelo", "MODELO");
            Encabezado("serie", "SERIE");
            Encabezado("proyecto", "PROYECTO");
            Encabezado("propiedad", "PROPIEDAD");
            Encabezado("cliente", "CLIENTE");
            Encabezado("sistema", "SISTEMA");
            Encabezado(
                "codigo_componente_num",
                "CÓDIGO COMPONENTE NUM"
            );
            Encabezado(
                "codigo_componente_letra",
                "CÓDIGO COMPONENTE LETRA"
            );
            Encabezado(
                "codigo_componente_arroba",
                "CÓDIGO COMPONENTE @"
            );
            Encabezado("subsistema", "SUBSISTEMA");
            Encabezado("servicio", "SERVICIO");
            Encabezado(
                "Id_tipo_mantenimiento",
                "TIPO DE MANTTO"
            );
            Encabezado("supervisor", "SUPERVISOR");
            Encabezado("inicio", "INICIO");
            Encabezado("fin", "FIN");
            Encabezado("tiempo", "TIEMPO");
            Encabezado("demora", "DEMORA");
            Encabezado(
                "horas_mantto_efectivas",
                "HRS. MANTTO EFECTIVAS HH"
            );
            Encabezado("numero_tecnicos", "NO. TÉCNICOS");
            Encabezado("hh", "HH");
            Encabezado("tipo_tarea", "TIPO DE TAREA");
            Encabezado(
                "responsabilidad_tarea",
                "RESPONSABILIDAD DE LA TAREA"
            );
            Encabezado("descripcion", "DESCRIPCIÓN");
            Encabezado("estatus", "ESTATUS");
            Encabezado("origen_trabajo", "ORIGEN DEL TRABAJO");
            Encabezado("codigo_sintoma", "CÓDIGO DE SÍNTOMA");
            Encabezado("codigo_causa", "CÓDIGO DE CAUSA");
            Encabezado(
                "codigo_reparacion",
                "CÓDIGO DE REPARACIÓN"
            );
            Encabezado(
                "lubricante_sistema",
                "LUBRICANTE EN SISTEMA"
            );
            Encabezado("tipo_aceite", "TIPO DE ACEITE");
            Encabezado("cantidad_litros", "CANTIDAD EN LITROS");
            Encabezado("aplicacion", "APLICACIÓN");
            Encabezado("es_falla", "FALLA REPORTADA");
            Encabezado("comentarios", "COMENTARIOS");
            Encabezado("fecha", "F. RAP.");

            MarcarSoloLectura(
                "equipo",
                "modelo",
                "serie",
                "proyecto",
                "propiedad",
                "cliente"
                
            );

            Ancho("registro", 90);
            Ancho("equipo", 145);
            Ancho("modelo", 125);
            Ancho("serie", 170);
            Ancho("proyecto", 145);
            Ancho("propiedad", 110);
            Ancho("cliente", 160);
            Ancho("sistema", 150);
            Ancho("codigo_componente_num", 145);
            Ancho("codigo_componente_letra", 145);
            Ancho("codigo_componente_arroba", 145);
            Ancho("subsistema", 140);
            Ancho("servicio", 125);
            Ancho("Id_tipo_mantenimiento", 155);
            Ancho("supervisor", 165);
            Ancho("inicio", 80);
            Ancho("fin", 80);
            Ancho("tiempo", 90);
            Ancho("demora", 90);
            Ancho("horas_mantto_efectivas", 160);
            Ancho("numero_tecnicos", 110);
            Ancho("hh", 85);
            Ancho("tipo_tarea", 150);
            Ancho("responsabilidad_tarea", 190);
            Ancho("descripcion", 420);
            Ancho("estatus", 110);
            Ancho("origen_trabajo", 160);
            Ancho("codigo_sintoma", 135);
            Ancho("codigo_causa", 135);
            Ancho("codigo_reparacion", 145);
            Ancho("lubricante_sistema", 155);
            Ancho("tipo_aceite", 135);
            Ancho("cantidad_litros", 120);
            Ancho("aplicacion", 145);
            Ancho("es_falla", 105);
            Ancho("comentarios", 260);
            Ancho("fecha", 105);

            Formato("fecha", "dd/MM/yyyy");
            Formato("inicio", @"hh\:mm");
            Formato("fin", @"hh\:mm");
            Formato("tiempo", "0.##");
            Formato("demora", "0.##");
            Formato("horas_mantto_efectivas", "0.##");
            Formato("hh", "0.##");
            Formato("cantidad_litros", "0.##");

            

            if (tablaMantenimientos.Columns["descripcion"]
                is DataGridViewColumn descripcion)
            {
                descripcion.DefaultCellStyle.WrapMode =
                    DataGridViewTriState.True;
            }
            tablaMantenimientos.AutoGenerateColumns = false;
        }

        

        private void ReemplazarColumnaTipoMantenimiento()
        {
            if (tablaMantenimientos.Columns["Id_tipo_mantenimiento"]
                is DataGridViewColumn idAnterior)
            {
                tablaMantenimientos.Columns.Remove(idAnterior);
            }

            if (tablaMantenimientos.Columns["tipo_mantenimiento"]
                is DataGridViewColumn textoAnterior)
            {
                tablaMantenimientos.Columns.Remove(textoAnterior);
            }

            int indice = tablaMantenimientos.Columns["servicio"]
                is DataGridViewColumn servicio
                    ? servicio.Index + 1
                    : tablaMantenimientos.Columns.Count;

            DataGridViewComboBoxColumn columna =
                new DataGridViewComboBoxColumn
                {
                    Name = "Id_tipo_mantenimiento",
                    DataPropertyName = "Id_tipo_mantenimiento",
                    HeaderText = "TIPO DE MANTTO",
                    DataSource = tiposMantenimiento.Copy(),
                    DisplayMember = "tipo",
                    ValueMember = "Id_tipo_mantenimiento",
                    ValueType = typeof(int),
                    DisplayStyle =
                        DataGridViewComboBoxDisplayStyle.DropDownButton,
                    FlatStyle = FlatStyle.Flat,
                    SortMode = DataGridViewColumnSortMode.Automatic
                };

            tablaMantenimientos.Columns.Insert(
                Math.Min(indice, tablaMantenimientos.Columns.Count),
                columna
            );
        }

  

        private void tablaMantenimientos_DefaultValuesNeeded(
            object? sender,
            DataGridViewRowEventArgs e)
        {
            e.Row.Cells["es_taller"].Value = false;
            e.Row.Cells["es_falla"].Value = false;
            e.Row.Cells["fecha"].Value = DateTime.Today;
        }

        private void tablaMantenimientos_CurrentCellDirtyStateChanged(
            object? sender,
            EventArgs e)
        {
            if (!tablaMantenimientos.IsCurrentCellDirty)
                return;

            tablaMantenimientos.CommitEdit(
                DataGridViewDataErrorContexts.Commit
            );
        }

        private void tablaMantenimientos_CellValueChanged(
        object? sender,
        DataGridViewCellEventArgs e)
        {
            if (preparando || e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string nombreColumna =
                tablaMantenimientos.Columns[e.ColumnIndex].Name;

            if (nombreColumna == "registro")
            {
                preparando = true;

                try
                {
                    AplicarEquipo(
                        tablaMantenimientos.Rows[e.RowIndex]
                    );
                }
                finally
                {
                    preparando = false;
                }
            }

            ActualizarBotones();
        }

        private void AplicarEquipo(DataGridViewRow fila)
        {
            string registro = TextoCelda(fila, "registro") ?? "";

            if (!int.TryParse(registro, out int eco))
                return;

            DataRow? equipo = equipos.AsEnumerable()
                .FirstOrDefault(r => Convert.ToInt32(r["Eco"]) == eco);

            if (equipo == null)
                return;

            fila.Cells["Eco"].Value = eco;

            fila.Cells["equipo"].Value = equipo["equipo"];
            fila.Cells["modelo"].Value = equipo["modelo"];
            fila.Cells["serie"].Value = equipo["serie"];
            fila.Cells["proyecto"].Value = equipo["proyecto"];
            fila.Cells["propiedad"].Value = equipo["propiedad"];
            fila.Cells["cliente"].Value = equipo["cliente"];

        }

        private static void CopiarDatoEquipo(
            DataGridViewRow fila,
            DataRow equipo,
            string columna)
        {
            fila.Cells[columna].Value = equipo.IsNull(columna)
                ? DBNull.Value
                : equipo[columna];
        }

        private static void LimpiarDatosEquipo(DataGridViewRow fila)
        {
            string[] columnas =
            {
                "equipo",
                "modelo",
                "serie",
                "proyecto",
                "propiedad",
                "cliente"
            };

            foreach (string columna in columnas)
            {
                fila.Cells[columna].Value = DBNull.Value;
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            GuardarFilaActual();
        }

        private void GuardarFilaActual()
        {
            tablaMantenimientos.EndEdit();
            origen.EndEdit();

            DataGridViewRow? fila = ObtenerFilaActual();

            if (fila == null || fila.IsNewRow)
            {
                Advertencia(
                    "Captura los datos en la última fila de la tabla."
                );
                return;
            }

            MantenimientoRegistro? registro = LeerRegistro(fila);

            if (registro == null)
                return;

            try
            {
                bool esNuevo = registro.IdMantenimiento == 0;
                int filas = esNuevo
                    ? datos.Agregar(registro)
                    : datos.Actualizar(registro);

                if (filas <= 0)
                {
                    Advertencia(
                        "No se guardó ningún cambio en la base de datos."
                    );
                    return;
                }

                MessageBox.Show(
                    esNuevo
                        ? "Mantenimiento agregado correctamente."
                        : "Mantenimiento actualizado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ActualizarTabla();
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo guardar la fila.", ex);
            }
        }

        private void btnEditar_Click(object? sender, EventArgs e)
        {
            DataGridViewRow? fila = ObtenerFilaActual();

            if (fila == null || fila.IsNewRow)
            {
                Advertencia("Selecciona una celda para editarla.");
                return;
            }

            tablaMantenimientos.Focus();
            tablaMantenimientos.BeginEdit(true);
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            DataGridViewRow? fila = ObtenerFilaActual();

            if (fila == null || fila.IsNewRow)
                return;

            int id = EnteroCelda(fila, "Id_mantenimiento") ?? 0;

            if (id == 0)
            {
                ActualizarTabla();
                return;
            }

            DialogResult confirmar = MessageBox.Show(
                "¿Deseas eliminar el mantenimiento seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                if (datos.Eliminar(id) > 0)
                {
                    MessageBox.Show(
                        "Mantenimiento eliminado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    ActualizarTabla();
                }
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo eliminar la fila.", ex);
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            origen.CancelEdit();
            ActualizarTabla();
            IrFilaNueva();
        }

        private void IrFilaNueva()
        {
            if (tablaMantenimientos.Rows.Count == 0 ||
                tablaMantenimientos.Columns["registro"] == null)
            {
                return;
            }

            DataGridViewRow fila =
                tablaMantenimientos.Rows[
                    tablaMantenimientos.Rows.Count - 1];

            tablaMantenimientos.CurrentCell = fila.Cells["registro"];
            tablaMantenimientos.BeginEdit(true);
        }

        private MantenimientoRegistro? LeerRegistro(
            DataGridViewRow fila)
        {
            string registroTexto = TextoCelda(fila, "registro") ?? "";

            if (registroTexto == "")
            {
                AdvertenciaCelda(
                    fila,
                    "registro",
                    "Selecciona un ECO o TALLER."
                );

                return null;

            }

            bool esTaller = registroTexto.Equals(
                "TALLER",
                StringComparison.OrdinalIgnoreCase
            );
            int? eco = null;

            if (!esTaller)
            {
                if (!int.TryParse(registroTexto, out int ecoLeido))
                {
                    AdvertenciaCelda(
                        fila,
                        "registro",
                        "El ECO seleccionado no es válido."
                    );
                    return null;
                }

                eco = ecoLeido;
            }

            int idTipo =
                EnteroCelda(fila, "Id_tipo_mantenimiento") ?? 0;

            if (idTipo == 0)
            {
                AdvertenciaCelda(
                    fila,
                    "Id_tipo_mantenimiento",
                    "Selecciona el tipo de mantenimiento."
                );
                return null;
            }

            string sistema = TextoCelda(fila, "sistema") ?? "";

            if (sistema == "")
            {
                AdvertenciaCelda(
                    fila,
                    "sistema",
                    "Escribe el sistema."
                );
                return null;
            }

            string descripcion = TextoCelda(fila, "descripcion") ?? "";

            if (descripcion == "")
            {
                AdvertenciaCelda(
                    fila,
                    "descripcion",
                    "Escribe la descripción del trabajo."
                );
                return null;
            }

            if (!FechaCelda(fila, "fecha", out DateTime fecha))
            {
                AdvertenciaCelda(
                    fila,
                    "fecha",
                    "Escribe la fecha como dd/MM/yyyy."
                );
                return null;
            }

            if (!HoraCelda(fila, "inicio", out TimeSpan? inicio) ||
                !HoraCelda(fila, "fin", out TimeSpan? fin) ||
                !DecimalCelda(fila, "tiempo", out decimal? tiempo) ||
                !DecimalCelda(fila, "demora", out decimal? demora) ||
                !DecimalCelda(
                    fila,
                    "horas_mantto_efectivas",
                    out decimal? horasEfectivas) ||
                !EnteroNullableCelda(
                    fila,
                    "numero_tecnicos",
                    out int? numeroTecnicos) ||
                !DecimalCelda(fila, "hh", out decimal? hh) ||
                !DecimalCelda(
                    fila,
                    "cantidad_litros",
                    out decimal? cantidadLitros))
            {
                return null;
            }

            return new MantenimientoRegistro
            {
                IdMantenimiento =
                    EnteroCelda(fila, "Id_mantenimiento") ?? 0,
                Eco = eco,
                IdTipoMantenimiento = idTipo,
                EsTaller = esTaller,
                Sistema = sistema,
                CodigoComponenteNum =
                    TextoCelda(fila, "codigo_componente_num"),
                CodigoComponenteLetra =
                    TextoCelda(fila, "codigo_componente_letra"),
                CodigoComponenteArroba =
                    TextoCelda(fila, "codigo_componente_arroba"),
                Subsistema = TextoCelda(fila, "subsistema"),
                Servicio = TextoCelda(fila, "servicio"),
                Supervisor = TextoCelda(fila, "supervisor"),
                Inicio = inicio,
                Fin = fin,
                Tiempo = tiempo,
                Demora = demora,
                HorasEfectivas = horasEfectivas,
                NumeroTecnicos = numeroTecnicos,
                HH = hh,
                TipoTarea = TextoCelda(fila, "tipo_tarea"),
                ResponsabilidadTarea =
                    TextoCelda(fila, "responsabilidad_tarea"),
                Descripcion = descripcion,
                Estatus = TextoCelda(fila, "estatus"),
                OrigenTrabajo = TextoCelda(fila, "origen_trabajo"),
                CodigoSintoma = TextoCelda(fila, "codigo_sintoma"),
                CodigoCausa = TextoCelda(fila, "codigo_causa"),
                CodigoReparacion =
                    TextoCelda(fila, "codigo_reparacion"),
                LubricanteSistema =
                    TextoCelda(fila, "lubricante_sistema"),
                TipoAceite = TextoCelda(fila, "tipo_aceite"),
                CantidadLitros = cantidadLitros,
                Aplicacion = TextoCelda(fila, "aplicacion"),
                EsFalla = BooleanoCelda(fila, "es_falla"),
                Comentarios = TextoCelda(fila, "comentarios"),
                Fecha = fecha
            };
        }

        private void tablaMantenimientos_CellParsing(
            object? sender,
            DataGridViewCellParsingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string campo =
                tablaMantenimientos.Columns[e.ColumnIndex].DataPropertyName;
            string texto = Convert.ToString(e.Value)?.Trim() ?? "";

            if (texto == "" && campo != "fecha")
            {
                e.Value = DBNull.Value;
                e.ParsingApplied = true;
                return;
            }

            if (campo == "fecha" && TryFecha(texto, out DateTime fecha))
            {
                e.Value = fecha;
                e.ParsingApplied = true;
            }
            else if ((campo == "inicio" || campo == "fin") &&
                     TryHora(texto, out TimeSpan hora))
            {
                e.Value = hora;
                e.ParsingApplied = true;
            }
            else if (ColumnasDecimales.Contains(campo) &&
                     TryDecimal(texto, out decimal numero))
            {
                e.Value = numero;
                e.ParsingApplied = true;
            }
            else if (campo == "numero_tecnicos" &&
                     int.TryParse(texto, out int entero) &&
                     entero >= 0)
            {
                e.Value = entero;
                e.ParsingApplied = true;
            }
        }

        private void tablaMantenimientos_CellEndEdit(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                tablaMantenimientos.Rows[e.RowIndex]
                    .Cells[e.ColumnIndex].ErrorText = "";
            }

            ActualizarBotones();
        }

        private void tablaMantenimientos_DataError(
            object? sender,
            DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell celda =
                    tablaMantenimientos.Rows[e.RowIndex]
                        .Cells[e.ColumnIndex];
                celda.ErrorText =
                    "El valor no tiene el formato correcto para esta columna.";
            }
        }

        private void tablaMantenimientos_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            ActualizarBotones();
        }

        private void tablaMantenimientos_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                GuardarFilaActual();
                return;
            }

            if (e.KeyCode == Keys.Delete &&
                !tablaMantenimientos.IsCurrentCellInEditMode)
            {
                e.SuppressKeyPress = true;
                btnEliminar.PerformClick();
            }
        }

        private void ActualizarBotones()
        {
            if (preparando)
                return;

            DataGridViewRow? fila = ObtenerFilaActual();
            bool filaCapturada = fila != null && !fila.IsNewRow;
            bool existente = filaCapturada &&
                (EnteroCelda(fila!, "Id_mantenimiento") ?? 0) > 0;

            btnGuardar.Enabled = filaCapturada;
            btnEditar.Enabled = filaCapturada;
            btnEliminar.Enabled = filaCapturada;

            if (existente)
            {
                btnGuardar.Text = "Guardar cambios";
            }
            else
            {
                btnGuardar.Text = "Guardar fila";
            }
        }

        private DataGridViewRow? ObtenerFilaActual()
        {
            return tablaMantenimientos.CurrentCell == null
                ? null
                : tablaMantenimientos.Rows[
                    tablaMantenimientos.CurrentCell.RowIndex];
        }

        private void AdvertenciaCelda(
            DataGridViewRow fila,
            string columna,
            string mensaje)
        {
            Advertencia(mensaje);

            if (tablaMantenimientos.Columns[columna] == null)
                return;

            tablaMantenimientos.CurrentCell = fila.Cells[columna];
            tablaMantenimientos.Focus();
            tablaMantenimientos.BeginEdit(true);
        }

        private static string? TextoCelda(
            DataGridViewRow fila,
            string columna)
        {
            object? valor = fila.Cells[columna].Value;

            if (valor == null || valor == DBNull.Value)
                return null;

            string texto = Convert.ToString(valor)?.Trim() ?? "";
            return texto == "" ? null : texto;
        }

        private static int? EnteroCelda(
            DataGridViewRow fila,
            string columna)
        {
            object? valor = fila.Cells[columna].Value;

            if (valor == null || valor == DBNull.Value)
                return null;

            return Convert.ToInt32(valor);
        }

        private bool EnteroNullableCelda(
            DataGridViewRow fila,
            string columna,
            out int? resultado)
        {
            resultado = null;
            object? valor = fila.Cells[columna].Value;

            if (valor == null || valor == DBNull.Value ||
                string.IsNullOrWhiteSpace(Convert.ToString(valor)))
            {
                return true;
            }

            if (!int.TryParse(Convert.ToString(valor), out int numero) ||
                numero < 0)
            {
                AdvertenciaCelda(
                    fila,
                    columna,
                    "El número de técnicos debe ser un entero mayor o igual a cero."
                );
                return false;
            }

            resultado = numero;
            return true;
        }

        private bool DecimalCelda(
            DataGridViewRow fila,
            string columna,
            out decimal? resultado)
        {
            resultado = null;
            object? valor = fila.Cells[columna].Value;

            if (valor == null || valor == DBNull.Value ||
                string.IsNullOrWhiteSpace(Convert.ToString(valor)))
            {
                return true;
            }

            if (valor is decimal decimalDirecto)
            {
                if (decimalDirecto < 0)
                {
                    AdvertenciaCelda(
                        fila,
                        columna,
                        "El valor no puede ser negativo."
                    );
                    return false;
                }

                resultado = decimalDirecto;
                return true;
            }

            if (!TryDecimal(Convert.ToString(valor) ?? "", out decimal numero) ||
                numero < 0)
            {
                AdvertenciaCelda(
                    fila,
                    columna,
                    "Escribe un número mayor o igual a cero."
                );
                return false;
            }

            resultado = numero;
            return true;
        }

        private bool HoraCelda(
            DataGridViewRow fila,
            string columna,
            out TimeSpan? resultado)
        {
            resultado = null;
            object? valor = fila.Cells[columna].Value;

            if (valor == null || valor == DBNull.Value ||
                string.IsNullOrWhiteSpace(Convert.ToString(valor)))
            {
                return true;
            }

            if (valor is TimeSpan horaDirecta)
            {
                resultado = horaDirecta;
                return true;
            }

            if (!TryHora(Convert.ToString(valor) ?? "", out TimeSpan hora))
            {
                AdvertenciaCelda(
                    fila,
                    columna,
                    $"Escribe {columna} con el formato HH:mm."
                );
                return false;
            }

            resultado = hora;
            return true;
        }

        private static bool FechaCelda(
            DataGridViewRow fila,
            string columna,
            out DateTime resultado)
        {
            object? valor = fila.Cells[columna].Value;

            if (valor is DateTime fecha)
            {
                resultado = fecha.Date;
                return true;
            }

            return TryFecha(Convert.ToString(valor) ?? "", out resultado);
        }

        private static bool BooleanoCelda(
            DataGridViewRow fila,
            string columna)
        {
            object? valor = fila.Cells[columna].Value;

            return valor != null &&
                   valor != DBNull.Value &&
                   Convert.ToBoolean(valor);
        }

        private static bool TryFecha(string texto, out DateTime fecha)
        {
            string[] formatos =
            {
                "d/M/yyyy",
                "dd/MM/yyyy",
                "d-M-yyyy",
                "dd-MM-yyyy",
                "yyyy-MM-dd"
            };

            return DateTime.TryParseExact(
                       texto.Trim(),
                       formatos,
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out fecha) ||
                   DateTime.TryParse(
                       texto.Trim(),
                       CultureInfo.GetCultureInfo("es-MX"),
                       DateTimeStyles.None,
                       out fecha
                   );
        }

        private static bool TryHora(string texto, out TimeSpan hora)
        {
            string[] formatos =
            {
                @"h\:mm",
                @"hh\:mm",
                @"h\:mm\:ss",
                @"hh\:mm\:ss"
            };

            return TimeSpan.TryParseExact(
                       texto.Trim(),
                       formatos,
                       CultureInfo.InvariantCulture,
                       out hora) &&
                   hora >= TimeSpan.Zero &&
                   hora < TimeSpan.FromDays(1);
        }

        private static bool TryDecimal(string texto, out decimal numero)
        {
            texto = texto.Trim().Replace(',', '.');

            return decimal.TryParse(
                texto,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out numero
            );
        }

        private static readonly HashSet<string> ColumnasDecimales =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "tiempo",
                "demora",
                "horas_mantto_efectivas",
                "hh",
                "cantidad_litros"
            };

        private void Ocultar(string nombre)
        {
            if (tablaMantenimientos.Columns[nombre]
                is DataGridViewColumn columna)
            {
                columna.Visible = false;
            }
        }

        private void Encabezado(string nombre, string texto)
        {
            if (tablaMantenimientos.Columns[nombre]
                is DataGridViewColumn columna)
            {
                columna.HeaderText = texto;
            }
        }

        private void Ancho(string nombre, int ancho)
        {
            if (tablaMantenimientos.Columns[nombre]
                is DataGridViewColumn columna)
            {
                columna.Width = ancho;
            }
        }

        private void Formato(string nombre, string formato)
        {
            if (tablaMantenimientos.Columns[nombre]
                is DataGridViewColumn columna)
            {
                columna.DefaultCellStyle.Format = formato;
            }
        }

        private void MarcarSoloLectura(params string[] nombres)
        {
            foreach (string nombre in nombres)
            {
                if (tablaMantenimientos.Columns[nombre]
                    is DataGridViewColumn columna)
                {
                    columna.ReadOnly = true;
                    columna.DefaultCellStyle.BackColor =
                        Color.FromArgb(242, 242, 242);
                }
            }
        }

        private static void AsignarCelda(
            DataGridViewRow fila,
            string columna,
            object valor)
        {
            fila.Cells[columna].Value = valor;
        }

        private static void Advertencia(string mensaje)
        {
            MessageBox.Show(
                mensaje,
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        private static void MostrarError(string mensaje, Exception ex)
        {
            MessageBox.Show(
                mensaje + "\n\n" + ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
