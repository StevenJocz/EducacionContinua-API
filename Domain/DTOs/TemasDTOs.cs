using System; 
using System.Collections.Generic; 
using Domain.Entities; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace Domain.DTOs {
    public class TemasDTOs
    {
        public required int Id { get; set; }
        public required int ModuloId { get; set; }
        public required string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Video { get; set; }
        public required int Orden { get; set; }
        public bool Activo { get; set; } = true;
        public bool RequiereEvidencia { get; set; } = false;
        public string? DescripcionEvidencia { get; set; }

        public static TemasDTOs CreateDTO(TemasE temasE)
        {
            return new TemasDTOs
            {
                Id = temasE.id,
                ModuloId = temasE.modulo_id,
                Titulo = temasE.titulo,
                Descripcion = temasE.descripcion,
                Video = temasE.video,
                Orden = temasE.orden,
                Activo = temasE.activo,
                RequiereEvidencia = temasE.req_evidencia,
                DescripcionEvidencia = temasE.des_evidencia,
            };
        }

        public static TemasE CreateE(TemasDTOs temasDTOs)
        {
            return new TemasE
            {
                id = temasDTOs.Id,
                modulo_id = temasDTOs.ModuloId,
                titulo = temasDTOs.Titulo,
                descripcion = temasDTOs.Descripcion,
                video = temasDTOs.Video,
                orden = temasDTOs.Orden,
                activo = temasDTOs.Activo,
                req_evidencia = temasDTOs.RequiereEvidencia,
                des_evidencia = temasDTOs.DescripcionEvidencia,
            };
        }
    }
} 
