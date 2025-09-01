using library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class ENCarrito
    {
        private int carrito_id;
        private string usuario_UName;

        public int Carrito_ID
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

        public string Usuario_UName
        {
            get
            {
                return usuario_UName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre de usuario no puede estar vacío.");
                }
                usuario_UName = value;
            }
        }


        public bool Crear()
        {
            CADCarrito cadCarrito = new CADCarrito();
            return cadCarrito.Crear(this);
        }

        public bool Eliminar()
        {
            CADCarrito cadCarrito = new CADCarrito();
            return cadCarrito.Eliminar(this);
        }

        public bool Leer()
        {
            CADCarrito cadCarrito = new CADCarrito();
            return cadCarrito.Leer(this);
        }

        public List<ENLineaCarrito> ObtenerArticulos()
        {
            CADCarrito cadCarrito = new CADCarrito();
            return cadCarrito.ObtenerArticulos(this);
        }
        public bool ObtenerPorUsuario()
        {
            CADCarrito cad = new CADCarrito();
            return cad.ObtenerPorUsuario(this);
        }
    }
}
