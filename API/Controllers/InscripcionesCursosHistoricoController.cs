using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/InscripcionesCursosHistorico")]
    [ApiController]
    public class InscripcionesCursosHistoricoController : ControllerBase
    {
        private readonly IInscripcionesCursosHistoricoQuerie _inscripcionesCursosHistoricoQuerie;
        private readonly ILogger<IInscripcionesCursosHistoricoQuerie> _logger;

        public InscripcionesCursosHistoricoController(IInscripcionesCursosHistoricoQuerie inscripcionesCursosHistoricoQuerie, ILogger<IInscripcionesCursosHistoricoQuerie> logger)
        {
            _inscripcionesCursosHistoricoQuerie = inscripcionesCursosHistoricoQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllInscripcionesCursosHistorico")]
        public async Task<IActionResult> GetAllInscripcionesCursosHistorico()
        {
            _logger.LogTrace("Iniciando metodo InscripcionesCursosHistoricoController.GetAllInscripcionesCursosHistorico...");
            try
            {
                var uResult = await _inscripcionesCursosHistoricoQuerie.GetAllInscripcionesCursosHistorico();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosHistoricoController.GetAllInscripcionesCursosHistorico");
                throw;
            }
        }

        [HttpGet("GetInscripcionesCursosHistoricoById")]
        public async Task<IActionResult> GetInscripcionesCursosHistoricoById(int id)
        {
            _logger.LogTrace("Iniciando metodo InscripcionesCursosHistoricoController.GetInscripcionesCursosHistoricoById...");
            try
            {
                var uResult = await _inscripcionesCursosHistoricoQuerie.GetInscripcionesCursosHistoricoById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosHistoricoController.GetInscripcionesCursosHistoricoById");
                throw;
            }
        }

        [HttpPost("Post_Create_InscripcionesCursosHistorico")]
        public async Task<IActionResult> CreateInscripcionesCursosHistorico([FromBody] InscripcionesCursosHistoricoDTOs inscripcionesCursosHistoricoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesCursosHistoricoController.CreateInscripcionesCursosHistorico...");
                var respuesta = await _inscripcionesCursosHistoricoQuerie.AddInscripcionesCursosHistorico(inscripcionesCursosHistoricoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosHistoricoController.CreateInscripcionesCursosHistorico...");
                throw;
            }
        }

        [HttpPost("Post_Update_InscripcionesCursosHistorico")]
        public async Task<IActionResult> UpdateInscripcionesCursosHistorico(int id, [FromBody] InscripcionesCursosHistoricoDTOs inscripcionesCursosHistoricoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesCursosHistoricoController.UpdateInscripcionesCursosHistorico...");
                var respuesta = await _inscripcionesCursosHistoricoQuerie.UpdateInscripcionesCursosHistorico(id, inscripcionesCursosHistoricoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosHistoricoController.UpdateInscripcionesCursosHistorico...");
                throw;
            }
        }
    }
}
