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
    public class CADArticulo
    {
        private string constring { get; set; }

        public CADArticulo()
        {
            constring = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool CrearArticulo(ENArticulo articulo)
        {
            bool result = false;
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                try
                {
                    // Obtener nuevo ID
                    string idQuery = "SELECT ISNULL(MAX(articulo_id), 0) + 1 FROM articulo";
                    SqlCommand idCmd = new SqlCommand(idQuery, conn);
                    int newId = (int)idCmd.ExecuteScalar();
                    articulo.Articulo_id = newId;

                    // Insertar artículo con stock
                    string insertQuery = @"
                INSERT INTO articulo (
                    articulo_id, stock, marca_id, categoria_id, catalogo_id,
                    color, modelo, sistema_operativo, anyo,
                    estado, memoria, bateria, precio,
                    descripcion, valoracion, url_imagen, vendido,
                    vendedor_UName, proveedor_id
                ) VALUES (
                    @articulo_id, @stock, @marca_id, @categoria_id, NULL,
                    @color, @modelo, @sistema_operativo, @anyo,
                    @estado, @memoria, @bateria, @precio,
                    @descripcion, @valoracion, @url_imagen, @vendido,
                    @vendedor_UName, @proveedor_id
                )";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@articulo_id", articulo.Articulo_id);
                    cmd.Parameters.AddWithValue("@stock", articulo.Stock);
                    cmd.Parameters.AddWithValue("@marca_id", articulo.Marca_id);
                    cmd.Parameters.AddWithValue("@categoria_id", articulo.Categoria_id);
                    cmd.Parameters.AddWithValue("@color", articulo.Color ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@modelo", articulo.Modelo);
                    cmd.Parameters.AddWithValue("@sistema_operativo", articulo.Sistema_operativo);
                    cmd.Parameters.AddWithValue("@anyo", articulo.Anyo);
                    cmd.Parameters.AddWithValue("@estado", articulo.Estado);
                    cmd.Parameters.AddWithValue("@memoria", articulo.Memoria);
                    cmd.Parameters.AddWithValue("@bateria", articulo.Bateria ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@precio", articulo.Precio);
                    cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@valoracion", articulo.Valoracion);
                    cmd.Parameters.AddWithValue("@url_imagen", articulo.Url_imagen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@vendido", articulo.Vendido);
                    cmd.Parameters.AddWithValue("@vendedor_UName", articulo.Vendedor_UName);
                    cmd.Parameters.AddWithValue("@proveedor_id", articulo.Proveedor_id.HasValue ? (object)articulo.Proveedor_id.Value : DBNull.Value);


                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar el artículo: " + ex.Message);
                }
            }

            return result;
        }

        public ENArticulo ReadArticulo(int articuloID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM articulo WHERE articulo_id = @ArticuloID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ArticuloID", articuloID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ENArticulo
                                {
                                    Articulo_id = Convert.ToInt32(reader["articulo_id"]),
                                    //Stock = Convert.ToInt32(reader["stock"]),
                                    Marca_id = Convert.ToInt32(reader["marca_id"]),
                                    Categoria_id = Convert.ToInt32(reader["categoria_id"]),
                                    Catalogo_id = reader["catalogo_id"] != DBNull.Value ? Convert.ToInt32(reader["catalogo_id"]) : 0,
                                    Color = reader["color"]?.ToString(),
                                    Modelo = reader["modelo"]?.ToString(),
                                    Sistema_operativo = reader["sistema_operativo"]?.ToString(),
                                    Anyo = reader["anyo"] != DBNull.Value ? Convert.ToInt32(reader["anyo"]) : 0,
                                    //Estado = reader["estado"]?.ToString(),
                                    Memoria = reader["memoria"]?.ToString(),
                                    Bateria = reader["bateria"]?.ToString(),
                                    Precio = Convert.ToDecimal(reader["precio"]),
                                    Descripcion = reader["descripcion"]?.ToString(),
                                    Valoracion = reader["valoracion"] != DBNull.Value ? Convert.ToDecimal(reader["valoracion"]) : 0,
                                    Url_imagen = reader["url_imagen"]?.ToString(),
                                    Vendido = Convert.ToInt32(reader["vendido"]),
                                    Vendedor_UName = reader["vendedor_UName"]?.ToString(),
                                    Proveedor_id = reader["proveedor_id"] != DBNull.Value ? (int?)Convert.ToInt32(reader["proveedor_id"]) : null,
                                    Estado = reader["estado"] != DBNull.Value ? reader["estado"].ToString() : "Nuevo",
                                    Stock = reader["stock"] != DBNull.Value ? Convert.ToInt32(reader["stock"]) : 0,
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al leer el artículo: " + ex.Message);
                    }
                }
            }

            return null;
        }




        public bool ActualizarArticulo(ENArticulo articulo)
        {
            bool result = false;
            string query = "UPDATE articulo SET catalogo_id = @catalogo_id WHERE articulo_id = @articulo_id";

            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@catalogo_id", articulo.Catalogo_id);
                cmd.Parameters.AddWithValue("@articulo_id", articulo.Articulo_id);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    result = rowsAffected > 0;
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }
        public bool LeerArticulo(ENArticulo articulo)
        {
            bool resultado = false;

            using (SqlConnection conexion = new SqlConnection(constring))
            {
                string query = "SELECT * FROM articulo WHERE articulo_id = @Id";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Id", articulo.Articulo_id);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    articulo.Modelo = reader["modelo"].ToString();
                    articulo.Descripcion = reader["descripcion"].ToString();
                    articulo.Precio = Convert.ToDecimal(reader["precio"]);
                    articulo.Marca_id = Convert.ToInt32(reader["marca_id"]);
                    articulo.Sistema_operativo = reader["sistema_operativo"].ToString();
                    articulo.Memoria = reader["memoria"].ToString();
                    articulo.Bateria = reader["bateria"].ToString();
                    articulo.Anyo = Convert.ToInt32(reader["anyo"]);
                    articulo.Estado = reader["estado"].ToString();
                    articulo.Vendedor_UName = reader["vendedor_UName"].ToString();
                    articulo.Url_imagen = reader["url_imagen"].ToString();

                    resultado = true;
                }

                reader.Close();
            }

            return resultado;
        }


        public bool EliminarArticulo(ENArticulo articulo)
        {
            bool resultado = false;

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "DELETE FROM articulo WHERE articulo_id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", articulo.Articulo_id);

                try
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    resultado = rows > 0;
                }
                catch (Exception ex)
                {
                    // Opcional: loguear el error
                    throw new Exception("Error al eliminar el artículo: " + ex.Message);
                }
            }

            return resultado;
        }


        public DataSet ObtenerArticulos()
        {
            throw new NotImplementedException();
        }


        public bool ActualizarValoracion(int articuloId, int nuevaValoracion)
        {
            // Validación básica
            if (nuevaValoracion < 1 || nuevaValoracion > 5)
                return false;

            const string query = @"
        UPDATE articulo 
        SET valoracion = 
            CASE 
                WHEN valoracion IS NULL OR valoracion = 0 THEN @nuevaValoracion
                ELSE (valoracion + @nuevaValoracion) / 2 
            END
        WHERE articulo_id = @articuloId";

            try
            {
                using (var connection = new SqlConnection(constring))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@articuloId", articuloId);
                    command.Parameters.AddWithValue("@nuevaValoracion", nuevaValoracion);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                // Logger.Error("Error SQL al actualizar valoración", ex);
                throw;
            }
        }


        //listado producto
        public List<ENArticulo> ListarArticulos()
        {
            List<ENArticulo> articulos = new List<ENArticulo>();

            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM articulo";
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ENArticulo articulo = new ENArticulo
                        {
                            Articulo_id = Convert.ToInt32(reader["articulo_id"]),
                            Modelo = reader["modelo"].ToString(),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Marca_id = Convert.ToInt32(reader["marca_id"]),
                            Stock = Convert.ToInt32(reader["stock"]),
                            Vendido = Convert.ToInt32(reader["vendido"])
                            // Puedes agregar más campos si los necesitas en la vista
                        };

                        articulos.Add(articulo);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar artículos: " + ex.Message);
                }


            }

            return articulos;
        }

        public bool ActualizarArticuloCompleto(ENArticulo articulo)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UPDATE articulo SET modelo=@modelo, precio=@precio, marca_id=@marca, stock=@stock, vendido=@vendido WHERE articulo_id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@modelo", articulo.Modelo);
                cmd.Parameters.AddWithValue("@precio", articulo.Precio);
                cmd.Parameters.AddWithValue("@marca", articulo.Marca_id);
                cmd.Parameters.AddWithValue("@stock", articulo.Stock);
                cmd.Parameters.AddWithValue("@vendido", articulo.Vendido);
                cmd.Parameters.AddWithValue("@id", articulo.Articulo_id);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public DataTable ObtenerTopProductosVendidos()
        {
            DataTable dt = new DataTable();

            string query = @"
    SELECT TOP 10 
        a.modelo, 
        COUNT(lp.articulo_id) AS ventas,
        m.nombre AS marca,
        c.nombre AS categoria,
        u.nombre + ' ' + u.apellidos AS vendedor
    FROM lineapedido lp
    INNER JOIN articulo a ON a.articulo_id = lp.articulo_id
    INNER JOIN marcas m ON a.marca_id = m.marca_id
    INNER JOIN categoria c ON a.categoria_id = c.categoria_id
    INNER JOIN usuario u ON a.vendedor_UName = u.username
    GROUP BY a.modelo, m.nombre, c.nombre, u.nombre, u.apellidos
    ORDER BY ventas DESC";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }






    }
}
