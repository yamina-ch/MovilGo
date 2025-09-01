using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Library.CAD;
using Library;
using Library.EN;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace proWeb
{
    public partial class contacto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTo.Text = "movilgotienda@gmail.com";
                txtTo.ReadOnly = true; // evita que el usuario lo modifique
            }
        }




        // Método que se ejecuta cuando se hace clic en el botón de enviar (btnSend)
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string fromEmail = txtFrom.Text.Trim();
            string toEmail = txtTo.Text.Trim();
            string subject = txtSubject.Text.Trim();
            string message = txtMessage.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "campos", "Swal.fire('Campos requeridos', 'Por favor completa todos los campos.', 'warning');", true);
                    return;
                }

                MailMessage mail = new MailMessage(fromEmail, toEmail, subject, message);

                if (fileAttachment.HasFile)
                {
                    string fileName = Path.GetFileName(fileAttachment.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();
                    string[] extensionesPermitidas = { ".pdf", ".jpg", ".jpeg", ".png", ".docx", ".txt" };

                    if (!extensionesPermitidas.Contains(extension))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "tipoInvalido",
                            "Swal.fire('Archivo no permitido', 'Solo se permiten PDF, JPG, PNG, DOCX y TXT.', 'warning');", true);
                        return;
                    }

                    if (fileAttachment.PostedFile.ContentLength > 4 * 1024 * 1024)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "tamañoInvalido",
                            "Swal.fire('Archivo muy grande', 'El archivo debe ser menor a 4 MB.', 'warning');", true);
                        return;
                    }

                    Attachment adjunto = new Attachment(fileAttachment.PostedFile.InputStream, fileName);
                    mail.Attachments.Add(adjunto);
                }

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("movilgotienda@gmail.com", "mzez goak sjvw hyao")
                };

                smtpClient.Send(mail);

                // Guardar en la base de datos
                string username = null;
                string nombre = null;
                string apellidos = null;

                if (Session["usuarios"] != null)
                {
                    var user = (ENUsuario)Session["usuarios"];
                    username = user.Username;
                    nombre = user.Nombre;
                    apellidos = user.Apellidos;
                }
                else
                {
                    // Buscar por email del campo
                    ENUsuario tempUser = new ENUsuario { Email = fromEmail };
                    CADUsuario cad = new CADUsuario();

                    if (cad.FindUsuarioByEmail(tempUser))
                    {
                        username = tempUser.Username;
                        nombre = tempUser.Nombre;
                        apellidos = tempUser.Apellidos;
                    }
                }

                ENContacto contacto = new ENContacto
                {
                    usuario_UName = username,
                    nombre = nombre,
                    apellidos = apellidos,
                    email = fromEmail,
                    asunto = subject,
                    mensaje = message,
                    fechaContacto = DateTime.Now
                };

                if (contacto.crearContacto())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "exito", "Swal.fire('Mensaje guardado y enviado.', '', 'success');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'No se pudo guardar el mensaje.', 'error');", true);
                }

                // Limpiar campos
                txtFrom.Text = "";
                txtSubject.Text = "";
                txtMessage.Text = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", $"Swal.fire('Error', 'Hubo un problema: {HttpUtility.JavaScriptStringEncode(ex.Message)}', 'error');", true);
            }
        }




    }
}