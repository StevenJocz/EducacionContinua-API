using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/InscripcionesEventosHistorico")]
    [ApiController]
    public class InscripcionesEventosHistoricoController : ControllerBase
    {
        private readonly IInscripcionesEventosHistoricoQuerie _inscripcionesEventosHistoricoQuerie;
        private readonly ILogger<IInscripcionesEventosHistoricoQuerie> _logger;

        public InscripcionesEventosHistoricoController(IInscripcionesEventosHistoricoQuerie inscripcionesEventosHistoricoQuerie, ILogger<IInscripcionesEventosHistoricoQuerie> logger)
        {
            _inscripcionesEventosHistoricoQuerie = inscripcionesEventosHistoricoQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllInscripcionesEventosHistorico")]
        public async Task<IActionResult> GetAllInscripcionesEventosHistorico()
        {
            _logger.LogTrace("Iniciando metodo InscripcionesEventosHistoricoController.GetAllInscripcionesEventosHistorico...");
            try
            {
                var uResult = await _inscripcionesEventosHistoricoQuerie.GetAllInscripcionesEventosHistorico();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosHistoricoController.GetAllInscripcionesEventosHistorico");
                throw;
            }
        }

        [HttpGet("GetInscripcionesEventosHistoricoById")]
        public async Task<IActionResult> GetInscripcionesEventosHistoricoById(int id)
        {
            _logger.LogTrace("Iniciando metodo InscripcionesEventosHistoricoController.GetInscripcionesEventosHistoricoById...");
            try
            {
                var uResult = await _inscripcionesEventosHistoricoQuerie.GetInscripcionesEventosHistoricoById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosHistoricoController.GetInscripcionesEventosHistoricoById");
                throw;
            }
        }

        [HttpPost("Post_Create_InscripcionesEventosHistorico")]
        public async Task<IActionResult> CreateInscripcionesEventosHistorico([FromBody] InscripcionesEventosHistoricoDTOs inscripcionesEventosHistoricoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesEventosHistoricoController.CreateInscripcionesEventosHistorico...");
                var respuesta = await _inscripcionesEventosHistoricoQuerie.AddInscripcionesEventosHistorico(inscripcionesEventosHistoricoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosHistoricoController.CreateInscripcionesEventosHistorico...");
                throw;
            }
        }

        [HttpPost("Post_Update_InscripcionesEventosHistorico")]
        public async Task<IActionResult> UpdateInscripcionesEventosHistorico(int id, [FromBody] InscripcionesEventosHistoricoDTOs inscripcionesEventosHistoricoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesEventosHistoricoController.UpdateInscripcionesEventosHistorico...");
                var respuesta = await _inscripcionesEventosHistoricoQuerie.UpdateInscripcionesEventosHistorico(id, inscripcionesEventosHistoricoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesEventosHistoricoController.UpdateInscripcionesEventosHistorico...");
                throw;
            }
        }
    }
}
