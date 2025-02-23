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
        public required int DependenciaId { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ParaQuien { get; set; }
        public string? Imagen { get; set; }
        public required string Codigo { get; set; }

        public static CursosDTOs CreateDTO(CursosE cursosE)
        {
            return new CursosDTOs
            {
                Id = cursosE.id,
                DependenciaId = cursosE.dependencia_id,
                Nombre = cursosE.nombre,
                Descripcion = cursosE.descripcion,
                ParaQuien = cursosE.paraquien,
                Imagen = cursosE.imagen,
                Codigo = cursosE.codigo,
            };
        }

        public static CursosE CreateE(CursosDTOs cursosDTOs)
        {
            return new CursosE
            {
                id = cursosDTOs.Id,
                dependencia_id = cursosDTOs.DependenciaId,
                nombre = cursosDTOs.Nombre,
                descripcion = cursosDTOs.Descripcion,
                paraquien = cursosDTOs.ParaQuien,
                imagen = cursosDTOs.Imagen,
                codigo = cursosDTOs.Codigo,
            };
        }
    }

    public class AddCursoDTOs
    {
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public string Descripcion { get; set; }
        public List<CategoriasDTOs> Categoria { get; set; } 
        public int Dependencia { get; set; }
        public string Imagen { get; set; }
        public string Dirigido { get; set; } 
        public List<string> Aprendera { get; set; } 
    }

    public class CursoAdminDTOs
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; } 
        public string Estado { get; set; }
        public List<CategoriasDTOs> CursoCategorias { get; set; }
        public string Dependencia { get; set; }
        public bool Temario { get; set; }
        public bool Recursos { get; set; }
        public bool Grupos { get; set; }
        public bool Cupones { get; set; } 
    }

    public class CursoIdAdminDTOs
    { 
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public string Descripcion { get; set; } 
        public string? Imagen { get; set; }
        public string Dirigido { get; set; } 
        public int Dependencia { get; set; }
        public List<CategoriasDTOs> Categorias { get; set; } 
        public List<string> Aprendera { get; set; }
    }
} 
