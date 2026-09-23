using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Indicadores_Escoria
{
 
    internal static class HorometrosGuardadoBD
    {
 
        private const string CadenaConexion =
            @"Data Source=MXLF-WS-RSD01\SQLEXPRESS;" +
            @"Initial Catalog=ManejoEscoria;" +
            @"Integrated Security=True;" +
            @"TrustServerCertificate=True";

 
        public static void Guardar(
            int eco,
            DateTime fecha,
            decimal valor,
            int idEstatus,
            string? observaciones)
        {
            using SqlConnection conexion =
                new SqlConnection(CadenaConexion);

            conexion.Open();

 
            using SqlTransaction transaccion = conexion.BeginTransaction(
                IsolationLevel.Serializable
            );

            try
            {
                const string consultaSql = @"
                    UPDATE dbo.horometros
                    SET valor = @valor,
                        observaciones = @observaciones,
                        Id_estatus_h = @Id_estatus_h
                    WHERE Eco = @Eco
                      AND fecha_lectura = @fecha_lectura;

                    IF @@ROWCOUNT = 0
                    BEGIN
                        INSERT INTO dbo.horometros
                            (Eco, fecha_lectura, valor, observaciones, Id_estatus_h)
                        VALUES
                            (@Eco, @fecha_lectura, @valor, @observaciones, @Id_estatus_h);
                    END;";

                using SqlCommand comando = new SqlCommand(
                    consultaSql,
                    conexion,
                    transaccion
                );

                comando.Parameters.Add(
                    "@Eco",
                    SqlDbType.Int
                ).Value = eco;

                comando.Parameters.Add(
                    "@fecha_lectura",
                    SqlDbType.Date
                ).Value = fecha.Date;

                SqlParameter parametroValor = comando.Parameters.Add(
                    "@valor",
                    SqlDbType.Decimal
                );

                parametroValor.Precision = 10;
                parametroValor.Scale = 2;
                parametroValor.Value = valor;

                comando.Parameters.Add(
                    "@observaciones",
                    SqlDbType.NVarChar,
                    200
                ).Value = string.IsNullOrWhiteSpace(observaciones)
                    ? DBNull.Value
                    : observaciones.Trim();

                comando.Parameters.Add(
                    "@Id_estatus_h",
                    SqlDbType.Int
                ).Value = idEstatus;

                comando.ExecuteNonQuery();
                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
  
        public static Dictionary<(int Eco, DateTime Fecha), string>
            ObtenerObservacionesMes(int anio, int mes)
        {
            DateTime inicio = new DateTime(anio, mes, 1);
            DateTime fin = inicio.AddMonths(1);

            const string consultaSql = @"
                SELECT Eco, fecha_lectura, observaciones
                FROM dbo.horometros
                WHERE fecha_lectura >= @inicio
                  AND fecha_lectura < @fin;";

            Dictionary<(int Eco, DateTime Fecha), string> observaciones =
                new Dictionary<(int Eco, DateTime Fecha), string>();

            using SqlConnection conexion =
                new SqlConnection(CadenaConexion);

            using SqlCommand comando =
                new SqlCommand(consultaSql, conexion);

            comando.Parameters.Add(
                "@inicio",
                SqlDbType.Date
            ).Value = inicio.Date;

            comando.Parameters.Add(
                "@fin",
                SqlDbType.Date
            ).Value = fin.Date;

            conexion.Open();

            using SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                int eco = lector.GetInt32(0);
                DateTime fecha = lector.GetDateTime(1).Date;
                string observacion = lector.IsDBNull(2)
                    ? ""
                    : lector.GetString(2);

                observaciones[(eco, fecha)] = observacion;
            }

            return observaciones;
        }

 
        public static void ActualizarObservacion(
            int eco,
            DateTime fecha,
            string? observacion)
        {
            const string consultaSql = @"
                UPDATE dbo.horometros
                SET observaciones = @observaciones
                WHERE Eco = @Eco
                  AND fecha_lectura = @fecha_lectura;";

            using SqlConnection conexion =
                new SqlConnection(CadenaConexion);

            using SqlCommand comando =
                new SqlCommand(consultaSql, conexion);

            comando.Parameters.Add(
                "@Eco",
                SqlDbType.Int
            ).Value = eco;

            comando.Parameters.Add(
                "@fecha_lectura",
                SqlDbType.Date
            ).Value = fecha.Date;

            comando.Parameters.Add(
                "@observaciones",
                SqlDbType.NVarChar,
                200
            ).Value = string.IsNullOrWhiteSpace(observacion)
                ? DBNull.Value
                : observacion.Trim();

            conexion.Open();
            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas == 0)
            {
                throw new InvalidOperationException(
                    "La lectura todavía no existe. Primero escriba el " +
                    "horómetro y elija su estado."
                );
            }
        }


        public static string ObtenerObservacion(int eco, DateTime fecha)
        {
            const string consultaSql = @"
                SELECT observaciones
                FROM dbo.horometros
                WHERE Eco = @Eco
                  AND fecha_lectura = @fecha_lectura;";

            using SqlConnection conexion =
                new SqlConnection(CadenaConexion);

            using SqlCommand comando =
                new SqlCommand(consultaSql, conexion);

            comando.Parameters.Add(
                "@Eco",
                SqlDbType.Int
            ).Value = eco;

            comando.Parameters.Add(
                "@fecha_lectura",
                SqlDbType.Date
            ).Value = fecha.Date;

            conexion.Open();
            object? resultado = comando.ExecuteScalar();

            return resultado == null || resultado == DBNull.Value
                ? ""
                : Convert.ToString(resultado) ?? "";
        }
    }
}
