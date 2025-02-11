using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("personas")]
    public class PersonasE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int tipo_persona_id { get; set; }

        public required string nombres { get; set; }
        public required string apellidos { get; set; }
        public required int tipo_doc { get; set; }
        public required string correo { get; set; }
        public string? genero { get; set; }
        public string? celular { get; set; }
        public string? pais { get; set; }
        public string? departamento { get; set; }
        public string? ciudad { get; set; }
        public string? direccion { get; set; }
    }
} 
