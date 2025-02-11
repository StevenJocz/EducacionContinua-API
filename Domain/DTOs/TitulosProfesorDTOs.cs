using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class TitulosProfesorDTOs
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }

        public static TitulosProfesorDTOs CreateDTO(TitulosProfesorE titulosProfesorE)
        {
            return new TitulosProfesorDTOs
            {
                Id = titulosProfesorE.id,
                Nombre = titulosProfesorE.nombre,
            };
        }

        public static TitulosProfesorE CreateE(TitulosProfesorDTOs titulosProfesorDTOs)
        {
            return new TitulosProfesorE
            {
                id = titulosProfesorDTOs.Id,
                nombre = titulosProfesorDTOs.Nombre,
            };
        }
    }
} 
