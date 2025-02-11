using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class InscripcionesEventosHistoricoDTOs
    {
        public required int Id { get; set; }
        public required int InscripcionId { get; set; }
        public required int PersonaId { get; set; }
        public required int EventoId { get; set; }
        public required int Estado { get; set; }
        public required DateTime FechaRegistro { get; set; }
        public required DateTime FechaEdicion { get; set; }

        public static InscripcionesEventosHistoricoDTOs CreateDTO(InscripcionesEventosHistoricoE inscripcionesEventosHistoricoE)
        {
            return new InscripcionesEventosHistoricoDTOs
            {
                Id = inscripcionesEventosHistoricoE.id,
                InscripcionId = inscripcionesEventosHistoricoE.inscripcion_id,
                PersonaId = inscripcionesEventosHistoricoE.persona_id,
                EventoId = inscripcionesEventosHistoricoE.evento_id,
                Estado = inscripcionesEventosHistoricoE.estado,
                FechaRegistro = inscripcionesEventosHistoricoE.fecha_registro,
                FechaEdicion = inscripcionesEventosHistoricoE.fecha_edicion,
            };
        }

        public static InscripcionesEventosHistoricoE CreateE(InscripcionesEventosHistoricoDTOs inscripcionesEventosHistoricoDTOs)
        {
            return new InscripcionesEventosHistoricoE
            {
                id = inscripcionesEventosHistoricoDTOs.Id,
                inscripcion_id = inscripcionesEventosHistoricoDTOs.InscripcionId,
                persona_id = inscripcionesEventosHistoricoDTOs.PersonaId,
                evento_id = inscripcionesEventosHistoricoDTOs.EventoId,
                estado = inscripcionesEventosHistoricoDTOs.Estado,
                fecha_registro = inscripcionesEventosHistoricoDTOs.FechaRegistro,
                fecha_edicion = inscripcionesEventosHistoricoDTOs.FechaEdicion,
            };
        }
    }

}
