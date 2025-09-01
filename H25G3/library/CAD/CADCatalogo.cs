using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Library.EN;

namespace Library.CAD
{
    public class CADCatalogo
    {

        private string connectionString;

        public CADCatalogo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

      
        public DataTable ObtenerCatalogoConArticulos()
        {
            string query = @"
            SELECT 
                c.catalogo_id,
                c.nombre,
                c.precio,
                c.url_imagen AS ImagenUrl,
                ISNULL(a.color, '-') AS Color,
                ISNULL(a.memoria, '-') AS Memoria,
                ISNULL(a.categoria_id, c.categoria_id) AS categoria_id,
                ISNULL(a.marca_id, 0) AS marca_id,
                a.vendido,
                v.disponible_hasta
            FROM catalogo c
            INNER JOIN articulo a ON a.catalogo_id = c.catalogo_id
            LEFT JOIN venta v ON v.articulo_id = a.articulo_id";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public DataTable ObtenerColores()
        {
            string consulta = "SELECT DISTINCT color FROM articulo WHERE color IS NOT NULL AND color <> ''";
            DataTable dt = new DataTable();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        public int CrearCatalogo(ENCatalogo catalogo)
        {
            int nuevoId = -1;
            string query = @"
        INSERT INTO catalogo (nombre, descripcion, precio, vendido, categoria_id, url_imagen)
        OUTPUT INSERTED.catalogo_id
        VALUES (@nombre, @descripcion, @precio, @vendido, @categoria_id, @url_imagen)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nombre", catalogo.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", catalogo.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@precio", catalogo.Precio);
                cmd.Parameters.AddWithValue("@vendido", catalogo.Vendido);
                cmd.Parameters.AddWithValue("@categoria_id", catalogo.CategoriaId);
                cmd.Parameters.AddWithValue("@url_imagen", catalogo.UrlImagen ?? (object)DBNull.Value);

                try
                {
                    con.Open();
                    nuevoId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear catálogo: " + ex.Message);
                }
            }

            return nuevoId;
        }

    }
}
