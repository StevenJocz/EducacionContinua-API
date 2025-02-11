using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class CuponesDTOs
    {
        public required int Id { get; set; }
        public required int Descuento { get; set; }
        public required string Codigo { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }

        public static CuponesDTOs CreateDTO(CuponesE cuponesE)
        {
            return new CuponesDTOs
            {
                Id = cuponesE.id,
                Descuento = cuponesE.descuento,
                Codigo = cuponesE.codigo,
                FechaInicio = cuponesE.fecha_inicio,
                FechaFin = cuponesE.fecha_fin,
            };
        }

        public static CuponesE CreateE(CuponesDTOs cuponesDTOs)
        {
            return new CuponesE
            {
                id = cuponesDTOs.Id,
                descuento = cuponesDTOs.Descuento,
                codigo = cuponesDTOs.Codigo,
                fecha_inicio = cuponesDTOs.FechaInicio,
                fecha_fin = cuponesDTOs.FechaFin,
            };
        }
    }
} 
