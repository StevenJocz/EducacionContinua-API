using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class EventosDTOs
    {
        public required int Id { get; set; }
        public required int CategoriaId { get; set; }
        public required string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public required int Tipo { get; set; }
        public string? Imagen { get; set; }
        public string? Video { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public decimal? Precio { get; set; }
        public int? Capacidad { get; set; }
        public string? Localizacion { get; set; }

        public static EventosDTOs CreateDTO(EventosE eventosE)
        {
            return new EventosDTOs
            {
                Id = eventosE.id,
                CategoriaId = eventosE.categoria_id,
                Titulo = eventosE.titulo,
                Descripcion = eventosE.descripcion,
                Tipo = eventosE.tipo,
                Imagen = eventosE.imagen,
                Video = eventosE.video,
                FechaInicio = eventosE.fecha_inicio,
                FechaFin = eventosE.fecha_fin,
                Precio = eventosE.precio,
                Capacidad = eventosE.capacidad,
                Localizacion = eventosE.localizacion,
            };
        }

        public static EventosE CreateE(EventosDTOs eventosDTOs)
        {
            return new EventosE
            {
                id = eventosDTOs.Id,
                categoria_id = eventosDTOs.CategoriaId,
                titulo = eventosDTOs.Titulo,
                descripcion = eventosDTOs.Descripcion,
                tipo = eventosDTOs.Tipo,
                imagen = eventosDTOs.Imagen,
                video = eventosDTOs.Video,
                fecha_inicio = eventosDTOs.FechaInicio,
                fecha_fin = eventosDTOs.FechaFin,
                precio = eventosDTOs.Precio,
                capacidad = eventosDTOs.Capacidad,
                localizacion = eventosDTOs.Localizacion,
            };
        }
    }
} 
