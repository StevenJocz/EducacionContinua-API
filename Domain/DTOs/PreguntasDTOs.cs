using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class PreguntasDTOs
    {
        public required int Id { get; set; }
        public required int QuizId { get; set; }
        public required string Texto { get; set; }

        public static PreguntasDTOs CreateDTO(PreguntasE preguntasE)
        {
            return new PreguntasDTOs
            {
                Id = preguntasE.id,
                QuizId = preguntasE.quiz_id,
                Texto = preguntasE.texto,
            };
        }

        public static PreguntasE CreateE(PreguntasDTOs preguntasDTOs)
        {
            return new PreguntasE
            {
                id = preguntasDTOs.Id,
                quiz_id = preguntasDTOs.QuizId,
                texto = preguntasDTOs.Texto,
            };
        }
    }
} 
