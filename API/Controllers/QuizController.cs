using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizQuerie _quizQuerie;
        private readonly ILogger<IQuizQuerie> _logger;

        public QuizController(IQuizQuerie quizQuerie, ILogger<IQuizQuerie> logger)
        {
            _quizQuerie = quizQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllQuiz")]
        public async Task<IActionResult> GetAllQuiz()
        {
            _logger.LogTrace("Iniciando metodo QuizController.GetAllQuiz...");
            try
            {
                var uResult = await _quizQuerie.GetAllQuiz();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizController.GetAllQuiz");
                throw;
            }
        }

        [HttpGet("GetQuizById")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            _logger.LogTrace("Iniciando metodo QuizController.GetQuizById...");
            try
            {
                var uResult = await _quizQuerie.GetQuizById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizController.GetQuizById");
                throw;
            }
        }

        [HttpPost("Post_Create_Quiz")]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizDTOs quizDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando QuizController.CreateQuiz...");
                var respuesta = await _quizQuerie.AddQuiz(quizDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizController.CreateQuiz...");
                throw;
            }
        }

        [HttpPost("Post_Update_Quiz")]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizDTOs quizDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando QuizController.UpdateQuiz...");
                var respuesta = await _quizQuerie.UpdateQuiz(id, quizDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizController.UpdateQuiz...");
                throw;
            }
        }
    }
}
