using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Preguntas")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private readonly IPreguntasQuerie _preguntasQuerie;
        private readonly ILogger<IPreguntasQuerie> _logger;

        public PreguntasController(IPreguntasQuerie preguntasQuerie, ILogger<IPreguntasQuerie> logger)
        {
            _preguntasQuerie = preguntasQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllPreguntas")]
        public async Task<IActionResult> GetAllPreguntas()
        {
            _logger.LogTrace("Iniciando metodo PreguntasController.GetAllPreguntas...");
            try
            {
                var uResult = await _preguntasQuerie.GetAllPreguntas();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PreguntasController.GetAllPreguntas");
                throw;
            }
        }

        [HttpGet("GetPreguntasById")]
        public async Task<IActionResult> GetPreguntasById(int id)
        {
            _logger.LogTrace("Iniciando metodo PreguntasController.GetPreguntasById...");
            try
            {
                var uResult = await _preguntasQuerie.GetPreguntasById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PreguntasController.GetPreguntasById");
                throw;
            }
        }

        [HttpPost("Post_Create_Preguntas")]
        public async Task<IActionResult> CreatePreguntas([FromBody] PreguntasDTOs preguntasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando PreguntasController.CreatePreguntas...");
                var respuesta = await _preguntasQuerie.AddPreguntas(preguntasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PreguntasController.CreatePreguntas...");
                throw;
            }
        }

        [HttpPost("Post_Update_Preguntas")]
        public async Task<IActionResult> UpdatePreguntas(int id, [FromBody] PreguntasDTOs preguntasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando PreguntasController.UpdatePreguntas...");
                var respuesta = await _preguntasQuerie.UpdatePreguntas(id, preguntasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PreguntasController.UpdatePreguntas...");
                throw;
            }
        }
    }
}
