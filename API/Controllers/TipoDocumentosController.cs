using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Queries;

namespace EducacionContinua.Controllers
{
    [Route("api/TipoDocumentos")]
    [ApiController]
    public class TipoDocumentosController : ControllerBase
    {
        private readonly ITipoDocumentosQuerie _tipoDocumentosQuerie;
        private readonly ILogger<TipoDocumentosController> _logger;

        public TipoDocumentosController(ITipoDocumentosQuerie tipoDocumentosQuerie, ILogger<TipoDocumentosController> logger)
        {
            _tipoDocumentosQuerie = tipoDocumentosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllTiposDocumentos")]
        public async Task<IActionResult> GetAllTiposDocumentos()
        {
            _logger.LogTrace("Iniciando metodo TipoDocumentosController.GetAllTiposDocumentos...");
            try
            {
                var documentos = await _tipoDocumentosQuerie.GetAllTiposDocumentos();
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al iniciar TipoDocumentosController.GetAllTiposDocumentos: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetTipoDocumentoById")]
        public async Task<IActionResult> GetTipoDocumentoById(int id)
        {
            _logger.LogTrace("Iniciando metodo TipoDocumentosController.GetTipoDocumentoById...");
            try
            {
                var documento = await _tipoDocumentosQuerie.GetAddTipoDocumentoById(id);
                return documento != null ? Ok(documento) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al iniciar TipoDocumentosController.GetTipoDocumentoById: {ex.Message}");
                throw;
            }
        }

        [HttpPost("Post_Create_TipoDocumento")]
        public async Task<IActionResult> CreateTipoDocumento([FromBody] TipoDocumentosDTOs tipoDocumentosDTOs)
        {
            _logger.LogInformation("Iniciando TipoDocumentosController.CreateTipoDocumento...");
            try
            {
                var respuesta = await _tipoDocumentosQuerie.AddTipoDocumento(tipoDocumentosDTOs);
                return respuesta != null ? Ok(respuesta) : BadRequest("No se pudo crear el tipo de documento");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al iniciar TipoDocumentosController.CreateTipoDocumento: {ex.Message}");
                throw;
            }
        }

        [HttpPut("Put_Update_TipoDocumento")]
        public async Task<IActionResult> UpdateTipoDocumento([FromBody] TipoDocumentosDTOs tipoDocumentosDTOs)
        {
            _logger.LogInformation("Iniciando TipoDocumentosController.UpdateTipoDocumento...");
            try
            {
                var resultado = await _tipoDocumentosQuerie.UpdateDocumentos(tipoDocumentosDTOs);
                return resultado ? Ok("Actualización exitosa") : NotFound("Documento no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al iniciar TipoDocumentosController.UpdateTipoDocumento: {ex.Message}");
                throw;
            }
        }
    }
}
