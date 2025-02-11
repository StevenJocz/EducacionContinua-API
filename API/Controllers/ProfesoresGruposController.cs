using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/ProfesoresGrupos")]
    [ApiController]
    public class ProfesoresGruposController : ControllerBase
    {
        private readonly IProfesoresGruposQuerie _profesoresGruposQuerie;
        private readonly ILogger<IProfesoresGruposQuerie> _logger;

        public ProfesoresGruposController(IProfesoresGruposQuerie profesoresGruposQuerie, ILogger<IProfesoresGruposQuerie> logger)
        {
            _profesoresGruposQuerie = profesoresGruposQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllProfesoresGrupos")]
        public async Task<IActionResult> GetAllProfesoresGrupos()
        {
            _logger.LogTrace("Iniciando metodo ProfesoresGruposController.GetAllProfesoresGrupos...");
            try
            {
                var uResult = await _profesoresGruposQuerie.GetAllProfesoresGrupos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresGruposController.GetAllProfesoresGrupos");
                throw;
            }
        }

        [HttpGet("GetProfesoresGruposById")]
        public async Task<IActionResult> GetProfesoresGruposById(int id)
        {
            _logger.LogTrace("Iniciando metodo ProfesoresGruposController.GetProfesoresGruposById...");
            try
            {
                var uResult = await _profesoresGruposQuerie.GetProfesoresGruposById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresGruposController.GetProfesoresGruposById");
                throw;
            }
        }

        [HttpPost("Post_Create_ProfesoresGrupos")]
        public async Task<IActionResult> CreateProfesoresGrupos([FromBody] ProfesoresGruposDTOs profesoresGruposDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ProfesoresGruposController.CreateProfesoresGrupos...");
                var respuesta = await _profesoresGruposQuerie.AddProfesoresGrupos(profesoresGruposDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresGruposController.CreateProfesoresGrupos...");
                throw;
            }
        }

        [HttpPost("Post_Update_ProfesoresGrupos")]
        public async Task<IActionResult> UpdateProfesoresGrupos(int id, [FromBody] ProfesoresGruposDTOs profesoresGruposDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ProfesoresGruposController.UpdateProfesoresGrupos...");
                var respuesta = await _profesoresGruposQuerie.UpdateProfesoresGrupos(id, profesoresGruposDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresGruposController.UpdateProfesoresGrupos...");
                throw;
            }
        }
    }
}
