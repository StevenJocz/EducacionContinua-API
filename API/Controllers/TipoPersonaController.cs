using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Persistence.Queries;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/TipoPersona")]
    [ApiController]
    public class TipoPersonaController : ControllerBase
    {
        private readonly ITipoPersonaQuerie _tipoPersonaQuerie;
        private readonly ILogger<ITipoPersonaQuerie> _logger;

        public TipoPersonaController(ITipoPersonaQuerie tipoPersonaQuerie, ILogger<ITipoPersonaQuerie> logger)
        {
            _tipoPersonaQuerie = tipoPersonaQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllTipoPersona")]
        public async Task<IActionResult> GetAllTipoPersona()
        {
            _logger.LogTrace("Iniciando metodo TipoPersonaController.GetAllTipoPersona...");
            try
            {
                var uResult = await _tipoPersonaQuerie.GetAllTipoPersona();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TipoPersonaController.GetAllTipoPersona");
                throw;
            }
        }

        [HttpGet("GetTipoPersonaById")]
        public async Task<IActionResult> GetTipoPersonaById(int id)
        {
            _logger.LogTrace("Iniciando metodo TipoPersonaController.GetTipoPersonaById...");
            try
            {
                var uResult = await _tipoPersonaQuerie.GetTipoPersonaById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TipoPersonaController.GetTipoPersonaById");
                throw;
            }
        }

        [HttpPost("Post_Create_TipoPersona")]
        public async Task<IActionResult> CreateTipoPersona([FromBody] TipoPersonaDTOs tipoPersonaDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TipoPersonaController.CreateTipoPersona...");
                var respuesta = await _tipoPersonaQuerie.AddTipoPersona(tipoPersonaDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TipoPersonaController.CreateTipoPersona...");
                throw;
            }
        }

        [HttpPost("Post_Update_TipoPersona")]
        public async Task<IActionResult> UpdateTipoPersona(int id, [FromBody] TipoPersonaDTOs tipoPersonaDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TipoPersonaController.UpdateTipoPersona...");
                var respuesta = await _tipoPersonaQuerie.UpdateTipoPersona(id, tipoPersonaDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TipoPersonaController.UpdateTipoPersona...");
                throw;
            }
        }
    }
}
