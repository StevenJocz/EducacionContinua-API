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
    public interface IDependenciasQuerie
    {
        Task<List<DependenciasDTOs>> GetAllDependencias();
        Task<DependenciasDTOs> GetDependenciasById(int id);
        Task<DependenciasDTOs> AddDependencias(DependenciasDTOs dependenciasDTOs);
        Task<bool> UpdateDependencias(int id, DependenciasDTOs dependenciasDTOs);
    }

    public class DependenciasQuerie : IDependenciasQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<DependenciasQuerie> _logger;
        private readonly IConfiguration _configuration;

        public DependenciasQuerie(ILogger<DependenciasQuerie> logger, IConfiguration configuration)
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

        // Get all Dependencias records
        public async Task<List<DependenciasDTOs>> GetAllDependencias()
        {
            try
            {
                var dependencias = await _context.DependenciasE
                    .Select(d => DependenciasDTOs.CreateDTO(d))
                    .ToListAsync();

                return dependencias;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllDependencias)}: {ex.Message}");
                return new List<DependenciasDTOs>();
            }
        }

        // Get a Dependencias by ID
        public async Task<DependenciasDTOs> GetDependenciasById(int id)
        {
            try
            {
                var dependencia = await _context.DependenciasE
                    .Where(d => d.id == id)
                    .Select(d => DependenciasDTOs.CreateDTO(d))
                    .FirstOrDefaultAsync();

                return dependencia;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetDependenciasById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Dependencias
        public async Task<DependenciasDTOs> AddDependencias(DependenciasDTOs dependenciasDTOs)
        {
            try
            {
                var dependencia = DependenciasDTOs.CreateE(dependenciasDTOs);

                _context.DependenciasE.Add(dependencia);
                await _context.SaveChangesAsync();

                return DependenciasDTOs.CreateDTO(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddDependencias)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Dependencias
        public async Task<bool> UpdateDependencias(int id, DependenciasDTOs dependenciasDTOs)
        {
            try
            {
                var dependencia = await _context.DependenciasE.FindAsync(id);
                if (dependencia == null) return false;

                dependencia.nombre = dependenciasDTOs.Nombre;

                _context.DependenciasE.Update(dependencia);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateDependencias)}: {ex.Message}");
                return false;
            }
        }
    }
}
