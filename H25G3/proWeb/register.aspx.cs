using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.EN;
using Library;
using Library.CAD;

namespace proWeb
{
    public partial class register : System.Web.UI.Page
    {
        protected void registrar(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MostrarError("Las contraseñas no coinciden.");
                return;
            }

            Regex regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*\W).*$");
            if (txtPassword.Text.Length < 8 || !regex.IsMatch(txtPassword.Text))
            {
                MostrarError("La contraseña debe tener al menos 8 caracteres, incluir letras, números y símbolos.");
                return;
            }

            if (txtTelefono.Text.Length != 9 || !Regex.IsMatch(txtTelefono.Text, @"^\d{9}$"))
            {
                MostrarError("El teléfono debe tener exactamente 9 dígitos numéricos.");
                return;
            }

            Regex emailRegex = new Regex(@"^[\w-\.]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$");
            if (!emailRegex.IsMatch(txtEmail.Text))
            {
                MostrarError("Correo electrónico no válido.");
                return;
            }

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellidos.Text))
            {
                MostrarError("Nombre y Apellidos no pueden estar vacíos.");
                return;
            }

            string firstNameInitial = txtNombre.Text.Substring(0, 1).ToUpper();
            string lastNameInitial = txtApellidos.Text.Substring(0, 1).ToUpper();
            Regex userRegex = new Regex(@"^" + firstNameInitial + lastNameInitial + @"(\d{2})?$");
            if (!userRegex.IsMatch(txtUsername.Text.ToUpper()))
            {
                MostrarError("El nombre de usuario debe ser las iniciales del nombre/apellido, opcionalmente seguidas de dos dígitos. Ejemplo: PL o PL01");
                return;
            }

            if (!Regex.IsMatch(txtCodigoPostal.Text, @"^\d{5}$"))
            {
                MostrarError("El código postal debe tener exactamente 5 dígitos.");
                return;
            }

            ENUsuario nuevoUsuario = new ENUsuario
            {
                Username = txtUsername.Text,
                Nombre = txtNombre.Text,
                Apellidos = txtApellidos.Text,
                Telefono = txtTelefono.Text,
                Contrasenya = txtPassword.Text,
                Calle = txtCalle.Text,
                Localidad = txtLocalidad.Text,
                Provincia = txtProvincia.Text,
                Codigo_Postal = txtCodigoPostal.Text,
                Email = txtEmail.Text,
                Admin = false
            };

            if (new CADUsuario().FindUsuarioByEmail(nuevoUsuario))
            {
                MostrarError("El correo electrónico ya está registrado.");
                return;
            }

            if (new CADUsuario().FindUsuarioByUsername(nuevoUsuario))
            {
                MostrarError("El nombre de usuario ya está registrado.");
                return;
            }

            try
            {
                if (nuevoUsuario.createUsuario())
                {
                    HttpContext.Current.Session["usuarios"] = nuevoUsuario;

                        string script = @"
                        Swal.fire({
                          title: '¡Registro Exitoso!',
                          text: 'Serás redirigido al login en 2 segundos.',
                          icon: 'success',
                          timer: 2000,
                          showConfirmButton: false
                        }).then(() => {
                          window.location.href = 'login.aspx';
                        });
                        ";

                    ClientScript.RegisterStartupScript(this.GetType(), "successAlert", script, true);
                    ENCarrito c = new ENCarrito();
                    c.Usuario_UName = nuevoUsuario.Username;
                    c.Crear();

                }
                else
                {
                    MostrarError("No se pudo insertar en la base de datos. Revisa si faltan datos obligatorios o si ya existe.");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error: " + ex.Message);
            }

        }

        private void MostrarError(string mensaje)
        {
            lblErrorMessage.Text = mensaje;
            lblErrorMessage.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["usuarios"] != null)
            {
                Response.Redirect("/Default.aspx");
            }
        }
    }
}
