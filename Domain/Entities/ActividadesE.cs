using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("actividades")]
    public class ActividadesE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int evento_id { get; set; }

        public required string titulo { get; set; }
        public string? descripcion { get; set; }
        public string? imagen { get; set; }
        public decimal? precio { get; set; }
        public required string localizacion { get; set; }
    }
} 
