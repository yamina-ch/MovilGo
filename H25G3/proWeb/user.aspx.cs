using System;
using System.Web.UI;
using Library;
using Library.CAD;

namespace proWeb
{
    public partial class user : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuarios"] is ENUsuario user)
                {
                    LoadUserData(user);
                    btnAdmin.Visible = user.Admin; // mostrar botón admin si lo es
                }
                else
                {
                    Response.Redirect("~/login.aspx"); // si no hay sesión, redirige
                }
            }
        }

        private void LoadUserData(ENUsuario user)
        {
            lblUsername.Text = user.Username;
            txtName.Text = user.Nombre;
            txtSurname.Text = user.Apellidos;
            txtMobileNumber.Text = user.Telefono;
            txtAddressLine1.Text = user.Calle;
            txtPostcode.Text = user.Codigo_Postal;
            txtState.Text = user.Localidad;
            txtEmailID.Text = user.Email;
            txtArea.Text = user.Provincia;
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session["usuarios"] = null;
            Response.Redirect("~/login.aspx");
        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            txtSurname.Enabled = true;
            txtMobileNumber.Enabled = true;
            txtAddressLine1.Enabled = true;
            txtPostcode.Enabled = true;
            txtState.Enabled = true;
            txtEmailID.Enabled = true;
            txtArea.Enabled = true;
            btnSaveProfile.Visible = true;
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (Session["usuarios"] is ENUsuario user)
            {
                user.Nombre = txtName.Text.Trim();
                user.Apellidos = txtSurname.Text.Trim();
                user.Telefono = txtMobileNumber.Text.Trim();
                user.Calle = txtAddressLine1.Text.Trim();
                user.Codigo_Postal = txtPostcode.Text.Trim();
                user.Localidad = txtState.Text.Trim();
                user.Email = txtEmailID.Text.Trim();
                user.Provincia = txtArea.Text.Trim();

                if (user.UpdateUsuario())
                {
                    Session["usuarios"] = user;
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "Swal.fire('¡Éxito!','Perfil actualizado correctamente.','success');", true);
                    btnSaveProfile.Visible = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "Swal.fire('Error','Error al actualizar el perfil.','error');", true);
                }
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "changePwdModal", "showChangePasswordModal();", true);
        }

        protected void btnSubmitPasswordChange_Click(object sender, EventArgs e)
        {
            string newPwd = hiddenNewPassword.Value;
            string confirmPwd = hiddenConfirmPassword.Value;

            if (Session["usuarios"] is ENUsuario user)
            {
                if (newPwd == confirmPwd)
                {
                    user.Contrasenya = newPwd;
                    if (user.ChangePassword())
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "pwdOk", "Swal.fire('¡Éxito!', 'Contraseña cambiada correctamente.', 'success');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "pwdFail", "Swal.fire('Error', 'Error al cambiar la contraseña.', 'error');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "pwdMismatch", "Swal.fire('Error', 'Las contraseñas no coinciden.', 'warning');", true);
                }
            }
            hiddenNewPassword.Value = "";
            hiddenConfirmPassword.Value = "";
        }

        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "confirmDelete", @"
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'No podrás deshacer esta acción.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, eliminar cuenta',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            __doPostBack('" + ConfirmDeleteBtn.UniqueID + @"', '');
        }
    });
", true);

        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            if (Session["usuarios"] is ENUsuario user)
            {
                string usernameReal = user.Username;
                System.Diagnostics.Debug.WriteLine("Intentando eliminar usuario: " + usernameReal);

                CADUsuario cad = new CADUsuario();
                string errorMessage;

                bool eliminado = cad.DeleteUsuario(usernameReal, out errorMessage);
                System.Diagnostics.Debug.WriteLine("Resultado de DeleteUsuario: " + eliminado);
                if (!eliminado)
                {
                    System.Diagnostics.Debug.WriteLine("Error al eliminar: " + errorMessage);
                }

                if (eliminado)
                {
                    Session["usuarios"] = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "deleted", @"
                Swal.fire('Cuenta eliminada', 'Tu cuenta se eliminó correctamente', 'success')
                .then(() => { window.location.href = 'login.aspx'; });
            ", true);
                }
                else
                {
                    string safeError = errorMessage.Replace("'", "\\'");
                    ClientScript.RegisterStartupScript(this.GetType(), "deleteFail",
                        $"Swal.fire('Error','{safeError}','error');", true);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Sesión de usuario es null en btnConfirmDelete_Click");
                ClientScript.RegisterStartupScript(this.GetType(), "noSession",
                    "Swal.fire('Error','No hay sesión activa','error');", true);
            }
        }


        protected void btncompra_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "comprar", "Swal.fire('Comprar', 'Funcionalidad aún no implementada', 'info');", true);
        }

        protected void btnSell_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "vender", "Swal.fire('Publicar anuncio', 'Funcionalidad aún no implementada', 'info');", true);
        }

        protected void btnpedido_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "pedidos", "Swal.fire('Mis pedidos', 'Funcionalidad aún no implementada', 'info');", true);
        }

        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin.aspx");
        }
    }
}
