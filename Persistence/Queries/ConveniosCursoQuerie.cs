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
    public interface IConveniosCursoQuerie
    {
        Task<List<ConveniosCursoDTOs>> GetAllConveniosCurso();
        Task<ConveniosCursoDTOs> GetConveniosCursoById(int id);
        Task<ConveniosCursoDTOs> AddConveniosCurso(ConveniosCursoDTOs conveniosCursoDTOs);
        Task<bool> UpdateConveniosCurso(int id, ConveniosCursoDTOs conveniosCursoDTOs);
    }

    public class ConveniosCursoQuerie : IConveniosCursoQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ConveniosCursoQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ConveniosCursoQuerie(ILogger<ConveniosCursoQuerie> logger, IConfiguration configuration)
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

        // Get all ConveniosCurso records
        public async Task<List<ConveniosCursoDTOs>> GetAllConveniosCurso()
        {
            try
            {
                var conveniosCurso = await _context.ConveniosCursoE
                    .Select(c => ConveniosCursoDTOs.CreateDTO(c))
                    .ToListAsync();

                return conveniosCurso;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllConveniosCurso)}: {ex.Message}");
                return new List<ConveniosCursoDTOs>();
            }
        }

        // Get a ConveniosCurso by ID
        public async Task<ConveniosCursoDTOs> GetConveniosCursoById(int id)
        {
            try
            {
                var conveniosCurso = await _context.ConveniosCursoE
                    .Where(c => c.id == id)
                    .Select(c => ConveniosCursoDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return conveniosCurso;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetConveniosCursoById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new ConveniosCurso
        public async Task<ConveniosCursoDTOs> AddConveniosCurso(ConveniosCursoDTOs conveniosCursoDTOs)
        {
            try
            {
                var conveniosCurso = ConveniosCursoDTOs.CreateE(conveniosCursoDTOs);

                _context.ConveniosCursoE.Add(conveniosCurso);
                await _context.SaveChangesAsync();

                return ConveniosCursoDTOs.CreateDTO(conveniosCurso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddConveniosCurso)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing ConveniosCurso
        public async Task<bool> UpdateConveniosCurso(int id, ConveniosCursoDTOs conveniosCursoDTOs)
        {
            try
            {
                var conveniosCurso = await _context.ConveniosCursoE.FindAsync(id);
                if (conveniosCurso == null) return false;

                conveniosCurso.convenio_id = conveniosCursoDTOs.ConvenioId;
                conveniosCurso.curso_id = conveniosCursoDTOs.CursoId;

                _context.ConveniosCursoE.Update(conveniosCurso);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateConveniosCurso)}: {ex.Message}");
                return false;
            }
        }
    }
}
