using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("curso_categoria")]
    public class CursoCategoriaE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }
        public required int curso_id { get; set; }
        public required int categoria_id { get; set; }
    }
}
