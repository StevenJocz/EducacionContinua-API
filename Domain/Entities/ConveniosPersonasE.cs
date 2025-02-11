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
        public required int convenio_id { get; set; }
        public required string documento { get; set; }
        public required int tipo_documento { get; set; }
        public required string nombre { get; set; }
    }
} 
