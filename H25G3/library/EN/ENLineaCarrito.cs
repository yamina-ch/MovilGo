using library.CAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class ENLineaCarrito
    {
        private int linea_carrito_id;
        private int carrito_id;
        private int articulo_id;
        private float importe;
        private int cantidad;

        public int Linea_carrito_id
        {
            get
            {
                return linea_carrito_id;
            }
            set
            {
                linea_carrito_id = value;
            }
        }
        public int Carrito_id
        {
            get
            {
                return carrito_id;
            }
            set
            {
                carrito_id = value;
            }
        }
        public int Articulo_id
        {
            get
            {
                return articulo_id;
            }
            set
            {
                articulo_id = value;
            }
        }
        public float Importe
        {
            get
            {
                return importe;
            }
            set
            {
                importe = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return cantidad;
            }
            set
            {
                cantidad = value;
            }
        }

        public bool Crear()
        {
            CADLineaCarrito linea = new CADLineaCarrito();
            return linea.Crear(this);
        }

        public bool Eliminar()
        {
            CADLineaCarrito linea = new CADLineaCarrito();
            return linea.Eliminar(this);
        }

        public bool Leer()
        {
            CADLineaCarrito linea = new CADLineaCarrito();
            return linea.Leer(this);
        }

        public bool Leer_User_Articulo()
        {
            CADLineaCarrito linea = new CADLineaCarrito();
            return linea.Leer_User_Articulo(this);
        }

        public bool Actualizar()
        {
            CADLineaCarrito linea = new CADLineaCarrito();
            return linea.Actualizar(this);
        }
        public List<ENLineaCarrito> ObtenerPorCarrito()
        {
            CADLineaCarrito l = new CADLineaCarrito();
            return l.ObtenerPorCarrito(this.Carrito_id);
        }
    }
}
