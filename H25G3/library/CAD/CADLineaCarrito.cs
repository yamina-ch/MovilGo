using library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.CAD
{
    public class CADLineaCarrito
    {
        private string connectionString;
        public CADLineaCarrito()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool Crear(ENLineaCarrito linea)
        {
            bool created = false;
            SqlConnection conx = null;

            if (linea.Leer_User_Articulo())
            {
                return false;
            }

            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO lineacarrito (carrito_id, articulo_id, importe, cantidad) 
                            VALUES (@CarritoId, @ArticuloId, @Importe, @Cantidad);
                            SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@CarritoId", linea.Carrito_id);
                    cmd.Parameters.AddWithValue("@ArticuloId", linea.Articulo_id);
                    cmd.Parameters.AddWithValue("@Importe", linea.Importe);
                    cmd.Parameters.AddWithValue("@Cantidad", linea.Cantidad);

                    conx.Open();

                    // Ejecutar la consulta y obtener el ID generado
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        linea.Linea_carrito_id = Convert.ToInt32(result);
                        created = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                created = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                created = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            finally
            {
                if (conx != null && conx.State != ConnectionState.Closed)
                {
                    conx.Close();
                }
            }

            return created;
        }

        public bool Eliminar(ENLineaCarrito linea)
        {
            bool eliminated = false;
            SqlConnection conx = null;

            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM lineacarrito WHERE linea_carrito_id = @LineaCarritoId";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@LineaCarritoId", linea.Linea_carrito_id);

                    conx.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    eliminated = (rowsAffected > 0); // True si se eliminó al menos una fila
                }
            }
            catch (SqlException ex)
            {
                eliminated = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                eliminated = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            finally
            {
                if (conx != null && conx.State != ConnectionState.Closed)
                {
                    conx.Close();
                }
            }

            return eliminated;
        }

        public bool Leer(ENLineaCarrito linea)
        {
            bool readed = false;
            SqlConnection conx = null;

            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    string query = @"SELECT carrito_id, articulo_id, importe, cantidad 
                             FROM lineacarrito 
                             WHERE linea_carrito_id = @LineaCarritoId";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@LineaCarritoId", linea.Linea_carrito_id);

                    conx.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los valores leídos a las propiedades del objeto
                            linea.Carrito_id = reader.GetInt32(0);
                            linea.Articulo_id = reader.GetInt32(1);
                            linea.Importe = reader.GetFloat(2);
                            linea.Cantidad = reader.GetInt32(3);

                            readed = true; // Se encontró y leyó el registro
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                readed = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                readed = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            finally
            {
                if (conx != null && conx.State != ConnectionState.Closed)
                {
                    conx.Close();
                }
            }

            return readed;
        }

        public bool Leer_User_Articulo(ENLineaCarrito linea)
        {
            bool readed = false;
            SqlConnection conx = null;

            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    string query = @"SELECT carrito_id, articulo_id, importe, cantidad 
                             FROM lineacarrito 
                             WHERE carrito_id = @CarritoId and articulo_id = @ArticuloId";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@CarritoId", linea.Carrito_id);
                    cmd.Parameters.AddWithValue("@ArticuloId", linea.Articulo_id);

                    conx.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los valores leídos a las propiedades del objeto
                            linea.Carrito_id = reader.GetInt32(0);
                            linea.Articulo_id = reader.GetInt32(1);

                            readed = true; // Se encontró y leyó el registro
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                readed = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                readed = false;
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            finally
            {
                if (conx != null && conx.State != ConnectionState.Closed)
                {
                    conx.Close();
                }
            }

            return readed;
        }

        public bool Actualizar(ENLineaCarrito linea)
        {
            bool updated = false;
            SqlConnection conx = null;
            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE lineacarrito 
                           SET cantidad = @Cantidad 
                           WHERE linea_carrito_id = @LineaCarritoId";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@Cantidad", linea.Cantidad);
                    cmd.Parameters.AddWithValue("@LineaCarritoId", linea.Linea_carrito_id);

                    conx.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    updated = (rowsAffected > 0); // True si se actualizó al menos una fila

                    // Debug para verificar la operación
                    System.Diagnostics.Debug.WriteLine($"Actualizando línea {linea.Linea_carrito_id} con cantidad {linea.Cantidad}. Filas afectadas: {rowsAffected}");
                }
            }
            catch (SqlException ex)
            {
                updated = false;
                System.Diagnostics.Debug.WriteLine($"Error SQL en Actualizar LineaCarrito: {ex.Message}");
                Console.WriteLine("Lineacarrito operation has failed. SQL ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                updated = false;
                System.Diagnostics.Debug.WriteLine($"Error general en Actualizar LineaCarrito: {ex.Message}");
                Console.WriteLine("Lineacarrito operation has failed. ERROR: {0}", ex.Message);
            }
            finally
            {
                if (conx != null && conx.State != ConnectionState.Closed)
                {
                    conx.Close();
                }
            }
            return updated;
        }

        public List<ENLineaCarrito> ObtenerPorCarrito(int carritoId)
        {
            List<ENLineaCarrito> lineas = new List<ENLineaCarrito>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT linea_carrito_id, articulo_id, importe, cantidad 
                        FROM lineacarrito 
                        WHERE carrito_id = @carritoId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@carritoId", carritoId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ENLineaCarrito linea = new ENLineaCarrito();
                        linea.Linea_carrito_id = Convert.ToInt32(reader["linea_carrito_id"]);
                        linea.Carrito_id = carritoId;
                        linea.Articulo_id = Convert.ToInt32(reader["articulo_id"]);
                        linea.Importe = Convert.ToSingle(reader["importe"]);
                        linea.Cantidad = Convert.ToInt32(reader["cantidad"]);

                        lineas.Add(linea);
                    }
                }
            }
            return lineas;
        }
    }
}
