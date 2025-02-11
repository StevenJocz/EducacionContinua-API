using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("quiz_persona")]
    public class QuizPersonaE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int quiz_id { get; set; }
        public required int persona_id { get; set; }

        public required int intentos { get; set; } = 0;
        public required int calificacion { get; set; } = 0;
    }
} 
