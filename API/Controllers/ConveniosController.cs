using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Convenios")]
    [ApiController]
    public class ConveniosController : ControllerBase
    {
        private readonly IConveniosQuerie _conveniosQuerie;
        private readonly ILogger<IConveniosQuerie> _logger;

        public ConveniosController(IConveniosQuerie conveniosQuerie, ILogger<IConveniosQuerie> logger)
        {
            _conveniosQuerie = conveniosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllConvenios")]
        public async Task<IActionResult> GetAllConvenios()
        {
            _logger.LogTrace("Iniciando metodo ConveniosController.GetAllConvenios...");
            try
            {
                var uResult = await _conveniosQuerie.GetAllConvenios();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosController.GetAllConvenios");
                throw;
            }
        }

        [HttpGet("GetConvenioById")]
        public async Task<IActionResult> GetConvenioById(int id)
        {
            _logger.LogTrace("Iniciando metodo ConveniosController.GetConvenioById...");
            try
            {
                var uResult = await _conveniosQuerie.GetConveniosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosController.GetConvenioById");
                throw;
            }
        }

        [HttpPost("Post_Create_Convenio")]
        public async Task<IActionResult> CreateConvenio([FromBody] ConveniosDTOs conveniosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConveniosController.CreateConvenio...");
                var respuesta = await _conveniosQuerie.AddConvenios(conveniosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosController.CreateConvenio...");
                throw;
            }
        }

        [HttpPost("Post_Update_Convenio")]
        public async Task<IActionResult> UpdateConvenio(int id, [FromBody] ConveniosDTOs conveniosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConveniosController.UpdateConvenio...");
                var respuesta = await _conveniosQuerie.UpdateConvenios(id, conveniosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConveniosController.UpdateConvenio...");
                throw;
            }
        }
    }
}
