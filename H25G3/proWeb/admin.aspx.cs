using Library;
using Library.CAD;
using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Library.ENUsuario;

namespace proWeb
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                
                Response.Redirect("login.aspx");
            }
        }


      



        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string username = e.CommandArgument.ToString();

            if (e.CommandName == "Editar")
            {
                // Llama al método JavaScript para editar
                ScriptManager.RegisterStartupScript(this, GetType(), "editar", $"editarUsuarioSwal('{username}');", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                // Llama al método JavaScript para eliminar
                ScriptManager.RegisterStartupScript(this, GetType(), "eliminar", $"eliminarUsuario('{username}');", true);
            }
        }


        [System.Web.Services.WebMethod]
        public static bool CambiarEstadoAdmin(string username, string nuevoRol)
        {
            bool rol = nuevoRol == "1";  // If the new role is "1", the user is an admin, otherwise they are not.
            ENUsuario usuario = new ENUsuario();
            usuario.Username = username;
            usuario.Admin = rol;

            // Update the user's role in the database
            return usuario.ActualizarAdmin(); // Ensure this only updates the admin status
        }


        protected void MostrarUsuarios_Click(object sender, EventArgs e)
        {
            //phUserTable.Visible = true;
        }

        

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session["usuarios"] = null;
            Response.Redirect("~/login.aspx");
        }
        [WebMethod]
        public static ENUsuario ObtenerDatosUsuario(string username)
        {
            ENUsuario u = new ENUsuario { Username = username };
            return u.ObtenerDatos() ? u : null;
        }

        [WebMethod]
        public static bool GuardarEdicionUsuario(ENUsuario usuario)
        {
            return usuario.UpdateUsuario();
        }

        [WebMethod]
        public static string EliminarUsuario(string username)
        {
            try
            {
                var cad = new Library.CAD.CADUsuario();
                string errorMessage;

                bool deleted = cad.DeleteUsuario(username, out errorMessage);

                if (!deleted)
                {
                    return "Error: " + errorMessage;
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return "Excepción: " + ex.Message;
            }
        }

        protected void btnTransaccion_Click(object sender, EventArgs e)
        {

            Response.Redirect("transaccion.aspx");
        }





    }
}
