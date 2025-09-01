using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class ENArticulo
    {
        private int articulo_id;
        private int stock;
        private int marca_id;
        private int categoria_id;
        private int catalogo_id;
        private string color;
        private string modelo;
        private string sistema_operativo;
        private int anyo;
        private string estado;
        private string memoria;
        private string bateria;
        private decimal precio;
        private string descripcion;
        private decimal valoracion;
        private string url_imagen;
        private int vendido;
        private string vendedor_UName;
        //private int proveedor_id;
        private int? proveedor_id;
        public int Articulo_id
        {
            get { return articulo_id; }
            set { articulo_id = value; }
        }
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        public int Marca_id
        {
            get { return marca_id; }
            set { marca_id = value; }
        }

        public int Categoria_id
        {
            get { return categoria_id; }
            set { categoria_id = value; }
        }

        public int Catalogo_id
        {
            get { return catalogo_id; }
            set { catalogo_id = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        public string Sistema_operativo
        {
            get { return sistema_operativo; }
            set { sistema_operativo = value; }
        }

        public int Anyo
        {
            get { return anyo; }
            set { anyo = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string Memoria
        {
            get { return memoria; }
            set { memoria = value; }
        }

        public string Bateria
        {
            get { return bateria; }
            set { bateria = value; }
        }

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public decimal Valoracion
        {
            get { return valoracion; }
            set { valoracion = value; }
        }

        public string Url_imagen
        {
            get { return url_imagen; }
            set { url_imagen = value; }
        }

        public int Vendido
        {
            get { return vendido; }
            set { vendido = value; }
        }

        public string Vendedor_UName
        {
            get { return vendedor_UName; }
            set { vendedor_UName = value; }
        }

        // he cambiado la propiedad de proveedor_id a int? para que acepte null

        public int? Proveedor_id
        {
            get { return proveedor_id; }
            set { proveedor_id = value; }
        }


        public bool CrearArticulo()
        {
            CAD.CADArticulo c = new CAD.CADArticulo();
            return c.CrearArticulo(this);
        }

       

        public bool ActualizarArticulo()
        {
            CAD.CADArticulo c = new CAD.CADArticulo();
            return c.ActualizarArticulo(this);
        }

        public bool EliminarArticulo()
        {
            CAD.CADArticulo c = new CAD.CADArticulo();
            return c.EliminarArticulo(this);
        }
        public bool LeerArticulo()
        {
            CAD.CADArticulo c = new CAD.CADArticulo();
            return c.LeerArticulo(this);
        }


    }
}