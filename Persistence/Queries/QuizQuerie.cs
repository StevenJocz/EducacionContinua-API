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
    public interface IQuizQuerie
    {
        Task<List<QuizDTOs>> GetAllQuiz();
        Task<QuizDTOs> GetQuizById(int id);
        Task<QuizDTOs> AddQuiz(QuizDTOs quizDTOs);
        Task<bool> UpdateQuiz(int id, QuizDTOs quizDTOs);
    }

    public class QuizQuerie : IQuizQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<QuizQuerie> _logger;
        private readonly IConfiguration _configuration;

        public QuizQuerie(ILogger<QuizQuerie> logger, IConfiguration configuration)
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

        // Get all Quiz records
        public async Task<List<QuizDTOs>> GetAllQuiz()
        {
            try
            {
                var quiz = await _context.QuizE
                    .Select(q => QuizDTOs.CreateDTO(q))
                    .ToListAsync();

                return quiz;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllQuiz)}: {ex.Message}");
                return new List<QuizDTOs>();
            }
        }

        // Get a Quiz by ID
        public async Task<QuizDTOs> GetQuizById(int id)
        {
            try
            {
                var quiz = await _context.QuizE
                    .Where(q => q.id == id)
                    .Select(q => QuizDTOs.CreateDTO(q))
                    .FirstOrDefaultAsync();

                return quiz;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetQuizById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Quiz
        public async Task<QuizDTOs> AddQuiz(QuizDTOs quizDTOs)
        {
            try
            {
                var quiz = QuizDTOs.CreateE(quizDTOs);

                _context.QuizE.Add(quiz);
                await _context.SaveChangesAsync();

                return QuizDTOs.CreateDTO(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddQuiz)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Quiz
        public async Task<bool> UpdateQuiz(int id, QuizDTOs quizDTOs)
        {
            try
            {
                var quiz = await _context.QuizE.FindAsync(id);
                if (quiz == null) return false;

                quiz.modulo_id = quizDTOs.ModuloId;
                quiz.nombre = quizDTOs.Nombre;
                quiz.descripcion = quizDTOs.Descripcion;

                _context.QuizE.Update(quiz);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateQuiz)}: {ex.Message}");
                return false;
            }
        }
    }
}
