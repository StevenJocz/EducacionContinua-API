using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("temas")]
    public class TemasE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int modulo_id { get; set; }

        public required string titulo { get; set; }
        public string? descripcion { get; set; }
        public string? video { get; set; }
        public required int orden { get; set; }
        public bool activo { get; set; } = true;
        public bool req_evidencia { get; set; } = false;
        public string? des_evidencia { get; set; }
    }
} 
