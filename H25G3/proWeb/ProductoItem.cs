using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proWeb
{
    [Serializable]
    public class ProductoItem
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Color { get; set; }
        public string Memoria { get; set; }
        public string ImagenUrl { get; set; }
        public int Cantidad { get; set; } = 1;
        public int Id { get; set; }
        public decimal Importe { get; set; }

        public int Linea_carrito_id {get; set;}

        public int Lista_favorito_id { get; set; }
    }
}