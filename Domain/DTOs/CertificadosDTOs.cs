using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class CertificadosDTOs
    {
        public required int Id { get; set; }
        public required int EventoId { get; set; }
        public decimal? Precio { get; set; }
        public string? Imagen { get; set; }

        public static CertificadosDTOs CreateDTO(CertificadosE certificadosE)
        {
            return new CertificadosDTOs
            {
                Id = certificadosE.id,
                EventoId = certificadosE.evento_id,
                Precio = certificadosE.precio,
                Imagen = certificadosE.imagen,
            };
        }

        public static CertificadosE CreateE(CertificadosDTOs certificadosDTOs)
        {
            return new CertificadosE
            {
                id = certificadosDTOs.Id,
                evento_id = certificadosDTOs.EventoId,
                precio = certificadosDTOs.Precio,
                imagen = certificadosDTOs.Imagen,
            };
        }
    }
} 
