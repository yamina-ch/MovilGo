using library.EN;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuarios"] != null)
                {
                    ENUsuario user = (ENUsuario)Session["usuarios"];
                    string username = user.Username.ToString();
                    LoadUserFavoritos(username);
                }
                else
                {
                    Response.Redirect("login.aspx?returnUrl=" + Server.UrlEncode(Request.Url.PathAndQuery));
                }
            }
        }

        private void LoadUserFavoritos(string username)
        {
            try
            {
                ENListaFavoritos linea = new ENListaFavoritos();
                linea.Usuario_UName = username;
                List<ENListaFavoritos> lista = linea.ObtenerPorUsuario();

                if(lista == null)
                {
                    LoadEmptyFavoritos();
                }

                List<ProductoItem> productos = new List<ProductoItem>();


                foreach (var l in lista)
                {
                    ENArticulo articulo = new ENArticulo();
                    articulo.Articulo_id = l.Articulo_id;
                    if (articulo.LeerArticulo())
                    {
                        ProductoItem item = new ProductoItem
                        {
                            Id = articulo.Articulo_id,
                            Nombre = articulo.Modelo,
                            Precio = (double)articulo.Precio,
                            ImagenUrl = articulo.Url_imagen,
                            Color = articulo.Color,
                            Lista_favorito_id = l.Lista_favorito_id
                        };
                        productos.Add(item);
                    }
                }

                // 4. Mostrar los productos en el carrito
                Session["Facvoritos"] = productos;
                container.DataSource = productos;
                container.DataBind();
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error al cargar la lista de favoritos: {ex.Message}');", true);
                LoadEmptyFavoritos();
            }
        }

        private void LoadEmptyFavoritos()
        {
            Session["Carrito"] = new List<ProductoItem>();
            container.DataSource = null;
            container.DataBind();
        }

        [WebMethod]
        public static string EliminarFavorito(int listaFavoritoId)
        {
            try
            {
                ENListaFavoritos favorito = new ENListaFavoritos
                {
                    Lista_favorito_id = listaFavoritoId
                };

                bool resultado = favorito.Eliminar();

                if (resultado)
                {
                    return "success";
                }
                else
                {
                    return "No se pudo eliminar el favorito (operación fallida)";
                }
            }
            catch (Exception ex)
            {
                return $"Error al eliminar favorito: {ex.Message}";
            }
        }
    }
}