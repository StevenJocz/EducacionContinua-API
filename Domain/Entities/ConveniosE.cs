using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("convenios")]
    public class ConveniosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }
        public required int curso_id { get; set; }
        public required string nombre { get; set; }
        public required string nit { get; set; }
        public required string correo { get; set; }
        public required string telefono { get; set; }
        public required DateTime fecha_inicio { get; set; }
        public required DateTime fecha_fin { get; set; }
        public required string observacion { get; set; }
    }
} 
