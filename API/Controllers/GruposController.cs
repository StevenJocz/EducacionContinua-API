using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Grupos")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        private readonly IGruposQuerie _gruposQuerie;
        private readonly ILogger<IGruposQuerie> _logger;

        public GruposController(IGruposQuerie gruposQuerie, ILogger<IGruposQuerie> logger)
        {
            _gruposQuerie = gruposQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllGrupos")]
        public async Task<IActionResult> GetAllGrupos()
        {
            _logger.LogTrace("Iniciando metodo GruposController.GetAllGrupos...");
            try
            {
                var uResult = await _gruposQuerie.GetAllGrupos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposController.GetAllGrupos");
                throw;
            }
        }

        [HttpGet("GetGruposById")]
        public async Task<IActionResult> GetGruposById(int id)
        {
            _logger.LogTrace("Iniciando metodo GruposController.GetGruposById...");
            try
            {
                var uResult = await _gruposQuerie.GetGruposById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposController.GetGruposById");
                throw;
            }
        }

        [HttpPost("Post_Create_Grupos")]
        public async Task<IActionResult> CreateGrupos([FromBody] GruposDTOs gruposDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GruposController.CreateGrupos...");
                var respuesta = await _gruposQuerie.AddGrupos(gruposDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposController.CreateGrupos...");
                throw;
            }
        }

        [HttpPost("Post_Update_Grupos")]
        public async Task<IActionResult> UpdateGrupos(int id, [FromBody] GruposDTOs gruposDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GruposController.UpdateGrupos...");
                var respuesta = await _gruposQuerie.UpdateGrupos(id, gruposDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposController.UpdateGrupos...");
                throw;
            }
        }
    }
}
