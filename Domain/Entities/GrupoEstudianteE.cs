using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("grupo_estudiante")]
    public class GrupoEstudianteE
    {
        public required int persona_id { get; set; }
        public required int grupo_id { get; set; }
    }
} 
