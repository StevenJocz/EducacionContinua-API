using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ConveniosPersonasDTOs
    {
        public required int Id { get; set; }
        public required int ConvenioId { get; set; }
        public required string Documento { get; set; }
        public required int TipoDocumento { get; set; }
        public required string Nombre { get; set; }

        public static ConveniosPersonasDTOs CreateDTO(ConveniosPersonasE conveniosPersonasE)
        {
            return new ConveniosPersonasDTOs
            {
                Id = conveniosPersonasE.id,
                ConvenioId = conveniosPersonasE.convenio_id,
                Documento = conveniosPersonasE.documento,
                TipoDocumento = conveniosPersonasE.tipo_documento,
                Nombre = conveniosPersonasE.nombre,
            };
        }

        public static ConveniosPersonasE CreateE(ConveniosPersonasDTOs conveniosPersonasDTOs)
        {
            return new ConveniosPersonasE
            {
                id = conveniosPersonasDTOs.Id,
                convenio_id = conveniosPersonasDTOs.ConvenioId,
                documento = conveniosPersonasDTOs.Documento,
                tipo_documento = conveniosPersonasDTOs.TipoDocumento,
                nombre = conveniosPersonasDTOs.Nombre,
            };
        }
    }

}
