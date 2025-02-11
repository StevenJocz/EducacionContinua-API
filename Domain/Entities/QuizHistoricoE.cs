using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("quiz_historico")]
    public class QuizHistoricoE
    {
        public required int quiz_id { get; set; }
        public required int persona_id { get; set; }
        public int intentos { get; set; } = 0;
        public int calificacion { get; set; } = 0;
        public DateTime fecha { get; set; } = DateTime.Now;
    }
} 
