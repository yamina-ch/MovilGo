using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Library;
using Library.EN;
using Library.CAD;
using System.Configuration;

namespace proWeb
{
    public partial class confirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ENUsuario usuario = (ENUsuario)Session["usuarios"];
                if (usuario == null)
                {
                    Response.Redirect("login.aspx?redirect=confirmacion.aspx");
                    return;
                }

                if (Session["Carrito"] is List<ProductoItem> productos &&
                    Session["DatosEnvio"] != null &&
                    Session["MetodoPago"] != null)
                {
                    var datosEnvio = Session["DatosEnvio"] as dynamic;
                    var metodoPago = Session["MetodoPago"] as dynamic;

                    string numeroPedido = "PED-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    litNumeroPedido.Text = numeroPedido;

                    litDatosCliente.Text = $"{datosEnvio.Nombre}<br>" +
                                           $"{datosEnvio.Direccion}<br>" +
                                           $"{datosEnvio.Ciudad}, {datosEnvio.Provincia} {datosEnvio.CP}<br>" +
                                           $"Teléfono: {datosEnvio.Telefono}";

                    litMetodoPago.Text = $"{metodoPago.Tipo} terminada en {metodoPago.Ultimos4}";

                    rptResumen.DataSource = productos.Select(p => new
                    {
                        p.Nombre,
                        p.Precio,
                        p.Cantidad,
                        Subtotal = (p.Precio * p.Cantidad)
                    }).ToList();
                    rptResumen.DataBind();

                    double subtotal = productos.Sum(p => p.Precio * p.Cantidad);
                    double envio = 4.5;
                    double total = subtotal + envio;

                    litSubtotal.Text = subtotal.ToString("0.00");
                    litEnvio.Text = envio.ToString("0.00");
                    litTotal.Text = total.ToString("0.00");
                    int nuevoIdPedido = new Random().Next(1000, 9999);

                    var pedido = new ENPedido
                    {
                        PedidoId = nuevoIdPedido,
                        CompradorUsername = usuario.Username,
                        FechaPedido = DateTime.Now,
                        ImporteTotal = (decimal)total,
                    };

                    var cadPedido = new CADPedido();
                    bool pedidoCreado = cadPedido.CrearPedido(pedido);

                    if (pedidoCreado)
                    {
                        string _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
                        CADLineaPedido cadLineaPedido = new CADLineaPedido(_connectionString);

                        using (SqlConnection conn = new SqlConnection(_connectionString))
                        {
                            conn.Open();

                            foreach (var producto in productos)
                            {
                                string queryArticulo = "SELECT articulo_id, vendedor_UName, precio FROM articulo WHERE modelo = @Modelo";
                                SqlCommand cmdArticulo = new SqlCommand(queryArticulo, conn);
                                cmdArticulo.Parameters.AddWithValue("@Modelo", producto.Nombre);

                                using (SqlDataReader reader = cmdArticulo.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int idArticulo = (int)reader["articulo_id"];
                                        string vendedor = reader["vendedor_UName"].ToString();
                                        decimal precio = (decimal)reader["precio"];
                                        reader.Close();

                                        for (int i = 0; i < producto.Cantidad; i++)
                                        {
                                            int nuevaLineaId = cadLineaPedido.ObtenerNuevoIdLineaPedido();

                                            ENLineaPedido linea = new ENLineaPedido
                                            {
                                                LineaPedidoId = nuevaLineaId,
                                                PedidoId = nuevoIdPedido,
                                                Comprador_UName = usuario.Username,
                                                Vendedor_UName = vendedor,
                                                ArticuloId = idArticulo,
                                                Importe = precio
                                            };

                                            bool exitoLinea = cadLineaPedido.CrearLineaPedido(linea);
                                            if (!exitoLinea)
                                            {
                                                Response.Write($"Error guardando línea pedido para artículo {producto.Nombre}");
                                                return;
                                            }


                                            string selectLineaPedido = "SELECT TOP 1 linea_pedido_id FROM lineapedido WHERE articulo_id = @idArticulo ORDER BY linea_pedido_id DESC";
                                            using (SqlCommand selectCmd = new SqlCommand(selectLineaPedido, conn))
                                            {
                                                selectCmd.Parameters.AddWithValue("@idArticulo", idArticulo);
                                                object result = selectCmd.ExecuteScalar();

                                                if (result != null)
                                                {
                                                    int lineaPedidoId = Convert.ToInt32(result);
                                                    GuardarTransaccion(idArticulo, lineaPedidoId, precio, conn);
                                                }
                                            }


                                        }
                                    }
                                    else
                                    {
                                        Response.Write($"No se encontró el artículo {producto.Nombre} en base de datos.");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("carrito.aspx");
                }
            }
        }
        private void GuardarTransaccion(int articuloId, int lineaPedidoId, decimal precioVenta, SqlConnection conn)
        {
            string query = @"
        SELECT u.username, u.admin 
        FROM usuario u 
        JOIN articulo a ON u.username = a.vendedor_UName 
        WHERE a.articulo_id = @articuloId";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@articuloId", articuloId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string vendedorUsername = reader["username"].ToString();
                        int esAdmin = Convert.ToInt32(reader["admin"]);
                        reader.Close();

                        int nuevoId = GenerarTransaccionIdUnico(conn);

                        decimal comisionEmpresa;
                        decimal comisionVendedor;

                        if (esAdmin == 1)
                        {
                            // Vendedor es admin → 100% para la empresa
                            comisionEmpresa = precioVenta;
                            comisionVendedor = 0;
                        }
                        else
                        {
                            // Vendedor normal → 90% para él, 10% para la empresa
                            comisionEmpresa = Math.Round(precioVenta * 0.10M, 2);
                            comisionVendedor = Math.Round(precioVenta * 0.90M, 2);
                        }

                        string insert = @"
                    INSERT INTO transaccion (transaccion_id, articulo_id, importe_total, vendedor_UName, linea_pedido_id, comision_vendedor, comision_empresa)
                    VALUES (@transaccionId, @articuloId, @precio, @username, @lineaPedidoId, @comisionVendedor, @comisionEmpresa)";

                        using (SqlCommand insertCmd = new SqlCommand(insert, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@transaccionId", nuevoId);
                            insertCmd.Parameters.AddWithValue("@articuloId", articuloId);
                            insertCmd.Parameters.AddWithValue("@precio", precioVenta);
                            insertCmd.Parameters.AddWithValue("@username", vendedorUsername);
                            insertCmd.Parameters.AddWithValue("@lineaPedidoId", lineaPedidoId);
                            insertCmd.Parameters.AddWithValue("@comisionEmpresa", comisionEmpresa);
                            insertCmd.Parameters.AddWithValue("@comisionVendedor", comisionVendedor);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }



        int GenerarTransaccionIdUnico(SqlConnection conn)
        {
            int transaccionId = 1;
            bool existe;

            do
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM transaccion WHERE transaccion_id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", transaccionId);
                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }

                if (existe)
                {
                    transaccionId++; // probar con el siguiente número
                }

            } while (existe);

            return transaccionId;
        }


        private int ObtenerArticuloIdPorModelo(string modelo, SqlConnection conn)
        {
            string query = "SELECT articulo_id FROM articulo WHERE modelo = @Modelo";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Modelo", modelo);
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                string htmlFactura = GenerarHTMLFactura();
                GenerarPDFAlternativo(htmlFactura);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error al generar PDF: {ex.Message.Replace("'", "")}');", true);
            }
        }

        private void GenerarPDFAlternativo(string htmlContent)
        {
            
            string fileName = $"Factura_{litNumeroPedido.Text}_{DateTime.Now:yyyyMMddHHmmss}.html";

            
            string tempPath = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllText(tempPath, htmlContent);

            
            Response.Clear();
            Response.ContentType = "text/html";
            Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");

            
            string printScript = @"
                <script>
                window.onload = function() {
                    setTimeout(function() {
                        window.print();
                        setTimeout(function() {
                            window.close();
                        }, 100);
                    }, 500);
                };
                </script>";

            Response.Write(htmlContent + printScript);
            Response.End();

            
            Task.Run(async () =>
            {
                await Task.Delay(5000); 
                try { File.Delete(tempPath); } catch { }
            });
        }

        private string GenerarHTMLFactura()
        {
            var productos = Session["Carrito"] as List<ProductoItem>;
            var datosEnvio = Session["DatosEnvio"] as dynamic;
            var metodoPago = Session["MetodoPago"] as dynamic;
            string numeroPedido = litNumeroPedido.Text;
            double total = Convert.ToDouble(litTotal.Text);

            StringBuilder html = new StringBuilder();
            html.Append(@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8' />
                    <title>Factura " + numeroPedido + @"</title>
                    <style>
                        body { font-family: Arial; margin: 20px; }
                        h1 { color: #333; text-align: center; }
                        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
                        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
                        th { background-color: #f2f2f2; }
                        .total { font-weight: bold; }
                    </style>
                </head>
                <body>
                    <h1>FACTURA #" + numeroPedido + @"</h1>
                    <p><strong>Fecha:</strong> " + DateTime.Now.ToString("dd/MM/yyyy") + @"</p>
                    
                    <h3>Datos del Cliente</h3>
                    <p>" + datosEnvio.Nombre + @"<br>" + datosEnvio.Direccion + @"<br>" +
                    datosEnvio.Ciudad + ", " + datosEnvio.Provincia + " " + datosEnvio.CP + @"<br>
                    Teléfono: " + datosEnvio.Telefono + @"</p>
                    
                    <h3>Detalles del Pedido</h3>
                    <table>
                        <tr>
                            <th>Producto</th>
                            <th>Precio</th>
                            <th>Cantidad</th>
                            <th>Subtotal</th>
                        </tr>");

            foreach (var producto in productos)
            {
                html.Append($@"
                    <tr>
                        <td>{producto.Nombre}</td>
                        <td>{producto.Precio.ToString("0.00")} €</td>
                        <td>{producto.Cantidad}</td>
                        <td>{(producto.Precio * producto.Cantidad).ToString("0.00")} €</td>
                    </tr>");
            }

            html.Append($@"
                <tr class='total'>
                    <td colspan='3'>Subtotal</td>
                    <td>{litSubtotal.Text} €</td>
                </tr>
                <tr>
                    <td colspan='3'>Envío</td>
                    <td>{litEnvio.Text} €</td>
                </tr>
                <tr class='total'>
                    <td colspan='3'>TOTAL</td>
                    <td>{litTotal.Text} €</td>
                </tr>
            </table>

            <h3>Método de Pago</h3>
            <p>{metodoPago.Tipo} terminada en {metodoPago.Ultimos4}</p>

            <p style='margin-top: 30px; text-align: center;'>
                <em>Gracias por su compra</em>
            </p>
            </body>
            </html>");


            return html.ToString();
        }

        private void GenerarPDFConContenido(string htmlContent)
        {
            try
            {
                
                string tempHtmlPath = Path.Combine(Path.GetTempPath(), "factura_temp.html");
                File.WriteAllText(tempHtmlPath, htmlContent);

                
                string tempPdfPath = Path.Combine(Path.GetTempPath(), "factura.pdf");

                
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "msedge.exe",
                    Arguments = $"--headless --disable-gpu --run-all-compositor-stages-before-draw --print-to-pdf=\"{tempPdfPath}\" \"file://{tempHtmlPath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit(5000); 
                }

                
                if (File.Exists(tempPdfPath))
                {
                    
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", $"attachment; filename=Factura_{litNumeroPedido.Text}.pdf");
                    Response.WriteFile(tempPdfPath);
                    Response.End();
                }
                else
                {
                    throw new Exception("No se pudo generar el PDF");
                }
            }
            catch (Exception ex)
            {
                
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al generar PDF: {ex.Message.Replace("'", "")}');", true);
            }
            finally
            {
               
                try
                {
                    File.Delete(Path.Combine(Path.GetTempPath(), "factura_temp.html"));
                    File.Delete(Path.Combine(Path.GetTempPath(), "factura.pdf"));
                }
                catch { }
            }
        }
       
    }
}
    