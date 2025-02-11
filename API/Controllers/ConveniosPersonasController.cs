using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/ConveniosPersonas")]
    [ApiController]
    public class ConveniosPersonasController : ControllerBase
    {
        private readonly IConveniosPersonasQuerie _conveniosPersonasQuerie;
        private readonly ILogger<IConveniosPersonasQuerie> _logger;

        public ConveniosPersonasController(IConveniosPersonasQuerie conveniosPersonasQuerie, ILogger<IConveniosPersonasQuerie> logger)
        {
            _conveniosPersonasQuerie = conveniosPersonasQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllConveniosPersonas")]
        public async Task<IActionResult> GetAllConveniosPersonas()
        {
            _logger.LogTrace("Iniciando metodo ConveniosPersonasController.GetAllConveniosPersonas...");
            try
            {
                var uResult = await _conveniosPersonasQuerie.GetAllConveniosPersonas();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosPersonasController.GetAllConveniosPersonas");
                throw;
            }
        }

        [HttpGet("GetConveniosPersonasById")]
        public async Task<IActionResult> GetConveniosPersonasById(int id)
        {
            _logger.LogTrace("Iniciando metodo ConveniosPersonasController.GetConveniosPersonasById...");
            try
            {
                var uResult = await _conveniosPersonasQuerie.GetConveniosPersonasById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosPersonasController.GetConveniosPersonasById");
                throw;
            }
        }

        [HttpPost("Post_Create_ConveniosPersonas")]
        public async Task<IActionResult> CreateConveniosPersonas([FromBody] ConveniosPersonasDTOs conveniosPersonasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConveniosPersonasController.CreateConveniosPersonas...");
                var respuesta = await _conveniosPersonasQuerie.AddConveniosPersonas(conveniosPersonasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosPersonasController.CreateConveniosPersonas...");
                throw;
            }
        }

        [HttpPost("Post_Update_ConveniosPersonas")]
        public async Task<IActionResult> UpdateConveniosPersonas(int id, [FromBody] ConveniosPersonasDTOs conveniosPersonasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConveniosPersonasController.UpdateConveniosPersonas...");
                var respuesta = await _conveniosPersonasQuerie.UpdateConveniosPersonas(id, conveniosPersonasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosPersonasController.UpdateConveniosPersonas...");
                throw;
            }
        }
    }
}
