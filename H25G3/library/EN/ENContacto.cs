using System;
using System.Collections.Generic;
using Library.CAD;

namespace Library.EN
{
    public class ENContacto
    {
        public int id { get; set; }
        public string usuario_UName { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string asunto { get; set; }
        public string mensaje { get; set; }
        public DateTime fechaContacto { get; set; } = DateTime.Now;

        public bool crearContacto()
        {
            return new CADContacto().crearContacto(this);
        }
    }
}