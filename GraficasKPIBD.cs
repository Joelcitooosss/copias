using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Indicadores_Escoria
{
    internal sealed class ResultadoGraficasKPI
    {
        public DataTable PorEco { get; init; } = new();
        public DataTable PorModelo { get; init; } = new();
        public DataTable MantenimientoEcoTipo { get; init; } = new();
        public DataTable MantenimientoModeloTipo { get; init; } = new();
    }

    internal static class GraficasKPIBD
    {
        private const string CadenaConexion =
            @"Data Source=MXLF-WS-RSD01\SQLEXPRESS;" +
            @"Initial Catalog=ManejoEscoria;" +
            @"Integrated Security=True;" +
            @"TrustServerCertificate=True";

        public static ResultadoGraficasKPI Obtener(
            DateTime fechaInicio,
            DateTime fechaFin,
            decimal horasOperacionPorDia)
        {
            DateTime inicio = fechaInicio.Date;
            DateTime fin = fechaFin.Date;

            if (fin < inicio)
            {
                throw new ArgumentException(
                    "La fecha final no puede ser anterior a la fecha inicial."
                );
            }

            if (horasOperacionPorDia <= 0m ||
                horasOperacionPorDia > 24m)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(horasOperacionPorDia),
                    "Las horas de operación por día deben estar entre 1 y 24."
                );
            }

            int dias = (fin - inicio).Days + 1;
            DateTime finExclusivo = fin.AddDays(1);

            DataSet datos = Consultar(inicio, finExclusivo);
            List<EquipoKpi> equipos = ConstruirEquipos(
                datos.Tables[0],
                dias,
                horasOperacionPorDia
            );
            List<ModeloKpi> modelos = ConstruirModelos(equipos);
            List<MantenimientoDetalle> mantenimientos =
                ConstruirMantenimientos(datos.Tables[1]);

            return new ResultadoGraficasKPI
            {
                PorEco = CrearTablaPorEco(equipos),
                PorModelo = CrearTablaPorModelo(modelos),
                MantenimientoEcoTipo =
                    CrearMantenimientoPorEco(mantenimientos),
                MantenimientoModeloTipo =
                    CrearMantenimientoPorModelo(mantenimientos)
            };
        }

        private static DataSet Consultar(
            DateTime inicio,
            DateTime finExclusivo)
        {
            const string consultaSql = @"
WITH LecturasPeriodo AS
(
    SELECT
        h.Eco,
        h.fecha_lectura,
        h.valor,
        ROW_NUMBER() OVER
        (
            PARTITION BY h.Eco
            ORDER BY h.fecha_lectura ASC
        ) AS OrdenInicial,
        ROW_NUMBER() OVER
        (
            PARTITION BY h.Eco
            ORDER BY h.fecha_lectura DESC
        ) AS OrdenFinal
    FROM dbo.horometros AS h
    WHERE h.fecha_lectura >= @Inicio
      AND h.fecha_lectura < @FinExclusivo
),
ResumenHorometros AS
(
    SELECT
        Eco,
        MAX(CASE WHEN OrdenInicial = 1 THEN valor END) AS SMRInicial,
        MAX(CASE WHEN OrdenFinal = 1 THEN valor END) AS SMRFinal
    FROM LecturasPeriodo
    GROUP BY Eco
),
MantenimientoBase AS
(
    SELECT
        m.Eco,
        CAST
        (
            COALESCE
            (
                m.tiempo,
                CASE
                    WHEN m.inicio IS NOT NULL AND m.fin IS NOT NULL THEN
                        CASE
                            WHEN DATEDIFF(MINUTE, m.inicio, m.fin) >= 0
                                THEN DATEDIFF(MINUTE, m.inicio, m.fin) / 60.0
                            ELSE
                                (DATEDIFF(MINUTE, m.inicio, m.fin) + 1440) / 60.0
                        END
                    ELSE 0
                END
            )
            AS decimal(18, 2)
        ) AS Horas,
        UPPER(LTRIM(RTRIM(ISNULL(tm.tipo, N''))))
            AS TipoMantenimiento
    FROM dbo.mantenimientos AS m
    LEFT JOIN dbo.tipo_mantenimiento AS tm
        ON tm.Id_tipo_mantenimiento = m.Id_tipo_mantenimiento
    WHERE m.Eco IS NOT NULL
      AND m.fecha >= @Inicio
      AND m.fecha < @FinExclusivo
),
ResumenMantenimientos AS
(
    SELECT
        Eco,
        SUM
        (
            CASE
                WHEN TipoMantenimiento IN
                (
                    N'PREVENTIVO',
                    N'CORRECTIVO',
                    N'CORRECTIVO C'
                )
                    THEN Horas
                ELSE 0
            END
        ) AS HorasMantenimiento,
        SUM
        (
            CASE
                WHEN TipoMantenimiento IN
                     (N'CORRECTIVO', N'CORRECTIVO C')
                    THEN 1
                ELSE 0
            END
        ) AS NumeroFallas,
        SUM
        (
            CASE
                WHEN TipoMantenimiento IN
                     (N'CORRECTIVO', N'CORRECTIVO C')
                    THEN Horas
                ELSE 0
            END
        ) AS HorasCorrectivas
    FROM MantenimientoBase
    GROUP BY Eco
)
SELECT
    e.Eco,
    e.modelo AS Modelo,
    h.SMRInicial,
    h.SMRFinal,
    ISNULL(m.HorasMantenimiento, 0) AS HorasMantenimiento,
    ISNULL(m.NumeroFallas, 0) AS NumeroFallas,
    ISNULL(m.HorasCorrectivas, 0) AS HorasCorrectivas
FROM dbo.equipos AS e
LEFT JOIN ResumenHorometros AS h
    ON h.Eco = e.Eco
LEFT JOIN ResumenMantenimientos AS m
    ON m.Eco = e.Eco
ORDER BY e.Eco;

SELECT
    m.Eco,
    e.modelo AS Modelo,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(tm.tipo)), N'') IS NULL
            THEN N'SIN TIPO'
        ELSE UPPER(LTRIM(RTRIM(tm.tipo)))
    END AS TipoMantenimiento,
    CAST
    (
        COALESCE
        (
            m.tiempo,
            CASE
                WHEN m.inicio IS NOT NULL AND m.fin IS NOT NULL THEN
                    CASE
                        WHEN DATEDIFF(MINUTE, m.inicio, m.fin) >= 0
                            THEN DATEDIFF(MINUTE, m.inicio, m.fin) / 60.0
                        ELSE
                            (DATEDIFF(MINUTE, m.inicio, m.fin) + 1440) / 60.0
                    END
                ELSE 0
            END
        )
        AS decimal(18, 2)
    ) AS Horas
FROM dbo.mantenimientos AS m
INNER JOIN dbo.equipos AS e
    ON e.Eco = m.Eco
LEFT JOIN dbo.tipo_mantenimiento AS tm
    ON tm.Id_tipo_mantenimiento = m.Id_tipo_mantenimiento
WHERE m.Eco IS NOT NULL
  AND m.fecha >= @Inicio
  AND m.fecha < @FinExclusivo
ORDER BY m.Eco, TipoMantenimiento;";

            DataSet datos = new DataSet();

            using SqlConnection conexion =
                new SqlConnection(CadenaConexion);
            using SqlCommand comando =
                new SqlCommand(consultaSql, conexion);

            comando.Parameters.Add("@Inicio", SqlDbType.Date).Value = inicio;
            comando.Parameters.Add(
                "@FinExclusivo",
                SqlDbType.Date
            ).Value = finExclusivo;

            using SqlDataAdapter adaptador =
                new SqlDataAdapter(comando);
            adaptador.Fill(datos);

            if (datos.Tables.Count < 2)
            {
                throw new InvalidOperationException(
                    "La consulta no devolvió los datos necesarios."
                );
            }

            return datos;
        }

        private static List<EquipoKpi> ConstruirEquipos(
            DataTable origen,
            int dias,
            decimal horasOperacionPorDia)
        {
            List<EquipoKpi> equipos = new List<EquipoKpi>();
            decimal horasCalendario = dias * 24m;
            decimal horasOperacion = dias * horasOperacionPorDia;

            foreach (DataRow fila in origen.Rows)
            {
                decimal? smrInicial = DecimalNullable(fila, "SMRInicial");
                decimal? smrFinal = DecimalNullable(fila, "SMRFinal");
                decimal horasTrabajadas =
                    smrInicial.HasValue && smrFinal.HasValue
                        ? Math.Max(0m, smrFinal.Value - smrInicial.Value)
                        : 0m;
                decimal horasMantenimiento =
                    Decimal(fila, "HorasMantenimiento");
                int numeroFallas = Entero(fila, "NumeroFallas");
                decimal horasCorrectivas =
                    Decimal(fila, "HorasCorrectivas");
                decimal? mtbf = numeroFallas > 0
                    ? horasTrabajadas / numeroFallas
                    : null;
                decimal? mttr = numeroFallas > 0
                    ? horasCorrectivas / numeroFallas
                    : null;

                equipos.Add(new EquipoKpi
                {
                    Eco = Convert.ToInt32(fila["Eco"]),
                    Modelo = Texto(fila, "Modelo", "SIN MODELO"),
                    HorasTrabajadas = horasTrabajadas,
                    HorasMantenimiento = horasMantenimiento,
                    HorasCalendario = horasCalendario,
                    HorasOperacion = horasOperacion,
                    NumeroFallas = numeroFallas,
                    HorasCorrectivas = horasCorrectivas,
                    DisponibilidadFisica = PorcentajeLimitado(
                        horasCalendario - horasMantenimiento,
                        horasCalendario
                    ),
                    DisponibilidadMecanica = PorcentajeNullable(
                        horasTrabajadas,
                        horasTrabajadas + horasMantenimiento
                    ),
                    Utilizacion = PorcentajeLimitado(
                        horasTrabajadas,
                        horasOperacion
                    ),
                    Mtbf = mtbf,
                    Mttr = mttr,
                    Confiabilidad = CalcularConfiabilidad(mtbf, mttr),
                    Fiabilidad = PorcentajeLimitado(
                        horasCalendario - horasCorrectivas,
                        horasCalendario
                    )
                });
            }

            return equipos;
        }

        private static List<ModeloKpi> ConstruirModelos(
            IEnumerable<EquipoKpi> equipos)
        {
            List<ModeloKpi> modelos = new List<ModeloKpi>();

            var grupos = equipos.GroupBy(
                equipo => equipo.Modelo,
                StringComparer.OrdinalIgnoreCase
            );

            foreach (var grupo in grupos.OrderBy(g => g.Key))
            {
                decimal horasTrabajadas =
                    grupo.Sum(e => e.HorasTrabajadas);
                decimal horasMantenimiento =
                    grupo.Sum(e => e.HorasMantenimiento);
                decimal horasCalendario =
                    grupo.Sum(e => e.HorasCalendario);
                decimal horasOperacion =
                    grupo.Sum(e => e.HorasOperacion);
                int numeroFallas = grupo.Sum(e => e.NumeroFallas);
                decimal horasCorrectivas =
                    grupo.Sum(e => e.HorasCorrectivas);
                decimal? mtbf = numeroFallas > 0
                    ? horasTrabajadas / numeroFallas
                    : null;
                decimal? mttr = numeroFallas > 0
                    ? horasCorrectivas / numeroFallas
                    : null;

                modelos.Add(new ModeloKpi
                {
                    Modelo = grupo.Key,
                    CantidadEquipos = grupo.Count(),
                    DisponibilidadFisica = PorcentajeLimitado(
                        horasCalendario - horasMantenimiento,
                        horasCalendario
                    ),
                    DisponibilidadMecanica = PorcentajeNullable(
                        horasTrabajadas,
                        horasTrabajadas + horasMantenimiento
                    ),
                    Utilizacion = PorcentajeLimitado(
                        horasTrabajadas,
                        horasOperacion
                    ),
                    Mtbf = mtbf,
                    Mttr = mttr,
                    Confiabilidad = CalcularConfiabilidad(mtbf, mttr),
                    Fiabilidad = PorcentajeLimitado(
                        horasCalendario - horasCorrectivas,
                        horasCalendario
                    )
                });
            }

            return modelos;
        }

        private static List<MantenimientoDetalle> ConstruirMantenimientos(
            DataTable origen)
        {
            return origen.AsEnumerable()
                .Select(fila => new MantenimientoDetalle
                {
                    Eco = Convert.ToInt32(fila["Eco"]),
                    Modelo = Texto(fila, "Modelo", "SIN MODELO"),
                    Tipo = Texto(
                        fila,
                        "TipoMantenimiento",
                        "SIN TIPO"
                    ),
                    Horas = Decimal(fila, "Horas")
                })
                .ToList();
        }

        private static DataTable CrearTablaPorEco(
            IEnumerable<EquipoKpi> equipos)
        {
            DataTable tabla = CrearTablaIndicadores(
                "PorEco",
                ("Eco", typeof(int)),
                ("Modelo", typeof(string))
            );

            foreach (EquipoKpi equipo in equipos.OrderBy(e => e.Eco))
            {
                DataRow fila = tabla.NewRow();
                fila["Eco"] = equipo.Eco;
                fila["Modelo"] = equipo.Modelo;
                AsignarIndicadores(fila, equipo);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearTablaPorModelo(
            IEnumerable<ModeloKpi> modelos)
        {
            DataTable tabla = CrearTablaIndicadores(
                "PorModelo",
                ("Modelo", typeof(string)),
                ("CantidadEquipos", typeof(int))
            );

            foreach (ModeloKpi modelo in modelos.OrderBy(m => m.Modelo))
            {
                DataRow fila = tabla.NewRow();
                fila["Modelo"] = modelo.Modelo;
                fila["CantidadEquipos"] = modelo.CantidadEquipos;
                AsignarIndicadores(fila, modelo);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearTablaIndicadores(
            string nombre,
            params (string Nombre, Type Tipo)[] identificadores)
        {
            DataTable tabla = new DataTable(nombre);

            foreach (var columna in identificadores)
            {
                tabla.Columns.Add(columna.Nombre, columna.Tipo);
            }

            tabla.Columns.Add("DisponibilidadFisica", typeof(decimal));
            tabla.Columns.Add("DisponibilidadMecanica", typeof(decimal));
            tabla.Columns.Add("Utilizacion", typeof(decimal));
            tabla.Columns.Add("MTBF", typeof(decimal));
            tabla.Columns.Add("MTTR", typeof(decimal));
            tabla.Columns.Add("Confiabilidad", typeof(decimal));
            tabla.Columns.Add("Fiabilidad", typeof(decimal));
            return tabla;
        }

        private static void AsignarIndicadores(
            DataRow fila,
            IValoresKpi valores)
        {
            fila["DisponibilidadFisica"] = valores.DisponibilidadFisica;
            Asignar(fila, "DisponibilidadMecanica",
                valores.DisponibilidadMecanica);
            fila["Utilizacion"] = valores.Utilizacion;
            Asignar(fila, "MTBF", valores.Mtbf);
            Asignar(fila, "MTTR", valores.Mttr);
            Asignar(fila, "Confiabilidad", valores.Confiabilidad);
            fila["Fiabilidad"] = valores.Fiabilidad;
        }

        private static DataTable CrearMantenimientoPorEco(
            IEnumerable<MantenimientoDetalle> mantenimientos)
        {
            DataTable tabla = CrearTablaMantenimiento(
                "MantenimientoEcoTipo",
                ("Eco", typeof(int)),
                ("Modelo", typeof(string))
            );

            var grupos = mantenimientos.GroupBy(m => new
            {
                m.Eco,
                m.Modelo,
                m.Tipo
            });

            foreach (var grupo in grupos
                .OrderBy(g => g.Key.Eco)
                .ThenBy(g => g.Key.Tipo))
            {
                DataRow fila = tabla.NewRow();
                fila["Eco"] = grupo.Key.Eco;
                fila["Modelo"] = grupo.Key.Modelo;
                fila["TipoMantenimiento"] = grupo.Key.Tipo;
                fila["Cantidad"] = grupo.Count();
                fila["Horas"] = grupo.Sum(m => m.Horas);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearMantenimientoPorModelo(
            IEnumerable<MantenimientoDetalle> mantenimientos)
        {
            DataTable tabla = CrearTablaMantenimiento(
                "MantenimientoModeloTipo",
                ("Modelo", typeof(string))
            );

            var grupos = mantenimientos.GroupBy(m => new
            {
                m.Modelo,
                m.Tipo
            });

            foreach (var grupo in grupos
                .OrderBy(g => g.Key.Modelo)
                .ThenBy(g => g.Key.Tipo))
            {
                DataRow fila = tabla.NewRow();
                fila["Modelo"] = grupo.Key.Modelo;
                fila["TipoMantenimiento"] = grupo.Key.Tipo;
                fila["Cantidad"] = grupo.Count();
                fila["Horas"] = grupo.Sum(m => m.Horas);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearTablaMantenimiento(
            string nombre,
            params (string Nombre, Type Tipo)[] identificadores)
        {
            DataTable tabla = new DataTable(nombre);

            foreach (var columna in identificadores)
            {
                tabla.Columns.Add(columna.Nombre, columna.Tipo);
            }

            tabla.Columns.Add("TipoMantenimiento", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(int));
            tabla.Columns.Add("Horas", typeof(decimal));
            return tabla;
        }

        private static decimal PorcentajeLimitado(
            decimal numerador,
            decimal denominador)
        {
            if (denominador == 0m)
            {
                return 0m;
            }

            decimal porcentaje = numerador / denominador * 100m;
            return Math.Min(100m, Math.Max(0m, porcentaje));
        }

        private static decimal? PorcentajeNullable(
            decimal numerador,
            decimal denominador)
        {
            return denominador == 0m
                ? null
                : PorcentajeLimitado(numerador, denominador);
        }

        private static decimal? CalcularConfiabilidad(
            decimal? mtbf,
            decimal? mttr)
        {
            if (!mtbf.HasValue || !mttr.HasValue ||
                mtbf.Value + mttr.Value == 0m)
            {
                return null;
            }

            return PorcentajeLimitado(
                mtbf.Value,
                mtbf.Value + mttr.Value
            );
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

        private static int Entero(DataRow fila, string columna)
        {
            return fila.IsNull(columna)
                ? 0
                : Convert.ToInt32(fila[columna]);
        }

        private static string Texto(
            DataRow fila,
            string columna,
            string valorPredeterminado)
        {
            string texto = fila.IsNull(columna)
                ? ""
                : Convert.ToString(fila[columna]) ?? "";

            return string.IsNullOrWhiteSpace(texto)
                ? valorPredeterminado
                : texto.Trim();
        }

        private static void Asignar(
            DataRow fila,
            string columna,
            decimal? valor)
        {
            fila[columna] = valor.HasValue
                ? valor.Value
                : DBNull.Value;
        }

        private interface IValoresKpi
        {
            decimal DisponibilidadFisica { get; }
            decimal? DisponibilidadMecanica { get; }
            decimal Utilizacion { get; }
            decimal? Mtbf { get; }
            decimal? Mttr { get; }
            decimal? Confiabilidad { get; }
            decimal Fiabilidad { get; }
        }

        private sealed class EquipoKpi : IValoresKpi
        {
            public int Eco { get; init; }
            public string Modelo { get; init; } = "";
            public decimal HorasTrabajadas { get; init; }
            public decimal HorasMantenimiento { get; init; }
            public decimal HorasCalendario { get; init; }
            public decimal HorasOperacion { get; init; }
            public int NumeroFallas { get; init; }
            public decimal HorasCorrectivas { get; init; }
            public decimal DisponibilidadFisica { get; init; }
            public decimal? DisponibilidadMecanica { get; init; }
            public decimal Utilizacion { get; init; }
            public decimal? Mtbf { get; init; }
            public decimal? Mttr { get; init; }
            public decimal? Confiabilidad { get; init; }
            public decimal Fiabilidad { get; init; }
        }

        private sealed class ModeloKpi : IValoresKpi
        {
            public string Modelo { get; init; } = "";
            public int CantidadEquipos { get; init; }
            public decimal DisponibilidadFisica { get; init; }
            public decimal? DisponibilidadMecanica { get; init; }
            public decimal Utilizacion { get; init; }
            public decimal? Mtbf { get; init; }
            public decimal? Mttr { get; init; }
            public decimal? Confiabilidad { get; init; }
            public decimal Fiabilidad { get; init; }
        }

        private sealed class MantenimientoDetalle
        {
            public int Eco { get; init; }
            public string Modelo { get; init; } = "";
            public string Tipo { get; init; } = "";
            public decimal Horas { get; init; }
        }
    }
}
