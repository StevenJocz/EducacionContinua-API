using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/InscripcionesCursos")]
    [ApiController]
    public class InscripcionesCursosController : ControllerBase
    {
        private readonly IInscripcionesCursosQuerie _inscripcionesCursosQuerie;
        private readonly ILogger<IInscripcionesCursosQuerie> _logger;

        public InscripcionesCursosController(IInscripcionesCursosQuerie inscripcionesCursosQuerie, ILogger<IInscripcionesCursosQuerie> logger)
        {
            _inscripcionesCursosQuerie = inscripcionesCursosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllInscripcionesCursos")]
        public async Task<IActionResult> GetAllInscripcionesCursos()
        {
            _logger.LogTrace("Iniciando metodo InscripcionesCursosController.GetAllInscripcionesCursos...");
            try
            {
                var uResult = await _inscripcionesCursosQuerie.GetAllInscripcionesCursos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosController.GetAllInscripcionesCursos");
                throw;
            }
        }

        [HttpGet("GetInscripcionesCursosById")]
        public async Task<IActionResult> GetInscripcionesCursosById(int id)
        {
            _logger.LogTrace("Iniciando metodo InscripcionesCursosController.GetInscripcionesCursosById...");
            try
            {
                var uResult = await _inscripcionesCursosQuerie.GetInscripcionesCursosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosController.GetInscripcionesCursosById");
                throw;
            }
        }

        [HttpPost("Post_Create_InscripcionesCursos")]
        public async Task<IActionResult> CreateInscripcionesCursos([FromBody] InscripcionesCursosDTOs inscripcionesCursosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesCursosController.CreateInscripcionesCursos...");
                var respuesta = await _inscripcionesCursosQuerie.AddInscripcionesCursos(inscripcionesCursosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosController.CreateInscripcionesCursos...");
                throw;
            }
        }

        [HttpPost("Post_Update_InscripcionesCursos")]
        public async Task<IActionResult> UpdateInscripcionesCursos(int id, [FromBody] InscripcionesCursosDTOs inscripcionesCursosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionesCursosController.UpdateInscripcionesCursos...");
                var respuesta = await _inscripcionesCursosQuerie.UpdateInscripcionesCursos(id, inscripcionesCursosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionesCursosController.UpdateInscripcionesCursos...");
                throw;
            }
        }
    }
}
