using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class GrupoEstudianteDTOs
    {
        public required int PersonaId { get; set; }
        public required int GrupoId { get; set; }

        public static GrupoEstudianteDTOs CreateDTO(GrupoEstudianteE grupoEstudianteE)
        {
            return new GrupoEstudianteDTOs
            {
                PersonaId = grupoEstudianteE.persona_id,
                GrupoId = grupoEstudianteE.grupo_id,
            };
        }

        public static GrupoEstudianteE CreateE(GrupoEstudianteDTOs grupoEstudianteDTOs)
        {
            return new GrupoEstudianteE
            {
                persona_id = grupoEstudianteDTOs.PersonaId,
                grupo_id = grupoEstudianteDTOs.GrupoId,
            };
        }
    }

}
