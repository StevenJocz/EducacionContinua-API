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
    public interface IGruposQuerie
    {
        Task<List<GruposDTOs>> GetAllGrupos();
        Task<GruposDTOs> GetGruposById(int id);
        Task<GruposDTOs> AddGrupos(GruposDTOs gruposDTOs);
        Task<bool> UpdateGrupos(int id, GruposDTOs gruposDTOs);
    }

    public class GruposQuerie : IGruposQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<GruposQuerie> _logger;
        private readonly IConfiguration _configuration;

        public GruposQuerie(ILogger<GruposQuerie> logger, IConfiguration configuration)
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

        // Get all Grupos records
        public async Task<List<GruposDTOs>> GetAllGrupos()
        {
            try
            {
                var grupos = await _context.GruposE
                    .Select(g => GruposDTOs.CreateDTO(g))
                    .ToListAsync();

                return grupos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllGrupos)}: {ex.Message}");
                return new List<GruposDTOs>();
            }
        }

        // Get a Grupos by ID
        public async Task<GruposDTOs> GetGruposById(int id)
        {
            try
            {
                var grupos = await _context.GruposE
                    .Where(g => g.id == id)
                    .Select(g => GruposDTOs.CreateDTO(g))
                    .FirstOrDefaultAsync();

                return grupos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetGruposById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Grupos
        public async Task<GruposDTOs> AddGrupos(GruposDTOs gruposDTOs)
        {
            try
            {
                var grupos = GruposDTOs.CreateE(gruposDTOs);

                _context.GruposE.Add(grupos);
                await _context.SaveChangesAsync();

                return GruposDTOs.CreateDTO(grupos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddGrupos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Grupos
        public async Task<bool> UpdateGrupos(int id, GruposDTOs gruposDTOs)
        {
            try
            {
                var grupos = await _context.GruposE.FindAsync(id);
                if (grupos == null) return false;

                grupos.nombre = gruposDTOs.Nombre;
                grupos.precio = gruposDTOs.Precio;
                grupos.fecha_inicio = gruposDTOs.FechaInicio;
                grupos.fecha_fin = gruposDTOs.FechaFin;

                _context.GruposE.Update(grupos);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateGrupos)}: {ex.Message}");
                return false;
            }
        }
    }
}
