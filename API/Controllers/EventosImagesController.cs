using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/EventosImages")]
    [ApiController]
    public class EventosImagesController : ControllerBase
    {
        private readonly IEventosImagesQuerie _eventosImagesQuerie;
        private readonly ILogger<IEventosImagesQuerie> _logger;

        public EventosImagesController(IEventosImagesQuerie eventosImagesQuerie, ILogger<IEventosImagesQuerie> logger)
        {
            _eventosImagesQuerie = eventosImagesQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllEventosImages")]
        public async Task<IActionResult> GetAllEventosImages()
        {
            _logger.LogTrace("Iniciando metodo EventosImagesController.GetAllEventosImages...");
            try
            {
                var uResult = await _eventosImagesQuerie.GetAllEventosImages();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosImagesController.GetAllEventosImages");
                throw;
            }
        }

        [HttpGet("GetEventosImagesById")]
        public async Task<IActionResult> GetEventosImagesById(int id)
        {
            _logger.LogTrace("Iniciando metodo EventosImagesController.GetEventosImagesById...");
            try
            {
                var uResult = await _eventosImagesQuerie.GetEventosImagesById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosImagesController.GetEventosImagesById");
                throw;
            }
        }

        [HttpPost("Post_Create_EventosImages")]
        public async Task<IActionResult> CreateEventosImages([FromBody] EventosImagesDTOs eventosImagesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando EventosImagesController.CreateEventosImages...");
                var respuesta = await _eventosImagesQuerie.AddEventosImages(eventosImagesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosImagesController.CreateEventosImages...");
                throw;
            }
        }

        [HttpPost("Post_Update_EventosImages")]
        public async Task<IActionResult> UpdateEventosImages(int id, [FromBody] EventosImagesDTOs eventosImagesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando EventosImagesController.UpdateEventosImages...");
                var respuesta = await _eventosImagesQuerie.UpdateEventosImages(id, eventosImagesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosImagesController.UpdateEventosImages...");
                throw;
            }
        }
    }
}
