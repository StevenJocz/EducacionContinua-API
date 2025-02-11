using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ProfesoresGruposDTOs
    {
        public required int Id { get; set; }
        public required int GrupoId { get; set; }
        public required int PersonaId { get; set; }
        public bool Activo { get; set; } = true;

        public static ProfesoresGruposDTOs CreateDTO(ProfesoresGruposE profesoresGruposE)
        {
            return new ProfesoresGruposDTOs
            {
                Id = profesoresGruposE.id,
                GrupoId = profesoresGruposE.grupo_id,
                PersonaId = profesoresGruposE.persona_id,
                Activo = profesoresGruposE.activo,
            };
        }

        public static ProfesoresGruposE CreateE(ProfesoresGruposDTOs profesoresGruposDTOs)
        {
            return new ProfesoresGruposE
            {
                id = profesoresGruposDTOs.Id,
                grupo_id = profesoresGruposDTOs.GrupoId,
                persona_id = profesoresGruposDTOs.PersonaId,
                activo = profesoresGruposDTOs.Activo,
            };
        }
    }
} 
