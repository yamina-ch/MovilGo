using System;
using System.Collections.Generic;
using System.Linq;

namespace proWeb
{
    public partial class pedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validar sesión
            if (Session["usuarios"] == null)
            {
                Response.Redirect("login.aspx?redirect=pedido");
                return;
            }

            if (!IsPostBack)
            {
                if (Session["Carrito"] is List<ProductoItem> productos && productos.Count > 0)
                {
                    rptPedido.DataSource = productos;
                    rptPedido.DataBind();

                    double subtotal = productos.Sum(p => p.Precio * p.Cantidad);
                    
                    double total = subtotal + 4.5;

                    litSubtotal.Text = subtotal.ToString("0.00");
                   
                    litTotal.Text = total.ToString("0.00");
                    Session["Subtotal"] = subtotal;
                    
                }
                else
                {
                    Response.Redirect("carrito.aspx");
                }
            }
        }

        protected void btnContinuarPago_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string direccionCompleta = $"{txtCalle.Text} {txtNumero.Text}, " +
                                      $"{(!string.IsNullOrEmpty(txtPlanta.Text) ? txtPlanta.Text + "ª " : "")}" +
                                      $"{(!string.IsNullOrEmpty(txtPuerta.Text) ? txtPuerta.Text : "")}";

                double costoEnvio = 4.5; 

                Session["DatosEnvio"] = new
                {
                    Nombre = txtNombre.Text,
                    Telefono = "+34" + txtTelefono.Text,
                    Direccion = direccionCompleta,
                    Ciudad = txtCiudad.Text,
                    Provincia = ddlProvincia.SelectedValue,
                    CP = txtCP.Text,
                    MetodoEnvio = "estándar",
                    CostoEnvio = costoEnvio
                };

                Response.Redirect("metodopago.aspx");
            }
        }
    }
}