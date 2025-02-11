using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ConveniosDTOs
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }

        public static ConveniosDTOs CreateDTO(ConveniosE conveniosE)
        {
            return new ConveniosDTOs
            {
                Id = conveniosE.id,
                Nombre = conveniosE.nombre,
            };
        }

        public static ConveniosE CreateE(ConveniosDTOs conveniosDTOs)
        {
            return new ConveniosE
            {
                id = conveniosDTOs.Id,
                nombre = conveniosDTOs.Nombre,
            };
        }
    }
} 
