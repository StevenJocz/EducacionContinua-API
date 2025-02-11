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
    public interface IQuizHistoricoQuerie
    {
        Task<List<QuizHistoricoDTOs>> GetAllQuizHistorico();
        Task<QuizHistoricoDTOs> GetQuizHistoricoById(int id);
        Task<QuizHistoricoDTOs> AddQuizHistorico(QuizHistoricoDTOs quizHistoricoDTOs);
        Task<bool> UpdateQuizHistorico(int id, QuizHistoricoDTOs quizHistoricoDTOs);
    }

    public class QuizHistoricoQuerie : IQuizHistoricoQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<QuizHistoricoQuerie> _logger;
        private readonly IConfiguration _configuration;

        public QuizHistoricoQuerie(ILogger<QuizHistoricoQuerie> logger, IConfiguration configuration)
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

        // Get all QuizHistorico records
        public async Task<List<QuizHistoricoDTOs>> GetAllQuizHistorico()
        {
            try
            {
                var quizHistorico = await _context.QuizHistoricoE
                    .Select(qh => QuizHistoricoDTOs.CreateDTO(qh))
                    .ToListAsync();

                return quizHistorico;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllQuizHistorico)}: {ex.Message}");
                return new List<QuizHistoricoDTOs>();
            }
        }

        // Get a QuizHistorico by ID
        public async Task<QuizHistoricoDTOs> GetQuizHistoricoById(int id)
        {
            try
            {
                var quizHistorico = await _context.QuizHistoricoE
                    .Where(qh => qh.quiz_id == id)
                    .Select(qh => QuizHistoricoDTOs.CreateDTO(qh))
                    .FirstOrDefaultAsync();

                return quizHistorico;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetQuizHistoricoById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new QuizHistorico
        public async Task<QuizHistoricoDTOs> AddQuizHistorico(QuizHistoricoDTOs quizHistoricoDTOs)
        {
            try
            {
                var quizHistorico = QuizHistoricoDTOs.CreateE(quizHistoricoDTOs);

                _context.QuizHistoricoE.Add(quizHistorico);
                await _context.SaveChangesAsync();

                return QuizHistoricoDTOs.CreateDTO(quizHistorico);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddQuizHistorico)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing QuizHistorico
        public async Task<bool> UpdateQuizHistorico(int id, QuizHistoricoDTOs quizHistoricoDTOs)
        {
            try
            {
                var quizHistorico = await _context.QuizHistoricoE.FindAsync(id);
                if (quizHistorico == null) return false;

                quizHistorico.persona_id = quizHistoricoDTOs.PersonaId;
                quizHistorico.intentos = quizHistoricoDTOs.Intentos;
                quizHistorico.calificacion = quizHistoricoDTOs.Calificacion;

                _context.QuizHistoricoE.Update(quizHistorico);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateQuizHistorico)}: {ex.Message}");
                return false;
            }
        }
    }
}
