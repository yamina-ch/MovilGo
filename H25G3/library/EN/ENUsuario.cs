using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CAD;
using System.Configuration;

namespace Library 
{
    public class ENUsuario
    {
        public string username;
        private string nombre;
        private string apellidos;
        private string email;
        private string telefono;
        private string contrasenya;
        private string calle;
        private string localidad;
        private string provincia;
        private string codigo_postal;
        private bool admin;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }



        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Contrasenya
        {
            get { return contrasenya; }
            set { contrasenya = value; }
        }

        public string Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }

        public string Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }

        public string Codigo_Postal
        {
            get { return codigo_postal; }
            set { codigo_postal = value; }
        }

        public string Calle
        {
            get { return calle; }
            set { calle = value; }
        }


        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }
        public class UsuarioDTO
        {
            public string Username { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string Calle { get; set; }
            public string Localidad { get; set; }
            public string Provincia { get; set; }
            public string Codigo_Postal { get; set; }
            public bool Admin { get; set; }
        }


        public ENUsuario()
        {
            Username = "";
            Nombre = "";
            Apellidos = "";
            Telefono = "";
            Contrasenya = "";
            Calle = "";
            Localidad = "";
            Provincia = "";
            Codigo_Postal = "";
            Email = "";
            Admin = false;
        }

        public ENUsuario(string username, string nombre, string apellidos, string telefono,
                         string contrasenya, string calle, string localidad, string provincia,
                         string codigoPostal, string email, bool admin)
        {
            Username = username;
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Contrasenya = contrasenya;
            Calle = calle;
            Localidad = localidad;
            Provincia = provincia;
            Codigo_Postal = codigoPostal;
            Email = email;
            Admin = admin;
        }

        public bool createUsuario()
        {
            CADUsuario user = new CADUsuario();
            bool creado = false;

            creado = user.CreateUsuario(this);

            return creado;
        }

        public bool UpdateUsuario()
        {
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["Database"].ConnectionString; // Obtener la cadena de conexión del web.config

                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string query = "UPDATE usuario SET " +
                                   "nombre = @Nombre, apellidos = @Apellidos, telefono = @Telefono, " +
                                   "calle = @Calle, localidad = @Localidad, provincia = @Provincia, " +
                                   "codigo_postal = @Codigo_Postal, email = @Email, admin = @Admin " +
                                   "WHERE username = @Username";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Nombre", this.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", this.Apellidos);
                    cmd.Parameters.AddWithValue("@Telefono", this.Telefono);
                    cmd.Parameters.AddWithValue("@Calle", this.Calle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Localidad", this.Localidad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Provincia", this.Provincia ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Codigo_Postal", this.Codigo_Postal ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", this.Email);
                    cmd.Parameters.AddWithValue("@Admin", this.Admin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Username", this.Username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UpdateUsuario: " + ex.Message);
            }
        }

        public bool ChangePassword()
{
    CADUsuario usuario = new CADUsuario();

    if (usuario.FindUsuario(this)) 
    {
        return usuario.ChangePassword(this);
    }

    return false;
}

        public bool DeleteUsuario(out string errorMessage)
        {
            CADUsuario usuario = new CADUsuario();
            return usuario.DeleteUsuario(username, out errorMessage); 
        }


        public bool FindUsuario()
        {
            CADUsuario cad = new CADUsuario();
            return cad.FindUsuario(this);
        }

        public bool ValidarContrasena(string inputPassword)
        {
            return inputPassword == this.Contrasenya;
        }


        public bool FindUsuarioByEmail()
        {
            CADUsuario cadUsuario = new CADUsuario();
            return cadUsuario.FindUsuarioByEmail(this); 
        }
        public bool ObtenerDatos()
        {
            CADUsuario cad = new CADUsuario();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM usuario WHERE username = @Username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", this.Username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.Nombre = reader["nombre"].ToString();
                            this.Apellidos = reader["apellidos"].ToString();
                            this.Email = reader["email"].ToString();
                            this.Telefono = reader["telefono"].ToString();
                            this.Calle = reader["calle"].ToString();
                            this.Localidad = reader["localidad"].ToString();
                            this.Provincia = reader["provincia"].ToString();
                            this.Codigo_Postal = reader["codigo_postal"].ToString();
                            this.Admin = Convert.ToBoolean(reader["admin"]);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos: " + ex.Message); // Añadir mensaje de error
            }

            return false;
        }

        public bool ActualizarAdmin()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
                {
                    con.Open();
                    string query = "UPDATE usuario SET admin = @Admin WHERE username = @Username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Admin", this.Admin ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Username", this.Username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch
            {
                return false;
            }
        }













    }
}
