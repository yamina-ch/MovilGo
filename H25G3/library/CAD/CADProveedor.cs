using library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace library.CAD
{
    public class CADProveedor
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

        public bool CrearProveedor(ENProveedor proveedor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO proveedor (proveedor_id, nombre, cif, direccion, telefono, email) 
                               VALUES (@proveedor_id, @nombre, @cif, @direccion, @telefono, @email)";

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Obtener el próximo ID disponible (podrías usar IDENTITY en la tabla en lugar de esto)
                    int nextId = ObtenerSiguienteIdProveedor();

                    cmd.Parameters.AddWithValue("@proveedor_id", nextId);
                    cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@cif", proveedor.CIF);
                    cmd.Parameters.AddWithValue("@direccion", proveedor.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", proveedor.Email ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        proveedor.Proveedor_id = nextId;
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear proveedor: " + ex.Message);
                    return false;
                }
            }
        }

        private int ObtenerSiguienteIdProveedor()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ISNULL(MAX(proveedor_id), 0) + 1 FROM proveedor";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool LeerProveedor(ENProveedor proveedor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT nombre, cif, direccion, telefono, email FROM proveedor WHERE proveedor_id = @id";

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", proveedor.Proveedor_id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proveedor.Nombre = reader["nombre"].ToString();
                            proveedor.CIF = reader["cif"].ToString();
                            proveedor.Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : null;
                            proveedor.Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : null;
                            proveedor.Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : null;
                            return true;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al leer proveedor: " + ex.Message);
                    return false;
                }
            }
        }

        public DataSet ObtenerProveedores()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM proveedor";

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                try
                {
                    da.Fill(ds, "Proveedores");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener proveedores: " + ex.Message);
                }
            }

            return ds;
        }

        public bool ActualizarProveedor(ENProveedor proveedor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE proveedor SET 
                        nombre = @nombre, 
                        cif = @cif, 
                        direccion = @direccion, 
                        telefono = @telefono, 
                        email = @email
                        WHERE proveedor_id = @id";

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agrego logging para depuración para probar si me funciona simpre
                    Console.WriteLine($"Actualizando proveedor ID: {proveedor.Proveedor_id}");

                    cmd.Parameters.AddWithValue("@id", proveedor.Proveedor_id);
                    cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@cif", proveedor.CIF);
                    cmd.Parameters.AddWithValue("@direccion", proveedor.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", proveedor.Email ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Filas afectadas: {rowsAffected}");

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar proveedor: " + ex.Message);
                    return false;
                }
            }
        }
        public bool EliminarProveedor(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM proveedor WHERE proveedor_id = @id";

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Filas afectadas al eliminar: {rowsAffected}");

                    return rowsAffected > 0;
                }
                catch (SqlException sqlEx)
                {
                    // Manejo específico para errores SQL
                    Console.WriteLine($"Error SQL al eliminar proveedor {id}: {sqlEx.Message}");
                    if (sqlEx.Number == 547) // Error de clave foránea
                    {
                        throw new Exception("No se puede eliminar el proveedor porque tiene registros relacionados");
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error general al eliminar proveedor {id}: {ex.Message}");
                    return false;
                }
            }
        }


        public DataSet ObtenerProductosPorProveedor(int proveedorId)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT articulo_id, modelo, marca_id, precio, stock, estado 
                        FROM articulo 
                        WHERE proveedor_id = @proveedorId";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@proveedorId", proveedorId);

                try
                {
                    da.Fill(ds, "Productos");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener productos por proveedor: " + ex.Message);
                }
            }

            return ds;
        }

        public DataSet ObtenerProveedores(string filtro = "all", string busqueda = "", string orden = "nombre_asc")
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM proveedor WHERE 1=1";

                // Aplicar filtro de búsqueda
                if (!string.IsNullOrEmpty(busqueda))
                {
                    query += @" AND (nombre LIKE @busqueda 
                      OR cif LIKE @busqueda 
                      OR ISNULL(telefono, '') LIKE @busqueda 
                      OR ISNULL(email, '') LIKE @busqueda)";
                }

                // Aplicar filtros adicionales
                switch (filtro)
                {
                    case "with_address":
                        query += " AND direccion IS NOT NULL AND direccion <> ''";
                        break;
                    case "without_address":
                        query += " AND (direccion IS NULL OR direccion = '')";
                        break;
                        // "all" no necesita condición adicional
                }

                // Aplicar ordenación
                switch (orden)
                {
                    case "nombre_asc":
                        query += " ORDER BY nombre ASC";
                        break;
                    case "nombre_desc":
                        query += " ORDER BY nombre DESC";
                        break;
                    case "cif_asc":
                        query += " ORDER BY cif ASC";
                        break;
                    case "cif_desc":
                        query += " ORDER BY cif DESC";
                        break;
                    default:
                        query += " ORDER BY nombre ASC";
                        break;
                }

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                if (!string.IsNullOrEmpty(busqueda))
                {
                    da.SelectCommand.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
                }

                try
                {
                    da.Fill(ds, "Proveedores");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener proveedores: " + ex.Message);
                }
            }

            return ds;
        }
    }
}