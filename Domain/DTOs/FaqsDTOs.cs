using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class FaqsDTOs
    {
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }

        public static FaqsDTOs CreateDTO(FaqsE entity)
        {
            return new FaqsDTOs
            {
                Id = entity.id,
                Pregunta = entity.pregunta,
                Respuesta = entity.respuesta
            };
        }

        public static FaqsE CreateEntity(FaqsDTOs dto)
        {
            return new FaqsE
            {
                id = dto.Id,
                pregunta = dto.Pregunta,
                respuesta = dto.Respuesta
            };
        }
    }
}
