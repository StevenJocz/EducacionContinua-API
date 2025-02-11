using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class RespuestasDTOs
    {
        public required int Id { get; set; }
        public required int PreguntaId { get; set; }
        public required string Texto { get; set; }
        public required bool Correcta { get; set; }

        public static RespuestasDTOs CreateDTO(RespuestasE respuestasE)
        {
            return new RespuestasDTOs
            {
                Id = respuestasE.id,
                PreguntaId = respuestasE.pregunta_id,
                Texto = respuestasE.texto,
                Correcta = respuestasE.correcta,
            };
        }

        public static RespuestasE CreateE(RespuestasDTOs respuestasDTOs)
        {
            return new RespuestasE
            {
                id = respuestasDTOs.Id,
                pregunta_id = respuestasDTOs.PreguntaId,
                texto = respuestasDTOs.Texto,
                correcta = respuestasDTOs.Correcta,
            };
        }
    }
} 
