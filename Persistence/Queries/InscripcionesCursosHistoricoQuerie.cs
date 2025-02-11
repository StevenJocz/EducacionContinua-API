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
    public interface IInscripcionesCursosHistoricoQuerie
    {
        Task<List<InscripcionesCursosHistoricoDTOs>> GetAllInscripcionesCursosHistorico();
        Task<InscripcionesCursosHistoricoDTOs> GetInscripcionesCursosHistoricoById(int id);
        Task<InscripcionesCursosHistoricoDTOs> AddInscripcionesCursosHistorico(InscripcionesCursosHistoricoDTOs inscripcionesCursosHistoricoDTOs);
        Task<bool> UpdateInscripcionesCursosHistorico(int id, InscripcionesCursosHistoricoDTOs inscripcionesCursosHistoricoDTOs);
    }

    public class InscripcionesCursosHistoricoQuerie : IInscripcionesCursosHistoricoQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<InscripcionesCursosHistoricoQuerie> _logger;
        private readonly IConfiguration _configuration;

        public InscripcionesCursosHistoricoQuerie(ILogger<InscripcionesCursosHistoricoQuerie> logger, IConfiguration configuration)
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

        // Get all InscripcionesCursosHistorico records
        public async Task<List<InscripcionesCursosHistoricoDTOs>> GetAllInscripcionesCursosHistorico()
        {
            try
            {
                var inscripcionesCursosHistorico = await _context.InscripcionesCursosHistoricoE
                    .Select(ich => InscripcionesCursosHistoricoDTOs.CreateDTO(ich))
                    .ToListAsync();

                return inscripcionesCursosHistorico;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllInscripcionesCursosHistorico)}: {ex.Message}");
                return new List<InscripcionesCursosHistoricoDTOs>();
            }
        }

        // Get an InscripcionesCursosHistorico by ID
        public async Task<InscripcionesCursosHistoricoDTOs> GetInscripcionesCursosHistoricoById(int id)
        {
            try
            {
                var inscripcionesCursosHistorico = await _context.InscripcionesCursosHistoricoE
                    .Where(ich => ich.id == id)
                    .Select(ich => InscripcionesCursosHistoricoDTOs.CreateDTO(ich))
                    .FirstOrDefaultAsync();

                return inscripcionesCursosHistorico;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetInscripcionesCursosHistoricoById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new InscripcionesCursosHistorico
        public async Task<InscripcionesCursosHistoricoDTOs> AddInscripcionesCursosHistorico(InscripcionesCursosHistoricoDTOs inscripcionesCursosHistoricoDTOs)
        {
            try
            {
                var inscripcionesCursosHistorico = InscripcionesCursosHistoricoDTOs.CreateE(inscripcionesCursosHistoricoDTOs);

                _context.InscripcionesCursosHistoricoE.Add(inscripcionesCursosHistorico);
                await _context.SaveChangesAsync();

                return InscripcionesCursosHistoricoDTOs.CreateDTO(inscripcionesCursosHistorico);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddInscripcionesCursosHistorico)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing InscripcionesCursosHistorico
        public async Task<bool> UpdateInscripcionesCursosHistorico(int id, InscripcionesCursosHistoricoDTOs inscripcionesCursosHistoricoDTOs)
        {
            try
            {
                var inscripcionesCursosHistorico = await _context.InscripcionesCursosHistoricoE.FindAsync(id);
                if (inscripcionesCursosHistorico == null) return false;

                inscripcionesCursosHistorico.estado = inscripcionesCursosHistoricoDTOs.Estado;
                inscripcionesCursosHistorico.fecha_edicion = inscripcionesCursosHistoricoDTOs.FechaEdicion;

                _context.InscripcionesCursosHistoricoE.Update(inscripcionesCursosHistorico);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateInscripcionesCursosHistorico)}: {ex.Message}");
                return false;
            }
        }
    }
}
