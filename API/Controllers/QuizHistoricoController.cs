using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/QuizHistorico")]
    [ApiController]
    public class QuizHistoricoController : ControllerBase
    {
        private readonly IQuizHistoricoQuerie _quizHistoricoQuerie;
        private readonly ILogger<IQuizHistoricoQuerie> _logger;

        public QuizHistoricoController(IQuizHistoricoQuerie quizHistoricoQuerie, ILogger<IQuizHistoricoQuerie> logger)
        {
            _quizHistoricoQuerie = quizHistoricoQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllQuizHistorico")]
        public async Task<IActionResult> GetAllQuizHistorico()
        {
            _logger.LogTrace("Iniciando metodo QuizHistoricoController.GetAllQuizHistorico...");
            try
            {
                var uResult = await _quizHistoricoQuerie.GetAllQuizHistorico();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizHistoricoController.GetAllQuizHistorico");
                throw;
            }
        }

        [HttpGet("GetQuizHistoricoById")]
        public async Task<IActionResult> GetQuizHistoricoById(int id)
        {
            _logger.LogTrace("Iniciando metodo QuizHistoricoController.GetQuizHistoricoById...");
            try
            {
                var uResult = await _quizHistoricoQuerie.GetQuizHistoricoById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizHistoricoController.GetQuizHistoricoById");
                throw;
            }
        }

        [HttpPost("Post_Create_QuizHistorico")]
        public async Task<IActionResult> CreateQuizHistorico([FromBody] QuizHistoricoDTOs quizHistoricoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando QuizHistoricoController.CreateQuizHistorico...");
                var respuesta = await _quizHistoricoQuerie.AddQuizHistorico(quizHistoricoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizHistoricoController.CreateQuizHistorico...");
                throw;
            }
        }

        [HttpPost("Post_Update_QuizHistorico")]
        public async Task<IActionResult> UpdateQuizHistorico(int quizId, [FromBody] QuizHistoricoDTOs quizHistoricoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando QuizHistoricoController.UpdateQuizHistorico...");
                var respuesta = await _quizHistoricoQuerie.UpdateQuizHistorico(quizId, quizHistoricoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizHistoricoController.UpdateQuizHistorico...");
                throw;
            }
        }
    }
}
