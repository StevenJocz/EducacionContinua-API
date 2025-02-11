using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/InscripcionesEventos")]
    [ApiController]
    public class InscripcionesEventosController : ControllerBase
    {
        private readonly IInscripcionesEventosQuerie _inscripcionesEventosQuerie;
        private readonly ILogger<IInscripcionesEventosQuerie> _logger;

        public InscripcionesEventosController(IInscripcionesEventosQuerie inscripcionesEventosQuerie, ILogger<IInscripcionesEventosQuerie> logger)
        {
            _inscripcionesEventosQuerie = inscripcionesEventosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllInscripcionesEventos")]
        public async Task<IActionResult> GetAllInscripcionesEventos()
        {
            _logger.LogTrace("Iniciando metodo InscripcionesEventosController.GetAllInscripcionesEventos...");
            try
            {
                var uResult = await _inscripcionesEventosQuerie.GetAllInscripcionesEventos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosController.GetAllInscripcionesEventos");
                throw;
            }
        }

        [HttpGet("GetInscripcionesEventosById")]
        public async Task<IActionResult> GetInscripcionesEventosById(int id)
        {
            _logger.LogTrace("Iniciando metodo InscripcionesEventosController.GetInscripcionesEventosById...");
            try
            {
                var uResult = await _inscripcionesEventosQuerie.GetInscripcionesEventosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosController.GetInscripcionesEventosById");
                throw;
            }
        }

        [HttpPost("Post_Create_InscripcionesEventos")]
        public async Task<IActionResult> CreateInscripcionesEventos([FromBody] InscripcionesEventosDTOs inscripcionesEventosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesEventosController.CreateInscripcionesEventos...");
                var respuesta = await _inscripcionesEventosQuerie.AddInscripcionesEventos(inscripcionesEventosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosController.CreateInscripcionesEventos...");
                throw;
            }
        }

        [HttpPost("Post_Update_InscripcionesEventos")]
        public async Task<IActionResult> UpdateInscripcionesEventos(int id, [FromBody] InscripcionesEventosDTOs inscripcionesEventosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesEventosController.UpdateInscripcionesEventos...");
                var respuesta = await _inscripcionesEventosQuerie.UpdateInscripcionesEventos(id, inscripcionesEventosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosController.UpdateInscripcionesEventos...");
                throw;
            }
        }
    }
}
