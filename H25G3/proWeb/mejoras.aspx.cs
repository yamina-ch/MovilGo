using library.CAD;
using Library.CAD;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace proWeb
{
    public partial class mejoras : Page
    {
        public string Labels { get; set; }
        public string SalesData { get; set; }
        public decimal TotalGanancias { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal MetaMensual { get; set; } = 92000;
        public string HtmlTablaTopProductos { get; set; }
        public string HtmlTablaTopClientes { get; set; }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosGrafico();
                CalcularTotales();
                CargarTopProductos();
                CargarTopClientes();

            }
        }
        private void CargarTopClientes()
        {
            var cadPedido = new CADPedido();
            DataTable dt = cadPedido.ObtenerTopClientes();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table class='table table-bordered table-striped mt-4'><thead><tr>");
            sb.Append("<th>Cliente</th><th>Número de Compras</th>");
            sb.Append("</tr></thead><tbody>");

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", row["Cliente"]);
                sb.AppendFormat("<td>{0}</td>", row["NumeroCompras"]);
                sb.Append("</tr>");
            }

            sb.Append("</tbody></table>");
            HtmlTablaTopClientes = sb.ToString();
        }

        private void CargarTopProductos()
        {
            var cad = new CADArticulo();
            DataTable dt = cad.ObtenerTopProductosVendidos();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table class='table table-bordered table-striped'><thead><tr>");
            sb.Append("<th>Modelo</th><th>Ventas</th><th>Marca</th><th>Categoría</th><th>Vendedor</th>");
            sb.Append("</tr></thead><tbody>");

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", row["modelo"]);
                sb.AppendFormat("<td>{0}</td>", row["ventas"]);
                sb.AppendFormat("<td>{0}</td>", row["marca"]);
                sb.AppendFormat("<td>{0}</td>", row["categoria"]);
                sb.AppendFormat("<td>{0}</td>", row["vendedor"]);
                sb.Append("</tr>");
            }

            sb.Append("</tbody></table>");
            HtmlTablaTopProductos = sb.ToString();
        }
        private void CargarDatosGrafico()
        {
            var cadTransaccion = new CADTransaccion();
            var datos = cadTransaccion.ObtenerVentasPorMes();

            List<string> etiquetas = new List<string>();
            List<int> valores = new List<int>();

            foreach (var item in datos)
            {
                etiquetas.Add($"'{item.Key}'");
                valores.Add(item.Value);
            }

            Labels = "[" + string.Join(",", etiquetas) + "]";
            SalesData = "[" + string.Join(",", valores) + "]";
        }


        private void CalcularTotales()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                conn.Open();

                // SUMA DE LA COMISIÓN DE LA EMPRESA
                SqlCommand cmdGanancias = new SqlCommand("SELECT SUM(comision_empresa) FROM transaccion", conn);
                var resultGanancias = cmdGanancias.ExecuteScalar();
                TotalGanancias = resultGanancias != DBNull.Value ? Convert.ToDecimal(resultGanancias) : 0;

                //SUMA DE IMPORTE GASTADO (pedidos realizados)
                SqlCommand cmdGastos = new SqlCommand("SELECT SUM(importe) FROM lineapedido", conn);
                var resultGastos = cmdGastos.ExecuteScalar();
                TotalGastos = resultGastos != DBNull.Value ? Convert.ToDecimal(resultGastos) : 0;
            }
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Cerrando sesión...');</script>");
            Session["usuarios"] = null;
            Response.Redirect("~/login.aspx");
        }


    }
}
