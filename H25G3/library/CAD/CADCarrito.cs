using Library.EN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.EN;
using System.Configuration;
using System.Collections;

namespace library.CAD
{
    public class CADCarrito
    {
        private string connectionString;

        public CADCarrito()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool Crear(ENCarrito carro)
        {
            bool creado = true;
            SqlConnection conx = null;
            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    conx.Open();

                    string consult = @"INSERT INTO carrito (usuario_UName) 
                             VALUES (@usuario_UName);
                             SELECT SCOPE_IDENTITY();";  // Cambio aquí

                    SqlCommand sc = new SqlCommand(consult, conx);
                    sc.Parameters.AddWithValue("@usuario_UName", carro.Usuario_UName);

                    // Ejecutar y obtener el ID
                    object result = sc.ExecuteScalar();
                    if (result != null)
                    {
                        carro.Carrito_ID = Convert.ToInt32(result);
                    }
                    System.Diagnostics.Debug.WriteLine($"{carro.Carrito_ID}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("carrito operation has failed. ERROR: {0}", ex.Message);
                creado = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("carrito operation has failed. ERROR: {0}", ex.Message);
                creado = false;
            }
            return creado;
        }


        public bool Eliminar(ENCarrito carro)
        {
            bool deleted = true;
            SqlConnection conx = null;
            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    conx.Open();
                    string consult = "DELETE FROM [dbo].[carrito] WHERE carrito_id = @carrito_id";
                    SqlCommand sc = new SqlCommand(consult, conx);
                    sc.Parameters.AddWithValue("@carrito_id", carro.Carrito_ID);
                    sc.ExecuteNonQuery();
                    deleted = true;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("carrito operation has failed. ERROR: {0}", ex.Message);
                deleted = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("carrito operation has failed. ERROR: {0}", ex.Message);
                deleted = false;
            }
            finally
            {
                if (conx != null && conx.State == ConnectionState.Open)
                {
                    conx.Close();
                }
            }
            return deleted;
        }

        public bool Leer(ENCarrito carro)
        {
            bool found = false;
            SqlConnection conx = null;
            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    conx.Open();
                    //consulta para obtener un producto por su código
                    string consult = "SELECT * FROM [dbo].[carrito] WHERE carrito_id = @id";
                    SqlCommand cs = new SqlCommand(consult, conx);
                    cs.Parameters.AddWithValue("@id", carro.Carrito_ID);
                    SqlDataReader read = cs.ExecuteReader();
                    found = true;
                }
            }
            catch (SqlException ex)
            {
                found = false;
                Console.WriteLine("carrito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                found = false;
                Console.WriteLine("carrito operation has failed. ERROR: {0}", ex.Message);
            }
            finally
            {
                if (conx != null && conx.State == ConnectionState.Open)
                {
                    conx.Close();
                }
            }
            return found;
        }

        // Obtiene articulos de un mismo carrito
        public List<ENLineaCarrito> ObtenerArticulos(ENCarrito carro)
        {
            SqlConnection conx = null;
            List<ENLineaCarrito> lista = new List<ENLineaCarrito>();
            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    conx.Open();
                    // Consulta corregida - seleccionamos de lineacarrito (l)
                    string consult = "SELECT * FROM lineacarrito WHERE carrito_id = @id";

                    SqlCommand cs = new SqlCommand(consult, conx);
                    cs.Parameters.AddWithValue("@id", carro.Carrito_ID);
                    SqlDataReader read = cs.ExecuteReader();

                    while (read.Read())
                    {
                        ENLineaCarrito articulo = new ENLineaCarrito();
                        articulo.Linea_carrito_id = Convert.ToInt32(read["linea_carrito_id"]);
                        articulo.Carrito_id = Convert.ToInt32(read["carrito_id"]);
                        articulo.Articulo_id = Convert.ToInt32(read["articulo_id"]);
                        articulo.Importe = Convert.ToSingle(read["importe"]);
                        articulo.Cantidad = Convert.ToInt32(read["cantidad"]);

                        lista.Add(articulo);
                    }

                    read.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al obtener artículos: {0}", ex.Message);
            }

            finally
            {
                if (conx != null && conx.State == ConnectionState.Open)
                {
                    conx.Close();
                }
            }
            return lista;
        }

        public bool ObtenerPorUsuario(ENCarrito carrito)
        {
            string username = carrito.Usuario_UName;
            SqlConnection con = null;
            bool found = false;

            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT carrito_id FROM carrito WHERE usuario_UName = @username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            carrito.Carrito_ID = Convert.ToInt32(reader["carrito_id"]);
                            carrito.Usuario_UName = username;
                            found = true;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"No se encontró carrito para el usuario: {username}");
                            found = false;
                        }
                    }
                }
                
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error SQL: {ex.Message}");
                found = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error general: {ex.Message}");
                found = false;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return found;
        }
    }
}
