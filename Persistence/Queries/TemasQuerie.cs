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
    public interface ITemasQuerie
    {
        Task<List<TemasDTOs>> GetAllTemas();
        Task<TemasDTOs> GetTemasById(int id);
        Task<TemasDTOs> AddTemas(TemasDTOs temasDTOs);
        Task<bool> UpdateTemas(int id, TemasDTOs temasDTOs);
    }

    public class TemasQuerie : ITemasQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<TemasQuerie> _logger;
        private readonly IConfiguration _configuration;

        public TemasQuerie(ILogger<TemasQuerie> logger, IConfiguration configuration)
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

        // Get all Temas records
        public async Task<List<TemasDTOs>> GetAllTemas()
        {
            try
            {
                var temas = await _context.TemasE
                    .Select(t => TemasDTOs.CreateDTO(t))
                    .ToListAsync();

                return temas;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllTemas)}: {ex.Message}");
                return new List<TemasDTOs>();
            }
        }

        // Get a Tema by ID
        public async Task<TemasDTOs> GetTemasById(int id)
        {
            try
            {
                var tema = await _context.TemasE
                    .Where(t => t.id == id)
                    .Select(t => TemasDTOs.CreateDTO(t))
                    .FirstOrDefaultAsync();

                return tema;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetTemasById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Tema
        public async Task<TemasDTOs> AddTemas(TemasDTOs temasDTOs)
        {
            try
            {
                var tema = TemasDTOs.CreateE(temasDTOs);

                _context.TemasE.Add(tema);
                await _context.SaveChangesAsync();

                return TemasDTOs.CreateDTO(tema);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddTemas)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Tema
        public async Task<bool> UpdateTemas(int id, TemasDTOs temasDTOs)
        {
            try
            {
                var tema = await _context.TemasE.FindAsync(id);
                if (tema == null) return false;

                tema.modulo_id = temasDTOs.ModuloId;
                tema.titulo = temasDTOs.Titulo;
                tema.descripcion = temasDTOs.Descripcion;
                tema.video = temasDTOs.Video;
                tema.orden = temasDTOs.Orden;
                tema.activo = temasDTOs.Activo;
                tema.req_evidencia = temasDTOs.RequiereEvidencia;
                tema.des_evidencia = temasDTOs.DescripcionEvidencia;

                _context.TemasE.Update(tema);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateTemas)}: {ex.Message}");
                return false;
            }
        }
    }
}
