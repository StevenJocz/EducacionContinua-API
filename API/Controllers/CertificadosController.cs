using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Certificados")]
    [ApiController]
    public class CertificadosController : ControllerBase
    {
        private readonly ICertificadosQuerie _certificadosQuerie;
        private readonly ILogger<ICertificadosQuerie> _logger;

        public CertificadosController(ICertificadosQuerie certificadosQuerie, ILogger<ICertificadosQuerie> logger)
        {
            _certificadosQuerie = certificadosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllCertificados")]
        public async Task<IActionResult> GetAllCertificados()
        {
            _logger.LogTrace("Iniciando metodo CertificadosController.GetAllCertificados...");
            try
            {
                var uResult = await _certificadosQuerie.GetAllCertificados();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CertificadosController.GetAllCertificados");
                throw;
            }
        }

        [HttpGet("GetCertificadoById")]
        public async Task<IActionResult> GetCertificadoById(int id)
        {
            _logger.LogTrace("Iniciando metodo CertificadosController.GetCertificadoById...");
            try
            {
                var uResult = await _certificadosQuerie.GetCertificadosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CertificadosController.GetCertificadoById");
                throw;
            }
        }

        [HttpPost("Post_Create_Certificado")]
        public async Task<IActionResult> CreateCertificado([FromBody] CertificadosDTOs certificadosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CertificadosController.CreateCertificado...");
                var respuesta = await _certificadosQuerie.AddCertificados(certificadosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CertificadosController.CreateCertificado...");
                throw;
            }
        }

        [HttpPost("Post_Update_Certificado")]
        public async Task<IActionResult> UpdateCertificado(int id, [FromBody] CertificadosDTOs certificadosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CertificadosController.UpdateCertificado...");
                var respuesta = await _certificadosQuerie.UpdateCertificados(id, certificadosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CertificadosController.UpdateCertificado...");
                throw;
            }
        }
    }
}
