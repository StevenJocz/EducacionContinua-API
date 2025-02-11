using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class CategoriasDTOs
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }

        public static CategoriasDTOs CreateDTO(CategoriasE categoriasE)
        {
            return new CategoriasDTOs
            {
                Id = categoriasE.id,
                Nombre = categoriasE.nombre,
            };
        }

        public static CategoriasE CreateE(CategoriasDTOs categoriasDTOs)
        {
            return new CategoriasE
            {
                id = categoriasDTOs.Id,
                nombre = categoriasDTOs.Nombre,
            };
        }
    }
} 
