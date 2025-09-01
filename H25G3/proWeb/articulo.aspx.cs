using library.EN;
using library;
using System;
using System.Data;
using System.Web;
using library.CAD;
using System.Collections.Generic;
using Library;
using System.Web.UI.WebControls;
using System.Linq;
using Library.CAD;
using System.Web.UI;
using Library.EN;


namespace proWeb
{
    public partial class articulo : System.Web.UI.Page
    {
        //protected ENComentario enComentario = new ENComentario();
        //private ENArticulo enArticulo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si se ha proporcionado un articulo_id en la consulta
                if (Request.QueryString["id"] != null)
                {
                    int articuloId = Convert.ToInt32(Request.QueryString["id"]);

                    // Obtener los detalles del artículo usando el articulo_id proporcionado
                    CADArticulo cadArticulo = new CADArticulo();
                    ENArticulo enArticulo = cadArticulo.ReadArticulo(articuloId);

                    // Mostrar los detalles del artículo en la página
                    MostrarDetallesArticulo(enArticulo);

                    MostrarValoracion(enArticulo);
                    // Cargar comentarios
                    CargarComentarios(articuloId);
                }
                else
                {
                    // Si no se proporciona un articulo_id, redirigir a página de error
                    Response.Redirect("Error.aspx");
                }
            }
        }

        private void MostrarValoracion(ENArticulo articulo)
        {
            if (articulo != null)
            {
                LiteralValoracion.Text = articulo.Valoracion.ToString("0.0");
                LiteralStars.Text = GenerarEstrellas(articulo.Valoracion);

                // Mostrar controles solo si el usuario está logueado
                if (Session["usuarios"] != null)
                {
                    lblRating.Visible = true;
                    ddlRating.Visible = true;
                    btnEnviarValoracion.Visible = true;
                }
            }
        }

        // Genera las estrellas visuales (★ llenas, ☆ vacías)
        private string GenerarEstrellas(decimal valoracion)
        {
            int estrellasLlenas = (int)Math.Round(valoracion);
            string estrellasHTML = new string('★', estrellasLlenas) + new string('☆', 5 - estrellasLlenas);
            return $"<span style='color: gold; font-size: 18px;'>{estrellasHTML}</span>";
        }

        // Evento al enviar la valoración
        protected void SubmitRating_Click(object sender, EventArgs e)
        {
            if (Session["usuarios"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            int articuloId = Convert.ToInt32(Request.QueryString["id"]);
            int nuevaValoracion = Convert.ToInt32(ddlRating.SelectedValue);

            CADArticulo cadArticulo = new CADArticulo();
            bool exito = cadArticulo.ActualizarValoracion(articuloId, nuevaValoracion);

            if (exito)
            {
                // Recargar los datos para mostrar la nueva valoración
                ENArticulo articulo = cadArticulo.ReadArticulo(articuloId);
                MostrarValoracion(articulo);

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                    "alert('¡Valoración enviada correctamente!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showError",
                    "alert('No se pudo registrar la valoración.');", true);
            }
        }

        // Método para mostrar los detalles del artículo en la página
        private void MostrarDetallesArticulo(ENArticulo articulo)
        {
            if (articulo != null)
            {
                // Mostrar modelo y marca
                LiteralModelo.Text = articulo.Modelo;
                CADMarcas cadMarcas = new CADMarcas();
                string nombreMarca = cadMarcas.ObtenerMarca(articulo.Marca_id);
                LiteralMarca.Text = !string.IsNullOrEmpty(nombreMarca) ? nombreMarca : "Marca desconocida";


                // Mostrar características técnicas
                LiteralSistemaOperativo.Text = articulo.Sistema_operativo;
                LiteralMemoria.Text = articulo.Memoria;
                LiteralBateria.Text = articulo.Bateria ?? "No especificado";
                LiteralColor.Text = articulo.Color ?? "No especificado";
                LiteralAnyo.Text = articulo.Anyo != 0 ? articulo.Anyo.ToString() : "N/A";
                LiteralEstado.Text = !string.IsNullOrEmpty(articulo.Estado) ? articulo.Estado : "Nuevo";
                LiteralStock.Text = articulo.Stock > 0 ? articulo.Stock.ToString() : "Agotado";

                // Mostrar precio y descripción
                LiteralPrecio.Text = $"{articulo.Precio.ToString("C")}";
                LiteralDescripcion.Text = articulo.Descripcion ?? "Descripción no disponible";

                // Mostrar vendedor y proveedor
                LiteralVendedor.Text = articulo.Vendedor_UName;

                if (articulo.Proveedor_id.HasValue)
                {
                    CADProveedor cadProveedor = new CADProveedor();
                    ENProveedor proveedor = new ENProveedor();
                    proveedor.Proveedor_id = articulo.Proveedor_id.Value;

                    if (cadProveedor.LeerProveedor(proveedor))
                    {
                        LiteralProveedor.Text = $" | Proveedor: {proveedor.Nombre}";
                        LiteralProveedor.Visible = true;
                    }

                }

                // Mostrar estado de venta
                if (articulo.Vendido == 1)
                {
                    LiteralVendido.Text = "Vendido";
                    btnComprar.Visible = false;
                    btnCarrito.Visible = false;
                }
                else
                {
                    LiteralVendido.Text = articulo.Stock > 0 ? "Disponible" : "Agotado";//en la bbdd no hay ejemplos para agotado pero funciona ,lo he probado
                }

                // Mostrar imagen
                imgArticulo.ImageUrl = !string.IsNullOrEmpty(articulo.Url_imagen) ?
                    articulo.Url_imagen : "/imagenes/default-product.png";
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }
        protected bool IsVisible(string type, string username)
        {
            if (Session["usuarios"] != null)
            {
                ENUsuario usuario = (ENUsuario)Session["usuarios"];
                if (usuario.Admin || usuario.Username == username)
                {
                    return type == "Edit" || type == "Delete";
                }
            }
            return false;
        }


        

        private void CargarComentarios(int articuloId)
        {
            CADComentario cadComentario = new CADComentario();
            List<ENComentario> comentarios = cadComentario.GetComentarios(articuloId: articuloId);
            RepeaterComentarios.DataSource = comentarios;
            RepeaterComentarios.DataBind();
        }

        protected void ButtonAgregarComentario_Click(object sender, EventArgs e)
        {
            if (Session["usuarios"] != null)
            {
                ENUsuario usuario = (ENUsuario)Session["usuarios"];
                int articuloId = Convert.ToInt32(Request.QueryString["id"]);
                string comentarioTexto = TextBoxComentario.Text;
                DateTime fechaComentario = DateTime.Now;

                ENComentario nuevoComentario = new ENComentario(0, articuloId, usuario.Username, comentarioTexto, fechaComentario);
                CADComentario cadComentario = new CADComentario();
                cadComentario.AddComentario(nuevoComentario);


                CargarComentarios(articuloId);
                TextBoxComentario.Text = string.Empty;


            }
            else
            {
                // Redirigir al usuario a la página de inicio de sesión si no está iniciado sesión
                Response.Redirect("login.aspx");
            }
        }

        protected void ButtonEditar_Click(object sender, EventArgs e)
        {
            // Implementación de ButtonEditar_Click...
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;


            Label labelComentario = (Label)item.FindControl("LabelComentario");
            TextBox textBoxEditComentario = (TextBox)item.FindControl("TextBoxEditComentario");
            Button buttonGuardar = (Button)item.FindControl("ButtonGuardar");
            Button buttonEditar = (Button)item.FindControl("ButtonEditar");

            labelComentario.Visible = false;
            textBoxEditComentario.Visible = true;
            buttonGuardar.Visible = true;
            buttonEditar.Visible = false;
        }



        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int comentarioId = int.Parse(btn.CommandArgument);
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;

            TextBox textBoxEditComentario = (TextBox)item.FindControl("TextBoxEditComentario");
            string comentarioTexto = textBoxEditComentario.Text;

            ENUsuario usuario = (ENUsuario)Session["usuarios"];
            int articuloId = Convert.ToInt32(Request.QueryString["id"]);
            ENComentario comentarioEditado = new ENComentario(comentarioId, articuloId, usuario.Username, comentarioTexto, DateTime.Now);
            CADComentario cadComentario = new CADComentario();
            cadComentario.UpdateComentario(comentarioEditado);

            CargarComentarios(articuloId);

        }

        protected void ButtonEliminar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int comentarioId = int.Parse(btn.CommandArgument);
            CADComentario cadComentario = new CADComentario();

            if (Session["usuarios"] != null)
            {
                ENUsuario usuario = (ENUsuario)Session["usuarios"];
                ENComentario comentario = cadComentario.GetComentarios(comentarioId: comentarioId).FirstOrDefault();

                if (comentario.UsuarioUName == usuario.Username || usuario.Admin)
                {
                    cadComentario.DeleteComentario(comentarioId);
                    int articuloId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarComentarios(articuloId);
                }
            }

        }

        protected bool IsEditButtonVisible(string comentarioUsername)
        {
            if (Session["usuarios"] != null)
            {
                ENUsuario usuario = (ENUsuario)Session["usuarios"];
                return usuario.Username == comentarioUsername;
            }
            return false;
        }


        protected bool IsDeleteButtonVisible(string comentarioUsername)
        {
            if (Session["usuarios"] != null)
            {
                ENUsuario usuario = (ENUsuario)Session["usuarios"];
                return usuario.Username == comentarioUsername || usuario.Admin;
            }
            return false;
        }

        protected void AccionArticulo_Click(object sender, EventArgs e)
        {
            if (Session["usuarios"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            Button btn = (Button)sender;
            int articuloId = Convert.ToInt32(Request.QueryString["id"]);

            CADArticulo cadCatalogo = new CADArticulo();
            ENArticulo articulo = cadCatalogo.ReadArticulo(articuloId);

            ENUsuario user = (ENUsuario)Session["usuarios"];

            if (articulo.Vendido == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "alert('No está permitida esta acción porque este artículo está vendido.');", true);
                return;
            }
            else
            {
                if (btn.CommandName == "Favoritos")
                {
                    // Redirigir a página de favoritos
                    ENListaFavoritos l = new ENListaFavoritos();
                    l.Articulo_id = articulo.Articulo_id;
                    
                    l.Usuario_UName = user.Username;
                    l.Crear();

                    Response.Redirect($"favorito.aspx?action=add&articuloId={articuloId}");
                }
                else if (btn.CommandName == "Carrito")
                {
                    // Redirigir a página de carrito
                    ENLineaCarrito linea_carro = new ENLineaCarrito();
                    linea_carro.Articulo_id = articulo.Articulo_id;

                    ENCarrito carro = new ENCarrito();
                    carro.Usuario_UName = user.Username;
                    carro.ObtenerPorUsuario();

                    linea_carro.Carrito_id = carro.Carrito_ID;
                    linea_carro.Importe = (float)articulo.Precio;
                    linea_carro.Cantidad = 1;
                    linea_carro.Crear();

                    Response.Redirect($"carrito.aspx?action=add&articuloId={articuloId}");
                }
                else if (btn.CommandName == "Compra")
                {
                    // Verificar stock antes de comprar
                    if (articulo.Stock > 0)
                    {
                        ENLineaCarrito linea_carro = new ENLineaCarrito();
                        linea_carro.Articulo_id = articulo.Articulo_id;

                        ENCarrito carro = new ENCarrito();
                        carro.Usuario_UName = user.Username;
                        carro.ObtenerPorUsuario();

                        linea_carro.Carrito_id = carro.Carrito_ID;
                        linea_carro.Importe = (float)articulo.Precio;
                        linea_carro.Cantidad = 1;
                        linea_carro.Crear();
                        // Redirigir a la página de compra
                        Response.Redirect($"metodopago.aspx?articuloId={articuloId}");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                            "alert('Este artículo no tiene stock disponible.');", true);
                    }
                }
            }
        }


    }
}
