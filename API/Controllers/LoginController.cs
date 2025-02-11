using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginQuerie _loginQuerie;
        private readonly ILogger<ILoginQuerie> _logger;

        public LoginController(ILoginQuerie loginQuerie, ILogger<ILoginQuerie> logger)
        {
            _loginQuerie = loginQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllLogin")]
        public async Task<IActionResult> GetAllLogin()
        {
            _logger.LogTrace("Iniciando metodo LoginController.GetAllLogin...");
            try
            {
                var uResult = await _loginQuerie.GetAllLogin();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar LoginController.GetAllLogin");
                throw;
            }
        }

        [HttpGet("GetLoginById")]
        public async Task<IActionResult> GetLoginById(int id)
        {
            _logger.LogTrace("Iniciando metodo LoginController.GetLoginById...");
            try
            {
                var uResult = await _loginQuerie.GetLoginById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar LoginController.GetLoginById");
                throw;
            }
        }

        [HttpPost("Post_Create_Login")]
        public async Task<IActionResult> CreateLogin([FromBody] LoginDTOs loginDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando LoginController.CreateLogin...");
                var respuesta = await _loginQuerie.AddLogin(loginDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar LoginController.CreateLogin...");
                throw;
            }
        }

        [HttpPost("Post_Update_Login")]
        public async Task<IActionResult> UpdateLogin(int id, [FromBody] LoginDTOs loginDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando LoginController.UpdateLogin...");
                var respuesta = await _loginQuerie.UpdateLogin(id, loginDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar LoginController.UpdateLogin...");
                throw;
            }
        }
    }
}
