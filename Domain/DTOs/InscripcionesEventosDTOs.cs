using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class InscripcionesEventosDTOs
    {
        public required int Id { get; set; }
        public required int PersonaId { get; set; }
        public required int EventoId { get; set; }
        public required int Estado { get; set; }
        public required DateTime FechaRegistro { get; set; }

        public static InscripcionesEventosDTOs CreateDTO(InscripcionesEventosE inscripcionesEventosE)
        {
            return new InscripcionesEventosDTOs
            {
                Id = inscripcionesEventosE.id,
                PersonaId = inscripcionesEventosE.persona_id,
                EventoId = inscripcionesEventosE.evento_id,
                Estado = inscripcionesEventosE.estado,
                FechaRegistro = inscripcionesEventosE.fecha_registro,
            };
        }

        public static InscripcionesEventosE CreateE(InscripcionesEventosDTOs inscripcionesEventosDTOs)
        {
            return new InscripcionesEventosE
            {
                id = inscripcionesEventosDTOs.Id,
                persona_id = inscripcionesEventosDTOs.PersonaId,
                evento_id = inscripcionesEventosDTOs.EventoId,
                estado = inscripcionesEventosDTOs.Estado,
                fecha_registro = inscripcionesEventosDTOs.FechaRegistro,
            };
        }
    }

}
