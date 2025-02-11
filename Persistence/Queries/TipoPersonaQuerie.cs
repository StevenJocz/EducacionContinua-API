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
    public interface ITipoPersonaQuerie
    {
        Task<List<TipoPersonaDTOs>> GetAllTipoPersona();
        Task<TipoPersonaDTOs> GetTipoPersonaById(int id);
        Task<TipoPersonaDTOs> AddTipoPersona(TipoPersonaDTOs tipoPersonaDTOs);
        Task<bool> UpdateTipoPersona(int id, TipoPersonaDTOs tipoPersonaDTOs);
    }

    public class TipoPersonaQuerie : ITipoPersonaQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<TipoPersonaQuerie> _logger;
        private readonly IConfiguration _configuration;

        public TipoPersonaQuerie(ILogger<TipoPersonaQuerie> logger, IConfiguration configuration)
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

        // Get all TipoPersona records
        public async Task<List<TipoPersonaDTOs>> GetAllTipoPersona()
        {
            try
            {
                var tipoPersonas = await _context.TipoPersonaE
                    .Select(tp => TipoPersonaDTOs.CreateDTO(tp))
                    .ToListAsync();

                return tipoPersonas;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllTipoPersona)}: {ex.Message}");
                return new List<TipoPersonaDTOs>();
            }
        }

        // Get a TipoPersona by ID
        public async Task<TipoPersonaDTOs> GetTipoPersonaById(int id)
        {
            try
            {
                var tipoPersona = await _context.TipoPersonaE
                    .Where(tp => tp.id == id)
                    .Select(tp => TipoPersonaDTOs.CreateDTO(tp))
                    .FirstOrDefaultAsync();

                return tipoPersona;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetTipoPersonaById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new TipoPersona
        public async Task<TipoPersonaDTOs> AddTipoPersona(TipoPersonaDTOs tipoPersonaDTOs)
        {
            try
            {
                var tipoPersona = TipoPersonaDTOs.CreateE(tipoPersonaDTOs);

                _context.TipoPersonaE.Add(tipoPersona);
                await _context.SaveChangesAsync();

                return TipoPersonaDTOs.CreateDTO(tipoPersona);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddTipoPersona)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing TipoPersona
        public async Task<bool> UpdateTipoPersona(int id, TipoPersonaDTOs tipoPersonaDTOs)
        {
            try
            {
                var tipoPersona = await _context.TipoPersonaE.FindAsync(id);
                if (tipoPersona == null) return false;

                tipoPersona.tipo = tipoPersonaDTOs.Tipo;

                _context.TipoPersonaE.Update(tipoPersona);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateTipoPersona)}: {ex.Message}");
                return false;
            }
        }

    }
}
