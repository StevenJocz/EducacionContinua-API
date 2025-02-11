using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ModulosDTOs
    {
        public required int Id { get; set; }
        public required int CursoId { get; set; }
        public required string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public required int Orden { get; set; }
        public bool Activo { get; set; } = true;

        public static ModulosDTOs CreateDTO(ModulosE modulosE)
        {
            return new ModulosDTOs
            {
                Id = modulosE.id,
                CursoId = modulosE.curso_id,
                Titulo = modulosE.titulo,
                Descripcion = modulosE.descripcion,
                Orden = modulosE.orden,
                Activo = modulosE.activo,
            };
        }

        public static ModulosE CreateE(ModulosDTOs modulosDTOs)
        {
            return new ModulosE
            {
                id = modulosDTOs.Id,
                curso_id = modulosDTOs.CursoId,
                titulo = modulosDTOs.Titulo,
                descripcion = modulosDTOs.Descripcion,
                orden = modulosDTOs.Orden,
                activo = modulosDTOs.Activo,
            };
        }
    }
} 
