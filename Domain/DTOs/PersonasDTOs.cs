using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class PersonasDTOs
    {
        public required int Id { get; set; }
        public required int TipoPersonaId { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required int TipoDoc { get; set; }
        public required string Correo { get; set; }
        public string? Genero { get; set; }
        public string? Celular { get; set; }
        public string? Pais { get; set; }
        public string? Departamento { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }

        public static PersonasDTOs CreateDTO(PersonasE personasE)
        {
            return new PersonasDTOs
            {
                Id = personasE.id,
                TipoPersonaId = personasE.tipo_persona_id,
                Nombres = personasE.nombres,
                Apellidos = personasE.apellidos,
                TipoDoc = personasE.tipo_doc,
                Correo = personasE.correo,
                Genero = personasE.genero,
                Celular = personasE.celular,
                Pais = personasE.pais,
                Departamento = personasE.departamento,
                Ciudad = personasE.ciudad,
                Direccion = personasE.direccion,
            };
        }

        public static PersonasE CreateE(PersonasDTOs personasDTOs)
        {
            return new PersonasE
            {
                id = personasDTOs.Id,
                tipo_persona_id = personasDTOs.TipoPersonaId,
                nombres = personasDTOs.Nombres,
                apellidos = personasDTOs.Apellidos,
                tipo_doc = personasDTOs.TipoDoc,
                correo = personasDTOs.Correo,
                genero = personasDTOs.Genero,
                celular = personasDTOs.Celular,
                pais = personasDTOs.Pais,
                departamento = personasDTOs.Departamento,
                ciudad = personasDTOs.Ciudad,
                direccion = personasDTOs.Direccion,
            };
        }
    }
} 
