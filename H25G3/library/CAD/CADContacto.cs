using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Library.EN;

namespace Library.CAD
{
    public class CADContacto
    {
        private string constring = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

        public CADContacto()
        {

        }

        public bool crearContacto(ENContacto contacto)
        {
            bool creado = false;
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    string query = @"INSERT INTO contactos (usuario_UName, nombre, apellidos, email, asunto, mensaje, fecha_contacto)
                             VALUES (@usuario, @nombre, @apellidos, @correo, @asunto, @mensaje, @fecha)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@usuario", (object)contacto.usuario_UName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@nombre", (object)contacto.nombre ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@apellidos", (object)contacto.apellidos ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@correo", contacto.email);
                    cmd.Parameters.AddWithValue("@asunto", contacto.asunto);
                    cmd.Parameters.AddWithValue("@mensaje", contacto.mensaje);
                    cmd.Parameters.AddWithValue("@fecha", contacto.fechaContacto);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    creado = rows > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en CADContacto.crearContacto: " + ex.Message);
            }
            return creado;
        }



    }
}
