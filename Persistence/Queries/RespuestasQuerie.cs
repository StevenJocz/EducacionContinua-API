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
    public interface IRespuestasQuerie
    {
        Task<List<RespuestasDTOs>> GetAllRespuestas();
        Task<RespuestasDTOs> GetRespuestasById(int id);
        Task<RespuestasDTOs> AddRespuestas(RespuestasDTOs respuestasDTOs);
        Task<bool> UpdateRespuestas(int id, RespuestasDTOs respuestasDTOs);
    }

    public class RespuestasQuerie : IRespuestasQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<RespuestasQuerie> _logger;
        private readonly IConfiguration _configuration;

        public RespuestasQuerie(ILogger<RespuestasQuerie> logger, IConfiguration configuration)
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

        // Get all Respuestas records
        public async Task<List<RespuestasDTOs>> GetAllRespuestas()
        {
            try
            {
                var respuestas = await _context.RespuestasE
                    .Select(r => RespuestasDTOs.CreateDTO(r))
                    .ToListAsync();

                return respuestas;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllRespuestas)}: {ex.Message}");
                return new List<RespuestasDTOs>();
            }
        }

        // Get a Respuesta by ID
        public async Task<RespuestasDTOs> GetRespuestasById(int id)
        {
            try
            {
                var respuesta = await _context.RespuestasE
                    .Where(r => r.id == id)
                    .Select(r => RespuestasDTOs.CreateDTO(r))
                    .FirstOrDefaultAsync();

                return respuesta;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetRespuestasById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Respuesta
        public async Task<RespuestasDTOs> AddRespuestas(RespuestasDTOs respuestasDTOs)
        {
            try
            {
                var respuesta = RespuestasDTOs.CreateE(respuestasDTOs);

                _context.RespuestasE.Add(respuesta);
                await _context.SaveChangesAsync();

                return RespuestasDTOs.CreateDTO(respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddRespuestas)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Respuesta
        public async Task<bool> UpdateRespuestas(int id, RespuestasDTOs respuestasDTOs)
        {
            try
            {
                var respuesta = await _context.RespuestasE.FindAsync(id);
                if (respuesta == null) return false;

                respuesta.pregunta_id = respuestasDTOs.PreguntaId;
                respuesta.texto = respuestasDTOs.Texto;
                respuesta.correcta = respuestasDTOs.Correcta;

                _context.RespuestasE.Update(respuesta);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateRespuestas)}: {ex.Message}");
                return false;
            }
        }
    }
}
