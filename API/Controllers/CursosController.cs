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

        [HttpGet("GetAllCursosAdmin")]
        public async Task<IActionResult> GetAllCursosAdmin()
        {
            _logger.LogTrace("Iniciando metodo CursosController.GetAllCursos...");
            try
            {
                var uResult = await _cursosQuerie.GetAllCursosAdmin();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CursosController.GetAllCursos");
                throw;
            }
        }

        [HttpGet("GetCursosByIdAdmin")]
        public async Task<IActionResult> GetCursosByIdAdmin(int id)
        {
            _logger.LogTrace("Iniciando metodo CursosController.GetCursosById...");
            try
            {
                var uResult = await _cursosQuerie.GetCursosByIdAdmin(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CursosController.GetCursosById");
                throw;
            }
        }

        [HttpPost("Post_Create_Cursos")]
        public async Task<IActionResult> CreateCursos([FromBody] AddCursoDTOs addCursoDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CursosController.CreateCursos...");
                var respuesta = await _cursosQuerie.AddCursos(addCursoDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CursosController.CreateCursos...");
                throw;
            }
        }

        [HttpPut("Put_Update_Cursos")]
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
