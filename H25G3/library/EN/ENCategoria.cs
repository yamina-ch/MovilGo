using library.CAD;
using System.Collections.Generic;

using Library.CAD;

namespace Library.EN
{
    public class ENCategoria
    {

        public int CategoriaId { get; set; }
        public string Nombre { get; set; }

        public ENCategoria() { }

        public ENCategoria(int categoriaId, string nombre)
        {
            CategoriaId = categoriaId;
            Nombre = nombre;
        }

        public bool Crear()
        {
            CADCategoria cad = new CADCategoria();
            return cad.Crear(this);
        }

        public bool Editar()
        {
            CADCategoria cad = new CADCategoria();
            return cad.Editar(this);
        }

        public bool Eliminar()
        {
            CADCategoria cad = new CADCategoria();
            return cad.Eliminar(CategoriaId);
        }

        public ENCategoria Leer()
        {
            CADCategoria cad = new CADCategoria();
            return cad.Leer(CategoriaId);
        }

        public List<ENCategoria> ObtenerTodas()
        {
            CADCategoria cad = new CADCategoria();
            return cad.ObtenerTodas();
        }
    }
}
