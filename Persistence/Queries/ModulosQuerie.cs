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
    public interface IModulosQuerie
    {
        Task<List<ModulosDTOs>> GetAllModulos();
        Task<ModulosDTOs> GetModulosById(int id);
        Task<ModulosDTOs> AddModulos(ModulosDTOs modulosDTOs);
        Task<bool> UpdateModulos(int id, ModulosDTOs modulosDTOs);
    }

    public class ModulosQuerie : IModulosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ModulosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ModulosQuerie(ILogger<ModulosQuerie> logger, IConfiguration configuration)
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

        // Get all Modulos records
        public async Task<List<ModulosDTOs>> GetAllModulos()
        {
            try
            {
                var modulos = await _context.ModulosE
                    .Select(m => ModulosDTOs.CreateDTO(m))
                    .ToListAsync();

                return modulos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllModulos)}: {ex.Message}");
                return new List<ModulosDTOs>();
            }
        }

        // Get a Modulos by ID
        public async Task<ModulosDTOs> GetModulosById(int id)
        {
            try
            {
                var modulos = await _context.ModulosE
                    .Where(m => m.id == id)
                    .Select(m => ModulosDTOs.CreateDTO(m))
                    .FirstOrDefaultAsync();

                return modulos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetModulosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Modulos
        public async Task<ModulosDTOs> AddModulos(ModulosDTOs modulosDTOs)
        {
            try
            {
                var modulos = ModulosDTOs.CreateE(modulosDTOs);

                _context.ModulosE.Add(modulos);
                await _context.SaveChangesAsync();

                return ModulosDTOs.CreateDTO(modulos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddModulos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Modulos
        public async Task<bool> UpdateModulos(int id, ModulosDTOs modulosDTOs)
        {
            try
            {
                var modulos = await _context.ModulosE.FindAsync(id);
                if (modulos == null) return false;

                modulos.titulo = modulosDTOs.Titulo;
                modulos.descripcion = modulosDTOs.Descripcion;
                modulos.orden = modulosDTOs.Orden;
                modulos.activo = modulosDTOs.Activo;

                _context.ModulosE.Update(modulos);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateModulos)}: {ex.Message}");
                return false;
            }
        }
    }
}
