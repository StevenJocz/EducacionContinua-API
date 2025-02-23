using Domain.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries
{
    public interface ITipoDocumentosQuerie
    {
        Task<List<TipoDocumentosDTOs>> GetAllTiposDocumentos();
        Task<TipoDocumentosDTOs> GetAddTipoDocumentoById(int id);
        Task<TipoDocumentosDTOs> AddTipoDocumento(TipoDocumentosDTOs tipoDocumentosDTOs);
        Task<bool> UpdateDocumentos(TipoDocumentosDTOs tipoDocumentosDTOs);
    } 

    public class TipoDocumentosQuerie: ITipoDocumentosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<TipoDocumentosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public TipoDocumentosQuerie(ILogger<TipoDocumentosQuerie> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new DBContext(connectionString);
        }

        #region implementacion Disponse
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion

        // Get all Actividades records
        public async Task<List<TipoDocumentosDTOs>> GetAllTiposDocumentos()
        {
            try
            {
                var documentos = await _context.TipoDocumentosE
                    .Select(a => TipoDocumentosDTOs.CreateDTO(a))
                    .ToListAsync();

                return documentos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllTiposDocumentos)}: {ex.Message}");
                return new List<TipoDocumentosDTOs>();
            }
        }

        // Get a Documento by ID
        public async Task<TipoDocumentosDTOs> GetAddTipoDocumentoById(int id)
        {
            try
            {
                var documento = await _context.TipoDocumentosE
                    .Where(a => a.id == id)
                    .Select(a => TipoDocumentosDTOs.CreateDTO(a))
                    .FirstOrDefaultAsync();

                return documento;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAddTipoDocumentoById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Tipo Documento
        public async Task<TipoDocumentosDTOs> AddTipoDocumento(TipoDocumentosDTOs tipoDocumentosDTOs)
        {
            try
            {
                var documento = TipoDocumentosDTOs.CreateE(tipoDocumentosDTOs);

                _context.TipoDocumentosE.Add(documento);
                await _context.SaveChangesAsync();

                return TipoDocumentosDTOs.CreateDTO(documento);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddTipoDocumento)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Tipos Documentos
        public async Task<bool> UpdateDocumentos(TipoDocumentosDTOs tipoDocumentosDTOs)
        {
            try
            {
                var documento = await _context.TipoDocumentosE.FindAsync(tipoDocumentosDTOs.Id);
                if (documento == null) return false;

                documento.prefijo = tipoDocumentosDTOs.Prefijo;
                documento.nombre = tipoDocumentosDTOs.Nombre;
                

                _context.TipoDocumentosE.Update(documento);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateDocumentos)}: {ex.Message}");
                return false;
            }
        }
    }
}
