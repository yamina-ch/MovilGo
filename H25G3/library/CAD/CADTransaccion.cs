using library.EN;
using Library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.CAD
{
    public class CADTransaccion
    {

        private string _connectionString;

        public CADTransaccion()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public bool Crear(ENTransaccion transaccion)
        {
            bool creado = false;

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO transaccion 
                                (linea_pedido_id, articulo_id, vendedor_UName, importe_total, comision_vendedor, comision_empresa, fecha_transaccion)
                                 VALUES 
                                (@pedido_id, @articulo_id, @vendedor, @importe, @comision_vendedor, @comision_empresa, @fecha)";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@pedido_id", transaccion.Pedido_id);
                comando.Parameters.AddWithValue("@articulo_id", transaccion.Articulo_id);
                comando.Parameters.AddWithValue("@vendedor", transaccion.Vendedor_UName);
                comando.Parameters.AddWithValue("@importe", transaccion.Importe_total);
                comando.Parameters.AddWithValue("@comision_vendedor", transaccion.Comision_vendedor);
                comando.Parameters.AddWithValue("@comision_empresa", transaccion.Comision_empresa);
                comando.Parameters.AddWithValue("@fecha", transaccion.Fecha_transaccion);

                try
                {
                    conexion.Open();
                    int filas = comando.ExecuteNonQuery();
                    if (filas > 0) creado = true;
                }
                catch (Exception ex)
                {
                    // Manejar error si quieres (log, etc.)
                    Console.WriteLine("Error al crear transacción: " + ex.Message);
                }
            }

            return creado;
        }

        public bool Editar(ENTransaccion transaccion)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public ENCategoria Leer(int id)
        {
            throw new NotImplementedException();
        }

        public List<ENTransaccion> ObtenerTransaccionesPorVendedor(string vendedor)
        {
            List<ENTransaccion> lista = new List<ENTransaccion>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = @"
            SELECT t.transaccion_id, t.linea_pedido_id, t.articulo_id, t.vendedor_UName,
                   t.importe_total, t.comision_vendedor, t.comision_empresa, t.fecha_transaccion,
                   a.nombre AS nombre_articulo
            FROM transaccion t
            INNER JOIN articulo a ON t.articulo_id = a.articulo_id
            WHERE t.vendedor_UName = @vendedor";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@vendedor", vendedor);

                try
                {
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        ENTransaccion t = new ENTransaccion();
                        t.Transaccion_id = Convert.ToInt32(reader["transaccion_id"]);
                        t.Pedido_id = Convert.ToInt32(reader["linea_pedido_id"]);
                        t.Articulo_id = Convert.ToInt32(reader["articulo_id"]);
                        t.Vendedor_UName = reader["vendedor_UName"].ToString();
                        t.Importe_total = float.Parse(reader["importe_total"].ToString());
                        t.Comision_vendedor = float.Parse(reader["comision_vendedor"].ToString());
                        t.Comision_empresa = float.Parse(reader["comision_empresa"].ToString());
                        t.Fecha_transaccion = Convert.ToDateTime(reader["fecha_transaccion"]);
                        t.NombreArticulo = reader["nombre_articulo"].ToString(); // ← esto requiere una propiedad nueva

                        lista.Add(t);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al leer transacciones: " + ex.Message);
                }
            }

            return lista;
        }
        public Dictionary<string, int> ObtenerVentasPorMes()
        {
            Dictionary<string, int> ventasPorMes = new Dictionary<string, int>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = @"
            SELECT DATENAME(MONTH, fecha_transaccion) AS MesNombre, 
                   MONTH(fecha_transaccion) AS MesNumero, 
                   COUNT(*) AS Cantidad
            FROM transaccion
            GROUP BY DATENAME(MONTH, fecha_transaccion), MONTH(fecha_transaccion)
            ORDER BY MesNumero";

                SqlCommand comando = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        string mes = reader["MesNombre"].ToString();
                        int cantidad = Convert.ToInt32(reader["Cantidad"]);

                        // Capitalizamos por seguridad (p. ej., "mayo" => "Mayo")
                        mes = char.ToUpper(mes[0]) + mes.Substring(1).ToLower();

                        // Previene duplicados si ya existe (por error de idioma o cache)
                        if (!ventasPorMes.ContainsKey(mes))
                            ventasPorMes.Add(mes, cantidad);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener ventas por mes: " + ex.Message);
                }
            }

            return ventasPorMes;
        }



    }
}