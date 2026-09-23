using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Indicadores_Escoria
{
    internal sealed class MantenimientoRegistro
    {
        public int IdMantenimiento { get; set; }
        public int? Eco { get; set; }
        public int IdTipoMantenimiento { get; set; }
        public bool EsTaller { get; set; }

        public string? Sistema { get; set; }
        public string? CodigoComponenteNum { get; set; }
        public string? CodigoComponenteLetra { get; set; }
        public string? CodigoComponenteArroba { get; set; }
        public string? Subsistema { get; set; }
        public string? Servicio { get; set; }
        public string? Supervisor { get; set; }

        public TimeSpan? Inicio { get; set; }
        public TimeSpan? Fin { get; set; }
        public decimal? Tiempo { get; set; }
        public decimal? Demora { get; set; }
        public decimal? HorasEfectivas { get; set; }
        public int? NumeroTecnicos { get; set; }
        public decimal? HH { get; set; }

        public string? TipoTarea { get; set; }
        public string? ResponsabilidadTarea { get; set; }
        public string Descripcion { get; set; } = "";
        public string? Estatus { get; set; }
        public string? OrigenTrabajo { get; set; }
        public string? CodigoSintoma { get; set; }
        public string? CodigoCausa { get; set; }
        public string? CodigoReparacion { get; set; }
        public string? LubricanteSistema { get; set; }
        public string? TipoAceite { get; set; }
        public decimal? CantidadLitros { get; set; }
        public string? Aplicacion { get; set; }
        public bool EsFalla { get; set; }
        public string? Comentarios { get; set; }
        public DateTime Fecha { get; set; }
    }

    internal sealed class MantenimientosBD
    {
        private const string CadenaConexion =
            @"Data Source=MXLF-WS-RSD01\SQLEXPRESS;" +
            @"Initial Catalog=ManejoEscoria;" +
            @"Integrated Security=True;" +
            @"TrustServerCertificate=True";

        private static SqlConnection CrearConexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public DataTable ObtenerEquipos()
        {
            const string sql = @"
                SELECT
                    Eco,
                    CONVERT(NVARCHAR(20), Eco) AS registro,
                    CONCAT(Eco, N' - ', tipo_equipo, N' - ', modelo)
                        AS descripcion_equipo,
                    tipo_equipo AS equipo,
                    modelo,
                    serie,
                    proyecto,
                    propiedad,
                    cliente
                FROM equipos
                ORDER BY Eco;";

            return Consultar(sql);
        }

        public DataTable ObtenerTipos()
        {
            const string sql = @"
                SELECT Id_tipo_mantenimiento, tipo
                FROM tipo_mantenimiento
                ORDER BY tipo;";

            return Consultar(sql);
        }

        public DataTable ObtenerMantenimientos()
        {
            const string sql = @"
                SELECT
                    m.Id_mantenimiento,
                    m.Eco,
                    m.es_taller,
                    m.Id_tipo_mantenimiento,

                    CASE
                        WHEN m.es_taller = 1 THEN N'TALLER'
                        ELSE CONVERT(NVARCHAR(20), m.Eco)
                    END AS registro,

                    e.tipo_equipo AS equipo,
                    e.modelo,
                    e.serie,
                    e.proyecto,
                    e.propiedad,
                    e.cliente,

                    m.sistema,
                    m.codigo_componente_num,
                    m.codigo_componente_letra,
                    m.codigo_componente_arroba,
                    m.subsistema,
                    m.servicio,
                    tm.tipo AS tipo_mantenimiento,
                    m.supervisor,
                    m.inicio,
                    m.fin,
                    m.tiempo,
                    m.demora,
                    m.horas_mantto_efectivas,
                    m.numero_tecnicos,
                    m.hh,
                    m.tipo_tarea,
                    m.responsabilidad_tarea,
                    m.descripcion,
                    m.estatus,
                    m.origen_trabajo,
                    m.codigo_sintoma,
                    m.codigo_causa,
                    m.codigo_reparacion,
                    m.lubricante_sistema,
                    m.tipo_aceite,
                    m.cantidad_litros,
                    m.aplicacion,
                    m.es_falla,
                    m.comentarios,
                    m.fecha

                FROM mantenimientos AS m

                LEFT JOIN equipos AS e
                    ON e.Eco = m.Eco

                LEFT JOIN tipo_mantenimiento AS tm
                    ON tm.Id_tipo_mantenimiento =
                       m.Id_tipo_mantenimiento

                ORDER BY
                    m.fecha DESC,
                    m.Id_mantenimiento DESC;";

            return Consultar(sql);
        }

        public int Agregar(MantenimientoRegistro registro)
        {
            const string sql = @"
                INSERT INTO mantenimientos
                (
                    Eco,
                    Id_tipo_mantenimiento,
                    es_taller,
                    sistema,
                    codigo_componente_num,
                    codigo_componente_letra,
                    codigo_componente_arroba,
                    subsistema,
                    servicio,
                    supervisor,
                    inicio,
                    fin,
                    tiempo,
                    demora,
                    horas_mantto_efectivas,
                    numero_tecnicos,
                    hh,
                    tipo_tarea,
                    responsabilidad_tarea,
                    descripcion,
                    estatus,
                    origen_trabajo,
                    codigo_sintoma,
                    codigo_causa,
                    codigo_reparacion,
                    lubricante_sistema,
                    tipo_aceite,
                    cantidad_litros,
                    aplicacion,
                    es_falla,
                    comentarios,
                    fecha
                )
                VALUES
                (
                    @Eco,
                    @IdTipo,
                    @EsTaller,
                    @Sistema,
                    @CodigoNum,
                    @CodigoLetra,
                    @CodigoArroba,
                    @Subsistema,
                    @Servicio,
                    @Supervisor,
                    @Inicio,
                    @Fin,
                    @Tiempo,
                    @Demora,
                    @HorasEfectivas,
                    @NumeroTecnicos,
                    @HH,
                    @TipoTarea,
                    @Responsabilidad,
                    @Descripcion,
                    @Estatus,
                    @OrigenTrabajo,
                    @CodigoSintoma,
                    @CodigoCausa,
                    @CodigoReparacion,
                    @Lubricante,
                    @TipoAceite,
                    @CantidadLitros,
                    @Aplicacion,
                    @EsFalla,
                    @Comentarios,
                    @Fecha
                );";

            return Ejecutar(sql, registro);
        }

        public int Actualizar(MantenimientoRegistro registro)
        {
            const string sql = @"
                UPDATE mantenimientos
                SET
                    Eco = @Eco,
                    Id_tipo_mantenimiento = @IdTipo,
                    es_taller = @EsTaller,
                    sistema = @Sistema,
                    codigo_componente_num = @CodigoNum,
                    codigo_componente_letra = @CodigoLetra,
                    codigo_componente_arroba = @CodigoArroba,
                    subsistema = @Subsistema,
                    servicio = @Servicio,
                    supervisor = @Supervisor,
                    inicio = @Inicio,
                    fin = @Fin,
                    tiempo = @Tiempo,
                    demora = @Demora,
                    horas_mantto_efectivas = @HorasEfectivas,
                    numero_tecnicos = @NumeroTecnicos,
                    hh = @HH,
                    tipo_tarea = @TipoTarea,
                    responsabilidad_tarea = @Responsabilidad,
                    descripcion = @Descripcion,
                    estatus = @Estatus,
                    origen_trabajo = @OrigenTrabajo,
                    codigo_sintoma = @CodigoSintoma,
                    codigo_causa = @CodigoCausa,
                    codigo_reparacion = @CodigoReparacion,
                    lubricante_sistema = @Lubricante,
                    tipo_aceite = @TipoAceite,
                    cantidad_litros = @CantidadLitros,
                    aplicacion = @Aplicacion,
                    es_falla = @EsFalla,
                    comentarios = @Comentarios,
                    fecha = @Fecha
                WHERE Id_mantenimiento = @Id;";

            return Ejecutar(sql, registro, true);
        }

        public int Eliminar(int id)
        {
            const string sql = @"
                DELETE FROM mantenimientos
                WHERE Id_mantenimiento = @Id;";

            using SqlConnection conexion = CrearConexion();
            using SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            conexion.Open();
            return comando.ExecuteNonQuery();
        }

        public int AgregarTipo(string tipo)
        {
            const string sql = @"
                IF EXISTS
                (
                    SELECT 1
                    FROM tipo_mantenimiento
                    WHERE tipo = @Tipo
                )
                BEGIN
                    SELECT Id_tipo_mantenimiento
                    FROM tipo_mantenimiento
                    WHERE tipo = @Tipo;
                END
                ELSE
                BEGIN
                    INSERT INTO tipo_mantenimiento(tipo)
                    VALUES (@Tipo);

                    SELECT CONVERT(INT, SCOPE_IDENTITY());
                END;";

            using SqlConnection conexion = CrearConexion();
            using SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters
                .Add("@Tipo", SqlDbType.NVarChar, 50)
                .Value = tipo.Trim();

            conexion.Open();
            return Convert.ToInt32(comando.ExecuteScalar());
        }

        private static DataTable Consultar(string sql)
        {
            using SqlConnection conexion = CrearConexion();
            using SqlDataAdapter adaptador =
                new SqlDataAdapter(sql, conexion);

            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            return tabla;
        }

        private static int Ejecutar(
            string sql,
            MantenimientoRegistro registro,
            bool incluirId = false)
        {
            using SqlConnection conexion = CrearConexion();
            using SqlCommand comando = new SqlCommand(sql, conexion);

            AgregarParametros(comando, registro);

            if (incluirId)
            {
                comando.Parameters
                    .Add("@Id", SqlDbType.Int)
                    .Value = registro.IdMantenimiento;
            }

            conexion.Open();
            return comando.ExecuteNonQuery();
        }

        private static void AgregarParametros(
            SqlCommand comando,
            MantenimientoRegistro r)
        {
            Agregar(comando, "@Eco", SqlDbType.Int, r.Eco);
            Agregar(
                comando,
                "@IdTipo",
                SqlDbType.Int,
                r.IdTipoMantenimiento
            );
            Agregar(
                comando,
                "@EsTaller",
                SqlDbType.Bit,
                r.EsTaller
            );

            AgregarTexto(comando, "@Sistema", 100, r.Sistema);
            AgregarTexto(
                comando,
                "@CodigoNum",
                30,
                r.CodigoComponenteNum
            );
            AgregarTexto(
                comando,
                "@CodigoLetra",
                30,
                r.CodigoComponenteLetra
            );
            AgregarTexto(
                comando,
                "@CodigoArroba",
                50,
                r.CodigoComponenteArroba
            );
            AgregarTexto(comando, "@Subsistema", 100, r.Subsistema);
            AgregarTexto(comando, "@Servicio", 100, r.Servicio);
            AgregarTexto(comando, "@Supervisor", 150, r.Supervisor);

            Agregar(comando, "@Inicio", SqlDbType.Time, r.Inicio);
            Agregar(comando, "@Fin", SqlDbType.Time, r.Fin);
            AgregarDecimal(comando, "@Tiempo", r.Tiempo);
            AgregarDecimal(comando, "@Demora", r.Demora);
            AgregarDecimal(
                comando,
                "@HorasEfectivas",
                r.HorasEfectivas
            );
            Agregar(
                comando,
                "@NumeroTecnicos",
                SqlDbType.Int,
                r.NumeroTecnicos
            );
            AgregarDecimal(comando, "@HH", r.HH);

            AgregarTexto(comando, "@TipoTarea", 100, r.TipoTarea);
            AgregarTexto(
                comando,
                "@Responsabilidad",
                200,
                r.ResponsabilidadTarea
            );
            AgregarTexto(comando, "@Descripcion", -1, r.Descripcion);
            AgregarTexto(comando, "@Estatus", 50, r.Estatus);
            AgregarTexto(
                comando,
                "@OrigenTrabajo",
                100,
                r.OrigenTrabajo
            );
            AgregarTexto(
                comando,
                "@CodigoSintoma",
                50,
                r.CodigoSintoma
            );
            AgregarTexto(comando, "@CodigoCausa", 50, r.CodigoCausa);
            AgregarTexto(
                comando,
                "@CodigoReparacion",
                50,
                r.CodigoReparacion
            );
            AgregarTexto(
                comando,
                "@Lubricante",
                100,
                r.LubricanteSistema
            );
            AgregarTexto(comando, "@TipoAceite", 100, r.TipoAceite);
            AgregarDecimal(
                comando,
                "@CantidadLitros",
                r.CantidadLitros
            );
            AgregarTexto(comando, "@Aplicacion", 200, r.Aplicacion);
            Agregar(comando, "@EsFalla", SqlDbType.Bit, r.EsFalla);
            AgregarTexto(comando, "@Comentarios", -1, r.Comentarios);
            Agregar(comando, "@Fecha", SqlDbType.Date, r.Fecha.Date);
        }

        private static void Agregar(
            SqlCommand comando,
            string nombre,
            SqlDbType tipo,
            object? valor)
        {
            comando.Parameters
                .Add(nombre, tipo)
                .Value = valor ?? DBNull.Value;
        }

        private static void AgregarTexto(
            SqlCommand comando,
            string nombre,
            int longitud,
            string? valor)
        {
            comando.Parameters
                .Add(nombre, SqlDbType.NVarChar, longitud)
                .Value = string.IsNullOrWhiteSpace(valor)
                    ? DBNull.Value
                    : valor.Trim();
        }

        private static void AgregarDecimal(
            SqlCommand comando,
            string nombre,
            decimal? valor)
        {
            SqlParameter parametro =
                comando.Parameters.Add(nombre, SqlDbType.Decimal);

            parametro.Precision = 12;
            parametro.Scale = 2;
            parametro.Value = valor.HasValue
                ? valor.Value
                : DBNull.Value;
        }
    }
}
