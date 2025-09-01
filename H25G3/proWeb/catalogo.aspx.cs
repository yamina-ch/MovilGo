using library.EN;
using Library;
using Library.CAD;
using Library.EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb  
{
    public partial class catalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    CargarCategorias();
                    CargarMarcas();
                    CargarProductos();
                    CargarColores();

                    CADCatalogo cad = new CADCatalogo();
                    rptCatalogo.DataSource = cad.ObtenerCatalogoConArticulos();
                    rptCatalogo.DataBind();
                }
                catch
                {
                    // Si falla la conexión, mostrar productos vacíos simulados
                    DataTable dtVacio = new DataTable();
                    dtVacio.Columns.Add("catalogo_id");
                    dtVacio.Columns.Add("Nombre");
                    dtVacio.Columns.Add("Color");
                    dtVacio.Columns.Add("Memoria");
                    dtVacio.Columns.Add("Precio");
                    dtVacio.Columns.Add("ImagenUrl");

                    for (int i = 0; i < 6; i++)
                    {
                        DataRow row = dtVacio.NewRow();
                        row["catalogo_id"] = 0;
                        row["Nombre"] = "Producto temporal";
                        row["Color"] = "-";
                        row["Memoria"] = "-";
                        row["Precio"] = "0.00";
                        row["ImagenUrl"] = "~/imagenes/no-data.png"; 
                        dtVacio.Rows.Add(row);
                    }

                    rptCatalogo.DataSource = dtVacio;
                    rptCatalogo.DataBind();
                }
            }
        }

        private void CargarCategorias()
        {
            ENCategoria enCat = new ENCategoria();
            var categorias = enCat.ObtenerTodas();

            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "CategoriaId";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CargarMarcas()
        {
            CADMarcas cadMarcas = new CADMarcas();
            DataTable marcas = cadMarcas.ObtenerMarcasDesdeBaseDeDatos();

            ddlMarca.DataSource = marcas;
            ddlMarca.DataTextField = "nombre";
            ddlMarca.DataValueField = "marca_id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CargarProductos()
        {
            CADCatalogo cad = new CADCatalogo();
            DataTable productos = cad.ObtenerCatalogoConArticulos();

            rptCatalogo.DataSource = productos;
            rptCatalogo.DataBind();
        }
        private void CargarColores()
        {
            CADCatalogo cad = new CADCatalogo();
            DataTable colores = cad.ObtenerColores();

            ddlColor.DataSource = colores;
            ddlColor.DataTextField = "color";
            ddlColor.DataValueField = "color";
            ddlColor.DataBind();
            ddlColor.Items.Insert(0, new ListItem("Todos", ""));
        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaId = ddlCategoria.SelectedValue;
                string marcaId = ddlMarca.SelectedValue;
                string precioFiltro = ddlPrecio.SelectedValue;
                string colorFiltro = ddlColor.SelectedValue;

                CADCatalogo cad = new CADCatalogo();
                DataTable productos = cad.ObtenerCatalogoConArticulos();

                DataView vista = productos.DefaultView;

                string filtro = "1 = 1";

                if (!string.IsNullOrEmpty(categoriaId))
                    filtro += $" AND categoria_id = {categoriaId}";

                if (!string.IsNullOrEmpty(marcaId))
                    filtro += $" AND marca_id = {marcaId}";

                if (!string.IsNullOrEmpty(colorFiltro))
                    filtro += $" AND color = '{colorFiltro}'";

                if (!string.IsNullOrEmpty(precioFiltro))
                {
                    if (precioFiltro == "200")
                        filtro += " AND Precio < 200";
                    else if (precioFiltro == "500")
                        filtro += " AND Precio >= 200 AND Precio <= 500";
                    else if (precioFiltro == "1000")
                        filtro += " AND Precio > 500";
                }

                vista.RowFilter = filtro;

                rptCatalogo.DataSource = vista;
                rptCatalogo.DataBind();
            }
            catch
            {
                rptCatalogo.DataSource = null;
                rptCatalogo.DataBind();
            }
        }
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ddlCategoria.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;
            ddlPrecio.SelectedIndex = 0;
            ddlColor.SelectedIndex = 0;

            CargarProductos(); // Recarga todos los productos sin filtro
        }
        protected void lnkCategoria_Click(object sender, EventArgs e)
        {
            string categoriaId = ((LinkButton)sender).CommandArgument;

            CADCatalogo cad = new CADCatalogo();
            DataTable productos = cad.ObtenerCatalogoConArticulos();
            DataView vista = productos.DefaultView;

            if (!string.IsNullOrEmpty(categoriaId))
                vista.RowFilter = $"categoria_id = {categoriaId}";

            rptCatalogo.DataSource = vista;
            rptCatalogo.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                CargarProductos();
                return;
            }

            CADCatalogo cad = new CADCatalogo();
            DataTable productos = cad.ObtenerCatalogoConArticulos();
            DataView vista = productos.DefaultView;

            vista.RowFilter = $"Nombre LIKE '%{textoBusqueda.Replace("'", "''")}%'";

            rptCatalogo.DataSource = vista;
            rptCatalogo.DataBind();
        }

        protected void AddCarro(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddCarro")
            {
                // Verificar si el usuario está logueado
                if (Session["usuarios"] == null)
                {
                    // Redirigir a login o mostrar mensaje
                    Response.Redirect("login.aspx?returnUrl=catalogo.aspx");
                    return;
                }

                try
                {
                    ENLineaCarrito l = new ENLineaCarrito();
                    int id = Convert.ToInt32(e.CommandArgument);
                    l.Articulo_id = id;

                    ENUsuario user = (ENUsuario)Session["usuarios"];
                    ENCarrito c = new ENCarrito();
                    c.Usuario_UName = user.Username;

                    c.ObtenerPorUsuario();
                    l.Carrito_id = c.Carrito_ID;

                    ENArticulo a = new ENArticulo();
                    a.Articulo_id = id;
                    a.LeerArticulo();

                    l.Importe = (float)a.Precio;
                    l.Cantidad = 1;

                    l.Crear();
                }
                catch (Exception ex)
                {
                    // Log del error
                    System.Diagnostics.Debug.WriteLine($"Error al añadir al carrito: {ex.Message}");

                    // Mostrar mensaje de error
                    ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                        $"alert('Error al añadir al carrito: {ex.Message}');", true);
                }
            }
        }
        public string MostrarDisponibilidad(object disponibleObj)
        {
            if (disponibleObj == DBNull.Value || disponibleObj == null)
                return ""; // No mostrar nada si no hay fecha

            DateTime disponibleDesde = Convert.ToDateTime(disponibleObj);
            DateTime hoy = DateTime.Today;

            if (hoy < disponibleDesde)
            {
                // Todavía no está disponible
                return $"<span style='color:orange; font-weight:bold;'>Disponible a partir del {disponibleDesde:dd/MM/yyyy}</span>";
            }
            else if ((hoy - disponibleDesde).Days <= 3)
            {
                // Ya disponible pero solo si fue hace 3 días o menos
                return "<span style='color:green; font-weight:bold;'>¡Ya disponible!</span>";
            }
            else
            {
                // Más de 3 días desde la fecha de disponibilidad  no mostrar nada
                return "";
            }
        }





        protected void AddFavorito(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddFavorito")
            {
                // Verificar si el usuario está logueado
                if (Session["usuarios"] == null)
                {
                    // Redirigir a login o mostrar mensaje
                    Response.Redirect("login.aspx?returnUrl=catalogo.aspx");
                    return;
                }

                try
                {
                    ENListaFavoritos l = new ENListaFavoritos();
                    int id = Convert.ToInt32(e.CommandArgument);
                    l.Articulo_id = id;

                    ENUsuario user = (ENUsuario)Session["usuarios"];
                    l.Usuario_UName = user.Username;

                    l.Crear();
                }
                catch (Exception ex)
                {
                    // Log del error
                    System.Diagnostics.Debug.WriteLine($"Error al añadir a favoritos: {ex.Message}");

                    // Mostrar mensaje de error
                    ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                        $"alert('Error al añadir a favoritos: {ex.Message}');", true);
                }
            }
        }
    }
}