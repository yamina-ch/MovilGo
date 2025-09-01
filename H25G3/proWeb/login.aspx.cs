using System;
using System.Web;
using System.Web.UI;
using Library;

namespace proWeb
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["usuarios"] = null;

                if (Request.Cookies["UserCredentials"] != null)
                {
                    HttpCookie credentialsCookie = Request.Cookies["UserCredentials"];
                    string[] credentials = credentialsCookie.Value.Split('&');

                    foreach (string credential in credentials)
                    {
                        string[] parts = credential.Split('=');
                        if (parts[0] == txtEmail.Text.Trim())
                        {
                            txtPassword.Attributes["value"] = parts[1];
                            chkRememberMe.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        protected void Inicio(object sender, EventArgs e)
        {
            ENUsuario user = new ENUsuario();
            user.Email = txtEmail.Text.Trim();
            string inputPassword = txtPassword.Text;

            if (user.FindUsuarioByEmail())
            {
                if (user.ValidarContrasena(inputPassword))
                {
                    if (chkRememberMe.Checked)
                    {
                        HttpCookie credentialsCookie = Request.Cookies["UserCredentials"] ?? new HttpCookie("UserCredentials");
                        string newCredentials = txtEmail.Text.Trim() + "=" + txtPassword.Text;
                        credentialsCookie.Value += (string.IsNullOrEmpty(credentialsCookie.Value) ? "" : "&") + newCredentials;
                        credentialsCookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(credentialsCookie);
                    }
                    if (user.Admin)
                    {
                        Session["usuarios"] = user; 
                        Session["admin"] = true;    
                        Session["AdminName"] = user.Nombre;
                        Response.Redirect("user.aspx");
                    }

                    else
                    {
                        Session["usuarios"] = user;
                        Response.Redirect("user.aspx", false);
                    }

                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblErrorMessage.Text = "Contraseña incorrecta";
                    lblErrorMessage.Visible = true;
                }
            }
            else
            {
                lblErrorMessage.Text = "Usuario no encontrado";
                lblErrorMessage.Visible = true;
            }
        }

    }
}