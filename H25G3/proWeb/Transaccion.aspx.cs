using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Library.CAD;
namespace proWeb
{
    public partial class Transaccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTransacciones();
            }
        }

        private void CargarTransacciones()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
           

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT 
                        t.transaccion_id,
                        a.modelo AS nombre_articulo,
                        a.vendedor_UName AS vendedor_Name,
                        t.importe_total AS precio_venta,
                        t.comision_empresa AS comision,
                        (t.importe_total - t.comision_empresa) AS ganancia,
                        t.fecha_transaccion
                    FROM transaccion t
                    INNER JOIN articulo a ON t.articulo_id = a.articulo_id
                    ORDER BY t.fecha_transaccion DESC;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    try
                    {
                        con.Open();
                        da.Fill(dt);

                        gvTransacciones.DataSource = dt;
                        gvTransacciones.DataBind();

                        lblMensaje.Text = dt.Rows.Count == 0 ? "No hay transacciones registradas." : "";
                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = "Error al cargar transacciones: " + ex.Message;
                    }
                }
            }
        }
    }
}