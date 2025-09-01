using library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.CAD
{
    public class CADComentario
    {
        private string connectionString;

        public CADComentario()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }


        public List<ENComentario> GetComentarios(int? articuloId = null, int? comentarioId = null)
        {
            List<ENComentario> comentarios = new List<ENComentario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql;

                if (comentarioId.HasValue)
                {
                    sql = "SELECT comentario_id, articulo_id, usuario_UName, comentario, fecha_comentario FROM comentario WHERE comentario_id = @ComentarioId";
                }
                else if (articuloId.HasValue)
                {
                    sql = "SELECT comentario_id, articulo_id, usuario_UName, comentario, fecha_comentario FROM comentario WHERE articulo_id = @ArticuloId";
                }
                else
                {
                    throw new ArgumentException("Either articuloId or comentarioId must be provided.");
                }

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (comentarioId.HasValue)
                    {
                        command.Parameters.AddWithValue("@ComentarioId", comentarioId.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ArticuloId", articuloId.Value);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ENComentario comentario = new ENComentario(
                                Convert.ToInt32(reader["comentario_id"]),
                                Convert.ToInt32(reader["articulo_id"]),
                                reader["usuario_UName"].ToString(),
                                reader["comentario"].ToString(),
                                Convert.ToDateTime(reader["fecha_comentario"])
                            );
                            comentarios.Add(comentario);
                        }
                    }
                }
            }

            return comentarios;
        }

        public void AddComentario(ENComentario comentario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Obtener el valor maxde comentario_id
                string getMaxIdSql = "SELECT ISNULL(MAX(comentario_id), 0) + 1 FROM comentario";
                SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdSql, connection);
                int newComentarioId = (int)getMaxIdCommand.ExecuteScalar();

                comentario.ComentarioId = newComentarioId;

                string sql = "INSERT INTO comentario (comentario_id, articulo_id, usuario_UName, comentario, fecha_comentario) VALUES (@ComentarioId, @ArticuloId, @UsuarioUName, @Comentario, @FechaComentario)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ComentarioId", comentario.ComentarioId);
                    command.Parameters.AddWithValue("@ArticuloId", comentario.ArticuloId);
                    command.Parameters.AddWithValue("@UsuarioUName", comentario.UsuarioUName);
                    command.Parameters.AddWithValue("@Comentario", comentario.Comentario);
                    command.Parameters.AddWithValue("@FechaComentario", comentario.FechaComentario);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateComentario(ENComentario comentario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE comentario SET comentario = @Comentario WHERE comentario_id = @ComentarioId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Comentario", comentario.Comentario);
                    command.Parameters.AddWithValue("@ComentarioId", comentario.ComentarioId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteComentario(int comentarioId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM comentario WHERE comentario_id = @ComentarioId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ComentarioId", comentarioId);
                    command.ExecuteNonQuery();
                }
            }

        }


    }
}
