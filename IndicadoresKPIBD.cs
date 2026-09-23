using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Indicadores_Escoria
{
    internal sealed class ResultadoIndicadoresKPI
    {
        public DataTable DisponibilidadPorModelo { get; init; } = new();
        public DataTable MantenimientoPorModelo { get; init; } = new();
        public DataTable MtbfPorEco { get; init; } = new();
        public DataTable MtbfPorModelo { get; init; } = new();
        public DataTable MttrPorEco { get; init; } = new();
        public DataTable MttrPorModelo { get; init; } = new();
        public DataTable ConfiabilidadPorEco { get; init; } = new();
        public DataTable ConfiabilidadPorModelo { get; init; } = new();
        public DataTable FiabilidadPorEco { get; init; } = new();
        public DataTable FiabilidadPorModelo { get; init; } = new();
    }

    internal static class IndicadoresKPIBD
    {
        private const string CadenaConexion =
            @"Data Source=MXLF-WS-RSD01\SQLEXPRESS;" +
            @"Initial Catalog=ManejoEscoria;" +
            @"Integrated Security=True;" +
            @"TrustServerCertificate=True";

        public static ResultadoIndicadoresKPI ObtenerIndicadores(
            DateTime fechaInicio,
            DateTime fechaFin,
            decimal horasOperacionPorDia)
        {
            DateTime inicio = fechaInicio.Date;
            DateTime fin = fechaFin.Date;

            if (fin < inicio)
            {
                throw new ArgumentException(
                    "La fecha final no puede ser anterior a la inicial."
                );
            }

            if (horasOperacionPorDia <= 0m ||
                horasOperacionPorDia > 24m)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(horasOperacionPorDia),
                    "Las horas operables deben estar entre 1 y 24."
                );
            }

            int dias = (fin - inicio).Days + 1;
            DateTime finExclusivo = fin.AddDays(1);

            DataSet datos = ConsultarDatos(inicio, finExclusivo);
            List<EquipoKpi> equipos = ConstruirEquipos(
                datos.Tables[0],
                dias,
                horasOperacionPorDia
            );

            List<ModeloKpi> modelos = ConstruirModelos(equipos);

            return new ResultadoIndicadoresKPI
            {
                DisponibilidadPorModelo =
                    CrearDisponibilidadPorModelo(modelos),
                MantenimientoPorModelo =
                    CrearMantenimientoPorModelo(datos.Tables[1]),
                MtbfPorEco = CrearMtbfPorEco(equipos),
                MtbfPorModelo = CrearMtbfPorModelo(modelos),
                MttrPorEco = CrearMttrPorEco(equipos),
                MttrPorModelo = CrearMttrPorModelo(modelos),
                ConfiabilidadPorEco =
                    CrearConfiabilidadPorEco(equipos),
                ConfiabilidadPorModelo =
                    CrearConfiabilidadPorModelo(modelos),
                FiabilidadPorEco = CrearFiabilidadPorEco(equipos),
                FiabilidadPorModelo = CrearFiabilidadPorModelo(modelos)
            };
        }

        private static DataSet ConsultarDatos(
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
    e.tipo_equipo AS Equipo,
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
ORDER BY e.modelo, e.Eco;

WITH MantenimientoDetalle AS
(
    SELECT
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
    WHERE m.fecha >= @Inicio
      AND m.fecha < @FinExclusivo
)
SELECT
    Modelo,
    TipoMantenimiento,
    Horas
FROM MantenimientoDetalle
ORDER BY Modelo, TipoMantenimiento;";

            DataSet datos = new DataSet();

            using SqlConnection conexion =
                new SqlConnection(CadenaConexion);

            using SqlCommand comando =
                new SqlCommand(consultaSql, conexion);

            comando.Parameters.Add(
                "@Inicio",
                SqlDbType.Date
            ).Value = inicio;

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
                    "La consulta no devolvió todas las tablas necesarias."
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

                decimal disponibilidadFisica = PorcentajeLimitado(
                    horasCalendario - horasMantenimiento,
                    horasCalendario
                );

                decimal? disponibilidadMecanica = PorcentajeNullable(
                    horasTrabajadas,
                    horasTrabajadas + horasMantenimiento
                );

                decimal utilizacion = PorcentajeLimitado(
                    horasTrabajadas,
                    horasOperacion
                );

                decimal? mtbf = numeroFallas > 0
                    ? horasTrabajadas / numeroFallas
                    : null;

                decimal? mttr = numeroFallas > 0
                    ? horasCorrectivas / numeroFallas
                    : null;

                decimal? confiabilidad = CalcularConfiabilidad(mtbf, mttr);

                decimal fiabilidad = PorcentajeLimitado(
                    horasCalendario - horasCorrectivas,
                    horasCalendario
                );

                equipos.Add(new EquipoKpi
                {
                    Eco = Convert.ToInt32(fila["Eco"]),
                    Equipo = Texto(fila, "Equipo"),
                    Modelo = Texto(fila, "Modelo", "SIN MODELO"),
                    HorasTrabajadas = horasTrabajadas,
                    HorasMantenimiento = horasMantenimiento,
                    HorasCalendario = horasCalendario,
                    HorasOperacion = horasOperacion,
                    NumeroFallas = numeroFallas,
                    HorasCorrectivas = horasCorrectivas,
                    DisponibilidadFisica = disponibilidadFisica,
                    DisponibilidadMecanica = disponibilidadMecanica,
                    Utilizacion = utilizacion,
                    Mtbf = mtbf,
                    Mttr = mttr,
                    Confiabilidad = confiabilidad,
                    Fiabilidad = fiabilidad
                });
            }

            return equipos;
        }

        private static List<ModeloKpi> ConstruirModelos(
            List<EquipoKpi> equipos)
        {
            List<ModeloKpi> modelos = new List<ModeloKpi>();

            IEnumerable<IGrouping<string, EquipoKpi>> grupos =
                equipos.GroupBy(
                    equipo => equipo.Modelo,
                    StringComparer.OrdinalIgnoreCase
                );

            foreach (IGrouping<string, EquipoKpi> grupo in
                     grupos.OrderBy(g => g.Key))
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

            return modelos;
        }

        private static DataTable CrearDisponibilidadPorModelo(
            IEnumerable<ModeloKpi> modelos)
        {
            DataTable tabla = CrearTabla(
                "DisponibilidadPorModelo",
                ("Modelo", typeof(string)),
                ("CantidadEquipos", typeof(int)),
                ("HorasTrabajadas", typeof(decimal)),
                ("HorasMantenimiento", typeof(decimal)),
                ("DisponibilidadFisica", typeof(decimal)),
                ("DisponibilidadMecanica", typeof(decimal)),
                ("Utilizacion", typeof(decimal))
            );

            foreach (ModeloKpi modelo in modelos)
            {
                DataRow fila = tabla.NewRow();
                fila["Modelo"] = modelo.Modelo;
                fila["CantidadEquipos"] = modelo.CantidadEquipos;
                fila["HorasTrabajadas"] = modelo.HorasTrabajadas;
                fila["HorasMantenimiento"] = modelo.HorasMantenimiento;
                fila["DisponibilidadFisica"] = modelo.DisponibilidadFisica;
                Asignar(fila, "DisponibilidadMecanica",
                    modelo.DisponibilidadMecanica);
                fila["Utilizacion"] = modelo.Utilizacion;
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearMantenimientoPorModelo(
            DataTable origen)
        {
            DataTable tabla = CrearTabla(
                "MantenimientoPorModelo",
                ("Modelo", typeof(string)),
                ("TipoMantenimiento", typeof(string)),
                ("Cantidad", typeof(int)),
                ("Horas", typeof(decimal))
            );

            IEnumerable<MantenimientoDetalle> detalles =
                origen.AsEnumerable().Select(fila =>
                    new MantenimientoDetalle
                    {
                        Modelo = Texto(fila, "Modelo", "SIN MODELO"),
                        Tipo = Texto(
                            fila,
                            "TipoMantenimiento",
                            "SIN TIPO"
                        ),
                        Horas = Decimal(fila, "Horas")
                    });

            var grupos = detalles.GroupBy(
                detalle => new { detalle.Modelo, detalle.Tipo }
            );

            foreach (var grupo in grupos
                .OrderBy(g => g.Key.Modelo)
                .ThenBy(g => g.Key.Tipo))
            {
                DataRow fila = tabla.NewRow();
                fila["Modelo"] = grupo.Key.Modelo;
                fila["TipoMantenimiento"] = grupo.Key.Tipo;
                fila["Cantidad"] = grupo.Count();
                fila["Horas"] = grupo.Sum(d => d.Horas);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearMtbfPorEco(
            IEnumerable<EquipoKpi> equipos)
        {
            DataTable tabla = CrearTablaEco(
                "MtbfPorEco",
                ("HorasTrabajadas", typeof(decimal)),
                ("NumeroFallas", typeof(int)),
                ("MTBF", typeof(decimal))
            );

            foreach (EquipoKpi equipo in equipos.OrderBy(e => e.Eco))
            {
                DataRow fila = NuevaFilaEco(tabla, equipo);
                fila["HorasTrabajadas"] = equipo.HorasTrabajadas;
                fila["NumeroFallas"] = equipo.NumeroFallas;
                Asignar(fila, "MTBF", equipo.Mtbf);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearMtbfPorModelo(
            IEnumerable<ModeloKpi> modelos)
        {
            DataTable tabla = CrearTablaModelo(
                "MtbfPorModelo",
                ("HorasTrabajadas", typeof(decimal)),
                ("NumeroFallas", typeof(int)),
                ("MTBF", typeof(decimal))
            );

            foreach (ModeloKpi modelo in modelos)
            {
                DataRow fila = NuevaFilaModelo(tabla, modelo);
                fila["HorasTrabajadas"] = modelo.HorasTrabajadas;
                fila["NumeroFallas"] = modelo.NumeroFallas;
                Asignar(fila, "MTBF", modelo.Mtbf);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearMttrPorEco(
            IEnumerable<EquipoKpi> equipos)
        {
            DataTable tabla = CrearTablaEco(
                "MttrPorEco",
                ("HorasCorrectivas", typeof(decimal)),
                ("NumeroFallas", typeof(int)),
                ("MTTR", typeof(decimal))
            );

            foreach (EquipoKpi equipo in equipos.OrderBy(e => e.Eco))
            {
                DataRow fila = NuevaFilaEco(tabla, equipo);
                fila["HorasCorrectivas"] = equipo.HorasCorrectivas;
                fila["NumeroFallas"] = equipo.NumeroFallas;
                Asignar(fila, "MTTR", equipo.Mttr);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearMttrPorModelo(
            IEnumerable<ModeloKpi> modelos)
        {
            DataTable tabla = CrearTablaModelo(
                "MttrPorModelo",
                ("HorasCorrectivas", typeof(decimal)),
                ("NumeroFallas", typeof(int)),
                ("MTTR", typeof(decimal))
            );

            foreach (ModeloKpi modelo in modelos)
            {
                DataRow fila = NuevaFilaModelo(tabla, modelo);
                fila["HorasCorrectivas"] = modelo.HorasCorrectivas;
                fila["NumeroFallas"] = modelo.NumeroFallas;
                Asignar(fila, "MTTR", modelo.Mttr);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearConfiabilidadPorEco(
            IEnumerable<EquipoKpi> equipos)
        {
            DataTable tabla = CrearTablaEco(
                "ConfiabilidadPorEco",
                ("MTBF", typeof(decimal)),
                ("MTTR", typeof(decimal)),
                ("Confiabilidad", typeof(decimal))
            );

            foreach (EquipoKpi equipo in equipos.OrderBy(e => e.Eco))
            {
                DataRow fila = NuevaFilaEco(tabla, equipo);
                Asignar(fila, "MTBF", equipo.Mtbf);
                Asignar(fila, "MTTR", equipo.Mttr);
                Asignar(fila, "Confiabilidad", equipo.Confiabilidad);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearConfiabilidadPorModelo(
            IEnumerable<ModeloKpi> modelos)
        {
            DataTable tabla = CrearTablaModelo(
                "ConfiabilidadPorModelo",
                ("MTBF", typeof(decimal)),
                ("MTTR", typeof(decimal)),
                ("Confiabilidad", typeof(decimal))
            );

            foreach (ModeloKpi modelo in modelos)
            {
                DataRow fila = NuevaFilaModelo(tabla, modelo);
                Asignar(fila, "MTBF", modelo.Mtbf);
                Asignar(fila, "MTTR", modelo.Mttr);
                Asignar(fila, "Confiabilidad", modelo.Confiabilidad);
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearFiabilidadPorEco(
            IEnumerable<EquipoKpi> equipos)
        {
            DataTable tabla = CrearTablaEco(
                "FiabilidadPorEco",
                ("HorasCalendario", typeof(decimal)),
                ("HorasCorrectivas", typeof(decimal)),
                ("Fiabilidad", typeof(decimal))
            );

            foreach (EquipoKpi equipo in equipos.OrderBy(e => e.Eco))
            {
                DataRow fila = NuevaFilaEco(tabla, equipo);
                fila["HorasCalendario"] = equipo.HorasCalendario;
                fila["HorasCorrectivas"] = equipo.HorasCorrectivas;
                fila["Fiabilidad"] = equipo.Fiabilidad;
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearFiabilidadPorModelo(
            IEnumerable<ModeloKpi> modelos)
        {
            DataTable tabla = CrearTablaModelo(
                "FiabilidadPorModelo",
                ("HorasCalendario", typeof(decimal)),
                ("HorasCorrectivas", typeof(decimal)),
                ("Fiabilidad", typeof(decimal))
            );

            foreach (ModeloKpi modelo in modelos)
            {
                DataRow fila = NuevaFilaModelo(tabla, modelo);
                fila["HorasCalendario"] = modelo.HorasCalendario;
                fila["HorasCorrectivas"] = modelo.HorasCorrectivas;
                fila["Fiabilidad"] = modelo.Fiabilidad;
                tabla.Rows.Add(fila);
            }

            return tabla;
        }

        private static DataTable CrearTablaEco(
            string nombre,
            params (string Nombre, Type Tipo)[] adicionales)
        {
            List<(string Nombre, Type Tipo)> columnas = new()
            {
                ("Eco", typeof(int)),
                ("Modelo", typeof(string))
            };

            columnas.AddRange(adicionales);
            return CrearTabla(nombre, columnas.ToArray());
        }

        private static DataTable CrearTablaModelo(
            string nombre,
            params (string Nombre, Type Tipo)[] adicionales)
        {
            List<(string Nombre, Type Tipo)> columnas = new()
            {
                ("Modelo", typeof(string)),
                ("CantidadEquipos", typeof(int))
            };

            columnas.AddRange(adicionales);
            return CrearTabla(nombre, columnas.ToArray());
        }

        private static DataTable CrearTabla(
            string nombre,
            params (string Nombre, Type Tipo)[] columnas)
        {
            DataTable tabla = new DataTable(nombre);

            foreach ((string Nombre, Type Tipo) columna in columnas)
            {
                tabla.Columns.Add(columna.Nombre, columna.Tipo);
            }

            return tabla;
        }

        private static DataRow NuevaFilaEco(
            DataTable tabla,
            EquipoKpi equipo)
        {
            DataRow fila = tabla.NewRow();
            fila["Eco"] = equipo.Eco;
            fila["Modelo"] = equipo.Modelo;
            return fila;
        }

        private static DataRow NuevaFilaModelo(
            DataTable tabla,
            ModeloKpi modelo)
        {
            DataRow fila = tabla.NewRow();
            fila["Modelo"] = modelo.Modelo;
            fila["CantidadEquipos"] = modelo.CantidadEquipos;
            return fila;
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
            string valorPredeterminado = "")
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

        private sealed class EquipoKpi
        {
            public int Eco { get; init; }
            public string Equipo { get; init; } = "";
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

        private sealed class ModeloKpi
        {
            public string Modelo { get; init; } = "";
            public int CantidadEquipos { get; init; }
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

        private sealed class MantenimientoDetalle
        {
            public string Modelo { get; init; } = "";
            public string Tipo { get; init; } = "";
            public decimal Horas { get; init; }
        }
    }
}
