using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Queries;

namespace EducacionContinua.Controllers
{
    [Route("api/TipoDirecciones")]
    [ApiController]
    public class TipoDireccionesController : ControllerBase
    {
        private readonly ITipoDireccionesQuerie _tipoDireccionesQuerie;
        private readonly ILogger<TipoDireccionesController> _logger;

        public TipoDireccionesController(ITipoDireccionesQuerie tipoDireccionesQuerie, ILogger<TipoDireccionesController> logger)
        {
            _tipoDireccionesQuerie = tipoDireccionesQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllTiposDirecciones")]
        public async Task<IActionResult> GetAllTiposDirecciones()
        {
            _logger.LogTrace("Iniciando metodo TipoDireccionesController.GetAllTiposDirecciones...");
            try
            {
                var direcciones = await _tipoDireccionesQuerie.GetAllTiposDirecciones();
                return Ok(direcciones);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en TipoDireccionesController.GetAllTiposDirecciones: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetTipoDireccionById")]
        public async Task<IActionResult> GetTipoDireccionById(int id)
        {
            _logger.LogTrace("Iniciando metodo TipoDireccionesController.GetTipoDireccionById...");
            try
            {
                var direccion = await _tipoDireccionesQuerie.GetTipoDireccionById(id);
                return direccion != null ? Ok(direccion) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en TipoDireccionesController.GetTipoDireccionById: {ex.Message}");
                throw;
            }
        }

        [HttpPost("Post_Create_TipoDireccion")]
        public async Task<IActionResult> CreateTipoDireccion([FromBody] TipoDireccionesDTOs tipoDireccionesDTOs)
        {
            _logger.LogInformation("Iniciando TipoDireccionesController.CreateTipoDireccion...");
            try
            {
                var respuesta = await _tipoDireccionesQuerie.AddTipoDireccion(tipoDireccionesDTOs);
                return respuesta != null ? Ok(respuesta) : BadRequest("No se pudo crear el tipo de dirección");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en TipoDireccionesController.CreateTipoDireccion: {ex.Message}");
                throw;
            }
        }

        [HttpPut("Put_Update_TipoDireccion")]
        public async Task<IActionResult> UpdateTipoDireccion([FromBody] TipoDireccionesDTOs tipoDireccionesDTOs)
        {
            _logger.LogInformation("Iniciando TipoDireccionesController.UpdateTipoDireccion...");
            try
            {
                var resultado = await _tipoDireccionesQuerie.UpdateDireccion(tipoDireccionesDTOs);
                return resultado ? Ok("Actualización exitosa") : NotFound("Tipo de dirección no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en TipoDireccionesController.UpdateTipoDireccion: {ex.Message}");
                throw;
            }
        }
    }

}
