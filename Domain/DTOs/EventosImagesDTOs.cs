using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class EventosImagesDTOs
    {
        public required int Id { get; set; }
        public required int EventoId { get; set; }
        public required string Imagen { get; set; }

        public static EventosImagesDTOs CreateDTO(EventosImagesE eventosImagesE)
        {
            return new EventosImagesDTOs
            {
                Id = eventosImagesE.id,
                EventoId = eventosImagesE.evento_id,
                Imagen = eventosImagesE.imagen,
            };
        }

        public static EventosImagesE CreateE(EventosImagesDTOs eventosImagesDTOs)
        {
            return new EventosImagesE
            {
                id = eventosImagesDTOs.Id,
                evento_id = eventosImagesDTOs.EventoId,
                imagen = eventosImagesDTOs.Imagen,
            };
        }
    }
} 
