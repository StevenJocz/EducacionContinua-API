using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class InscripcionesCursosHistoricoDTOs
    {
        public required int Id { get; set; }
        public required int InscripcionId { get; set; }
        public required int PersonaId { get; set; }
        public required int CursoId { get; set; }
        public required int Estado { get; set; }
        public required DateTime FechaRegistro { get; set; }
        public required DateTime FechaEdicion { get; set; }

        public static InscripcionesCursosHistoricoDTOs CreateDTO(InscripcionesCursosHistoricoE inscripcionesCursosHistoricoE)
        {
            return new InscripcionesCursosHistoricoDTOs
            {
                Id = inscripcionesCursosHistoricoE.id,
                InscripcionId = inscripcionesCursosHistoricoE.inscripcion_id,
                PersonaId = inscripcionesCursosHistoricoE.persona_id,
                CursoId = inscripcionesCursosHistoricoE.curso_id,
                Estado = inscripcionesCursosHistoricoE.estado,
                FechaRegistro = inscripcionesCursosHistoricoE.fecha_registro,
                FechaEdicion = inscripcionesCursosHistoricoE.fecha_edicion,
            };
        }

        public static InscripcionesCursosHistoricoE CreateE(InscripcionesCursosHistoricoDTOs inscripcionesCursosHistoricoDTOs)
        {
            return new InscripcionesCursosHistoricoE
            {
                id = inscripcionesCursosHistoricoDTOs.Id,
                inscripcion_id = inscripcionesCursosHistoricoDTOs.InscripcionId,
                persona_id = inscripcionesCursosHistoricoDTOs.PersonaId,
                curso_id = inscripcionesCursosHistoricoDTOs.CursoId,
                estado = inscripcionesCursosHistoricoDTOs.Estado,
                fecha_registro = inscripcionesCursosHistoricoDTOs.FechaRegistro,
                fecha_edicion = inscripcionesCursosHistoricoDTOs.FechaEdicion,
            };
        }
    }

}
