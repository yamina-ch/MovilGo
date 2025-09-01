using library.EN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.CAD
{
    public class CADListaFavoritos
    {
        private string connectionString;
        public CADListaFavoritos()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool Crear(ENListaFavoritos linea)
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
                    string query = @"INSERT INTO lista_favoritos (usuario_UName, articulo_id) 
                            VALUES (@Name_Usuario, @ArticuloId);
                            SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@ArticuloId", linea.Articulo_id);
                    cmd.Parameters.AddWithValue("@Name_Usuario", linea.Usuario_UName);

                    conx.Open();

                    // Ejecutar la consulta y obtener el ID generado
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        linea.Lista_favorito_id = Convert.ToInt32(result);
                        created = true;
                    }
                }
            }
            catch(SqlException ex)
            {
                created = false;
                Console.WriteLine("LineaFavorito operation has failed. ERROR: {0}", ex.Message);
            }
            catch(Exception ex)
            {
                created = false;
                Console.WriteLine("LineaFavorito operation has failed. ERROR: {0}", ex.Message);
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

        public bool Eliminar(ENListaFavoritos linea)
        {
            bool eliminated = false;
            SqlConnection conx = null;
            try
            {
                using(conx = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM lista_favoritos WHERE lista_favorito_id = @ListaFavoritoId";
                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@ListaFavoritoId", linea.Lista_favorito_id);

                    conx.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    eliminated = (rowsAffected > 0); // True si se eliminó al menos una fila
                }
            }
            catch(SqlException ex)
            {
                eliminated = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
            }
            catch(Exception ex)
            {
                eliminated = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
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

        public bool Leer(ENListaFavoritos linea)
        {
            bool readed = false;
            SqlConnection conx = null;
            try
            {
                using(conx = new SqlConnection(connectionString)){
                    string query = @"SELECT lista_favorito_id, usuario_UName, articulo_id
                             FROM lista_favoritos 
                             WHERE lista_favorito_id = @LineaFavoritoId";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@LineaFavoritoId", linea.Lista_favorito_id);
                    conx.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los valores leídos a las propiedades del objeto
                            linea.Usuario_UName = reader["usuario_UName"] != DBNull.Value
                                        ? reader["usuario_UName"].ToString()
                                        : string.Empty;

                            linea.Articulo_id = reader["articulo_id"] != DBNull.Value
                                              ? Convert.ToInt32(reader["articulo_id"])
                                              : 0;

                            readed = true; // Se encontró y leyó el registro
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                readed = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                readed = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
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

        public bool Leer_User_Articulo(ENListaFavoritos linea)
        {
            bool readed = false;
            SqlConnection conx = null;
            try
            {
                using (conx = new SqlConnection(connectionString))
                {
                    string query = @"SELECT lista_favorito_id, usuario_UName, articulo_id
                             FROM lista_favoritos 
                             WHERE usuario_UName = @Usuario and articulo_id = @ArticuloId";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@Usuario", linea.Usuario_UName);
                    cmd.Parameters.AddWithValue("@ArticuloId", linea.Articulo_id);
                    conx.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los valores leídos a las propiedades del objeto
                            linea.Usuario_UName = reader["usuario_UName"] != DBNull.Value
                                        ? reader["usuario_UName"].ToString()
                                        : string.Empty;

                            linea.Articulo_id = reader["articulo_id"] != DBNull.Value
                                              ? Convert.ToInt32(reader["articulo_id"])
                                              : 0;

                            readed = true; // Se encontró y leyó el registro
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                readed = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                readed = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
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

        public bool Actualizar(ENListaFavoritos linea)
        {
            bool updated = false;
            SqlConnection conx = null;
            try
            {
                using(conx = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE lista_favoritos 
                            SET usuario_UName = @usuaeio_UName,
                                articulo_id = @ArticuloId
                            WHERE linea_carrito_id = @LineaFavoritoId";

                    SqlCommand cmd = new SqlCommand(query, conx);
                    cmd.Parameters.AddWithValue("@usuaeio_UName", linea.Usuario_UName);
                    cmd.Parameters.AddWithValue("@ArticuloId", linea.Articulo_id);
                    cmd.Parameters.AddWithValue("@LineaFavoritoId", linea.Lista_favorito_id);

                    conx.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    updated = (rowsAffected > 0); // True si se actualizó al menos una fila
                }
            }
            catch(SqlException ex)
            {
                updated = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                updated = false;
                Console.WriteLine("ListaFavorito operation has failed. ERROR: {0}", ex.Message);
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

        public List<ENListaFavoritos> ObtenerPorUsuario(string username)
        {
            List<ENListaFavoritos> lineas = new List<ENListaFavoritos>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT lista_favorito_id, usuario_UName, articulo_id 
                        FROM lista_favoritos 
                        WHERE usuario_UName = @UName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UName", username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ENListaFavoritos linea = new ENListaFavoritos();
                        linea.Lista_favorito_id = Convert.ToInt32(reader["lista_favorito_id"]);
                        linea.Usuario_UName = username;
                        linea.Articulo_id = Convert.ToInt32(reader["articulo_id"]);

                        lineas.Add(linea);
                    }
                }
                
            }
            return lineas;
        }
    }
}
