using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class DependenciasDTOs
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }

        public static DependenciasDTOs CreateDTO(DependenciasE dependenciasE)
        {
            return new DependenciasDTOs
            {
                Id = dependenciasE.id,
                Nombre = dependenciasE.nombre,
            };
        }

        public static DependenciasE CreateE(DependenciasDTOs dependenciasDTOs)
        {
            return new DependenciasE
            {
                id = dependenciasDTOs.Id,
                nombre = dependenciasDTOs.Nombre,
            };
        }
    }
} 
