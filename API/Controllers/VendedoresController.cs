using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Vendedores")]
    [ApiController]
    public class VendedoresController : ControllerBase
    {
        private readonly IVendedoresQuerie _vendedoresQuerie;
        private readonly ILogger<IVendedoresQuerie> _logger;

        public VendedoresController(IVendedoresQuerie vendedoresQuerie, ILogger<IVendedoresQuerie> logger)
        {
            _vendedoresQuerie = vendedoresQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllVendedores")]
        public async Task<IActionResult> GetAllVendedores()
        {
            _logger.LogTrace("Iniciando metodo VendedoresController.GetAllVendedores...");
            try
            {
                var uResult = await _vendedoresQuerie.GetAllVendedores();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar VendedoresController.GetAllVendedores");
                throw;
            }
        }

        [HttpGet("GetVendedoresById")]
        public async Task<IActionResult> GetVendedoresById(int id)
        {
            _logger.LogTrace("Iniciando metodo VendedoresController.GetVendedoresById...");
            try
            {
                var uResult = await _vendedoresQuerie.GetVendedoresById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar VendedoresController.GetVendedoresById");
                throw;
            }
        }

        [HttpPost("Post_Create_Vendedores")]
        public async Task<IActionResult> CreateVendedores([FromBody] VendedoresDTOs vendedoresDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando VendedoresController.CreateVendedores...");
                var respuesta = await _vendedoresQuerie.AddVendedores(vendedoresDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar VendedoresController.CreateVendedores...");
                throw;
            }
        }

        [HttpPost("Post_Update_Vendedores")]
        public async Task<IActionResult> UpdateVendedores(int id, [FromBody] VendedoresDTOs vendedoresDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando VendedoresController.UpdateVendedores...");
                var respuesta = await _vendedoresQuerie.UpdateVendedores(id, vendedoresDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar VendedoresController.UpdateVendedores...");
                throw;
            }
        }
    }
}
