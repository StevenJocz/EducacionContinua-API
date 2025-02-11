using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class QuizDTOs
    {
        public required int Id { get; set; }
        public required int ModuloId { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }

        public static QuizDTOs CreateDTO(QuizE quizE)
        {
            return new QuizDTOs
            {
                Id = quizE.id,
                ModuloId = quizE.modulo_id,
                Nombre = quizE.nombre,
                Descripcion = quizE.descripcion,
            };
        }

        public static QuizE CreateE(QuizDTOs quizDTOs)
        {
            return new QuizE
            {
                id = quizDTOs.Id,
                modulo_id = quizDTOs.ModuloId,
                nombre = quizDTOs.Nombre,
                descripcion = quizDTOs.Descripcion,
            };
        }
    }
} 
