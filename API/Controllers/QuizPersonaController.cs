using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/QuizPersona")]
    [ApiController]
    public class QuizPersonaController : ControllerBase
    {
        private readonly IQuizPersonaQuerie _quizPersonaQuerie;
        private readonly ILogger<IQuizPersonaQuerie> _logger;

        public QuizPersonaController(IQuizPersonaQuerie quizPersonaQuerie, ILogger<IQuizPersonaQuerie> logger)
        {
            _quizPersonaQuerie = quizPersonaQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllQuizPersona")]
        public async Task<IActionResult> GetAllQuizPersona()
        {
            _logger.LogTrace("Iniciando metodo QuizPersonaController.GetAllQuizPersona...");
            try
            {
                var uResult = await _quizPersonaQuerie.GetAllQuizPersona();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizPersonaController.GetAllQuizPersona");
                throw;
            }
        }

        [HttpGet("GetQuizPersonaById")]
        public async Task<IActionResult> GetQuizPersonaById(int id)
        {
            _logger.LogTrace("Iniciando metodo QuizPersonaController.GetQuizPersonaById...");
            try
            {
                var uResult = await _quizPersonaQuerie.GetQuizPersonaById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizPersonaController.GetQuizPersonaById");
                throw;
            }
        }

        [HttpPost("Post_Create_QuizPersona")]
        public async Task<IActionResult> CreateQuizPersona([FromBody] QuizPersonaDTOs quizPersonaDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando QuizPersonaController.CreateQuizPersona...");
                var respuesta = await _quizPersonaQuerie.AddQuizPersona(quizPersonaDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizPersonaController.CreateQuizPersona...");
                throw;
            }
        }

        [HttpPost("Post_Update_QuizPersona")]
        public async Task<IActionResult> UpdateQuizPersona(int id, [FromBody] QuizPersonaDTOs quizPersonaDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando QuizPersonaController.UpdateQuizPersona...");
                var respuesta = await _quizPersonaQuerie.UpdateQuizPersona(id, quizPersonaDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar QuizPersonaController.UpdateQuizPersona...");
                throw;
            }
        }
    }
}
