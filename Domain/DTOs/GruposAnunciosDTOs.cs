using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class GruposAnunciosDTOs
    {
        public required int Id { get; set; }
        public required int GrupoId { get; set; }
        public required int PersonaId { get; set; }
        public required DateTime Fecha { get; set; }
        public required string Comentario { get; set; }
        public string? Enlace { get; set; }

        public static GruposAnunciosDTOs CreateDTO(GruposAnunciosE gruposAnunciosE)
        {
            return new GruposAnunciosDTOs
            {
                Id   = gruposAnunciosE.id,
                GrupoId = gruposAnunciosE.grupo_id,
                PersonaId = gruposAnunciosE.persona_id,
                Fecha = gruposAnunciosE.fecha,
                Comentario = gruposAnunciosE.comentario,
                Enlace = gruposAnunciosE.enlace,
            };
        }

        public static GruposAnunciosE CreateE(GruposAnunciosDTOs gruposAnunciosDTOs)
        {
            return new GruposAnunciosE
            {
                id = gruposAnunciosDTOs.Id,
                grupo_id = gruposAnunciosDTOs.GrupoId,
                persona_id = gruposAnunciosDTOs.PersonaId,
                fecha = gruposAnunciosDTOs.Fecha,
                comentario = gruposAnunciosDTOs.Comentario,
                enlace = gruposAnunciosDTOs.Enlace,
            };
        }
    }

}
