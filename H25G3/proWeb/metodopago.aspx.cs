using Library.CAD;
using Library.EN;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proWeb
{
    public partial class metodopago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Carrito"] == null || Session["DatosEnvio"] == null)
                {
                    Response.Redirect("carrito.aspx");
                }

                
                double subtotal = Convert.ToDouble(Session["Subtotal"]);
                
                double total = subtotal + 4.5;

                litSubtotal.Text = subtotal.ToString("0.00");
         
                litTotal.Text = total.ToString("0.00");
                hdnTotal.Value = total.ToString();
            }
        }


        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                
                string numeroTarjeta = txtNumeroTarjeta.Text;
                string[] expiracion = txtExpiracion.Text.Split('/');

                if (expiracion.Length != 2 || !int.TryParse(expiracion[0], out int mes) ||
                    !int.TryParse(expiracion[1], out int ano))
                {
                    MostrarError("Formato de fecha inválido. Use MM/AA");
                    return;
                }

                
                int currentYear = DateTime.Now.Year % 100;
                int currentMonth = DateTime.Now.Month;

                if (ano < currentYear || (ano == currentYear && mes < currentMonth))
                {
                    MostrarError("La tarjeta está expirada");
                    return;
                }

               
                ENUsuario usuario = (ENUsuario)Session["usuarios"];
                if (usuario == null)
                {
                    Response.Redirect("login.aspx?redirect=metodopago.aspx");
                    return;
                }

                
                ENMetodoPago metodoPago = new ENMetodoPago(
                    numTarjeta: numeroTarjeta,
                    cvv: "000", 
                    mesCad: mes,
                    anoCad: ano,
                    username: usuario.Username
                );

                
                CADMetodoPago cadMetodoPago = new CADMetodoPago();

                if (cadMetodoPago.CrearMetodoPago(metodoPago))
                {
                    
                    Session["MetodoPago"] = new
                    {
                        NumeroTarjeta = metodoPago.NumeroTarjeta,
                        Ultimos4 = metodoPago.ObtenerUltimos4Digitos(),
                        Tipo = metodoPago.TipoTarjeta,
                        Expiracion = $"{mes:D2}/{ano:D2}"
                    };

                    Response.Redirect("confirmacion.aspx");
                }
            }
            catch (ArgumentException ex)
            {
                
                MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                
                MostrarError($"Error al procesar el pago: {ex.Message}");
            }
        }

        private void MostrarError(string mensaje)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert",
                $"alert('{mensaje.Replace("'", "\\'")}');", true);
        }
    }
}