using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ActividadesDTOs
    {
        public required int Id { get; set; }
        public required int EventoId { get; set; }
        public required string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public decimal? Precio { get; set; }
        public string? Localizacion { get; set; }

        public static ActividadesDTOs CreateDTO(ActividadesE actividadesE)
        {
            return new ActividadesDTOs
            {
                Id = actividadesE.id,
                EventoId = actividadesE.evento_id,
                Titulo = actividadesE.titulo,
                Descripcion = actividadesE.descripcion,
                Imagen = actividadesE.imagen,
                Precio = actividadesE.precio,
                Localizacion = actividadesE.localizacion,
            };
        }

        public static ActividadesE CreateE(ActividadesDTOs actividadesDTOs)
        {
            return new ActividadesE
            {
                id = actividadesDTOs.Id,
                evento_id = actividadesDTOs.EventoId,
                titulo = actividadesDTOs.Titulo,
                descripcion = actividadesDTOs.Descripcion,
                imagen = actividadesDTOs.Imagen,
                precio = actividadesDTOs.Precio,
                localizacion = actividadesDTOs.Localizacion,
            };
        }
    }
} 
