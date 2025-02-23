using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CursoCategoriaDTOs
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int CategoriaId { get; set; }

        public static CursoCategoriaDTOs CreateDTO(CursoCategoriaE entity)
        {
            return new CursoCategoriaDTOs
            {
                Id = entity.id,
                CursoId = entity.curso_id,
                CategoriaId = entity.categoria_id
            };
        }

        public static CursoCategoriaE CreateE(CursoCategoriaDTOs dto)
        {
            return new CursoCategoriaE
            {
                id = dto.Id,
                curso_id = dto.CursoId,
                categoria_id = dto.CategoriaId
            };
        }
    }
}
