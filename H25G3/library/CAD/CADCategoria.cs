using Library.EN;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data.SqlClient;


namespace Library.CAD
{
    public class CADCategoria
    {

        private string connectionString;

        public CADCategoria()
        {
            // Verifica si la cadena de conexión existe
            if (ConfigurationManager.ConnectionStrings["Database"] == null)
                throw new Exception("Cadena de conexión 'Database' no encontrada en Web.config");

            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool Crear(ENCategoria categoria)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO categoria (nombre) VALUES (@nombre)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al crear categoría: " + ex.Message);
                return false;
            }
        }

        public bool Editar(ENCategoria categoria)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE categoria SET nombre = @nombre WHERE categoria_id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@id", categoria.CategoriaId);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al editar categoría: " + ex.Message);
                return false;
            }
        }

        public bool Eliminar(int categoriaId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM categoria WHERE categoria_id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", categoriaId);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al eliminar categoría: " + ex.Message);
                return false;
            }
        }

        public ENCategoria Leer(int categoriaId)
        {
            ENCategoria categoria = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT categoria_id, nombre FROM categoria WHERE categoria_id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", categoriaId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoria = new ENCategoria
                            {
                                CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                                Nombre = reader["nombre"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al leer categoría por ID: " + ex.Message);
            }

            return categoria;
        }


        public List<ENCategoria> ObtenerTodas()
        {
            List<ENCategoria> lista = new List<ENCategoria>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT categoria_id, nombre FROM categoria ORDER BY nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ENCategoria
                            {
                                CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                                Nombre = reader["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener todas las categorías: " + ex.Message);
            }

            return lista;
        }
        public ENCategoria Leer(string nombre)
        {
            ENCategoria categoria = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT categoria_id, nombre FROM categoria WHERE nombre = @nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoria = new ENCategoria
                            {
                                CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                                Nombre = reader["nombre"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al leer categoría por nombre: " + ex.Message);
            }

            return categoria;
        }

        public int CheckAndInsertCategoria(string nombre)
        {
            ENCategoria existente = Leer(nombre);

            if (existente != null)
                return existente.CategoriaId;

            ENCategoria nueva = new ENCategoria { Nombre = nombre };
            bool creada = Crear(nueva);

            if (!creada)
                throw new Exception("Error al crear la nueva categoría");

            // Leer de nuevo para obtener el ID
            ENCategoria insertada = Leer(nombre);
            return insertada?.CategoriaId ?? 0;
        }

    }
}
