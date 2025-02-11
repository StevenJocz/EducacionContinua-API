using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("grupos_anuncios")]
    public class GruposAnunciosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int grupo_id { get; set; }
        public required int persona_id { get; set; }
        public required DateTime fecha { get; set; }
        public string? comentario { get; set; }
        public string? enlace { get; set; }
    }
} 
