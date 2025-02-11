using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class GrupoComentariosDTOs
    {
        public required int Id { get; set; }
        public required int GrupoId { get; set; }
        public required int PersonaId { get; set; }
        public int? RespuestaId { get; set; }
        public required DateTime Fecha { get; set; }
        public required string Comentario { get; set; }
        public int Likes { get; set; } = 0;
        public bool Aprobado { get; set; } = false;

        public static GrupoComentariosDTOs CreateDTO(GrupoComentariosE grupoComentariosE)
        {
            return new GrupoComentariosDTOs
            {
                Id = grupoComentariosE.id,
                GrupoId = grupoComentariosE.grupo_id,
                PersonaId = grupoComentariosE.persona_id,
                RespuestaId = grupoComentariosE.respuesta_id,
                Fecha = grupoComentariosE.fecha,
                Comentario = grupoComentariosE.comentario,
                Likes = grupoComentariosE.likes,
                Aprobado = grupoComentariosE.aprobado,
            };
        }

        public static GrupoComentariosE CreateE(GrupoComentariosDTOs grupoComentariosDTOs)
        {
            return new GrupoComentariosE
            {
                id = grupoComentariosDTOs.Id,
                grupo_id = grupoComentariosDTOs.GrupoId,
                persona_id = grupoComentariosDTOs.PersonaId,
                respuesta_id = grupoComentariosDTOs.RespuestaId,
                fecha = grupoComentariosDTOs.Fecha,
                comentario = grupoComentariosDTOs.Comentario,
                likes = grupoComentariosDTOs.Likes,
                aprobado = grupoComentariosDTOs.Aprobado,
            };
        }
    }

}
