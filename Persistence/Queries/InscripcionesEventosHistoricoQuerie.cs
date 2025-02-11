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
    public interface IInscripcionesEventosHistoricoQuerie
    {
        Task<List<InscripcionesEventosHistoricoDTOs>> GetAllInscripcionesEventosHistorico();
        Task<InscripcionesEventosHistoricoDTOs> GetInscripcionesEventosHistoricoById(int id);
        Task<InscripcionesEventosHistoricoDTOs> AddInscripcionesEventosHistorico(InscripcionesEventosHistoricoDTOs inscripcionesEventosHistoricoDTOs);
        Task<bool> UpdateInscripcionesEventosHistorico(int id, InscripcionesEventosHistoricoDTOs inscripcionesEventosHistoricoDTOs);
    }

    public class InscripcionesEventosHistoricoQuerie : IInscripcionesEventosHistoricoQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<InscripcionesEventosHistoricoQuerie> _logger;
        private readonly IConfiguration _configuration;

        public InscripcionesEventosHistoricoQuerie(ILogger<InscripcionesEventosHistoricoQuerie> logger, IConfiguration configuration)
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

        // Get all InscripcionesEventosHistorico records
        public async Task<List<InscripcionesEventosHistoricoDTOs>> GetAllInscripcionesEventosHistorico()
        {
            try
            {
                var inscripcionesEventosHistorico = await _context.InscripcionesEventosHistoricoE
                    .Select(ieh => InscripcionesEventosHistoricoDTOs.CreateDTO(ieh))
                    .ToListAsync();

                return inscripcionesEventosHistorico;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllInscripcionesEventosHistorico)}: {ex.Message}");
                return new List<InscripcionesEventosHistoricoDTOs>();
            }
        }

        // Get an InscripcionesEventosHistorico by ID
        public async Task<InscripcionesEventosHistoricoDTOs> GetInscripcionesEventosHistoricoById(int id)
        {
            try
            {
                var inscripcionesEventosHistorico = await _context.InscripcionesEventosHistoricoE
                    .Where(ieh => ieh.id == id)
                    .Select(ieh => InscripcionesEventosHistoricoDTOs.CreateDTO(ieh))
                    .FirstOrDefaultAsync();

                return inscripcionesEventosHistorico;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetInscripcionesEventosHistoricoById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new InscripcionesEventosHistorico
        public async Task<InscripcionesEventosHistoricoDTOs> AddInscripcionesEventosHistorico(InscripcionesEventosHistoricoDTOs inscripcionesEventosHistoricoDTOs)
        {
            try
            {
                var inscripcionesEventosHistorico = InscripcionesEventosHistoricoDTOs.CreateE(inscripcionesEventosHistoricoDTOs);

                _context.InscripcionesEventosHistoricoE.Add(inscripcionesEventosHistorico);
                await _context.SaveChangesAsync();

                return InscripcionesEventosHistoricoDTOs.CreateDTO(inscripcionesEventosHistorico);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddInscripcionesEventosHistorico)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing InscripcionesEventosHistorico
        public async Task<bool> UpdateInscripcionesEventosHistorico(int id, InscripcionesEventosHistoricoDTOs inscripcionesEventosHistoricoDTOs)
        {
            try
            {
                var inscripcionesEventosHistorico = await _context.InscripcionesEventosHistoricoE.FindAsync(id);
                if (inscripcionesEventosHistorico == null) return false;

                inscripcionesEventosHistorico.estado = inscripcionesEventosHistoricoDTOs.Estado;
                inscripcionesEventosHistorico.fecha_registro = inscripcionesEventosHistoricoDTOs.FechaRegistro;

                _context.InscripcionesEventosHistoricoE.Update(inscripcionesEventosHistorico);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateInscripcionesEventosHistorico)}: {ex.Message}");
                return false;
            }
        }
    }
}
