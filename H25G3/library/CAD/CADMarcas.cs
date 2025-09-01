using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Library.CAD
{
    public class CADMarcas
    {
        private string connectionString;

        public CADMarcas()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public DataTable ObtenerMarcasDesdeBaseDeDatos()
        {
            DataTable marcas = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT marca_id, nombre FROM marcas";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(marcas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las marcas desde la base de datos: " + ex.Message);
            }

            return marcas;
        }

        public string ObtenerMarca(int id)
        {
            string nombre = null;

            try
            {
                string query = "SELECT nombre FROM marcas WHERE marca_id = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombre = reader["nombre"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la marca desde la base de datos: " + ex.Message);
            }

            return nombre;
        }

        public int CheckAndInsertMarca(string nombre, SqlConnection conn)
        {
            int marcaId = 0;

            try
            {
                string checkQuery = "SELECT marca_id FROM marcas WHERE nombre = @nombre";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@nombre", nombre);
                    object result = checkCommand.ExecuteScalar();

                    if (result != null)
                    {
                        marcaId = Convert.ToInt32(result);
                    }
                    else
                    {
                        string maxIdQuery = "SELECT ISNULL(MAX(marca_id), 0) + 1 FROM marcas";
                        using (SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, conn))
                        {
                            marcaId = (int)maxIdCommand.ExecuteScalar();
                        }

                        string insertQuery = "INSERT INTO marcas (marca_id, nombre) VALUES (@marca_id, @nombre)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
                        {
                            insertCommand.Parameters.AddWithValue("@marca_id", marcaId);
                            insertCommand.Parameters.AddWithValue("@nombre", nombre);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al comprobar e insertar la marca: " + ex.Message);
            }

            return marcaId;
        }

        public int CheckAndInsertMarca(string nombre)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return CheckAndInsertMarca(nombre, conn);
            }
        }
    }
}
