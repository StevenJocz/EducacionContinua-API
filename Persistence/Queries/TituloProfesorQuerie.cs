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
    public interface ITitulosProfesorQuerie
    {
        Task<List<TitulosProfesorDTOs>> GetAllTitulosProfesor();
        Task<TitulosProfesorDTOs> GetTitulosProfesorById(int id);
        Task<TitulosProfesorDTOs> AddTitulosProfesor(TitulosProfesorDTOs titulosProfesorDTOs);
        Task<bool> UpdateTitulosProfesor(int id, TitulosProfesorDTOs titulosProfesorDTOs);
    }

    public class TitulosProfesorQuerie : ITitulosProfesorQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<TitulosProfesorQuerie> _logger;
        private readonly IConfiguration _configuration;

        public TitulosProfesorQuerie(ILogger<TitulosProfesorQuerie> logger, IConfiguration configuration)
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

        // Get all TitulosProfesor records
        public async Task<List<TitulosProfesorDTOs>> GetAllTitulosProfesor()
        {
            try
            {
                var titulosProfesor = await _context.TitulosProfesorE
                    .Select(tp => TitulosProfesorDTOs.CreateDTO(tp))
                    .ToListAsync();

                return titulosProfesor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllTitulosProfesor)}: {ex.Message}");
                return new List<TitulosProfesorDTOs>();
            }
        }

        // Get a TitulosProfesor by ID
        public async Task<TitulosProfesorDTOs> GetTitulosProfesorById(int id)
        {
            try
            {
                var titulosProfesor = await _context.TitulosProfesorE
                    .Where(tp => tp.id == id)
                    .Select(tp => TitulosProfesorDTOs.CreateDTO(tp))
                    .FirstOrDefaultAsync();

                return titulosProfesor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetTitulosProfesorById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new TitulosProfesor
        public async Task<TitulosProfesorDTOs> AddTitulosProfesor(TitulosProfesorDTOs titulosProfesorDTOs)
        {
            try
            {
                var titulosProfesor = TitulosProfesorDTOs.CreateE(titulosProfesorDTOs);

                _context.TitulosProfesorE.Add(titulosProfesor);
                await _context.SaveChangesAsync();

                return TitulosProfesorDTOs.CreateDTO(titulosProfesor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddTitulosProfesor)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing TitulosProfesor
        public async Task<bool> UpdateTitulosProfesor(int id, TitulosProfesorDTOs titulosProfesorDTOs)
        {
            try
            {
                var titulosProfesor = await _context.TitulosProfesorE.FindAsync(id);
                if (titulosProfesor == null) return false;

                titulosProfesor.nombre = titulosProfesorDTOs.Nombre;

                _context.TitulosProfesorE.Update(titulosProfesor);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateTitulosProfesor)}: {ex.Message}");
                return false;
            }
        }
    }
}
