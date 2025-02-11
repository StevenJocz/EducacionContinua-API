using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/GrupoComentarios")]
    [ApiController]
    public class GrupoComentariosController : ControllerBase
    {
        private readonly IGrupoComentariosQuerie _grupoComentariosQuerie;
        private readonly ILogger<IGrupoComentariosQuerie> _logger;

        public GrupoComentariosController(IGrupoComentariosQuerie grupoComentariosQuerie, ILogger<IGrupoComentariosQuerie> logger)
        {
            _grupoComentariosQuerie = grupoComentariosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllGrupoComentarios")]
        public async Task<IActionResult> GetAllGrupoComentarios()
        {
            _logger.LogTrace("Iniciando metodo GrupoComentariosController.GetAllGrupoComentarios...");
            try
            {
                var uResult = await _grupoComentariosQuerie.GetAllGrupoComentarios();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoComentariosController.GetAllGrupoComentarios");
                throw;
            }
        }

        [HttpGet("GetGrupoComentariosById")]
        public async Task<IActionResult> GetGrupoComentariosById(int id)
        {
            _logger.LogTrace("Iniciando metodo GrupoComentariosController.GetGrupoComentariosById...");
            try
            {
                var uResult = await _grupoComentariosQuerie.GetGrupoComentariosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoComentariosController.GetGrupoComentariosById");
                throw;
            }
        }

        [HttpPost("Post_Create_GrupoComentarios")]
        public async Task<IActionResult> CreateGrupoComentarios([FromBody] GrupoComentariosDTOs grupoComentariosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GrupoComentariosController.CreateGrupoComentarios...");
                var respuesta = await _grupoComentariosQuerie.AddGrupoComentarios(grupoComentariosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoComentariosController.CreateGrupoComentarios...");
                throw;
            }
        }

        [HttpPost("Post_Update_GrupoComentarios")]
        public async Task<IActionResult> UpdateGrupoComentarios(int id, [FromBody] GrupoComentariosDTOs grupoComentariosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GrupoComentariosController.UpdateGrupoComentarios...");
                var respuesta = await _grupoComentariosQuerie.UpdateGrupoComentarios(id, grupoComentariosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GrupoComentariosController.UpdateGrupoComentarios...");
                throw;
            }
        }
    }
}
