using System;
using System.Data.SqlClient;
using Library;

namespace Library.EN
{
    public class ENMarcas
    {
        public int Marca_id { get; set; }
        public string Nombre { get; set; } // Mayúscula para seguir convención de propiedades

        public ENMarcas() { }

        public ENMarcas(int marca_id, string nombre)
        {
            Marca_id = marca_id;
            Nombre = nombre;
        }


    }
}
