using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.Entities {
    [Table("inscripciones_eventos_historico")]
    public class InscripcionesEventosHistoricoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }

        public required int inscripcion_id { get; set; }
        public required int persona_id { get; set; }
        public required int evento_id { get; set; }

        public required int estado { get; set; }
        public required DateTime fecha_registro { get; set; }
        public required DateTime fecha_edicion { get; set; }
    }
} 
