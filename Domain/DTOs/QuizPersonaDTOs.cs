using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class QuizPersonaDTOs
    {
        public required int Id { get; set; }
        public required int QuizId { get; set; }
        public required int PersonaId { get; set; }
        public required int Intentos { get; set; }
        public required int Calificacion { get; set; }

        public static QuizPersonaDTOs CreateDTO(QuizPersonaE quizPersonaE)
        {
            return new QuizPersonaDTOs
            {
                Id = quizPersonaE.id,
                QuizId = quizPersonaE.quiz_id,
                PersonaId = quizPersonaE.persona_id,
                Intentos = quizPersonaE.intentos,
                Calificacion = quizPersonaE.calificacion,
            };
        }

        public static QuizPersonaE CreateE(QuizPersonaDTOs quizPersonaDTOs)
        {
            return new QuizPersonaE
            {
                id = quizPersonaDTOs.Id,
                quiz_id = quizPersonaDTOs.QuizId,
                persona_id = quizPersonaDTOs.PersonaId,
                intentos = quizPersonaDTOs.Intentos,
                calificacion = quizPersonaDTOs.Calificacion,
            };
        }
    }
} 
