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
    public interface IInscripcionesEventosQuerie
    {
        Task<List<InscripcionesEventosDTOs>> GetAllInscripcionesEventos();
        Task<InscripcionesEventosDTOs> GetInscripcionesEventosById(int id);
        Task<InscripcionesEventosDTOs> AddInscripcionesEventos(InscripcionesEventosDTOs inscripcionesEventosDTOs);
        Task<bool> UpdateInscripcionesEventos(int id, InscripcionesEventosDTOs inscripcionesEventosDTOs);
    }

    public class InscripcionesEventosQuerie : IInscripcionesEventosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<InscripcionesEventosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public InscripcionesEventosQuerie(ILogger<InscripcionesEventosQuerie> logger, IConfiguration configuration)
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

        // Get all InscripcionesEventos records
        public async Task<List<InscripcionesEventosDTOs>> GetAllInscripcionesEventos()
        {
            try
            {
                var inscripcionesEventos = await _context.InscripcionesEventosE
                    .Select(ie => InscripcionesEventosDTOs.CreateDTO(ie))
                    .ToListAsync();

                return inscripcionesEventos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllInscripcionesEventos)}: {ex.Message}");
                return new List<InscripcionesEventosDTOs>();
            }
        }

        // Get an InscripcionesEventos by ID
        public async Task<InscripcionesEventosDTOs> GetInscripcionesEventosById(int id)
        {
            try
            {
                var inscripcionesEventos = await _context.InscripcionesEventosE
                    .Where(ie => ie.id == id)
                    .Select(ie => InscripcionesEventosDTOs.CreateDTO(ie))
                    .FirstOrDefaultAsync();

                return inscripcionesEventos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetInscripcionesEventosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new InscripcionesEventos
        public async Task<InscripcionesEventosDTOs> AddInscripcionesEventos(InscripcionesEventosDTOs inscripcionesEventosDTOs)
        {
            try
            {
                var inscripcionesEventos = InscripcionesEventosDTOs.CreateE(inscripcionesEventosDTOs);

                _context.InscripcionesEventosE.Add(inscripcionesEventos);
                await _context.SaveChangesAsync();

                return InscripcionesEventosDTOs.CreateDTO(inscripcionesEventos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddInscripcionesEventos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing InscripcionesEventos
        public async Task<bool> UpdateInscripcionesEventos(int id, InscripcionesEventosDTOs inscripcionesEventosDTOs)
        {
            try
            {
                var inscripcionesEventos = await _context.InscripcionesEventosE.FindAsync(id);
                if (inscripcionesEventos == null) return false;

                inscripcionesEventos.estado = inscripcionesEventosDTOs.Estado;
                inscripcionesEventos.fecha_registro = inscripcionesEventosDTOs.FechaRegistro;

                _context.InscripcionesEventosE.Update(inscripcionesEventos);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateInscripcionesEventos)}: {ex.Message}");
                return false;
            }
        }
    }
}
