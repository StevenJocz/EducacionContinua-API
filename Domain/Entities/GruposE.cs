using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("grupos")]
    public class GruposE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int curso_id { get; set; }
        public required int cupon_id { get; set; }

        public required string nombre { get; set; }
        public required DateTime fecha_inicio { get; set; }
        public required DateTime fecha_fin { get; set; }
        public required decimal precio { get; set; }
    }
} 
