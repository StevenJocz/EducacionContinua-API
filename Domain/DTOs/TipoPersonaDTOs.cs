using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class TipoPersonaDTOs
    {
        public required int Id { get; set; }
        public required string Tipo { get; set; }

        public static TipoPersonaDTOs CreateDTO(TipoPersonaE tipoPersonaE)
        {
            return new TipoPersonaDTOs
            {
                Id = tipoPersonaE.id,
                Tipo = tipoPersonaE.tipo,
            };
        }

        public static TipoPersonaE CreateE(TipoPersonaDTOs tipoPersonaDTOs)
        {
            return new TipoPersonaE
            {
                id = tipoPersonaDTOs.Id,
                tipo = tipoPersonaDTOs.Tipo,
            };
        }
    }
} 
