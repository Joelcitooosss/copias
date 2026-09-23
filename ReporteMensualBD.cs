using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Indicadores_Escoria
{
    internal static class ReporteMensualBD
    {
        private const string CadenaConexion =
            @"Data Source=MXLF-WS-RSD01\SQLEXPRESS;" +
            @"Initial Catalog=ManejoEscoria;" +
            @"Integrated Security=True;" +
            @"TrustServerCertificate=True";

        public static DataTable ObtenerReporte(
            DateTime fechaInicio,
            DateTime fechaFin,
            decimal horasOperacionPorDia)
        {
            DateTime inicio = fechaInicio.Date;
            DateTime fin = fechaFin.Date;

            if (fin < inicio)
            {
                throw new ArgumentException(
                    "La fecha final no puede ser anterior a la fecha inicial.",
                    nameof(fechaFin)
                );
            }

            if (horasOperacionPorDia <= 0m ||
                horasOperacionPorDia > 24m)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(horasOperacionPorDia),
                    "Las horas operables por día deben ser mayores que 0 " +
                    "y menores o iguales a 24."
                );
            }

            DateTime finExclusivo = fin.AddDays(1);
            int dias = (fin - inicio).Days + 1;

            // Disponibilidad física y fiabilidad siempre usan 24 horas.
            decimal horasCalendario = dias * 24m;

            // Utilización, reserva y aprovechamiento usan 21 horas por día
            // de forma predeterminada, pero este valor se puede ajustar.
            decimal horasOperacion = dias * horasOperacionPorDia;

            DataTable datosBase = ConsultarDatosBase(inicio, finExclusivo);
            DataTable reporte = CrearEstructura();
            int item = 1;

            foreach (DataRow origen in datosBase.Rows)
            {
                decimal? smrInicial = ObtenerDecimalNullable(
                    origen,
                    "SMRInicial"
                );

                decimal? smrFinal = ObtenerDecimalNullable(
                    origen,
                    "SMRFinal"
                );

                decimal horasTrabajadas =
                    smrInicial.HasValue && smrFinal.HasValue
                        ? Math.Max(0m, smrFinal.Value - smrInicial.Value)
                        : 0m;

                decimal horasMantenimiento = ObtenerDecimal(
                    origen,
                    "HorasMantenimiento"
                );

                int numeroFallas = ObtenerEntero(
                    origen,
                    "NumeroFallas"
                );

                decimal horasCorrectivas = ObtenerDecimal(
                    origen,
                    "HorasCorrectivas"
                );

                int numeroPreventivos = ObtenerEntero(
                    origen,
                    "NumeroPreventivos"
                );

                decimal horasPreventivas = ObtenerDecimal(
                    origen,
                    "HorasPreventivas"
                );

                decimal disponibilidadFisica = LimitarPorcentaje(
                    Porcentaje(
                        horasCalendario - horasMantenimiento,
                        horasCalendario
                    )
                );
                decimal DisponibilidadMecanica = LimitarPorcentaje(
                    Porcentaje(
                        horasTrabajadas, horasTrabajadas+horasMantenimiento
                        
                    )
                );

                decimal utilizacion = LimitarPorcentaje(
                    Porcentaje(horasTrabajadas, horasOperacion)
                );

                decimal reserva =
                    horasOperacion - horasTrabajadas - horasMantenimiento;

                decimal? aprovechamiento =
                    horasTrabajadas + reserva > 0m
                        ? LimitarPorcentaje(
                            Porcentaje(
                                horasTrabajadas,
                                horasTrabajadas + reserva
                            )
                        )
                        : null;

                decimal fiabilidad = LimitarPorcentaje(
                    Porcentaje(
                        horasCalendario - horasCorrectivas,
                        horasCalendario
                    )
                );

                DataRow fila = reporte.NewRow();

                fila["Item"] = item++;
                fila["Equipo"] = Texto(origen, "Equipo");
                fila["Modelo"] = Texto(origen, "Modelo");
                fila["Serie"] = Texto(origen, "Serie");
                fila["Eco"] = Convert.ToInt32(origen["Eco"]);
                fila["Proyecto"] = Texto(origen, "Proyecto");
                fila["Propiedad"] = Texto(origen, "Propiedad");
                fila["Cliente"] = Texto(origen, "Cliente");
                AsignarNullable(fila, "SMRInicial", smrInicial);
                AsignarNullable(fila, "SMRFinal", smrFinal);
                fila["HorasTrabajadas"] = horasTrabajadas;
                fila["HorasMantenimiento"] = horasMantenimiento;
                fila["DisponibilidadFisica"] = disponibilidadFisica;
                fila["DisponibilidadMecanica"] = DisponibilidadMecanica;
                fila["Utilizacion"] = utilizacion;
                fila["Reserva"] = reserva;
                AsignarNullable(fila, "Aprovechamiento", aprovechamiento);
                fila["NumeroFallas"] = numeroFallas;
                fila["HorasCorrectivas"] = horasCorrectivas;
                fila["NumeroPreventivos"] = numeroPreventivos;
                fila["HorasPreventivas"] = horasPreventivas;
                fila["Fiabilidad"] = fiabilidad;

                // Auxiliares de cálculo; no se muestran como columnas.

                reporte.Rows.Add(fila);
            }

            AgregarTotales(reporte);
            return reporte;
        }

        private static DataTable ConsultarDatosBase(
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
                WHEN TipoMantenimiento LIKE N'CORRECTIVO%'
                    THEN 1
                ELSE 0
            END
        ) AS NumeroFallas,
        SUM
        (
            CASE
                WHEN TipoMantenimiento LIKE N'CORRECTIVO%'
                    THEN Horas
                ELSE 0
            END
        ) AS HorasCorrectivas,
        SUM
        (
            CASE
                WHEN TipoMantenimiento LIKE N'PREVENTIVO%'
                    THEN 1
                ELSE 0
            END
        ) AS NumeroPreventivos,
        SUM
        (
            CASE
                WHEN TipoMantenimiento LIKE N'PREVENTIVO%'
                    THEN Horas
                ELSE 0
            END
        ) AS HorasPreventivas
    FROM MantenimientoBase
    GROUP BY Eco
)
SELECT
    e.Eco,
    e.tipo_equipo AS Equipo,
    e.modelo AS Modelo,
    e.serie AS Serie,
    e.proyecto AS Proyecto,
    e.propiedad AS Propiedad,
    e.cliente AS Cliente,
    h.SMRInicial,
    h.SMRFinal,
    ISNULL(m.HorasMantenimiento, 0) AS HorasMantenimiento,
    ISNULL(m.NumeroFallas, 0) AS NumeroFallas,
    ISNULL(m.HorasCorrectivas, 0) AS HorasCorrectivas,
    ISNULL(m.NumeroPreventivos, 0) AS NumeroPreventivos,
    ISNULL(m.HorasPreventivas, 0) AS HorasPreventivas
FROM dbo.equipos AS e
LEFT JOIN ResumenHorometros AS h
    ON h.Eco = e.Eco
LEFT JOIN ResumenMantenimientos AS m
    ON m.Eco = e.Eco
ORDER BY e.Eco;";

            DataTable tabla = new DataTable();

            using SqlConnection conexion =
                new SqlConnection(CadenaConexion);

            using SqlCommand comando =
                new SqlCommand(consultaSql, conexion);

            comando.Parameters.Add(
                "@Inicio",
                SqlDbType.Date
            ).Value = inicio.Date;

            comando.Parameters.Add(
                "@FinExclusivo",
                SqlDbType.Date
            ).Value = finExclusivo.Date;

            using SqlDataAdapter adaptador =
                new SqlDataAdapter(comando);

            adaptador.Fill(tabla);
            return tabla;
        }

        private static DataTable CrearEstructura()
        {
            DataTable tabla = new DataTable("ReportePeriodo");

            tabla.Columns.Add("Item", typeof(int));
            tabla.Columns.Add("Equipo", typeof(string));
            tabla.Columns.Add("Modelo", typeof(string));
            tabla.Columns.Add("Serie", typeof(string));
            tabla.Columns.Add("Eco", typeof(int));
            tabla.Columns.Add("Proyecto", typeof(string));
            tabla.Columns.Add("Propiedad", typeof(string));
            tabla.Columns.Add("Cliente", typeof(string));
            tabla.Columns.Add("SMRInicial", typeof(decimal));
            tabla.Columns.Add("SMRFinal", typeof(decimal));
            tabla.Columns.Add("HorasTrabajadas", typeof(decimal));
            tabla.Columns.Add("HorasMantenimiento", typeof(decimal));
            tabla.Columns.Add("DisponibilidadFisica", typeof(decimal));
            tabla.Columns.Add("DisponibilidadMecanica", typeof(decimal));
            tabla.Columns.Add("Utilizacion", typeof(decimal));
            tabla.Columns.Add("Reserva", typeof(decimal));
            tabla.Columns.Add("Aprovechamiento", typeof(decimal));
            tabla.Columns.Add("NumeroFallas", typeof(int));
            tabla.Columns.Add("HorasCorrectivas", typeof(decimal));
            tabla.Columns.Add("NumeroPreventivos", typeof(int));
            tabla.Columns.Add("HorasPreventivas", typeof(decimal));
            tabla.Columns.Add("Fiabilidad", typeof(decimal));

            return tabla;
        }

        private static void AgregarTotales(DataTable tabla)
        {
            if (tabla.Rows.Count == 0)
            {
                return;
            }

            DataRow total = tabla.NewRow();

            total["Cliente"] = "Totales / Promedios";
            total["HorasTrabajadas"] = Sumar(tabla, "HorasTrabajadas");
            total["HorasMantenimiento"] =
                Sumar(tabla, "HorasMantenimiento");
            total["DisponibilidadFisica"] =
                Promedio(tabla, "DisponibilidadFisica") ?? 0m;
            total["DisponibilidadMecanica"] =
                Promedio(tabla, "DisponibilidadMecanica") ?? 0m;
            total["Utilizacion"] =
                Promedio(tabla, "Utilizacion") ?? 0m;
            total["Reserva"] = Sumar(tabla, "Reserva");
            AsignarNullable(
                total,
                "Aprovechamiento",
                Promedio(tabla, "Aprovechamiento")
            );
            total["NumeroFallas"] = SumarEnteros(tabla, "NumeroFallas");
            total["HorasCorrectivas"] =
                Sumar(tabla, "HorasCorrectivas");
            total["NumeroPreventivos"] =
                SumarEnteros(tabla, "NumeroPreventivos");
            total["HorasPreventivas"] =
                Sumar(tabla, "HorasPreventivas");
            total["Fiabilidad"] =
                Promedio(tabla, "Fiabilidad") ?? 0m;

            tabla.Rows.Add(total);
        }

        private static decimal Porcentaje(
            decimal numerador,
            decimal denominador)
        {
            return denominador == 0m
                ? 0m
                : numerador / denominador * 100m;
        }

        private static decimal LimitarPorcentaje(decimal valor)
        {
            return Math.Min(100m, Math.Max(0m, valor));
        }

        private static decimal ObtenerDecimal(DataRow fila, string columna)
        {
            return fila.IsNull(columna)
                ? 0m
                : Convert.ToDecimal(fila[columna]);
        }

        private static decimal? ObtenerDecimalNullable(
            DataRow fila,
            string columna)
        {
            return fila.IsNull(columna)
                ? null
                : Convert.ToDecimal(fila[columna]);
        }

        private static int ObtenerEntero(DataRow fila, string columna)
        {
            return fila.IsNull(columna)
                ? 0
                : Convert.ToInt32(fila[columna]);
        }

        private static string Texto(DataRow fila, string columna)
        {
            return fila.IsNull(columna)
                ? ""
                : Convert.ToString(fila[columna]) ?? "";
        }

        private static void AsignarNullable(
            DataRow fila,
            string columna,
            decimal? valor)
        {
            fila[columna] = valor.HasValue
                ? valor.Value
                : DBNull.Value;
        }

        private static decimal Sumar(DataTable tabla, string columna)
        {
            decimal total = 0m;

            foreach (DataRow fila in tabla.Rows)
            {
                if (!fila.IsNull(columna))
                {
                    total += Convert.ToDecimal(fila[columna]);
                }
            }

            return total;
        }

        private static int SumarEnteros(DataTable tabla, string columna)
        {
            int total = 0;

            foreach (DataRow fila in tabla.Rows)
            {
                if (!fila.IsNull(columna))
                {
                    total += Convert.ToInt32(fila[columna]);
                }
            }

            return total;
        }

        private static decimal? Promedio(DataTable tabla, string columna)
        {
            decimal suma = 0m;
            int cantidad = 0;

            foreach (DataRow fila in tabla.Rows)
            {
                if (!fila.IsNull(columna))
                {
                    suma += Convert.ToDecimal(fila[columna]);
                    cantidad++;
                }
            }

            return cantidad == 0 ? null : suma / cantidad;
        }
    }
}
