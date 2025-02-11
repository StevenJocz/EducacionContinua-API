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
    public interface IPreguntasQuerie
    {
        Task<List<PreguntasDTOs>> GetAllPreguntas();
        Task<PreguntasDTOs> GetPreguntasById(int id);
        Task<PreguntasDTOs> AddPreguntas(PreguntasDTOs preguntasDTOs);
        Task<bool> UpdatePreguntas(int id, PreguntasDTOs preguntasDTOs);
    }

    public class PreguntasQuerie : IPreguntasQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<PreguntasQuerie> _logger;
        private readonly IConfiguration _configuration;

        public PreguntasQuerie(ILogger<PreguntasQuerie> logger, IConfiguration configuration)
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

        // Get all Preguntas records
        public async Task<List<PreguntasDTOs>> GetAllPreguntas()
        {
            try
            {
                var preguntas = await _context.PreguntasE
                    .Select(p => PreguntasDTOs.CreateDTO(p))
                    .ToListAsync();

                return preguntas;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllPreguntas)}: {ex.Message}");
                return new List<PreguntasDTOs>();
            }
        }

        // Get a Pregunta by ID
        public async Task<PreguntasDTOs> GetPreguntasById(int id)
        {
            try
            {
                var pregunta = await _context.PreguntasE
                    .Where(p => p.id == id)
                    .Select(p => PreguntasDTOs.CreateDTO(p))
                    .FirstOrDefaultAsync();

                return pregunta;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPreguntasById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Pregunta
        public async Task<PreguntasDTOs> AddPreguntas(PreguntasDTOs preguntasDTOs)
        {
            try
            {
                var pregunta = PreguntasDTOs.CreateE(preguntasDTOs);

                _context.PreguntasE.Add(pregunta);
                await _context.SaveChangesAsync();

                return PreguntasDTOs.CreateDTO(pregunta);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddPreguntas)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Pregunta
        public async Task<bool> UpdatePreguntas(int id, PreguntasDTOs preguntasDTOs)
        {
            try
            {
                var pregunta = await _context.PreguntasE.FindAsync(id);
                if (pregunta == null) return false;

                pregunta.quiz_id = preguntasDTOs.QuizId;
                pregunta.texto = preguntasDTOs.Texto;

                _context.PreguntasE.Update(pregunta);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdatePreguntas)}: {ex.Message}");
                return false;
            }
        }
    }
}
