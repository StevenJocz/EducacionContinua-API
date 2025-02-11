using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class InscripcionesCursosDTOs
    {
        public required int Id { get; set; }
        public required int PersonaId { get; set; }
        public required int CursoId { get; set; }
        public required int Estado { get; set; }
        public required DateTime FechaRegistro { get; set; }

        public static InscripcionesCursosDTOs CreateDTO(InscripcionesCursosE inscripcionesCursosE)
        {
            return new InscripcionesCursosDTOs
            {
                Id = inscripcionesCursosE.id,
                PersonaId = inscripcionesCursosE.persona_id,
                CursoId = inscripcionesCursosE.curso_id,
                Estado = inscripcionesCursosE.estado,
                FechaRegistro = inscripcionesCursosE.fecha_registro,
            };
        }

        public static InscripcionesCursosE CreateE(InscripcionesCursosDTOs inscripcionesCursosDTOs)
        {
            return new InscripcionesCursosE
            {
                id = inscripcionesCursosDTOs.Id,
                persona_id = inscripcionesCursosDTOs.PersonaId,
                curso_id = inscripcionesCursosDTOs.CursoId,
                estado = inscripcionesCursosDTOs.Estado,
                fecha_registro = inscripcionesCursosDTOs.FechaRegistro,
            };
        }
    }
} 
