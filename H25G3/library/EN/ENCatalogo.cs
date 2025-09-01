using System;
using System.Collections.Generic;
using library.CAD;
using Library.CAD;


namespace Library.EN
{
    public class ENCatalogo
    {
        public int CatalogoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Vendido { get; set; }
        public int CategoriaId { get; set; }
        public string UrlImagen { get; set; }

        public ENCatalogo() { }

        public ENCatalogo(int catalogoId, string nombre, string descripcion, decimal precio, int vendido, int categoriaId, string urlImagen)
        {
            CatalogoId = catalogoId;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Vendido = vendido;
            CategoriaId = categoriaId;
            UrlImagen = urlImagen;
        }




    }
}