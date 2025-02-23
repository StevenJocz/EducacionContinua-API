using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class TipoDocumentosDTOs
    {
        public required int Id { get; set; }
        public required string Prefijo { get; set; }
        public required string Nombre { get; set; }

        public static TipoDocumentosDTOs CreateDTO(TipoDocumentosE tipoDocumentosE)
        {
            return new TipoDocumentosDTOs
            {
                Id = tipoDocumentosE.id,
                Prefijo = tipoDocumentosE.prefijo,
                Nombre = tipoDocumentosE.nombre,
               
            };
        }

        public static TipoDocumentosE CreateE(TipoDocumentosDTOs tipoDocumentosDTOs)
        {
            return new TipoDocumentosE
            {
                id = tipoDocumentosDTOs.Id,
                prefijo = tipoDocumentosDTOs.Prefijo,
                nombre = tipoDocumentosDTOs.Nombre,
               
            };
        }
    }
}
