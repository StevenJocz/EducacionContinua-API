using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/ProfesoresDatos")]
    [ApiController]
    public class ProfesoresDatosController : ControllerBase
    {
        private readonly IProfesoresDatosQuerie _profesoresDatosQuerie;
        private readonly ILogger<IProfesoresDatosQuerie> _logger;

        public ProfesoresDatosController(IProfesoresDatosQuerie profesoresDatosQuerie, ILogger<IProfesoresDatosQuerie> logger)
        {
            _profesoresDatosQuerie = profesoresDatosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllProfesoresDatos")]
        public async Task<IActionResult> GetAllProfesoresDatos()
        {
            _logger.LogTrace("Iniciando metodo ProfesoresDatosController.GetAllProfesoresDatos...");
            try
            {
                var uResult = await _profesoresDatosQuerie.GetAllProfesoresDatos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresDatosController.GetAllProfesoresDatos");
                throw;
            }
        }

        [HttpGet("GetProfesoresDatosById")]
        public async Task<IActionResult> GetProfesoresDatosById(int id)
        {
            _logger.LogTrace("Iniciando metodo ProfesoresDatosController.GetProfesoresDatosById...");
            try
            {
                var uResult = await _profesoresDatosQuerie.GetProfesoresDatosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresDatosController.GetProfesoresDatosById");
                throw;
            }
        }

        [HttpPost("Post_Create_ProfesoresDatos")]
        public async Task<IActionResult> CreateProfesoresDatos([FromBody] ProfesoresDatosDTOs profesoresDatosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ProfesoresDatosController.CreateProfesoresDatos...");
                var respuesta = await _profesoresDatosQuerie.AddProfesoresDatos(profesoresDatosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresDatosController.CreateProfesoresDatos...");
                throw;
            }
        }

        [HttpPost("Post_Update_ProfesoresDatos")]
        public async Task<IActionResult> UpdateProfesoresDatos(int id, [FromBody] ProfesoresDatosDTOs profesoresDatosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ProfesoresDatosController.UpdateProfesoresDatos...");
                var respuesta = await _profesoresDatosQuerie.UpdateProfesoresDatos(id, profesoresDatosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProfesoresDatosController.UpdateProfesoresDatos...");
                throw;
            }
        }
    }
}
