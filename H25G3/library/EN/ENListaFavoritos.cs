using library.CAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class ENListaFavoritos
    {
        private int lista_favorito_id;
        private string usuario_UName;
        private int articulo_id;

        public int Lista_favorito_id
        {
            get
            {
                return lista_favorito_id;
            }
            set
            {
                lista_favorito_id = value;
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
                usuario_UName = value;
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

        public bool Crear()
        {
            CADListaFavoritos linea = new CADListaFavoritos();
            return linea.Crear(this);
        }

        public bool Eliminar()
        {
            CADListaFavoritos linea = new CADListaFavoritos();
            return linea.Eliminar(this);
        }

        public bool Leer()
        {
            CADListaFavoritos linea = new CADListaFavoritos();
            return linea.Leer(this);
        }

        public bool Leer_User_Articulo()
        {
            CADListaFavoritos linea = new CADListaFavoritos();
            return linea.Leer_User_Articulo(this);
        }

        public bool Actualizar()
        {
            CADListaFavoritos linea = new CADListaFavoritos();
            return linea.Actualizar(this);
        }

        public List<ENListaFavoritos> ObtenerPorUsuario()
        {
            CADListaFavoritos l = new CADListaFavoritos();
            return l.ObtenerPorUsuario(this.Usuario_UName);
        }
    }
}
