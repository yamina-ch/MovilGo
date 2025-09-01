using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.CAD;
using library.EN;
using Library;
using System.Web.Services;

namespace proWeb
{
    public partial class carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuarios"] != null)
                {
                    ENUsuario user = (ENUsuario)Session["usuarios"];
                    string username = user.Username.ToString();
                    LoadUserCart(username);
                }
                else
                {
                    Response.Redirect("login.aspx?returnUrl=" + Server.UrlEncode(Request.Url.PathAndQuery));
                }
            }
        }

        // listado de articulos de su carrito
        private void LoadUserCart(string username)
        {
            try
            {
                // 1. Comprobar si existe el carrito del usuario
                ENCarrito carritoUsuario = new ENCarrito();
                carritoUsuario.Usuario_UName = username;
                if (!carritoUsuario.ObtenerPorUsuario())
                {
                    carritoUsuario.Usuario_UName = username;
                    if (!carritoUsuario.Crear())
                    {
                        System.Diagnostics.Debug.WriteLine($"No se pudo crear el carrito");
                        return;
                    }
                }

                // 2. Obtener las líneas del carrito
                List<ENLineaCarrito> lineasCarrito = carritoUsuario.ObtenerArticulos();

                if (lineasCarrito == null || lineasCarrito.Count == 0)
                {
                    LoadEmptyCart();
                    return;
                }

                // 3. Obtener información de los artículos
                List<ProductoItem> productos = new List<ProductoItem>();
                decimal total = 0;

                foreach (var linea in lineasCarrito)
                {
                    ENArticulo articulo = new ENArticulo();
                    articulo.Articulo_id = linea.Articulo_id;
                    if (articulo.LeerArticulo())
                    {
                        ProductoItem item = new ProductoItem
                        {
                            Id = articulo.Articulo_id,
                            Nombre = articulo.Modelo,
                            Precio = (double)articulo.Precio,
                            Cantidad = linea.Cantidad,
                            ImagenUrl = articulo.Url_imagen,
                            Importe = (decimal)(articulo.Precio * linea.Cantidad),
                            Linea_carrito_id = linea.Linea_carrito_id
                        };
                        productos.Add(item);
                        total += item.Importe;
                    }
                }

                // 4. Mostrar los productos en el carrito
                Session["Carrito"] = productos;
                container.DataSource = productos;
                litTotal.Text = total.ToString("0.00");
                container.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error al cargar el carrito: {ex.Message}');", true);
                LoadEmptyCart();
            }
        }

        // muestra vacio si no existe carrito
        private void LoadEmptyCart()
        {
            Session["Carrito"] = new List<ProductoItem>();
            container.DataSource = null;
            litTotal.Text = "0.00";
            container.DataBind();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["usuarios"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            List<ProductoItem> carrito = Session["Carrito"] as List<ProductoItem>;
            if (carrito == null || carrito.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El carrito está vacío');", true);
                return;
            }

            Response.Redirect("pedido.aspx");
        }

        [WebMethod]
        public static bool EliminarLineaCarrito(int lineaId)
        {
            try
            {
                ENLineaCarrito linea = new ENLineaCarrito
                {
                    Linea_carrito_id = lineaId
                };
                return linea.Eliminar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al eliminar línea: {ex.Message}");
                return false;
            }
        }

        [WebMethod]
        public static bool ActualizarCantidad(int lineaId, int nuevaCantidad)
        {
            try
            {
                ENLineaCarrito linea = new ENLineaCarrito
                {
                    Linea_carrito_id = lineaId,
                    Cantidad = nuevaCantidad
                };

                System.Diagnostics.Debug.WriteLine($"Actualizando línea {lineaId} con cantidad {nuevaCantidad}");
                bool resultado = linea.Actualizar();
                System.Diagnostics.Debug.WriteLine($"Resultado actualización: {resultado}");

                return resultado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al actualizar cantidad: {ex.Message}");
                return false;
            }
        }

        // Eliminar completamente el método container_ItemDataBound ya que no es necesario
        // Los IDs se generan directamente en el HTML del ItemTemplate
    }
}