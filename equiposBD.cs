using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Indicadores_Escoria
{
    internal class equiposBD
    {
        // Propiedades privadas para almacenar la información del equipo.
        private string Eco;
        private string Serie;
        private string Equipo;
        private string Modelo;
        private string Proyecto;
        private string Propiedad;
        private string Cliente;

        //MITTAL: Data Source=MXLF-WS-RSD01\SQLEXPRESS;Integrated Security=True;Trust Server Certificate=True
        //JOEL:   Data Source=localhost\SQLEXPRESS;Initial Catalog=ManejoEscoria;Integrated Security=True;TrustServerCertificate=True
        private SqlConnection con = new SqlConnection(
            @"Data Source=localhost\SQLEXPRESS;Initial Catalog=ManejoEscoria;Integrated Security=True;TrustServerCertificate=True"
        );
        // NOTE: Added Initial Catalog to ensure the connection opens the ManejoEscoria database.
        // Constructor que inicializa las propiedades del equipo.
        public equiposBD(
            string Eco,
            string Serie,
            string Equipo,
            string Modelo,
            string Proyecto,
            string Propiedad,
            string Cliente)
        {
            // Inicializa las propiedades del equipo con los valores proporcionados.
            this.Eco = Eco;
            this.Serie = Serie;
            this.Equipo = Equipo;
            this.Modelo = Modelo;
            this.Proyecto = Proyecto;
            this.Propiedad = Propiedad;
            this.Cliente = Cliente;
        }
        public equiposBD()
        {
            // Constructor vacío para permitir la creación de instancias sin parámetros.
        }
        public equiposBD(int Eco)
        {
            this.Eco = Eco.ToString();
        }
        // Método para agregar un equipo a la base de datos.
        public int agregarequipo()
        {
            // Abre la conexión a la base de datos y verifica si ya existe un equipo con el mismo Eco.
            try
            {
                con.Open();

                // Verifica si ya existe un equipo con ese Eco.
                using (SqlCommand rectificar = new SqlCommand(
                    "SELECT COUNT(*) FROM equipos WHERE Eco = @Eco", con))
                {
                    rectificar.Parameters.AddWithValue("@Eco", Eco);

                    int existentes = Convert.ToInt32(
                        rectificar.ExecuteScalar()
                    );
                    // Si ya existe, muestra un mensaje de error y termina el método.
                    if (existentes > 0)
                    {
                        MessageBox.Show(
                            "Este equipo ya existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        // Termina el método sin insertar.
                        return 0;
                    }
                }

                // Si no existe, registra el equipo.
                using (SqlCommand consulta = new SqlCommand(
                    @"INSERT INTO equipos (Eco, serie, tipo_equipo, modelo, proyecto, propiedad, cliente)
                     VALUES (@Eco, @serie, @tipo_equipo, @modelo, @proyecto, @propiedad, @cliente)", con))
                {
                    consulta.Parameters.AddWithValue("@Eco", Eco);
                    consulta.Parameters.AddWithValue("@serie", Serie);
                    consulta.Parameters.AddWithValue("@tipo_equipo", Equipo);
                    consulta.Parameters.AddWithValue("@modelo", Modelo);
                    consulta.Parameters.AddWithValue("@proyecto", Proyecto);
                    consulta.Parameters.AddWithValue("@propiedad", Propiedad);
                    consulta.Parameters.AddWithValue("@cliente", Cliente);

                    // Devuelve la cantidad de filas insertadas.
                    return consulta.ExecuteNonQuery();
                }
            }
            finally
            {
                // Se ejecuta también cuando hay un return o un error.
                con.Close();
            }
        }

        // Método para cargar los equipos desde la base de datos y mostrarlos en un DataGridView.
        public void cargarequipos(DataGridView dtg)
        {

            con.Open();
            // Consulta SQL para seleccionar todos los equipos.
            string consultaSql = @"
                SELECT
                    Eco AS 'Eco',
                    serie AS 'Serie',
                    tipo_equipo AS 'Equipo',
                    modelo AS 'Modelo',
                    proyecto AS 'Proyecto',
                    propiedad AS 'Propiedad',
                    cliente AS 'Cliente'
                FROM equipos";

            // Crea un adaptador de datos para ejecutar la consulta y llenar un DataTable.
            SqlDataAdapter adaptador = new SqlDataAdapter(consultaSql, con);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtg.DataSource = tabla;




        }
        // Método para eliminar un equipo de la base de datos.
        public int eliminarEquipo()
        {

            // Elimina un equipo de la base de datos según su Eco.
            con.Open();
            SqlCommand consulta = new SqlCommand(
            "DELETE FROM equipos WHERE Eco = @Eco", con);
            // Agrega el parámetro Eco a la consulta.
            consulta.Parameters.AddWithValue("Eco", Eco);
            int filasAfectadas = consulta.ExecuteNonQuery();
            con.Close();
            return filasAfectadas;



        }
        // Método para actualizar un equipo en la base de datos.
        public int editarEquipo()
        {
            // Actualiza un equipo en la base de datos según su Eco.
            con.Open();
            // Crea un comando SQL para actualizar los campos del equipo.
            SqlCommand consulta = new SqlCommand(
                @"UPDATE equipos 
                  SET serie = @serie, 
                      tipo_equipo = @tipo_equipo, 
                      modelo = @modelo, 
                      proyecto = @proyecto, 
                      propiedad = @propiedad, 
                      cliente = @cliente 
                  WHERE Eco = @Eco", con);

            // Agrega los parámetros a la consulta SQL.
            consulta.Parameters.AddWithValue("@Eco", Eco);
            consulta.Parameters.AddWithValue("@serie", Serie);
            consulta.Parameters.AddWithValue("@tipo_equipo", Equipo);
            consulta.Parameters.AddWithValue("@modelo", Modelo);
            consulta.Parameters.AddWithValue("@proyecto", Proyecto);
            consulta.Parameters.AddWithValue("@propiedad", Propiedad);
            consulta.Parameters.AddWithValue("@cliente", Cliente);
            // Ejecuta la consulta y obtiene el número de filas afectadas.
            int filasAfectadas = consulta.ExecuteNonQuery();
            con.Close();
            return filasAfectadas;
        }
        public (int totalEquipos, int totalModelos) ObtenerKPIs()
        {
            try
            {
                con.Open();

                int totalEquipos;
                int totalModelos;

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM equipos", con))
                {
                    totalEquipos = Convert.ToInt32(cmd.ExecuteScalar());
                }

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(DISTINCT modelo) FROM equipos", con))
                {
                    totalModelos = Convert.ToInt32(cmd.ExecuteScalar());
                }

                return (totalEquipos, totalModelos);
            }
            finally
            {
                con.Close();
            }
        }
        public int ObtenerEquiposActivos()
        {
            try
            {
                con.Open();

                string sql = @"
        WITH UltimoRegistroMes AS
        (
            SELECT
                Eco,
                Id_estatus_h,
                ROW_NUMBER() OVER
                (
                    PARTITION BY Eco
                    ORDER BY fecha_lectura DESC
                ) AS rn
            FROM horometros
            WHERE MONTH(fecha_lectura) = MONTH(GETDATE())
              AND YEAR(fecha_lectura) = YEAR(GETDATE())
        )
        SELECT COUNT(*)
        FROM UltimoRegistroMes
        WHERE rn = 1
          AND Id_estatus_h IN (1,3)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            finally
            {
                con.Close();
            }
        }
        public int ObtenerEquiposInactivos()
        {
            try
            {
                con.Open();

                string sql = @"
        WITH UltimoRegistroMes AS
        (
            SELECT
                Eco,
                Id_estatus_h,
                ROW_NUMBER() OVER
                (
                    PARTITION BY Eco
                    ORDER BY fecha_lectura DESC
                ) AS rn
            FROM horometros
            WHERE MONTH(fecha_lectura) = MONTH(GETDATE())
              AND YEAR(fecha_lectura) = YEAR(GETDATE())
        )
        SELECT COUNT(*)
        FROM UltimoRegistroMes
        WHERE rn = 1
          AND Id_estatus_h NOT IN (1,3)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            finally
            {
                con.Close();
            }
        }



    }
}