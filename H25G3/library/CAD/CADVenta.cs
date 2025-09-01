using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Library.EN;

namespace Library.CAD
{
    public class CADVenta
    {

        private string connectionString;
        public CADVenta()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool CrearVenta(ENVenta venta) 
        {
            bool result = false;
            string query = @"
        INSERT INTO venta 
        (articulo_id, fecha_anuncio, vendedor_UName, precio_original, precio_publicado, motivo_venta, disponible_hasta)
        VALUES (@articulo_id, @fecha_anuncio, @vendedor_UName, @precio_original, @precio_publicado, @motivo_venta, @disponible_hasta)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@articulo_id", venta.ArticuloId);
                cmd.Parameters.AddWithValue("@fecha_anuncio", venta.FechaAnuncio);
                cmd.Parameters.AddWithValue("@vendedor_UName", venta.VendedorUName);
                cmd.Parameters.AddWithValue("@precio_original", venta.PrecioOriginal ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@precio_publicado", venta.PrecioPublicado);
                cmd.Parameters.AddWithValue("@motivo_venta", venta.MotivoVenta ?? (object)DBNull.Value);
                //DisponibleHasta me refiero ala fecha que esta disponible apartir de tal dia 
                cmd.Parameters.AddWithValue("@disponible_hasta", venta.DisponibleHasta ?? (object)DBNull.Value);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }
        public DataTable ObtenerProductosNuevosYProximos()
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT TOP 20 
            a.modelo AS Modelo,
            a.url_imagen AS Imagen,
            v.disponible_hasta AS FechaDisponible
        FROM venta v
        INNER JOIN articulo a ON a.articulo_id = v.articulo_id
        WHERE 
            (
                v.fecha_anuncio >= DATEADD(DAY, -7, GETDATE()) -- últimos 7 días
                OR v.disponible_hasta >= GETDATE() -- a punto de estar disponibles
            )
        ORDER BY 
            ISNULL(v.disponible_hasta, v.fecha_anuncio) ASC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        public List<ENVenta> ObtenerVentasPorVendedor(string vendedorUName)
        {
            // lógica para recuperar ventas por vendedor
            return new List<ENVenta>();
        }

        public bool EliminarVenta(int ventaId)
        {
            // lógica para eliminar venta
            return true;
        }

        public bool ActualizarVenta(ENVenta venta)
        {
            // lógica para actualizar venta
        
            return true;
        }
    }
}
