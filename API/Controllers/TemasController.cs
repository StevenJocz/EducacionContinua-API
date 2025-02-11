using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Temas")]
    [ApiController]
    public class TemasController : ControllerBase
    {
        private readonly ITemasQuerie _temasQuerie;
        private readonly ILogger<ITemasQuerie> _logger;

        public TemasController(ITemasQuerie temasQuerie, ILogger<ITemasQuerie> logger)
        {
            _temasQuerie = temasQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllTemas")]
        public async Task<IActionResult> GetAllTemas()
        {
            _logger.LogTrace("Iniciando metodo TemasController.GetAllTemas...");
            try
            {
                var uResult = await _temasQuerie.GetAllTemas();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TemasController.GetAllTemas");
                throw;
            }
        }

        [HttpGet("GetTemasById")]
        public async Task<IActionResult> GetTemasById(int id)
        {
            _logger.LogTrace("Iniciando metodo TemasController.GetTemasById...");
            try
            {
                var uResult = await _temasQuerie.GetTemasById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TemasController.GetTemasById");
                throw;
            }
        }

        [HttpPost("Post_Create_Temas")]
        public async Task<IActionResult> CreateTemas([FromBody] TemasDTOs temasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TemasController.CreateTemas...");
                var respuesta = await _temasQuerie.AddTemas(temasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TemasController.CreateTemas...");
                throw;
            }
        }

        [HttpPost("Post_Update_Temas")]
        public async Task<IActionResult> UpdateTemas(int id, [FromBody] TemasDTOs temasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TemasController.UpdateTemas...");
                var respuesta = await _temasQuerie.UpdateTemas(id, temasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TemasController.UpdateTemas...");
                throw;
            }
        }
    }
}
