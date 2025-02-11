using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/ConveniosCurso")]
    [ApiController]
    public class ConveniosCursoController : ControllerBase
    {
        private readonly IConveniosCursoQuerie _conveniosCursoQuerie;
        private readonly ILogger<IConveniosCursoQuerie> _logger;

        public ConveniosCursoController(IConveniosCursoQuerie conveniosCursoQuerie, ILogger<IConveniosCursoQuerie> logger)
        {
            _conveniosCursoQuerie = conveniosCursoQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllConveniosCurso")]
        public async Task<IActionResult> GetAllConveniosCurso()
        {
            _logger.LogTrace("Iniciando metodo ConveniosCursoController.GetAllConveniosCurso...");
            try
            {
                var uResult = await _conveniosCursoQuerie.GetAllConveniosCurso();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosCursoController.GetAllConveniosCurso");
                throw;
            }
        }

        [HttpGet("GetConveniosCursoById")]
        public async Task<IActionResult> GetConveniosCursoById(int id)
        {
            _logger.LogTrace("Iniciando metodo ConveniosCursoController.GetConveniosCursoById...");
            try
            {
                var uResult = await _conveniosCursoQuerie.GetConveniosCursoById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosCursoController.GetConveniosCursoById");
                throw;
            }
        }

        [HttpPost("Post_Create_ConveniosCurso")]
        public async Task<IActionResult> CreateConveniosCurso([FromBody] ConveniosCursoDTOs conveniosCursoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConveniosCursoController.CreateConveniosCurso...");
                var respuesta = await _conveniosCursoQuerie.AddConveniosCurso(conveniosCursoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosCursoController.CreateConveniosCurso...");
                throw;
            }
        }

        [HttpPost("Post_Update_ConveniosCurso")]
        public async Task<IActionResult> UpdateConveniosCurso(int id, [FromBody] ConveniosCursoDTOs conveniosCursoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConveniosCursoController.UpdateConveniosCurso...");
                var respuesta = await _conveniosCursoQuerie.UpdateConveniosCurso(id, conveniosCursoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosCursoController.UpdateConveniosCurso...");
                throw;
            }
        }
    }
}
