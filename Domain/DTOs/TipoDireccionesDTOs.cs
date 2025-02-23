using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class TipoDireccionesDTOs
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public static TipoDireccionesDTOs CreateDTO(TipoDireccionesE tipoDireccionesE)
        {
            return new TipoDireccionesDTOs
            {
                Id = tipoDireccionesE.id,
                Nombre = tipoDireccionesE.nombre
            };
        }

        public static TipoDireccionesE CreateE(TipoDireccionesDTOs tipoDireccionesDTOs)
        {
            return new TipoDireccionesE
            {
                id = tipoDireccionesDTOs.Id,
                nombre = tipoDireccionesDTOs.Nombre
            };
        }
    }
}
