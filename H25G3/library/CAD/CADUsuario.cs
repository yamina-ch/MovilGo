using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Library.EN;

namespace Library.CAD
{
    public class CADUsuario
    {
        private string constring { get; set; }

        public CADUsuario()
        {
            constring = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool CreateUsuario(ENUsuario usu)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "INSERT INTO usuario (username, nombre, apellidos, telefono, contrasenya, calle, localidad, provincia, codigo_postal, email, admin) " +
                                   "VALUES (@Username, @Nombre, @Apellidos, @Telefono, @Contrasenya, @Calle, @Localidad, @Provincia, @Codigo_Postal, @Email, @Admin)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", usu.Username);
                    cmd.Parameters.AddWithValue("@Nombre", usu.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", usu.Apellidos);
                    cmd.Parameters.AddWithValue("@Telefono", usu.Telefono);
                    cmd.Parameters.AddWithValue("@Contrasenya", usu.Contrasenya);
                    cmd.Parameters.AddWithValue("@Calle", usu.Calle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Localidad", usu.Localidad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Provincia", usu.Provincia ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Codigo_Postal", usu.Codigo_Postal ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", usu.Email);
                    cmd.Parameters.AddWithValue("@Admin", usu.Admin ? 1 : 0);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en CreateUsuario: " + ex.Message);
            }
        }

        public bool UpdateUsuario(ENUsuario usu)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "UPDATE usuario SET " +
                                   "nombre = @Nombre, apellidos = @Apellidos, telefono = @Telefono, " +
                                   "calle = @Calle, localidad = @Localidad, provincia = @Provincia, " +
                                   "codigo_postal = @Codigo_Postal, email = @Email, admin = @Admin " +
                                   "WHERE username = @Username";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Nombre", usu.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", usu.Apellidos);
                    cmd.Parameters.AddWithValue("@Telefono", usu.Telefono);
                    cmd.Parameters.AddWithValue("@Calle", usu.Calle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Localidad", usu.Localidad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Provincia", usu.Provincia ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Codigo_Postal", usu.Codigo_Postal ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", usu.Email);
                    cmd.Parameters.AddWithValue("@Admin", usu.Admin ? 1 : 0); // Asegurando que se usa 1 o 0 para el admin
                    cmd.Parameters.AddWithValue("@Username", usu.Username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Verifica si se actualizó al menos una fila
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UpdateUsuario: " + ex.Message);
            }
        }

        public bool FindUsuarioByUsername(ENUsuario usuario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "SELECT * FROM usuario WHERE username = @Username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en FindUsuarioByUsername: " + ex.Message);
            }
        }

        public bool FindUsuarioByTelefono(ENUsuario usuario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM usuario WHERE telefono = @Telefono";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en FindUsuarioByTelefono: " + ex.Message);
            }
        }

        public bool FindUsuarioByEmail(ENUsuario usuario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "SELECT * FROM usuario WHERE email = @Email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario.Username = reader["username"].ToString();
                            usuario.Nombre = reader["nombre"].ToString();
                            usuario.Apellidos = reader["apellidos"].ToString();
                            usuario.Telefono = reader["telefono"].ToString();
                            usuario.Contrasenya = reader["contrasenya"].ToString();
                            usuario.Calle = reader["calle"] != DBNull.Value ? reader["calle"].ToString() : null;
                            usuario.Localidad = reader["localidad"] != DBNull.Value ? reader["localidad"].ToString() : null;
                            usuario.Provincia = reader["provincia"] != DBNull.Value ? reader["provincia"].ToString() : null;
                            usuario.Codigo_Postal = reader["codigo_postal"] != DBNull.Value ? reader["codigo_postal"].ToString() : null;
                            usuario.Admin = Convert.ToInt32(reader["admin"]) == 1;

                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en FindUsuarioByEmail: " + ex.Message);
            }
        }

        public bool ChangePassword(ENUsuario usu)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "UPDATE usuario SET contrasenya = @Contrasenya WHERE username = @Username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Contrasenya", usu.Contrasenya);
                    cmd.Parameters.AddWithValue("@Username", usu.Username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ChangePassword: " + ex.Message);
            }
        }

        public bool FindUsuario(ENUsuario usuario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM usuario WHERE email = @Email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en FindUsuario: " + ex.Message);
            }
        }

        public bool ValidarContrasena(ENUsuario usuario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "SELECT contrasenya FROM usuario WHERE email = @Email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);

                    string contrasenaAlmacenada = cmd.ExecuteScalar()?.ToString();
                    return contrasenaAlmacenada == usuario.Contrasenya;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ValidarContrasena: " + ex.Message);
            }
        }
        public bool DeleteUsuario(string username, out string errorMessage)
        {
            errorMessage = string.Empty;
            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Comentarios hechos por el usuario
                    Ejecutar("DELETE FROM comentario WHERE usuario_UName = @username", username);

                    // 2. Favoritos hechos por el usuario
                    Ejecutar("DELETE FROM lista_favoritos WHERE usuario_UName = @username", username);

                    // 3. Contactos enviados por el usuario
                    Ejecutar("DELETE FROM contactos WHERE usuario_UName = @username", username);

                    // 4. Métodos de pago del usuario
                    Ejecutar("DELETE FROM metodos_pago WHERE usuario = @username", username);

                    // 5. LíneaCarrito (por carrito del usuario)
                    Ejecutar(@"
                DELETE FROM lineacarrito 
                WHERE carrito_id IN (
                    SELECT carrito_id FROM carrito WHERE usuario_UName = @username
                )", username);

                    // 6. Carrito del usuario
                    Ejecutar("DELETE FROM carrito WHERE usuario_UName = @username", username);

                    // 7. LíneaPedido (como comprador)
                    Ejecutar("DELETE FROM lineapedido WHERE comprador_UName = @username", username);

                    // 8. LíneaPedido (por artículos del usuario)
                    Ejecutar(@"
                DELETE FROM lineapedido 
                WHERE articulo_id IN (
                    SELECT articulo_id FROM articulo WHERE vendedor_UName = @username
                )", username);

                    // 9. Transacción (por artículos del usuario)
                    Ejecutar(@"
                DELETE FROM transaccion 
                WHERE articulo_id IN (
                    SELECT articulo_id FROM articulo WHERE vendedor_UName = @username
                )", username);

                    // 10. Transacción (por pedidos del usuario)
                    Ejecutar(@"
                DELETE FROM transaccion 
                WHERE pedido_id IN (
                    SELECT pedido_id FROM pedido 
                    WHERE comprador_UName = @username OR vendedor_UName = @username
                )", username);

                    // 11. Ventas (por artículos del usuario)
                    Ejecutar(@"
                DELETE FROM venta 
                WHERE articulo_id IN (
                    SELECT articulo_id FROM articulo WHERE vendedor_UName = @username
                )", username);

                    // 12. Venta (por vendedor directamente, si hay ventas sin artículo)
                    Ejecutar("DELETE FROM venta WHERE vendedor_UName = @username", username);

                    // 13. Comentarios sobre artículos del usuario
                    Ejecutar(@"
                DELETE FROM comentario 
                WHERE articulo_id IN (
                    SELECT articulo_id FROM articulo WHERE vendedor_UName = @username
                )", username);

                    // 14. Favoritos de artículos del usuario
                    Ejecutar(@"
                DELETE FROM lista_favoritos 
                WHERE articulo_id IN (
                    SELECT articulo_id FROM articulo WHERE vendedor_UName = @username
                )", username);

                    // 15. LíneaCarrito (por artículos del usuario)
                    Ejecutar(@"
                DELETE FROM lineacarrito 
                WHERE articulo_id IN (
                    SELECT articulo_id FROM articulo WHERE vendedor_UName = @username
                )", username);

                    // 16. Pedido (como comprador o vendedor)
                    Ejecutar("DELETE FROM pedido WHERE comprador_UName = @username OR vendedor_UName = @username", username);

                    // 17. Artículos del usuario
                    Ejecutar("DELETE FROM articulo WHERE vendedor_UName = @username", username);

                    // 18. Finalmente, eliminar usuario
                    Ejecutar("DELETE FROM usuario WHERE username = @username", username);

                    transaction.Commit();
                    return true;

                    // Sub-función local para evitar duplicación
                    void Ejecutar(string query, string uname)
                    {
                        using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@username", uname);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    errorMessage = "Error al eliminar el usuario: " + ex.Message;
                    return false;
                }
            }
        }

        public List<ENUsuario> ListarUsuarios()
        {
            List<ENUsuario> usuarios = new List<ENUsuario>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string query = "SELECT * FROM usuario";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ENUsuario user = new ENUsuario
                    {
                        Username = reader["username"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        Apellidos = reader["apellidos"].ToString(),
                        Email = reader["email"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Calle = reader["calle"].ToString(),
                        Localidad = reader["localidad"].ToString(),
                        Provincia = reader["provincia"].ToString(),
                        Codigo_Postal = reader["codigo_postal"].ToString(),
                        Admin = Convert.ToBoolean(reader["admin"])
                    };
                    usuarios.Add(user);
                }
            }
            return usuarios;
        }


    }
}
