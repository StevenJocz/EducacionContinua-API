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
    public interface IGrupoComentariosQuerie
    {
        Task<List<GrupoComentariosDTOs>> GetAllGrupoComentarios();
        Task<GrupoComentariosDTOs> GetGrupoComentariosById(int id);
        Task<GrupoComentariosDTOs> AddGrupoComentarios(GrupoComentariosDTOs grupoComentariosDTOs);
        Task<bool> UpdateGrupoComentarios(int id, GrupoComentariosDTOs grupoComentariosDTOs);
    }

    public class GrupoComentariosQuerie : IGrupoComentariosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<GrupoComentariosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public GrupoComentariosQuerie(ILogger<GrupoComentariosQuerie> logger, IConfiguration configuration)
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

        // Get all GrupoComentarios records
        public async Task<List<GrupoComentariosDTOs>> GetAllGrupoComentarios()
        {
            try
            {
                var grupoComentarios = await _context.GrupoComentariosE
                    .Select(gc => GrupoComentariosDTOs.CreateDTO(gc))
                    .ToListAsync();

                return grupoComentarios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllGrupoComentarios)}: {ex.Message}");
                return new List<GrupoComentariosDTOs>();
            }
        }

        // Get a GrupoComentario by ID
        public async Task<GrupoComentariosDTOs> GetGrupoComentariosById(int id)
        {
            try
            {
                var grupoComentario = await _context.GrupoComentariosE
                    .Where(gc => gc.id == id)
                    .Select(gc => GrupoComentariosDTOs.CreateDTO(gc))
                    .FirstOrDefaultAsync();

                return grupoComentario;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetGrupoComentariosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new GrupoComentario
        public async Task<GrupoComentariosDTOs> AddGrupoComentarios(GrupoComentariosDTOs grupoComentariosDTOs)
        {
            try
            {
                var grupoComentario = GrupoComentariosDTOs.CreateE(grupoComentariosDTOs);

                _context.GrupoComentariosE.Add(grupoComentario);
                await _context.SaveChangesAsync();

                return GrupoComentariosDTOs.CreateDTO(grupoComentario);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddGrupoComentarios)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing GrupoComentario
        public async Task<bool> UpdateGrupoComentarios(int id, GrupoComentariosDTOs grupoComentariosDTOs)
        {
            try
            {
                var grupoComentario = await _context.GrupoComentariosE.FindAsync(id);
                if (grupoComentario == null) return false;

                grupoComentario.comentario = grupoComentariosDTOs.Comentario;
                grupoComentario.likes = grupoComentariosDTOs.Likes;
                grupoComentario.aprobado = grupoComentariosDTOs.Aprobado;

                _context.GrupoComentariosE.Update(grupoComentario);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateGrupoComentarios)}: {ex.Message}");
                return false;
            }
        }
    }
}
