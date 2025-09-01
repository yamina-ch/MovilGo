using library.CAD;
using library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class producto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }

            string eventTarget = Request["__EVENTTARGET"];
            string eventArg = Request["__EVENTARGUMENT"];

            if (eventTarget == "GuardarEdicion")
            {
                GuardarDesdeCamposOcultos();
            }
            else if (eventTarget == "EliminarArticulo" && int.TryParse(eventArg, out int id))
            {
                EliminarArticulo(id);
            }
        }

        private void EliminarArticulo(int id)
        {
            ENArticulo art = new ENArticulo { Articulo_id = id };
            new CADArticulo().EliminarArticulo(art);
            Response.Redirect(Request.RawUrl); // recarga la página
        }


        private void CargarProductos()
        {
            CADArticulo cad = new CADArticulo();
            gvProductos.DataSource = cad.ListarArticulos();
            gvProductos.DataBind();
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            // Cierra la sesión y redirige al login
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx"); // Cambia la URL si es diferente en tu proyecto
        }



        private void GuardarDesdeCamposOcultos()
        {
            try
            {
                ENArticulo art = new ENArticulo
                {
                    Articulo_id = int.Parse(hfEditID.Value),
                    Modelo = hfEditModelo.Value,
                    Precio = decimal.Parse(hfEditPrecio.Value),
                    Marca_id = int.Parse(hfEditMarca.Value),
                    Stock = int.Parse(hfEditStock.Value),
                    Vendido = hfEditVendido.Value == "1" ? 1 : 0
                };

                new CADArticulo().ActualizarArticuloCompleto(art);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                // Muestra el error en pantalla temporalmente para pruebas
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }


        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string raw = e.CommandArgument.ToString();
            Response.Write($"<script>alert('CommandArgument: {raw}');</script>");

            // Ahora intenta convertir solo si es válido
            if (int.TryParse(raw, out int id))
            {
                if (e.CommandName == "Eliminar")
                {
                    ENArticulo art = new ENArticulo { Articulo_id = id };
                    new CADArticulo().EliminarArticulo(art);
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                Response.Write("<script>alert('Error: CommandArgument no es un entero válido.');</script>");
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
