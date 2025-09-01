using library.CAD;
using library.EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class proveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id;
                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    CargarProveedorParaEdicion(id);
                }
            }
            else
            {
                MostrarListado();
                CargarProveedores();
            }
        }

        private void CargarProveedorParaEdicion(int id)
        {
            ENProveedor proveedor = new ENProveedor { Proveedor_id = id };
            CADProveedor cad = new CADProveedor();

            if (cad.LeerProveedor(proveedor))
            {
                hdnProveedorId.Value = proveedor.Proveedor_id.ToString();
                txtNombre.Text = proveedor.Nombre;
                txtCIF.Text = proveedor.CIF;
                txtDireccion.Text = proveedor.Direccion;
                txtTelefono.Text = proveedor.Telefono;
                txtEmail.Text = proveedor.Email;
                formTitle.InnerText = "Editar Proveedor";
                MostrarFormulario();
            }
            else
            {
                MostrarListado();
                CargarProveedores();
                ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                    "alert('No se encontró el proveedor solicitado');", true);
            }
        }

        private void MostrarListado()
        {
            listSection.Visible = true;
            formProveedor.Visible = false;
        }

        private void MostrarFormulario()
        {
            listSection.Visible = false;
            formProveedor.Visible = true;
        }

        private void CargarProveedores()
        {
            string filtro = ddlFiltro.SelectedValue;
            string busqueda = txtBusqueda.Text.Trim();
            string orden = ddlOrden.SelectedValue;

            CADProveedor cadProveedor = new CADProveedor();
            DataSet ds = cadProveedor.ObtenerProveedores(filtro, busqueda, orden);

            rptProveedores.DataSource = ds.Tables.Count > 0 ? ds.Tables[0] : null;
            rptProveedores.DataBind();

            // Mostrar mensaje si no hay resultados
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "noResults",
                    "alert('No se encontraron proveedores con los criterios actuales');", true);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtCIF.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = ddlFiltro.SelectedValue;
            string busqueda = txtBusqueda.Text.Trim();
            string orden = ddlOrden.SelectedValue;

            CADProveedor cadProveedor = new CADProveedor();
            DataSet ds = cadProveedor.ObtenerProveedores(filtro, busqueda, orden);

            // Asegurarse de que siempre mostramos el listado
            MostrarListado();
            productosProveedorSection.Visible = false;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rptProveedores.DataSource = ds.Tables[0];
                rptProveedores.DataBind();
            }
            else
            {
                // Mostrar mensaje de no resultados
                rptProveedores.DataSource = null;
                rptProveedores.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "noResults",
                    "alert('No se encontraron proveedores con los criterios de búsqueda');", true);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            hdnProveedorId.Value = "0";//nuevo proveedor
            formTitle.InnerText = "Nuevo Proveedor";
            MostrarFormulario();
        }

        protected void HandleAction(object sender, CommandEventArgs e)
        {
            string commandName = e.CommandName;
            string proveedorId = e.CommandArgument.ToString();

            switch (commandName)
            {
                case "View":
                    Response.Redirect($"proveedor.aspx?id={proveedorId}");
                    break;

                case "Edit":
                    if (int.TryParse(proveedorId, out int id))
                    {
                        CargarProveedorParaEdicion(id);
                    }
                    break;
                case "Delete":
                    if (int.TryParse(proveedorId, out int idDelete))
                    {
                        EliminarProveedor(idDelete);
                    }
                    break;
                case "ViewProducts":
                    if (int.TryParse(proveedorId, out int idProducts))
                    {
                        VerProductosProveedor(idProducts);
                    }
                    break;


            }
        }

        private void EliminarProveedor(int id)
        {
            CADProveedor cad = new CADProveedor();

            try
            {
                bool resultado = cad.EliminarProveedor(id);

                if (resultado)
                {
                    // Recargar la lista después de eliminar
                    CargarProveedores();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "alert('Proveedor eliminado correctamente');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                        "alert('No se pudo eliminar el proveedor ya que tiene artículos asociados');", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                    "alert('No se pudo eliminar el proveedor ya que tiene artículos asociados');", true);
            }
        }



        protected void rptProveedores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var proveedor = (DataRowView)e.Item.DataItem;
                string nombre = proveedor["nombre"].ToString();

                // Ejemplo: encontrar un control en la fila y establecerle valor
                Label lblNombre = (Label)e.Item.FindControl("lblNombre");
                if (lblNombre != null)
                {
                    lblNombre.Text = nombre;
                }

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) // Verifica todas las validaciones
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                    "alert('Por favor complete correctamente todos los campos requeridos');", true);
                return;
            }

            ENProveedor proveedor = new ENProveedor
            {
                Proveedor_id = int.Parse(hdnProveedorId.Value),
                Nombre = txtNombre.Text.Trim(),
                CIF = txtCIF.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            CADProveedor cad = new CADProveedor();
            bool resultado;

            if (proveedor.Proveedor_id == 0)
            {
                resultado = cad.CrearProveedor(proveedor);
            }
            else
            {
                resultado = cad.ActualizarProveedor(proveedor);
            }

            if (resultado)
            {
                CargarProveedores();
                MostrarListado();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                    "alert('Proveedor guardado correctamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showerror",
                    "alert('Error al guardar el proveedor');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarListado();
        }








        [System.Web.Services.WebMethod]
        public static string GetProductosPorProveedor(int proveedorId)
        {
            ENProveedor proveedor = new ENProveedor { Proveedor_id = proveedorId };
            DataSet ds = proveedor.ObtenerProductosPorProveedor();

            // Convertir DataSet a JSON para devolverlo al cliente
            return DataSetToJson(ds);
        }

        private static string DataSetToJson(DataSet ds)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataTable dt in ds.Tables)
            {
                var arr = new List<Dictionary<string, object>>();

                foreach (DataRow dr in dt.Rows)
                {
                    var row = new Dictionary<string, object>();

                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }

                    arr.Add(row);
                }

                dict.Add(dt.TableName, arr);
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(dict);
        }

        private void VerProductosProveedor(int proveedorId)
        {
            CADProveedor cad = new CADProveedor();
            DataSet ds = cad.ObtenerProductosPorProveedor(proveedorId);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    Debug.WriteLine("Columna: " + column.ColumnName);
                }
                // Configurar el GridView
                gvProductos.DataSource = ds.Tables[0];
                gvProductos.DataBind();

                // Mostrar la sección de productos
                productosProveedorSection.Visible = true;
                listSection.Visible = false;
                formProveedor.Visible = false;

                // Opcional: Obtener y mostrar el nombre del proveedor
                ENProveedor proveedor = new ENProveedor { Proveedor_id = proveedorId };
                CADProveedor cadProveedor = new CADProveedor();
                if (cadProveedor.LeerProveedor(proveedor))
                {
                    lblProveedorNombre.Text = proveedor.Nombre;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "NoProducts",
                    "alert('Este proveedor no tiene productos asociados');", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Ocultar la sección de productos y mostrar el listado principal
            productosProveedorSection.Visible = false;
            MostrarListado();
            CargarProveedores();
        }

    }
}