using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class GruposDTOs
    {
        public required int Id { get; set; }
        public required int CursoId { get; set; }
        public required int CuponId { get; set; }
        public required string Nombre { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public required decimal Precio { get; set; }

        public static GruposDTOs CreateDTO(GruposE gruposE)
        {
            return new GruposDTOs
            {
                Id = gruposE.id,
                CursoId = gruposE.curso_id,
                CuponId = gruposE.cupon_id,
                Nombre = gruposE.nombre,
                FechaInicio = gruposE.fecha_inicio,
                FechaFin = gruposE.fecha_fin,
                Precio = gruposE.precio,
            };
        }

        public static GruposE CreateE(GruposDTOs gruposDTOs)
        {
            return new GruposE
            {
                id = gruposDTOs.Id,
                curso_id = gruposDTOs.CursoId,
                cupon_id = gruposDTOs.CuponId,
                nombre = gruposDTOs.Nombre,
                fecha_inicio = gruposDTOs.FechaInicio,
                fecha_fin = gruposDTOs.FechaFin,
                precio = gruposDTOs.Precio,
            };
        }
    }
} 
