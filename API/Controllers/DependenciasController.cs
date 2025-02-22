using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Dependencias")]
    [ApiController]
    public class DependenciasController : ControllerBase
    {
        private readonly IDependenciasQuerie _dependenciasQuerie;
        private readonly ILogger<IDependenciasQuerie> _logger;

        public DependenciasController(IDependenciasQuerie dependenciasQuerie, ILogger<IDependenciasQuerie> logger)
        {
            _dependenciasQuerie = dependenciasQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllDependencias")]
        public async Task<IActionResult> GetAllDependencias()
        {
            _logger.LogTrace("Iniciando metodo DependenciasController.GetAllDependencias...");
            try
            {
                var uResult = await _dependenciasQuerie.GetAllDependencias();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar DependenciasController.GetAllDependencias");
                throw;
            }
        }

        [HttpGet("GetDependenciasById")]
        public async Task<IActionResult> GetDependenciasById(int id)
        {
            _logger.LogTrace("Iniciando metodo DependenciasController.GetDependenciasById...");
            try
            {
                var uResult = await _dependenciasQuerie.GetDependenciasById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar DependenciasController.GetDependenciasById");
                throw;
            }
        }

        [HttpPost("Post_Create_Dependencias")]
        public async Task<IActionResult> CreateDependencias([FromBody] DependenciasDTOs dependenciasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando DependenciasController.CreateDependencias...");
                var respuesta = await _dependenciasQuerie.AddDependencias(dependenciasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar DependenciasController.CreateDependencias...");
                throw;
            }
        }

        [HttpPut("Put_Update_Dependencias")]
        public async Task<IActionResult> UpdateDependencias([FromBody] DependenciasDTOs dependenciasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando DependenciasController.UpdateDependencias...");
                var respuesta = await _dependenciasQuerie.UpdateDependencias(dependenciasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar DependenciasController.UpdateDependencias...");
                throw;
            }
        }
    }
}
