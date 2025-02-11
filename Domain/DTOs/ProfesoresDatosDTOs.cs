using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ProfesoresDatosDTOs
    {
        public required int Id { get; set; }
        public required int PersonaId { get; set; }
        public required int TituloId { get; set; }
        public string? Foto { get; set; }
        public string? Descripcion { get; set; }

        public static ProfesoresDatosDTOs CreateDTO(ProfesoresDatosE profesoresDatosE)
        {
            return new ProfesoresDatosDTOs
            {
                Id = profesoresDatosE.id,
                PersonaId = profesoresDatosE.persona_id,
                TituloId = profesoresDatosE.titulo_id,
                Foto = profesoresDatosE.foto,
                Descripcion = profesoresDatosE.descripcion,
            };
        }

        public static ProfesoresDatosE CreateE(ProfesoresDatosDTOs profesoresDatosDTOs)
        {
            return new ProfesoresDatosE
            {
                id = profesoresDatosDTOs.Id,
                persona_id = profesoresDatosDTOs.PersonaId,
                titulo_id = profesoresDatosDTOs.TituloId,
                foto = profesoresDatosDTOs.Foto,
                descripcion = profesoresDatosDTOs.Descripcion,
            };
        }
    }
} 
