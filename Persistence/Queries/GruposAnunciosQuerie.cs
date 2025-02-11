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
    public interface IGruposAnunciosQuerie
    {
        Task<List<GruposAnunciosDTOs>> GetAllGruposAnuncios();
        Task<GruposAnunciosDTOs> GetGruposAnunciosById(int id);
        Task<GruposAnunciosDTOs> AddGruposAnuncios(GruposAnunciosDTOs gruposAnunciosDTOs);
        Task<bool> UpdateGruposAnuncios(int id, GruposAnunciosDTOs gruposAnunciosDTOs);
    }

    public class GruposAnunciosQuerie : IGruposAnunciosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<GruposAnunciosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public GruposAnunciosQuerie(ILogger<GruposAnunciosQuerie> logger, IConfiguration configuration)
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

        // Get all GruposAnuncios records
        public async Task<List<GruposAnunciosDTOs>> GetAllGruposAnuncios()
        {
            try
            {
                var gruposAnuncios = await _context.GruposAnunciosE
                    .Select(ga => GruposAnunciosDTOs.CreateDTO(ga))
                    .ToListAsync();

                return gruposAnuncios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllGruposAnuncios)}: {ex.Message}");
                return new List<GruposAnunciosDTOs>();
            }
        }

        // Get a GruposAnuncios by ID
        public async Task<GruposAnunciosDTOs> GetGruposAnunciosById(int id)
        {
            try
            {
                var gruposAnuncios = await _context.GruposAnunciosE
                    .Where(ga => ga.id == id)
                    .Select(ga => GruposAnunciosDTOs.CreateDTO(ga))
                    .FirstOrDefaultAsync();

                return gruposAnuncios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetGruposAnunciosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new GruposAnuncios
        public async Task<GruposAnunciosDTOs> AddGruposAnuncios(GruposAnunciosDTOs gruposAnunciosDTOs)
        {
            try
            {
                var gruposAnuncios = GruposAnunciosDTOs.CreateE(gruposAnunciosDTOs);

                _context.GruposAnunciosE.Add(gruposAnuncios);
                await _context.SaveChangesAsync();

                return GruposAnunciosDTOs.CreateDTO(gruposAnuncios);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddGruposAnuncios)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing GruposAnuncios
        public async Task<bool> UpdateGruposAnuncios(int id, GruposAnunciosDTOs gruposAnunciosDTOs)
        {
            try
            {
                var gruposAnuncios = await _context.GruposAnunciosE.FindAsync(id);
                if (gruposAnuncios == null) return false;

                gruposAnuncios.comentario = gruposAnunciosDTOs.Comentario;
                gruposAnuncios.enlace = gruposAnunciosDTOs.Enlace;

                _context.GruposAnunciosE.Update(gruposAnuncios);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateGruposAnuncios)}: {ex.Message}");
                return false;
            }
        }
    }
}
