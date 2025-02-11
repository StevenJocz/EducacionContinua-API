using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/TitulosProfesor")]
    [ApiController]
    public class TitulosProfesorController : ControllerBase
    {
        private readonly ITitulosProfesorQuerie _titulosProfesorQuerie;
        private readonly ILogger<ITitulosProfesorQuerie> _logger;

        public TitulosProfesorController(ITitulosProfesorQuerie titulosProfesorQuerie, ILogger<ITitulosProfesorQuerie> logger)
        {
            _titulosProfesorQuerie = titulosProfesorQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllTitulosProfesor")]
        public async Task<IActionResult> GetAllTitulosProfesor()
        {
            _logger.LogTrace("Iniciando metodo TitulosProfesorController.GetAllTitulosProfesor...");
            try
            {
                var uResult = await _titulosProfesorQuerie.GetAllTitulosProfesor();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TitulosProfesorController.GetAllTitulosProfesor");
                throw;
            }
        }

        [HttpGet("GetTitulosProfesorById")]
        public async Task<IActionResult> GetTitulosProfesorById(int id)
        {
            _logger.LogTrace("Iniciando metodo TitulosProfesorController.GetTitulosProfesorById...");
            try
            {
                var uResult = await _titulosProfesorQuerie.GetTitulosProfesorById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TitulosProfesorController.GetTitulosProfesorById");
                throw;
            }
        }

        [HttpPost("Post_Create_TitulosProfesor")]
        public async Task<IActionResult> CreateTitulosProfesor([FromBody] TitulosProfesorDTOs titulosProfesorDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TitulosProfesorController.CreateTitulosProfesor...");
                var respuesta = await _titulosProfesorQuerie.AddTitulosProfesor(titulosProfesorDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TitulosProfesorController.CreateTitulosProfesor...");
                throw;
            }
        }

        [HttpPost("Post_Update_TitulosProfesor")]
        public async Task<IActionResult> UpdateTitulosProfesor(int id, [FromBody] TitulosProfesorDTOs titulosProfesorDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TitulosProfesorController.UpdateTitulosProfesor...");
                var respuesta = await _titulosProfesorQuerie.UpdateTitulosProfesor(id, titulosProfesorDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TitulosProfesorController.UpdateTitulosProfesor...");
                throw;
            }
        }
    }
}
