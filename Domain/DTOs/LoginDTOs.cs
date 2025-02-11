using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class LoginDTOs
    {
        public required int Id { get; set; }
        public required int PersonaId { get; set; }
        public required string Contrasena { get; set; }

        public static LoginDTOs CreateDTO(LoginE loginE)
        {
            return new LoginDTOs
            {
                Id = loginE.id,
                PersonaId = loginE.persona_id,
                Contrasena = loginE.contrasena,
            };
        }

        public static LoginE CreateE(LoginDTOs loginDTOs)
        {
            return new LoginE
            {
                id = loginDTOs.Id,
                persona_id = loginDTOs.PersonaId,
                contrasena = loginDTOs.Contrasena,
            };
        }
    }
} 
