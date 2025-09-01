using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetUserMenu();

                ENUsuario enusuario = (ENUsuario)Session["usuarios"];

                if (enusuario != null && enusuario.Admin)
                {
                    MenuSuperior.Visible = true; 
                }
            }
        }
        private void SetUserMenu()
        {
            MenuItem userMenuItem = MenuSuperior.FindItem("UsuarioMenu");
            if (userMenuItem != null)
            {
                userMenuItem.ChildItems.Clear();

                if (Session["usuarios"] != null)
                {
                    ENUsuario user = (ENUsuario)Session["usuarios"];
                    userMenuItem.Text = $"👤 {user.Username}";
                    userMenuItem.NavigateUrl = ""; 

                    userMenuItem.ChildItems.Add(new MenuItem("Perfil", "Perfil", "", "~/user.aspx"));
                    userMenuItem.ChildItems.Add(new MenuItem("Contacto", "Contacto", "", "~/contacto.aspx"));
                    userMenuItem.ChildItems.Add(new MenuItem("Cerrar sesión", "Logout", "", ""));
                }
                else
                {
                    userMenuItem.Text = "👤 Iniciar sesión";
                    userMenuItem.NavigateUrl = ""; // Evita error de navegación

                    userMenuItem.ChildItems.Add(new MenuItem("Login", "Login", "", "~/login.aspx"));
                    userMenuItem.ChildItems.Add(new MenuItem("Registrarse", "Registrarse", "", "~/register.aspx"));
                }
            }
        }


        protected void MenuSuperior_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Perfil":
                    Response.Redirect("~/user.aspx");
                    break;

                case "Contacto":
                    Response.Redirect("~/contacto.aspx");
                    break;

                case "Login":
                    Response.Redirect("~/login.aspx");
                    break;

                case "Registrarse":
                    Response.Redirect("~/register.aspx");
                    break;

                case "Logout":
                    Session["usuarios"] = null;
                    Response.Redirect("~/Default.aspx");
                    break;

                default:
                    break;
            }
        }
    }
}