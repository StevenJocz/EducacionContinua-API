using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("profesores_datos")]
    public class ProfesoresDatosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int persona_id { get; set; }
        public required int titulo_id { get; set; }

        public string? foto { get; set; }
        public string? descripcion { get; set; }
    }
} 
