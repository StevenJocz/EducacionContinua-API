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
    public interface ITipoDireccionesQuerie
    {
        Task<List<TipoDireccionesDTOs>> GetAllTiposDirecciones();
        Task<TipoDireccionesDTOs> GetTipoDireccionById(int id);
        Task<TipoDireccionesDTOs> AddTipoDireccion(TipoDireccionesDTOs tipoDireccionesDTOs);
        Task<bool> UpdateDireccion(TipoDireccionesDTOs tipoDireccionesDTOs);
    }

    public class TipoDireccionesQuerie : ITipoDireccionesQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<TipoDireccionesQuerie> _logger;
        private readonly IConfiguration _configuration;

        public TipoDireccionesQuerie(ILogger<TipoDireccionesQuerie> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new DBContext(connectionString);
        }

        #region Implementación Dispose
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

        // Obtener todas las direcciones
        public async Task<List<TipoDireccionesDTOs>> GetAllTiposDirecciones()
        {
            try
            {
                var direcciones = await _context.TipoDireccionesE
                    .Select(a => TipoDireccionesDTOs.CreateDTO(a))
                    .ToListAsync();

                return direcciones;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetAllTiposDirecciones)}: {ex.Message}");
                return new List<TipoDireccionesDTOs>();
            }
        }

        // Obtener una dirección por ID
        public async Task<TipoDireccionesDTOs> GetTipoDireccionById(int id)
        {
            try
            {
                var direccion = await _context.TipoDireccionesE
                    .Where(a => a.id == id)
                    .Select(a => TipoDireccionesDTOs.CreateDTO(a))
                    .FirstOrDefaultAsync();

                return direccion;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetTipoDireccionById)}: {ex.Message}");
                return null;
            }
        }

        // Agregar un nuevo tipo de dirección
        public async Task<TipoDireccionesDTOs> AddTipoDireccion(TipoDireccionesDTOs tipoDireccionesDTOs)
        {
            try
            {
                var direccion = TipoDireccionesDTOs.CreateE(tipoDireccionesDTOs);

                _context.TipoDireccionesE.Add(direccion);
                await _context.SaveChangesAsync();

                return TipoDireccionesDTOs.CreateDTO(direccion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddTipoDireccion)}: {ex.Message}");
                return null;
            }
        }

        // Actualizar una dirección existente
        public async Task<bool> UpdateDireccion(TipoDireccionesDTOs tipoDireccionesDTOs)
        {
            try
            {
                var direccion = await _context.TipoDireccionesE.FindAsync(tipoDireccionesDTOs.Id);
                if (direccion == null) return false;

                direccion.nombre = tipoDireccionesDTOs.Nombre;

                _context.TipoDireccionesE.Update(direccion);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateDireccion)}: {ex.Message}");
                return false;
            }
        }
    }
}
