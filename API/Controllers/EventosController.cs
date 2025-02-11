using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Persistence.Queries;

namespace PlanEstrategico.API.Controllers
{
    [Route("api/Eventos")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IEventosQuerie _eventosQuerie;
        private readonly ILogger<IEventosQuerie> _logger;

        public EventosController(IEventosQuerie eventosQuerie, ILogger<IEventosQuerie> logger)
        {
            _eventosQuerie = eventosQuerie;
            _logger = logger;
        }

        [HttpGet("GetAllEventos")]
        public async Task<IActionResult> GetAllEventos()
        {
            _logger.LogTrace("Iniciando metodo EventosController.GetAllEventos...");
            try
            {
                var uResult = await _eventosQuerie.GetAllEventos();
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosController.GetAllEventos");
                throw;
            }
        }

        [HttpGet("GetEventosById")]
        public async Task<IActionResult> GetEventosById(int id)
        {
            _logger.LogTrace("Iniciando metodo EventosController.GetEventosById...");
            try
            {
                var uResult = await _eventosQuerie.GetEventosById(id);
                return Ok(uResult);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosController.GetEventosById");
                throw;
            }
        }

        [HttpPost("Post_Create_Eventos")]
        public async Task<IActionResult> CreateEventos([FromBody] EventosDTOs eventosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando EventosController.CreateEventos...");
                var respuesta = await _eventosQuerie.AddEventos(eventosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosController.CreateEventos...");
                throw;
            }
        }

        [HttpPost("Post_Update_Eventos")]
        public async Task<IActionResult> UpdateEventos(int id, [FromBody] EventosDTOs eventosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando EventosController.UpdateEventos...");
                var respuesta = await _eventosQuerie.UpdateEventos(id, eventosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EventosController.UpdateEventos...");
                throw;
            }
        }
    }
}
