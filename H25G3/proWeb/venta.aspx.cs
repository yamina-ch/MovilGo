using library.CAD;
using library.EN;
using Library.CAD;
using Library.EN;
using Library;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class venta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuarios"] != null)
                {
                    ENUsuario user = (ENUsuario)Session["usuarios"];
                    if (!user.Admin)
                    {
                        divProveedor.Visible = false;
                        divStock.Visible = false;
                    }
                    else if (!IsPostBack)
                    {
                      
                        CargarProveedores();
                    }
                }

                CargarMarcas();
                CargarCategorias();
            }
            cvNuevaMarca.Enabled = ddlMarca.SelectedValue == "Otro";
            cvNuevaCategoria.Enabled = ddlCategoria.SelectedValue == "Otro";
        }

        private void CargarMarcas()
        {
            CADMarcas cad = new CADMarcas();
            DataTable marcas = cad.ObtenerMarcasDesdeBaseDeDatos();

            ddlMarca.Items.Clear();
            ddlMarca.Items.Add(new ListItem("-- Selecciona una marca --", ""));

            foreach (DataRow row in marcas.Rows)
            {
                string nombre = row["nombre"].ToString().Trim();
                ddlMarca.Items.Add(new ListItem(nombre, nombre));
            }

            ddlMarca.Items.Add(new ListItem("Otro...", "Otro"));
        }
        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNuevaMarca.Visible = ddlMarca.SelectedValue == "Otro";
        }
        private void CargarCategorias()
        {
            CADCategoria cad = new CADCategoria();
            var categorias = cad.ObtenerTodas();

            ddlCategoria.Items.Clear();
            ddlCategoria.Items.Add(new ListItem("-- Selecciona una categoría --", ""));

            foreach (var cat in categorias)
            {
                ddlCategoria.Items.Add(new ListItem(cat.Nombre, cat.Nombre));
            }

            ddlCategoria.Items.Add(new ListItem("Otro...", "Otro"));
        }
        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNuevaCategoria.Visible = ddlCategoria.SelectedValue == "Otro";
        }


        protected void Next1_Click(object sender, EventArgs e)
        {
            string mensajeError = "";

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
                mensajeError += "El modelo es obligatorio.\\n";

            if (ddlMarca.SelectedValue == "")
                mensajeError += "Debe seleccionar una marca.\\n";

            if (ddlMarca.SelectedValue == "Otro" && string.IsNullOrWhiteSpace(txtNuevaMarca.Text))
                mensajeError += "Debe introducir una nueva marca.\\n";

            if (ddlCategoria.SelectedValue == "")
                mensajeError += "Debe seleccionar una categoría.\\n";

            if (ddlCategoria.SelectedValue == "Otro" && string.IsNullOrWhiteSpace(txtNuevaCategoria.Text))
                mensajeError += "Debe introducir una nueva categoría.\\n";

            if (string.IsNullOrWhiteSpace(txtSistemaOperativo.Text))
                mensajeError += "El sistema operativo es obligatorio.\\n";

            
            if (!int.TryParse(txtAnyo.Text, out int anyo))
            {
                mensajeError += "El año debe ser un número válido.\\n";
            }
            else if (anyo > DateTime.Now.Year)
            {
                mensajeError += $"El año no puede ser mayor que {DateTime.Now.Year}.\\n";
            }

            if (!string.IsNullOrEmpty(mensajeError))
            {
                litErrores.Text = $"<div class='error-box'>{mensajeError.Replace("\\n", "<br/>")}</div>";
                return;
            }


            // Si todo está correcto, continúa
            MultiView1.ActiveViewIndex = 1;
            progressBarContainer.Style["width"] = "50%";
            step1.CssClass = "step";
            step2.CssClass = "step active";
        }


        protected void Back1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            progressBarContainer.Style["width"] = "25%";
            step1.CssClass = "step active";
            step2.CssClass = "step";
        }

        protected void Next2_Click(object sender, EventArgs e)
        {
            string errores = "";

            if (string.IsNullOrWhiteSpace(txtColor.Text))
                errores += "El color es obligatorio.\\n";

            if (string.IsNullOrWhiteSpace(txtMemoria.Text))
                errores += "La memoria es obligatoria.\\n";

            if (string.IsNullOrWhiteSpace(txtBateria.Text))
                errores += "La batería es obligatoria.\\n";

            if (string.IsNullOrWhiteSpace(txtEstado.Text))
                errores += "El estado del producto es obligatorio.\\n";

            if (!string.IsNullOrEmpty(errores))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errores}');", true);
                return;
            }

            // Si todo está bien, continuar
            MultiView1.ActiveViewIndex = 2;
            progressBarContainer.Style["width"] = "75%";
            step2.CssClass = "step";
            step3.CssClass = "step active";
        }


        protected void Back2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            progressBarContainer.Style["width"] = "50%";
            step2.CssClass = "step active";
            step3.CssClass = "step";
        }

        protected void Next3_Click(object sender, EventArgs e)
        {
            // Validación de imagen
            if (!fileImagen.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe subir una imagen antes de continuar.');", true);
                return;
            }

            try
            {
                // Guardar imagin tempo
                string fileName = Path.GetFileName(fileImagen.PostedFile.FileName);
                string filePath = Server.MapPath("~/imagenes/") + fileName;

                fileImagen.SaveAs(filePath);

                // Guardar la URL en Session para usar en btnPublicar_Click
                Session["UploadedImagePath"] = "/imagenes/" + fileName;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al guardar la imagen: {ex.Message}');", true);
                return;
            }

            // Si todo va bien, pasar al paso 4
            MultiView1.ActiveViewIndex = 3;
            progressBarContainer.Style["width"] = "100%";
            step3.CssClass = "step";
            step4.CssClass = "step active";
        }

        private void CargarProveedores()
        {
            CADProveedor cad = new CADProveedor();
            DataSet ds = cad.ObtenerProveedores();

            ddlProveedor.Items.Clear();
            ddlProveedor.Items.Add(new ListItem("-- Selecciona un proveedor --", ""));

            foreach (DataRow row in ds.Tables["Proveedores"].Rows)
            {
                string nombre = row["nombre"].ToString();
                string id = row["proveedor_id"].ToString();

                ddlProveedor.Items.Add(new ListItem(nombre, id));
            }
        }


        protected void Back3_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            progressBarContainer.Style["width"] = "75%";
            step3.CssClass = "step active";
            step4.CssClass = "step";
        }

        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            if (Session["usuarios"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }

            ENUsuario user = (ENUsuario)Session["usuarios"];

            // Obtener la imagen desde en paso 3 validada y guardada
            string fileName = Session["UploadedImagePath"] as string;

            if (string.IsNullOrEmpty(fileName))
            {
                Response.Write("<script>alert('Debe subir una imagen.');</script>");
                return;
            }
            // Obtener marca seleccionada o escrita
            string marcaSeleccionada = ddlMarca.SelectedValue;
            string marcaFinal = marcaSeleccionada;

            if (marcaSeleccionada == "Otro")
            {
                if (string.IsNullOrWhiteSpace(txtNuevaMarca.Text))
                {
                    Response.Write("<script>alert('Debe introducir una nueva marca.');</script>");
                    return;
                }

                marcaFinal = txtNuevaMarca.Text.Trim();
            }

            // Obtener o insertar marca y obtener su ID
            CADMarcas cadMarcas = new CADMarcas();
            int marcaId = cadMarcas.CheckAndInsertMarca(marcaFinal);
            string categoriaSeleccionada = ddlCategoria.SelectedValue;
            string categoriaFinal = categoriaSeleccionada;

            if (categoriaSeleccionada == "Otro")
            {
                if (string.IsNullOrWhiteSpace(txtNuevaCategoria.Text))
                {
                    Response.Write("<script>alert('Debe introducir una nueva categoría.');</script>");
                    return;
                }

                categoriaFinal = txtNuevaCategoria.Text.Trim();
            }

            CADCategoria cadCategoria = new CADCategoria();
            int categoriaId = cadCategoria.CheckAndInsertCategoria(categoriaFinal);

            // Crear artículo
            ENArticulo articulo = new ENArticulo
            {
                Modelo = txtModelo.Text,
                Marca_id = marcaId,

                Categoria_id = categoriaId,

                Sistema_operativo = txtSistemaOperativo.Text,
                Anyo = int.Parse(txtAnyo.Text),
                Color = txtColor.Text,
                Memoria = txtMemoria.Text,
                Bateria = txtBateria.Text,
                Estado = txtEstado.Text,
                Precio = decimal.Parse(txtPrecioPublicado.Text),
                Descripcion = txtDescripcion.Text,
                Vendedor_UName = user.Username,
                Vendido = 0,
                Valoracion = 0,
                Url_imagen = fileName
            };

            // Stock y proveedor solo para admin
            if (user.Admin)
            {
                articulo.Stock = !string.IsNullOrWhiteSpace(txtStock.Text) ? int.Parse(txtStock.Text) : 0;

                if (!string.IsNullOrWhiteSpace(ddlProveedor.SelectedValue))
                    articulo.Proveedor_id = int.Parse(ddlProveedor.SelectedValue);

            }
            else
            {
                articulo.Stock = 1;
                articulo.Proveedor_id = null;
            }

            if (!articulo.CrearArticulo())
            {
                Response.Write("<script>alert('Error al crear el artículo');</script>");
                return;
            }

            // Crear venta
            decimal? po = null;
            if (decimal.TryParse(txtPrecioOriginal.Text, out decimal parsedPo))
                po = parsedPo;

            DateTime? disp = null;
            if (DateTime.TryParse(txtDisponibleHasta.Text, out DateTime parsedDisp))
                disp = parsedDisp;

            ENVenta venta = new ENVenta
            {
                ArticuloId = articulo.Articulo_id,
                FechaAnuncio = DateTime.Now,
                VendedorUName = user.Username,
                PrecioOriginal = po,
                PrecioPublicado = decimal.Parse(txtPrecioPublicado.Text),
                MotivoVenta = txtMotivo.Text,
                DisponibleHasta = disp
            };

            if (!venta.CrearVenta())
            {
                Response.Write("<script>alert('Error al crear la venta');</script>");
                return;
            }

            // Crear catálogo
            ENCatalogo catalogo = new ENCatalogo
            {
                Nombre = articulo.Modelo,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                Vendido = 0,
                CategoriaId = articulo.Categoria_id,
                UrlImagen = articulo.Url_imagen
            };

            CADCatalogo cadCatalogo = new CADCatalogo();
            int nuevoCatalogoId = cadCatalogo.CrearCatalogo(catalogo);

            if (nuevoCatalogoId <= 0)
            {
                Response.Write("<script>alert('Error al crear en el catálogo');</script>");
                return;
            }

            // Actualizar artículo con ID de catálogo
            articulo.Catalogo_id = nuevoCatalogoId;
            if (!articulo.ActualizarArticulo())
            {
                Response.Write("<script>alert('Error al actualizar el artículo con el catálogo');</script>");
                return;
            }

            // Limpia la sesión de imagen después de usarla
            Session.Remove("UploadedImagePath");

            //Response.Redirect("~/Catalogo.aspx");
            string imagenUrl = articulo.Url_imagen;
            string nombre = articulo.Modelo;
            string precio = articulo.Precio.ToString("F2");
            string categoria = ddlCategoria.SelectedValue;
            string descripcion = articulo.Descripcion;

            // Usar ScriptManager para inyectar el SweetAlert en el cliente
            string script = $@"
    Swal.fire({{
        title: '¡Anuncio publicado!',
        html: `
            <img src='{imagenUrl}' style='max-width:200px; margin-bottom:10px;' />
            <p><strong>Modelo:</strong> {nombre}</p>
            <p><strong>Precio:</strong> €{precio}</p>
            <p><strong>Categoría:</strong> {categoria}</p>
            <p><strong>Descripción:</strong> {descripcion}</p>
        `,
        icon: 'success',
        confirmButtonText: 'Ir al catálogo'
    }}).then((result) => {{
        if (result.isConfirmed) {{
            window.location.href = '/Catalogo.aspx';
        }}
    }});
";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "sweetalert", script, true);

        }

    }
}