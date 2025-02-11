using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Actividades")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        private readonly IActividadesQuerie _actividadesQuerie;
        private readonly ILogger<IActividadesQuerie> _logger;

        public ActividadesController(IActividadesQuerie actividadesQuerie, ILogger<IActividadesQuerie> logger)
        {
            _actividadesQuerie = actividadesQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllActividades")]
        public async Task<IActionResult> GetAllActividades()
        {
            _logger.LogTrace("Iniciando metodo ActividadesController.GetAllActividades...");
            try
            {
                var uResult = await _actividadesQuerie.GetAllActividades();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ActividadesController.GetAllActividades");
                throw;
            }
        }

        [HttpGet("GetActividadesById")]
        public async Task<IActionResult> GetActividadesById(int id)
        {
            _logger.LogTrace("Iniciando metodo ActividadesController.GetActividadesById...");
            try
            {
                var uResult = await _actividadesQuerie.GetActividadesById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ActividadesController.GetActividadesById");
                throw;
            }
        }

        [HttpPost("Post_Create_Actividad")]
        public async Task<IActionResult> CreateActividad([FromBody] ActividadesDTOs actividadesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ActividadesController.CreateActividad...");
                var respuesta = await _actividadesQuerie.AddActividades(actividadesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ActividadesController.CreateActividad...");
                throw;
            }
        }

        [HttpPost("Post_Update_Actividad")]
        public async Task<IActionResult> UpdateActividad(int id, [FromBody] ActividadesDTOs actividadesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ActividadesController.UpdateActividad...");
                var respuesta = await _actividadesQuerie.UpdateActividades(id, actividadesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ActividadesController.UpdateActividad...");
                throw;
            }
        }
    }
}
