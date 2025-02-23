using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CursoAprendizajeDTOs
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Descripcion { get; set; }

        public static CursoAprendizajeDTOs CreateDTO(CursoAprendizajeE entity)
        {
            return new CursoAprendizajeDTOs
            {
                Id = entity.id,
                CursoId = entity.curso_id,
                Descripcion = entity.descripcion
            };
        }

        public static CursoAprendizajeE CreateE(CursoAprendizajeDTOs dto)
        {
            return new CursoAprendizajeE
            {
                id = dto.Id,
                curso_id = dto.CursoId,
                descripcion = dto.Descripcion
            };
        }
    }
}
