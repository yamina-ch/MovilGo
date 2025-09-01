using Library.EN;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Library.CAD
{
    public class CADPedido
    {
        private string _connectionString;

        public CADPedido()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool CrearPedido(ENPedido pedido)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Pedido (pedido_id, comprador_UName, fecha_pedido, importe) " +
                               "VALUES (@PedidoId, @Comprador, @Fecha, @Importe)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PedidoId", pedido.PedidoId);
                cmd.Parameters.AddWithValue("@Comprador", pedido.CompradorUsername);
                cmd.Parameters.AddWithValue("@Fecha", pedido.FechaPedido);
                cmd.Parameters.AddWithValue("@Importe", pedido.ImporteTotal);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public ENPedido ObtenerPedido(int pedidoId)
        {
            ENPedido pedido = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Pedido WHERE pedido_id = @PedidoId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PedidoId", pedidoId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pedido = new ENPedido(
                        (int)reader["pedido_id"],
                        reader["comprador_UName"].ToString(),
                        (DateTime)reader["fecha_pedido"],
                        (decimal)reader["importe"]

                    );
                }
            }
            return pedido;
        }



      public bool ActualizarPedido(ENPedido pedido)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Pedido SET comprador_UName = @Comprador, " +
                               "fecha_pedido = @Fecha, importe = @Importe" +
                               "WHERE pedido_id = @PedidoId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PedidoId", pedido.PedidoId);
                cmd.Parameters.AddWithValue("@Comprador", pedido.CompradorUsername);

                cmd.Parameters.AddWithValue("@Fecha", pedido.FechaPedido);
                cmd.Parameters.AddWithValue("@Importe", pedido.ImporteTotal);


                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }



        public bool EliminarPedido(int pedidoId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Pedido WHERE pedido_id = @PedidoId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PedidoId", pedidoId);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        
        public DataTable BuscarPedidos(string keyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Pedido WHERE estado LIKE @Keyword OR comprador_UName LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable ObtenerTopClientes()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                string query = @"
            SELECT TOP 10 comprador_UName AS Cliente, COUNT(*) AS NumeroCompras
            FROM pedido
            GROUP BY comprador_UName
            ORDER BY COUNT(*) DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}