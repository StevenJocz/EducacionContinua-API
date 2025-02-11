using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class QuizHistoricoDTOs
    {
        public required int QuizId { get; set; }
        public required int PersonaId { get; set; }
        public int Intentos { get; set; } = 0;
        public int Calificacion { get; set; } = 0;
        public required DateTime Fecha { get; set; }

        public static QuizHistoricoDTOs CreateDTO(QuizHistoricoE quizHistoricoE)
        {
            return new QuizHistoricoDTOs
            {
                QuizId = quizHistoricoE.quiz_id,
                PersonaId = quizHistoricoE.persona_id,
                Intentos = quizHistoricoE.intentos,
                Calificacion = quizHistoricoE.calificacion,
                Fecha = quizHistoricoE.fecha,
            };
        }

        public static QuizHistoricoE CreateE(QuizHistoricoDTOs quizHistoricoDTOs)
        {
            return new QuizHistoricoE
            {
                quiz_id = quizHistoricoDTOs.QuizId,
                persona_id = quizHistoricoDTOs.PersonaId,
                intentos = quizHistoricoDTOs.Intentos,
                calificacion = quizHistoricoDTOs.Calificacion,
                fecha = quizHistoricoDTOs.Fecha,
            };
        }
    }

}
