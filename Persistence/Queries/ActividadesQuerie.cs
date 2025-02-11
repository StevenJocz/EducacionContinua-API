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
    public interface IActividadesQuerie
    {
        Task<List<ActividadesDTOs>> GetAllActividades();
        Task<ActividadesDTOs> GetActividadesById(int id);
        Task<ActividadesDTOs> AddActividades(ActividadesDTOs actividadesDTOs);
        Task<bool> UpdateActividades(int id, ActividadesDTOs actividadesDTOs);
    }

    public class ActividadesQuerie : IActividadesQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ActividadesQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ActividadesQuerie(ILogger<ActividadesQuerie> logger, IConfiguration configuration)
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
        public async Task<List<ActividadesDTOs>> GetAllActividades()
        {
            try
            {
                var actividades = await _context.ActividadesE
                    .Select(a => ActividadesDTOs.CreateDTO(a))
                    .ToListAsync();

                return actividades;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllActividades)}: {ex.Message}");
                return new List<ActividadesDTOs>();
            }
        }

        // Get a Actividades by ID
        public async Task<ActividadesDTOs> GetActividadesById(int id)
        {
            try
            {
                var actividad = await _context.ActividadesE
                    .Where(a => a.id == id)
                    .Select(a => ActividadesDTOs.CreateDTO(a))
                    .FirstOrDefaultAsync();

                return actividad;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetActividadesById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Actividades
        public async Task<ActividadesDTOs> AddActividades(ActividadesDTOs actividadesDTOs)
        {
            try
            {
                var actividad = ActividadesDTOs.CreateE(actividadesDTOs);

                _context.ActividadesE.Add(actividad);
                await _context.SaveChangesAsync();

                return ActividadesDTOs.CreateDTO(actividad);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddActividades)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Actividades
        public async Task<bool> UpdateActividades(int id, ActividadesDTOs actividadesDTOs)
        {
            try
            {
                var actividad = await _context.ActividadesE.FindAsync(id);
                if (actividad == null) return false;

                actividad.titulo = actividadesDTOs.Titulo;
                actividad.descripcion = actividadesDTOs.Descripcion;
                actividad.imagen = actividadesDTOs.Imagen;
                actividad.precio = actividadesDTOs.Precio;
                actividad.localizacion = actividadesDTOs.Localizacion;

                _context.ActividadesE.Update(actividad);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateActividades)}: {ex.Message}");
                return false;
            }
        }
    }
}
