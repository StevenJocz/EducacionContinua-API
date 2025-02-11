using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Modulos")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly IModulosQuerie _modulosQuerie;
        private readonly ILogger<IModulosQuerie> _logger;

        public ModulosController(IModulosQuerie modulosQuerie, ILogger<IModulosQuerie> logger)
        {
            _modulosQuerie = modulosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllModulos")]
        public async Task<IActionResult> GetAllModulos()
        {
            _logger.LogTrace("Iniciando metodo ModulosController.GetAllModulos...");
            try
            {
                var uResult = await _modulosQuerie.GetAllModulos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ModulosController.GetAllModulos");
                throw;
            }
        }

        [HttpGet("GetModulosById")]
        public async Task<IActionResult> GetModulosById(int id)
        {
            _logger.LogTrace("Iniciando metodo ModulosController.GetModulosById...");
            try
            {
                var uResult = await _modulosQuerie.GetModulosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ModulosController.GetModulosById");
                throw;
            }
        }

        [HttpPost("Post_Create_Modulos")]
        public async Task<IActionResult> CreateModulos([FromBody] ModulosDTOs modulosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ModulosController.CreateModulos...");
                var respuesta = await _modulosQuerie.AddModulos(modulosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ModulosController.CreateModulos...");
                throw;
            }
        }

        [HttpPost("Post_Update_Modulos")]
        public async Task<IActionResult> UpdateModulos(int id, [FromBody] ModulosDTOs modulosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ModulosController.UpdateModulos...");
                var respuesta = await _modulosQuerie.UpdateModulos(id, modulosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ModulosController.UpdateModulos...");
                throw;
            }
        }
    }
}
