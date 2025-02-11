using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.DTOs;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Queries
{
    public interface IConveniosPersonasQuerie
    {
        Task<List<ConveniosPersonasDTOs>> GetAllConveniosPersonas();
        Task<ConveniosPersonasDTOs> GetConveniosPersonasById(int id);
        Task<ConveniosPersonasDTOs> AddConveniosPersonas(ConveniosPersonasDTOs conveniosPersonasDTOs);
        Task<bool> UpdateConveniosPersonas(int id, ConveniosPersonasDTOs conveniosPersonasDTOs);
    }

    public class ConveniosPersonasQuerie : IConveniosPersonasQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ConveniosPersonasQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ConveniosPersonasQuerie(ILogger<ConveniosPersonasQuerie> logger, IConfiguration configuration)
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

        // Get all ConveniosPersonas records
        public async Task<List<ConveniosPersonasDTOs>> GetAllConveniosPersonas()
        {
            try
            {
                var conveniosPersonas = await _context.ConveniosPersonasE
                    .Select(c => ConveniosPersonasDTOs.CreateDTO(c))
                    .ToListAsync();

                return conveniosPersonas;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllConveniosPersonas)}: {ex.Message}");
                return new List<ConveniosPersonasDTOs>();
            }
        }

        // Get a ConveniosPersonas by ID
        public async Task<ConveniosPersonasDTOs> GetConveniosPersonasById(int id)
        {
            try
            {
                var conveniosPersonas = await _context.ConveniosPersonasE
                    .Where(c => c.convenio_id == id)
                    .Select(c => ConveniosPersonasDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return conveniosPersonas;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetConveniosPersonasById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new ConveniosPersonas
        public async Task<ConveniosPersonasDTOs> AddConveniosPersonas(ConveniosPersonasDTOs conveniosPersonasDTOs)
        {
            try
            {
                var conveniosPersonas = ConveniosPersonasDTOs.CreateE(conveniosPersonasDTOs);

                _context.ConveniosPersonasE.Add(conveniosPersonas);
                await _context.SaveChangesAsync();

                return ConveniosPersonasDTOs.CreateDTO(conveniosPersonas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddConveniosPersonas)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing ConveniosPersonas
        public async Task<bool> UpdateConveniosPersonas(int id, ConveniosPersonasDTOs conveniosPersonasDTOs)
        {
            try
            {
                var conveniosPersonas = await _context.ConveniosPersonasE.FindAsync(id);
                if (conveniosPersonas == null) return false;

                conveniosPersonas.convenio_id = conveniosPersonasDTOs.ConvenioId;
                conveniosPersonas.documento = conveniosPersonasDTOs.Documento;
                conveniosPersonas.tipo_documento = conveniosPersonasDTOs.TipoDocumento;
                conveniosPersonas.nombre = conveniosPersonasDTOs.Nombre;

                _context.ConveniosPersonasE.Update(conveniosPersonas);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateConveniosPersonas)}: {ex.Message}");
                return false;
            }
        }
    }
}
