using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("convenios_personas")]
    public class ConveniosPersonasE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }
        public required int convenio_id { get; set; }
        public required string documento { get; set; }
        public required string tipo_documento { get; set; }
        public required string nombre { get; set; }
    }
} 
