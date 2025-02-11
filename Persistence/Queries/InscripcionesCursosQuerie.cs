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
    public interface IInscripcionesCursosQuerie
    {
        Task<List<InscripcionesCursosDTOs>> GetAllInscripcionesCursos();
        Task<InscripcionesCursosDTOs> GetInscripcionesCursosById(int id);
        Task<InscripcionesCursosDTOs> AddInscripcionesCursos(InscripcionesCursosDTOs inscripcionesCursosDTOs);
        Task<bool> UpdateInscripcionesCursos(int id, InscripcionesCursosDTOs inscripcionesCursosDTOs);
    }

    public class InscripcionesCursosQuerie : IInscripcionesCursosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<InscripcionesCursosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public InscripcionesCursosQuerie(ILogger<InscripcionesCursosQuerie> logger, IConfiguration configuration)
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

        // Get all InscripcionesCursos records
        public async Task<List<InscripcionesCursosDTOs>> GetAllInscripcionesCursos()
        {
            try
            {
                var inscripcionesCursos = await _context.InscripcionesCursosE
                    .Select(ic => InscripcionesCursosDTOs.CreateDTO(ic))
                    .ToListAsync();

                return inscripcionesCursos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllInscripcionesCursos)}: {ex.Message}");
                return new List<InscripcionesCursosDTOs>();
            }
        }

        // Get an InscripcionesCursos by ID
        public async Task<InscripcionesCursosDTOs> GetInscripcionesCursosById(int id)
        {
            try
            {
                var inscripcionesCursos = await _context.InscripcionesCursosE
                    .Where(ic => ic.id == id)
                    .Select(ic => InscripcionesCursosDTOs.CreateDTO(ic))
                    .FirstOrDefaultAsync();

                return inscripcionesCursos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetInscripcionesCursosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new InscripcionesCursos
        public async Task<InscripcionesCursosDTOs> AddInscripcionesCursos(InscripcionesCursosDTOs inscripcionesCursosDTOs)
        {
            try
            {
                var inscripcionesCursos = InscripcionesCursosDTOs.CreateE(inscripcionesCursosDTOs);

                _context.InscripcionesCursosE.Add(inscripcionesCursos);
                await _context.SaveChangesAsync();

                return InscripcionesCursosDTOs.CreateDTO(inscripcionesCursos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddInscripcionesCursos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing InscripcionesCursos
        public async Task<bool> UpdateInscripcionesCursos(int id, InscripcionesCursosDTOs inscripcionesCursosDTOs)
        {
            try
            {
                var inscripcionesCursos = await _context.InscripcionesCursosE.FindAsync(id);
                if (inscripcionesCursos == null) return false;

                inscripcionesCursos.estado = inscripcionesCursosDTOs.Estado;
                inscripcionesCursos.fecha_registro = inscripcionesCursosDTOs.FechaRegistro;

                _context.InscripcionesCursosE.Update(inscripcionesCursos);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateInscripcionesCursos)}: {ex.Message}");
                return false;
            }
        }
    }
}
