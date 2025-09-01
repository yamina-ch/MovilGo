using Library.EN;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Library.CAD
{
    public class CADMetodoPago
    {
        private string _connectionString;

        public CADMetodoPago()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }
        public bool CrearMetodoPago(ENMetodoPago metodo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO metodos_pago (numTarjeta, cvv, mes_cad, anyo_cad, usuario) " +
                                   "VALUES (@NumTarjeta, @CVV, @MesCad, @AnoCad, @Usuario)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NumTarjeta", metodo.NumeroTarjeta);
                    cmd.Parameters.AddWithValue("@CVV", metodo.CVV);
                    cmd.Parameters.AddWithValue("@MesCad",  metodo.MesCaducidad);
                    cmd.Parameters.AddWithValue("@AnoCad", 2000 + metodo.AnoCaducidad);
                    cmd.Parameters.AddWithValue("@Usuario", metodo.Username);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) // Violación de clave primaria
            {
                throw new Exception("Esta tarjeta ya está registrada");
            }
            catch (SqlException ex) when (ex.Number == 547) // Violación de FK
            {
                throw new Exception("El usuario no existe en la base de datos");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar método de pago: " + ex.Message);
            }
        }

        
        public DataTable ObtenerMetodosPorUsuario(string username)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = @"SELECT 
                                   numTarjeta, 
                                   CONCAT('****-****-****-', RIGHT(numTarjeta, 4)) as NumeroOculto,
                                   mes_cad, 
                                   anyo_cad,
                                   CASE 
                                       WHEN numTarjeta LIKE '4%' THEN 'Visa'
                                       WHEN numTarjeta LIKE '5%' THEN 'Mastercard'
                                       WHEN numTarjeta LIKE '34%' OR numTarjeta LIKE '37%' THEN 'American Express'
                                       WHEN numTarjeta LIKE '6%' THEN 'Discover'
                                       ELSE 'Otra'
                                   END as TipoTarjeta
                                   FROM metodos_pago 
                                   WHERE usuario = @Usuario";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Usuario", username);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener métodos de pago: " + ex.Message);
            }
        }


        public bool EliminarMetodoPago(string numTarjeta, string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = @"DELETE FROM metodos_pago 
                                   WHERE numTarjeta = @NumTarjeta 
                                   AND usuario = @Usuario";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NumTarjeta", numTarjeta);
                    cmd.Parameters.AddWithValue("@Usuario", username);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar método de pago: " + ex.Message);
            }
        }

        public bool ExisteMetodoPago(string numTarjeta, string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = @"SELECT COUNT(*) FROM metodos_pago 
                                   WHERE numTarjeta = @NumTarjeta 
                                   AND usuario = @Usuario";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NumTarjeta", numTarjeta);
                    cmd.Parameters.AddWithValue("@Usuario", username);

                    con.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar método de pago: " + ex.Message);
            }
        }

    }
}