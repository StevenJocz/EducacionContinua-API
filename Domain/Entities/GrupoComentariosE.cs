using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("grupo_comentarios")]
    public class GrupoComentariosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int grupo_id { get; set; }
        public required int persona_id { get; set; }
        public int? respuesta_id { get; set; }
        public required DateTime fecha { get; set; }
        public required string comentario { get; set; }
        public int likes { get; set; } = 0;
        public bool aprobado { get; set; } = false;
    }
} 
