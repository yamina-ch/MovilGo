using System;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using Library;
using Library.CAD;


namespace proWeb
{

    // Define una clase parcial llamada 'ForgotPassword' que hereda de 'System.Web.UI.Page'
    public partial class ForgotPassword : System.Web.UI.Page
    {
        // Método que se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            // Este método está vacío, lo que significa que no hay acciones específicas que se realicen al cargar la página
        }

        // Método que se ejecuta cuando se hace clic en el botón de enviar (btnSubmit)
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Limpiamos el mensaje de error anterior
            lblErrorMessage.Text = "";
            lblErrorMessage.Visible = false;

            // Obtiene el correo electrónico del usuario desde el campo de texto 'txtEmail'
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                // Verifica si se proporcionó un correo electrónico
                lblErrorMessage.Text = "Please enter an email address.";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Visible = true;
                return;
            }

            // Indica que la solicitud está en proceso
            lblErrorMessage.Text = "Submit button clicked. Processing...";
            lblErrorMessage.ForeColor = System.Drawing.Color.Black;
            lblErrorMessage.Visible = true;

            // Obtiene el usuario asociado al correo electrónico proporcionado
            ENUsuario user = GetUserByEmail(email);

            if (user != null)
            {
                SendEmail(user);

                //  el SweetAlert y redirigimos a login.aspx
                ScriptManager.RegisterStartupScript(this, this.GetType(), "correoEnviado",
                    "Swal.fire({ title: '¡Correo enviado!', text: 'Revisa tu correo electrónico para recuperar tu contraseña.', icon: 'success' }).then(() => { window.location = 'login.aspx'; });",
                    true);
            }
            else
            {
                // Muestra un mensaje de error si no se encuentra el usuario
                lblErrorMessage.Text = "Email not found.";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Visible = true;
            }
        }

        // Método para obtener un usuario por su correo electrónico desde la base de datos
        private ENUsuario GetUserByEmail(string email)
        {
            // Crea un objeto ENUsuario y establece su correo electrónico
            ENUsuario user = new ENUsuario { Email = email };
            // Crea una instancia de CADUsuario para interactuar con la base de datos
            CADUsuario cadUsuario = new CADUsuario();

            try
            {
                // Verifica si se encuentra el usuario por su correo electrónico
                if (cadUsuario.FindUsuarioByEmail(user))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Maneja errores al obtener el usuario de la base de datos
                lblErrorMessage.Text = "There was an error fetching user details. Error: " + ex.Message;
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Visible = true;
                return null;
            }
        }

        // Método para enviar un correo electrónico con los detalles de la cuenta del usuario
        private void SendEmail(ENUsuario user)
        {
            try
            {
                // Define la dirección de correo del remitente y la contraseña
                string fromAddress = "movilgotienda@gmail.com";
                string fromPassword = "mzez goak sjvw hyao";
                // Define la dirección de correo del destinatario
                string toAddress = user.Email;
                // Define el asunto del correo
                string subject = "Password Recovery";
                // Define el cuerpo del correo
                string body = $"Hola {user.Nombre},\n\nAquí están los detalles de tu cuenta:\n\nCorreo electrónico: {user.Email}\nContraseña: {user.Contrasenya}\n\nSaludos cordiales,\nEl equipo de MovilGo";

                // Crear un objeto de correo electrónico
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromAddress);  // Establece la dirección de correo del remitente
                mail.To.Add(toAddress);  // Añade la dirección de correo del destinatario
                mail.Subject = subject;  // Establece el asunto del correo
                mail.Body = body;  // Establece el cuerpo del mensaje del correo

                // Configurar el cliente SMTP para enviar el correo electrónico
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;  // Habilita SSL para asegurar la conexión
                smtpClient.UseDefaultCredentials = false;  // No usar las credenciales predeterminadas
                smtpClient.Credentials = new NetworkCredential(fromAddress, fromPassword);  // Establece las credenciales para autenticarse con el servidor SMTP

                smtpClient.Send(mail);  // Envía el correo electrónico

                // Mostrar un mensaje de éxito si el correo electrónico se envió correctamente
                lblErrorMessage.Text = "Email has been sent successfully.";
                lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                lblErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                // Maneja errores al intentar enviar el correo electrónico
                lblErrorMessage.Text = "There was an error sending the email. Please try again later. Error: " + ex.Message;
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Visible = true;
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}