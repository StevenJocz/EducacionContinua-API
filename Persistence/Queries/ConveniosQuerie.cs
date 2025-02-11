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
    public interface IConveniosQuerie
    {
        Task<List<ConveniosDTOs>> GetAllConvenios();
        Task<ConveniosDTOs> GetConveniosById(int id);
        Task<ConveniosDTOs> AddConvenios(ConveniosDTOs conveniosDTOs);
        Task<bool> UpdateConvenios(int id, ConveniosDTOs conveniosDTOs);
    }

    public class ConveniosQuerie : IConveniosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ConveniosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ConveniosQuerie(ILogger<ConveniosQuerie> logger, IConfiguration configuration)
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

        // Get all Convenios records
        public async Task<List<ConveniosDTOs>> GetAllConvenios()
        {
            try
            {
                var convenios = await _context.ConveniosE
                    .Select(c => ConveniosDTOs.CreateDTO(c))
                    .ToListAsync();

                return convenios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllConvenios)}: {ex.Message}");
                return new List<ConveniosDTOs>();
            }
        }

        // Get a Convenios by ID
        public async Task<ConveniosDTOs> GetConveniosById(int id)
        {
            try
            {
                var convenios = await _context.ConveniosE
                    .Where(c => c.id == id)
                    .Select(c => ConveniosDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return convenios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetConveniosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Convenios
        public async Task<ConveniosDTOs> AddConvenios(ConveniosDTOs conveniosDTOs)
        {
            try
            {
                var convenios = ConveniosDTOs.CreateE(conveniosDTOs);

                _context.ConveniosE.Add(convenios);
                await _context.SaveChangesAsync();

                return ConveniosDTOs.CreateDTO(convenios);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddConvenios)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Convenios
        public async Task<bool> UpdateConvenios(int id, ConveniosDTOs conveniosDTOs)
        {
            try
            {
                var convenios = await _context.ConveniosE.FindAsync(id);
                if (convenios == null) return false;

                convenios.nombre = conveniosDTOs.Nombre;

                _context.ConveniosE.Update(convenios);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateConvenios)}: {ex.Message}");
                return false;
            }
        }
    }
}
