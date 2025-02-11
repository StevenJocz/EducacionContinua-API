using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasQuerie _categoriasQuerie;
        private readonly ILogger<ICategoriasQuerie> _logger;

        public CategoriasController(ICategoriasQuerie categoriasQuerie, ILogger<ICategoriasQuerie> logger)
        {
            _categoriasQuerie = categoriasQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllCategorias")]
        public async Task<IActionResult> GetAllCategorias()
        {
            _logger.LogTrace("Iniciando metodo CategoriasController.GetAllCategorias...");
            try
            {
                var uResult = await _categoriasQuerie.GetAllCategorias();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriasController.GetAllCategorias");
                throw;
            }
        }

        [HttpGet("GetCategoriaById")]
        public async Task<IActionResult> GetCategoriaById(int id)
        {
            _logger.LogTrace("Iniciando metodo CategoriasController.GetCategoriaById...");
            try
            {
                var uResult = await _categoriasQuerie.GetCategoriasById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriasController.GetCategoriaById");
                throw;
            }
        }

        [HttpPost("Post_Create_Categoria")]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriasDTOs categoriasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CategoriasController.CreateCategoria...");
                var respuesta = await _categoriasQuerie.AddCategorias(categoriasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriasController.CreateCategoria...");
                throw;
            }
        }

        [HttpPost("Post_Update_Categoria")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriasDTOs categoriasDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CategoriasController.UpdateCategoria...");
                var respuesta = await _categoriasQuerie.UpdateCategorias(id, categoriasDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriasController.UpdateCategoria...");
                throw;
            }
        }
    }
}
