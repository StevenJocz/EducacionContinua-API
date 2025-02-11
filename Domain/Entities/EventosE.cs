using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("eventos")]
    public class EventosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int categoria_id { get; set; }

        public required string titulo { get; set; }
        public string? descripcion { get; set; }
        public required int tipo { get; set; }
        public string? imagen { get; set; }
        public string? video { get; set; }
        public required DateTime fecha_inicio { get; set; }
        public required DateTime fecha_fin { get; set; }
        public decimal? precio { get; set; }
        public required int? capacidad { get; set; }
        public required string localizacion { get; set; }
    }
} 
