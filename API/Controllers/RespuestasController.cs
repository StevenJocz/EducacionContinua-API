using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Respuestas")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly IRespuestasQuerie _respuestasQuerie;
        private readonly ILogger<IRespuestasQuerie> _logger;

        public RespuestasController(IRespuestasQuerie respuestasQuerie, ILogger<IRespuestasQuerie> logger)
        {
            _respuestasQuerie = respuestasQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllRespuestas")]
        public async Task<IActionResult> GetAllRespuestas()
        {
            _logger.LogTrace("Iniciando metodo RespuestasController.GetAllRespuestas...");
            try
            {
                var uResult = await _respuestasQuerie.GetAllRespuestas();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar RespuestasController.GetAllRespuestas");
                throw;
            }
        }

        [HttpGet("GetRespuestasById")]
        public async Task<IActionResult> GetRespuestasById(int id)
        {
            _logger.LogTrace("Iniciando metodo RespuestasController.GetRespuestasById...");
            try
            {
                var uResult = await _respuestasQuerie.GetRespuestasById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar RespuestasController.GetRespuestasById");
                throw;
            }
        }

        [HttpPost("Post_Create_Respuestas")]
        public async Task<IActionResult> CreateRespuestas([FromBody] RespuestasDTOs respuestasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando RespuestasController.CreateRespuestas...");
                var respuesta = await _respuestasQuerie.AddRespuestas(respuestasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar RespuestasController.CreateRespuestas...");
                throw;
            }
        }

        [HttpPost("Post_Update_Respuestas")]
        public async Task<IActionResult> UpdateRespuestas(int id, [FromBody] RespuestasDTOs respuestasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando RespuestasController.UpdateRespuestas...");
                var respuesta = await _respuestasQuerie.UpdateRespuestas(id, respuestasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar RespuestasController.UpdateRespuestas...");
                throw;
            }
        }
    }
}
