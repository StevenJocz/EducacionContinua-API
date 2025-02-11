using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("cupones")]
    public class CuponesE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int descuento { get; set; }
        public required string codigo { get; set; }
        public required DateTime fecha_inicio { get; set; }
        public required DateTime fecha_fin { get; set; }
    }
} 
