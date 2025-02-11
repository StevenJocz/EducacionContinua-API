using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/GruposAnuncios")]
    [ApiController]
    public class GruposAnunciosController : ControllerBase
    {
        private readonly IGruposAnunciosQuerie _gruposAnunciosQuerie;
        private readonly ILogger<IGruposAnunciosQuerie> _logger;

        public GruposAnunciosController(IGruposAnunciosQuerie gruposAnunciosQuerie, ILogger<IGruposAnunciosQuerie> logger)
        {
            _gruposAnunciosQuerie = gruposAnunciosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllGruposAnuncios")]
        public async Task<IActionResult> GetAllGruposAnuncios()
        {
            _logger.LogTrace("Iniciando metodo GruposAnunciosController.GetAllGruposAnuncios...");
            try
            {
                var uResult = await _gruposAnunciosQuerie.GetAllGruposAnuncios();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposAnunciosController.GetAllGruposAnuncios");
                throw;
            }
        }

        [HttpGet("GetGruposAnunciosById")]
        public async Task<IActionResult> GetGruposAnunciosById(int id)
        {
            _logger.LogTrace("Iniciando metodo GruposAnunciosController.GetGruposAnunciosById...");
            try
            {
                var uResult = await _gruposAnunciosQuerie.GetGruposAnunciosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposAnunciosController.GetGruposAnunciosById");
                throw;
            }
        }

        [HttpPost("Post_Create_GruposAnuncios")]
        public async Task<IActionResult> CreateGruposAnuncios([FromBody] GruposAnunciosDTOs gruposAnunciosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GruposAnunciosController.CreateGruposAnuncios...");
                var respuesta = await _gruposAnunciosQuerie.AddGruposAnuncios(gruposAnunciosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposAnunciosController.CreateGruposAnuncios...");
                throw;
            }
        }

        [HttpPost("Post_Update_GruposAnuncios")]
        public async Task<IActionResult> UpdateGruposAnuncios(int id, [FromBody] GruposAnunciosDTOs gruposAnunciosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GruposAnunciosController.UpdateGruposAnuncios...");
                var respuesta = await _gruposAnunciosQuerie.UpdateGruposAnuncios(id, gruposAnunciosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GruposAnunciosController.UpdateGruposAnuncios...");
                throw;
            }
        }
    }
}
