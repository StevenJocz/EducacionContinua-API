using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class ConveniosDTOs
    {
        public required int Id { get; set; }
        public required int IdCurso { get; set; }
        public required string Nombre { get; set; }
        public required string Nit { get; set; }
        public required string Correo { get; set; }
        public required string Celular { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public required string Observacion { get; set; }
        public List<ConveniosPersonasDTOs>? registros { get; set; }

        public static ConveniosDTOs CreateDTO(ConveniosE conveniosE)
        {
            return new ConveniosDTOs
            {
                Id = conveniosE.id,
                IdCurso = conveniosE.curso_id,
                Nombre = conveniosE.nombre,
                Nit = conveniosE.nit,
                Correo = conveniosE.correo,
                Celular = conveniosE.telefono,
                FechaInicio = conveniosE.fecha_inicio,
                FechaFin = conveniosE.fecha_fin,
                Observacion = conveniosE.observacion
            };
        }

        public static ConveniosE CreateE(ConveniosDTOs conveniosDTOs)
        {
            return new ConveniosE
            {
                id = conveniosDTOs.Id,
                curso_id = conveniosDTOs.IdCurso,
                nombre = conveniosDTOs.Nombre,
                nit = conveniosDTOs.Nit,
                correo = conveniosDTOs.Correo,
                telefono = conveniosDTOs.Celular,
                fecha_inicio = conveniosDTOs.FechaInicio,
                fecha_fin = conveniosDTOs.FechaFin,
                observacion = conveniosDTOs.Observacion
            };
        }
    }

    public class listConveniosDTOs
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Nit { get; set; }
        public required string FechaInicio { get; set; }
        public required string FechaFin { get; set; }
        public required string Curso { get; set; }
        public required bool Estado { get; set; }
    }

}
