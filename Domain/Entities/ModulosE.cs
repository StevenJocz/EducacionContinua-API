using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("modulos")]
    public class ModulosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int curso_id { get; set; }

        public required string titulo { get; set; }
        public string? descripcion { get; set; }
        public required int orden { get; set; }
        public bool activo { get; set; } = true;
    }
} 
