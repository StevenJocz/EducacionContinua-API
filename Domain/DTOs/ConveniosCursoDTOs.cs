using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ConveniosCursoDTOs
    {
        public required int Id { get; set; }
        public required int ConvenioId { get; set; }
        public required int CursoId { get; set; }

        public static ConveniosCursoDTOs CreateDTO(ConveniosCursoE conveniosCursoE)
        {
            return new ConveniosCursoDTOs
            {
                Id = conveniosCursoE.id,
                ConvenioId = conveniosCursoE.convenio_id,
                CursoId = conveniosCursoE.curso_id,
            };
        }

        public static ConveniosCursoE CreateE(ConveniosCursoDTOs conveniosCursoDTOs)
        {
            return new ConveniosCursoE
            {
                id = conveniosCursoDTOs.Id,
                convenio_id = conveniosCursoDTOs.ConvenioId,
                curso_id = conveniosCursoDTOs.CursoId,
            };
        }
    }
} 
