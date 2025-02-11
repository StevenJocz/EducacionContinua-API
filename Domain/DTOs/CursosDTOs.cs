using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class CursosDTOs
    {
        public required int Id { get; set; }
        public required int CategoriaId { get; set; }
        public required int DependenciaId { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public required string Codigo { get; set; }

        public static CursosDTOs CreateDTO(CursosE cursosE)
        {
            return new CursosDTOs
            {
                Id = cursosE.id,
                CategoriaId = cursosE.categoria_id,
                DependenciaId = cursosE.dependencia_id,
                Nombre = cursosE.nombre,
                Descripcion = cursosE.descripcion,
                Imagen = cursosE.imagen,
                Codigo = cursosE.codigo,
            };
        }

        public static CursosE CreateE(CursosDTOs cursosDTOs)
        {
            return new CursosE
            {
                id = cursosDTOs.Id,
                categoria_id = cursosDTOs.CategoriaId,
                dependencia_id = cursosDTOs.DependenciaId,
                nombre = cursosDTOs.Nombre,
                descripcion = cursosDTOs.Descripcion,
                imagen = cursosDTOs.Imagen,
                codigo = cursosDTOs.Codigo,
            };
        }
    }
} 
