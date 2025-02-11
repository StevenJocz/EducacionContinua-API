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
    public interface IQuizPersonaQuerie
    {
        Task<List<QuizPersonaDTOs>> GetAllQuizPersona();
        Task<QuizPersonaDTOs> GetQuizPersonaById(int id);
        Task<QuizPersonaDTOs> AddQuizPersona(QuizPersonaDTOs quizPersonaDTOs);
        Task<bool> UpdateQuizPersona(int id, QuizPersonaDTOs quizPersonaDTOs);
    }

    public class QuizPersonaQuerie : IQuizPersonaQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<QuizPersonaQuerie> _logger;
        private readonly IConfiguration _configuration;

        public QuizPersonaQuerie(ILogger<QuizPersonaQuerie> logger, IConfiguration configuration)
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

        // Get all QuizPersona records
        public async Task<List<QuizPersonaDTOs>> GetAllQuizPersona()
        {
            try
            {
                var quizPersona = await _context.QuizPersonaE
                    .Select(qp => QuizPersonaDTOs.CreateDTO(qp))
                    .ToListAsync();

                return quizPersona;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllQuizPersona)}: {ex.Message}");
                return new List<QuizPersonaDTOs>();
            }
        }

        // Get a QuizPersona by ID
        public async Task<QuizPersonaDTOs> GetQuizPersonaById(int id)
        {
            try
            {
                var quizPersona = await _context.QuizPersonaE
                    .Where(qp => qp.quiz_id == id)
                    .Select(qp => QuizPersonaDTOs.CreateDTO(qp))
                    .FirstOrDefaultAsync();

                return quizPersona;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetQuizPersonaById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new QuizPersona
        public async Task<QuizPersonaDTOs> AddQuizPersona(QuizPersonaDTOs quizPersonaDTOs)
        {
            try
            {
                var quizPersona = QuizPersonaDTOs.CreateE(quizPersonaDTOs);

                _context.QuizPersonaE.Add(quizPersona);
                await _context.SaveChangesAsync();

                return QuizPersonaDTOs.CreateDTO(quizPersona);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddQuizPersona)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing QuizPersona
        public async Task<bool> UpdateQuizPersona(int id, QuizPersonaDTOs quizPersonaDTOs)
        {
            try
            {
                var quizPersona = await _context.QuizPersonaE.FindAsync(id);
                if (quizPersona == null) return false;

                quizPersona.persona_id = quizPersonaDTOs.PersonaId;
                quizPersona.intentos = quizPersonaDTOs.Intentos;
                quizPersona.calificacion = quizPersonaDTOs.Calificacion;

                _context.QuizPersonaE.Update(quizPersona);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateQuizPersona)}: {ex.Message}");
                return false;
            }
        }
    }
}
