using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("cursos")]
    public class CursosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }
        public required int dependencia_id { get; set; }
        public required string nombre { get; set; }
        public string? descripcion { get; set; }
        public string? paraquien { get; set; }
        public string? imagen { get; set; }
        public required string codigo { get; set; }
    }
} 
