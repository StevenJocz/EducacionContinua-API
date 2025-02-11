using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Personas")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonasQuerie _personasQuerie;
        private readonly ILogger<IPersonasQuerie> _logger;

        public PersonasController(IPersonasQuerie personasQuerie, ILogger<IPersonasQuerie> logger)
        {
            _personasQuerie = personasQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllPersonas")]
        public async Task<IActionResult> GetAllPersonas()
        {
            _logger.LogTrace("Iniciando metodo PersonasController.GetAllPersonas...");
            try
            {
                var uResult = await _personasQuerie.GetAllPersonas();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PersonasController.GetAllPersonas");
                throw;
            }
        }

        [HttpGet("GetPersonasById")]
        public async Task<IActionResult> GetPersonasById(int id)
        {
            _logger.LogTrace("Iniciando metodo PersonasController.GetPersonasById...");
            try
            {
                var uResult = await _personasQuerie.GetPersonasById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PersonasController.GetPersonasById");
                throw;
            }
        }

        [HttpPost("Post_Create_Personas")]
        public async Task<IActionResult> CreatePersonas([FromBody] PersonasDTOs personasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando PersonasController.CreatePersonas...");
                var respuesta = await _personasQuerie.AddPersonas(personasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PersonasController.CreatePersonas...");
                throw;
            }
        }

        [HttpPost("Post_Update_Personas")]
        public async Task<IActionResult> UpdatePersonas(int id, [FromBody] PersonasDTOs personasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando PersonasController.UpdatePersonas...");
                var respuesta = await _personasQuerie.UpdatePersonas(id, personasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PersonasController.UpdatePersonas...");
                throw;
            }
        }
    }
}
