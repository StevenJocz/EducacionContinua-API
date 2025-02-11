using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;
using Domain.Entities;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/GrupoEstudiante")]
    [ApiController]
    public class GrupoEstudianteController : ControllerBase
    {
        private readonly IGrupoEstudianteQuerie _grupoEstudianteQuerie;
        private readonly ILogger<IGrupoEstudianteQuerie> _logger;

        public GrupoEstudianteController(IGrupoEstudianteQuerie grupoEstudianteQuerie, ILogger<IGrupoEstudianteQuerie> logger)
        {
            _grupoEstudianteQuerie = grupoEstudianteQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllGrupoEstudiante")]
        public async Task<IActionResult> GetAllGrupoEstudiante()
        {
            _logger.LogTrace("Iniciando metodo GrupoEstudianteController.GetAllGrupoEstudiante...");
            try
            {
                var uResult = await _grupoEstudianteQuerie.GetAllGrupoEstudiante();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoEstudianteController.GetAllGrupoEstudiante");
                throw;
            }
        }

        [HttpGet("GetGrupoEstudianteById")]
        public async Task<IActionResult> GetGrupoEstudianteById(int personaId, int grupoId)
        {
            _logger.LogTrace("Iniciando metodo GrupoEstudianteController.GetGrupoEstudianteById...");
            try
            {
                var uResult = await _grupoEstudianteQuerie.GetGrupoEstudianteById(personaId, grupoId);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoEstudianteController.GetGrupoEstudianteById");
                throw;
            }
        }

        [HttpPost("Post_Create_GrupoEstudiante")]
        public async Task<IActionResult> CreateGrupoEstudiante([FromBody] GrupoEstudianteDTOs grupoEstudianteDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GrupoEstudianteController.CreateGrupoEstudiante...");
                var respuesta = await _grupoEstudianteQuerie.AddGrupoEstudiante(grupoEstudianteDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoEstudianteController.CreateGrupoEstudiante...");
                throw;
            }
        }

        [HttpPost("Post_Update_GrupoEstudiante")]
        public async Task<IActionResult> UpdateGrupoEstudiante(int personaId, int grupoId, [FromBody] GrupoEstudianteDTOs grupoEstudianteDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GrupoEstudianteController.UpdateGrupoEstudiante...");
                var respuesta = await _grupoEstudianteQuerie.UpdateGrupoEstudiante(personaId, grupoId, grupoEstudianteDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoEstudianteController.UpdateGrupoEstudiante...");
                throw;
            }
        }
    }
}
