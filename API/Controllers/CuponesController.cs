using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Cupones")]
    [ApiController]
    public class CuponesController : ControllerBase
    {
        private readonly ICuponesQuerie _cuponesQuerie;
        private readonly ILogger<ICuponesQuerie> _logger;

        public CuponesController(ICuponesQuerie cuponesQuerie, ILogger<ICuponesQuerie> logger)
        {
            _cuponesQuerie = cuponesQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllCupones")]
        public async Task<IActionResult> GetAllCupones()
        {
            _logger.LogTrace("Iniciando metodo CuponesController.GetAllCupones...");
            try
            {
                var uResult = await _cuponesQuerie.GetAllCupones();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CuponesController.GetAllCupones");
                throw;
            }
        }

        [HttpGet("GetCuponesById")]
        public async Task<IActionResult> GetCuponesById(int id)
        {
            _logger.LogTrace("Iniciando metodo CuponesController.GetCuponesById...");
            try
            {
                var uResult = await _cuponesQuerie.GetCuponesById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CuponesController.GetCuponesById");
                throw;
            }
        }

        [HttpPost("Post_Create_Cupones")]
        public async Task<IActionResult> CreateCupones([FromBody] CuponesDTOs cuponesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CuponesController.CreateCupones...");
                var respuesta = await _cuponesQuerie.AddCupones(cuponesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CuponesController.CreateCupones...");
                throw;
            }
        }

        [HttpPost("Post_Update_Cupones")]
        public async Task<IActionResult> UpdateCupones(int id, [FromBody] CuponesDTOs cuponesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CuponesController.UpdateCupones...");
                var respuesta = await _cuponesQuerie.UpdateCupones(id, cuponesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CuponesController.UpdateCupones...");
                throw;
            }
        }
    }
}
