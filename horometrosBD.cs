using System;
using System.Data;
using System.Data.SqlClient;

namespace Indicadores_Escoria
{
    internal class horometrosBD
    {
        //mittal: MXLF-WS-RSD01\SQLEXPRESS
        //Joel: localhost\SQLEXPRESS
        // Cambia el servidor si estás usando otra computadora.
        private static readonly string cadena =
            @"Server=MXLF-WS-RSD01\SQLEXPRESS;Database=ManejoEscoria;Integrated Security=True;TrustServerCertificate=True;";

        public static DataTable ObtenerHorometros(int anio, int mes)
        {
            DateTime inicio = new DateTime(anio, mes, 1);

            string consulta = @"
                SELECT
                    e.Eco,
                    e.serie,
                    e.tipo_equipo,
                    e.modelo,
                    h.fecha_lectura,
                    h.valor,
                    h.Id_estatus_h AS Id_estatus
                FROM dbo.equipos AS e
                LEFT JOIN dbo.horometros AS h
                    ON h.Eco = e.Eco
                    AND h.fecha_lectura >= @inicio
                    AND h.fecha_lectura < @fin
                ORDER BY e.Eco, h.fecha_lectura;";

            DataTable datos = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadena))
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.Add("@inicio", SqlDbType.Date).Value =
                    inicio;

                comando.Parameters.Add("@fin", SqlDbType.Date).Value =
                    inicio.AddMonths(1);

                conexion.Open();

                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(datos);
                }
            }

            return datos;
        }

    }

}

