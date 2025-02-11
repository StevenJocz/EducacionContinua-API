using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Cursos")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursosQuerie _cursosQuerie;
        private readonly ILogger<ICursosQuerie> _logger;

        public CursosController(ICursosQuerie cursosQuerie, ILogger<ICursosQuerie> logger)
        {
            _cursosQuerie = cursosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllCursos")]
        public async Task<IActionResult> GetAllCursos()
        {
            _logger.LogTrace("Iniciando metodo CursosController.GetAllCursos...");
            try
            {
                var uResult = await _cursosQuerie.GetAllCursos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CursosController.GetAllCursos");
                throw;
            }
        }

        [HttpGet("GetCursosById")]
        public async Task<IActionResult> GetCursosById(int id)
        {
            _logger.LogTrace("Iniciando metodo CursosController.GetCursosById...");
            try
            {
                var uResult = await _cursosQuerie.GetCursosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CursosController.GetCursosById");
                throw;
            }
        }

        [HttpPost("Post_Create_Cursos")]
        public async Task<IActionResult> CreateCursos([FromBody] CursosDTOs cursosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CursosController.CreateCursos...");
                var respuesta = await _cursosQuerie.AddCursos(cursosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CursosController.CreateCursos...");
                throw;
            }
        }

        [HttpPost("Post_Update_Cursos")]
        public async Task<IActionResult> UpdateCursos(int id, [FromBody] CursosDTOs cursosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CursosController.UpdateCursos...");
                var respuesta = await _cursosQuerie.UpdateCursos(id, cursosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CursosController.UpdateCursos...");
                throw;
            }
        }
    }
}
