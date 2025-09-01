using Library.EN;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Library.CAD
{
    public class CADLineaPedido
    {
        private string _connectionString;

        public CADLineaPedido(string connectionString)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        // CREATE
        public bool CrearLineaPedido(ENLineaPedido linea)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO lineapedido 
                (linea_pedido_id, pedido_id, comprador_UName, vendedor_UName, articulo_id, importe) 
                VALUES (@LineaId, @PedidoId, @Comprador, @Vendedor, @ArticuloId, @Importe)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LineaId", linea.LineaPedidoId);
                cmd.Parameters.AddWithValue("@PedidoId", linea.PedidoId);
                cmd.Parameters.AddWithValue("@Comprador", linea.Comprador_UName);
                cmd.Parameters.AddWithValue("@Vendedor", linea.Vendedor_UName);
                cmd.Parameters.AddWithValue("@ArticuloId", linea.ArticuloId);
                cmd.Parameters.AddWithValue("@Importe", linea.Importe);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
        public int ObtenerNuevoIdLineaPedido()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT ISNULL(MAX(linea_pedido_id), 0) + 1 FROM lineapedido";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }


        // READ 
        public DataTable ObtenerLineasPorPedido(int pedidoId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM LineaPedido WHERE pedido_id = @PedidoId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PedidoId", pedidoId);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // UPDATE
        public bool ActualizarLineaPedido(ENLineaPedido linea)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE lineapedido SET 
                            comprador_UName = @Comprador, 
                            vendedor_UName = @Vendedor,
                            articulo_id = @ArticuloId,
                            importe = @Importe
                         WHERE linea_pedido_id = @LineaId AND pedido_id = @PedidoId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LineaId", linea.LineaPedidoId);
                cmd.Parameters.AddWithValue("@PedidoId", linea.PedidoId);
                cmd.Parameters.AddWithValue("@Comprador", linea.Comprador_UName);
                cmd.Parameters.AddWithValue("@Vendedor", linea.Vendedor_UName);
                cmd.Parameters.AddWithValue("@ArticuloId", linea.ArticuloId);
                cmd.Parameters.AddWithValue("@Importe", linea.Importe);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool EliminarLineaPedido(int lineaId, int pedidoId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM LineaPedido WHERE linea_pedido_id = @LineaId AND pedido_id = @PedidoId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LineaId", lineaId);
                cmd.Parameters.AddWithValue("@PedidoId", pedidoId);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}