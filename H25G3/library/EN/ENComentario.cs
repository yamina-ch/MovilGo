using library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class ENComentario
    {
        private int comentario_id;
        private int articulo_id;
        private string usuario_UName;
        private string comentario;
        private DateTime fecha_comentario;

        public int ComentarioId
        {
            get { return comentario_id; }
            set { comentario_id = value; }
        }

        public int ArticuloId
        {
            get { return articulo_id; }
            set { articulo_id = value; }
        }
        public string UsuarioUName
        {
            get { return usuario_UName; }
            set { usuario_UName = value; }
        }

        public string Comentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public DateTime FechaComentario
        {
            get { return fecha_comentario; }
            set { fecha_comentario = value; }
        }


        public ENComentario(int comentarioId, int articuloId, string usuarioUName, string comentario, DateTime fechaComentario)
        {
            ComentarioId = comentarioId;
            ArticuloId = articuloId;
            UsuarioUName = usuarioUName;
            Comentario = comentario;
            FechaComentario = fechaComentario;
        }

    }
}
