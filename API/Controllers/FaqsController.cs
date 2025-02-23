using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Queries;

namespace EducacionContinua.Controllers
{
    [Route("api/Faqs")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly IFaqsQuerie _faqsQuerie;
        private readonly ILogger<FaqsController> _logger;

        public FaqsController(IFaqsQuerie faqsQuerie, ILogger<FaqsController> logger)
        {
            _faqsQuerie = faqsQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllFaqs")]
        public async Task<IActionResult> GetAllFaqs()
        {
            _logger.LogTrace("Iniciando método FaqsController.GetAllFaqs...");
            try
            {
                var faqs = await _faqsQuerie.GetAllFaqs();
                return Ok(faqs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en FaqsController.GetAllFaqs: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("GetFaqById")]
        public async Task<IActionResult> GetFaqById(int id)
        {
            _logger.LogTrace("Iniciando método FaqsController.GetFaqById...");
            try
            {
                var faq = await _faqsQuerie.GetFaqById(id);
                return faq != null ? Ok(faq) : NotFound("FAQ no encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en FaqsController.GetFaqById: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("Post_Create_Faq")]
        public async Task<IActionResult> CreateFaq([FromBody] FaqsDTOs faqsDTO)
        {
            _logger.LogInformation("Iniciando método FaqsController.CreateFaq...");
            try
            {
                var respuesta = await _faqsQuerie.AddFaq(faqsDTO);
                return respuesta != null ? Ok(respuesta) : BadRequest("No se pudo crear la FAQ");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en FaqsController.CreateFaq: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("Put_Update_Faq")]
        public async Task<IActionResult> UpdateFaq([FromBody] FaqsDTOs faqsDTO)
        {
            _logger.LogInformation("Iniciando método FaqsController.UpdateFaq...");
            try
            {
                var resultado = await _faqsQuerie.UpdateFaq(faqsDTO);
                return resultado ? Ok("Actualización exitosa") : NotFound("FAQ no encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en FaqsController.UpdateFaq: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
