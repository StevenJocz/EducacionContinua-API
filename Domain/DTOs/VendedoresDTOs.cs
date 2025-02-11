using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class VendedoresDTOs
    {
        public required int Id { get; set; }
        public required int PersonaId { get; set; }
        public bool Aprobado { get; set; } = false;
        public int Porcentaje { get; set; } = 0;

        public static VendedoresDTOs CreateDTO(VendedoresE vendedoresE)
        {
            return new VendedoresDTOs
            {
                Id = vendedoresE.id,
                PersonaId = vendedoresE.persona_id,
                Aprobado = vendedoresE.aprobado,
                Porcentaje = vendedoresE.porcentaje,
            };
        }

        public static VendedoresE CreateE(VendedoresDTOs vendedoresDTOs)
        {
            return new VendedoresE
            {
                id = vendedoresDTOs.Id,
                persona_id = vendedoresDTOs.PersonaId,
                aprobado = vendedoresDTOs.Aprobado,
                porcentaje = vendedoresDTOs.Porcentaje,
            };
        }
    }

}
