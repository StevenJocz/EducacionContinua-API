using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.DTOs;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Queries
{
    public interface IEventosQuerie
    {
        Task<List<EventosDTOs>> GetAllEventos();
        Task<EventosDTOs> GetEventosById(int id);
        Task<EventosDTOs> AddEventos(EventosDTOs eventosDTOs);
        Task<bool> UpdateEventos(int id, EventosDTOs eventosDTOs);
    }

    public class EventosQuerie : IEventosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<EventosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public EventosQuerie(ILogger<EventosQuerie> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new DBContext(connectionString);
        }

        #region implementacion Disponse
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion

        // Get all Eventos records
        public async Task<List<EventosDTOs>> GetAllEventos()
        {
            try
            {
                var eventos = await _context.EventosE
                    .Select(e => EventosDTOs.CreateDTO(e))
                    .ToListAsync();

                return eventos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllEventos)}: {ex.Message}");
                return new List<EventosDTOs>();
            }
        }

        // Get an Evento by ID
        public async Task<EventosDTOs> GetEventosById(int id)
        {
            try
            {
                var evento = await _context.EventosE
                    .Where(e => e.id == id)
                    .Select(e => EventosDTOs.CreateDTO(e))
                    .FirstOrDefaultAsync();

                return evento;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEventosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Evento
        public async Task<EventosDTOs> AddEventos(EventosDTOs eventosDTOs)
        {
            try
            {
                var evento = EventosDTOs.CreateE(eventosDTOs);

                _context.EventosE.Add(evento);
                await _context.SaveChangesAsync();

                return EventosDTOs.CreateDTO(evento);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddEventos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Evento
        public async Task<bool> UpdateEventos(int id, EventosDTOs eventosDTOs)
        {
            try
            {
                var evento = await _context.EventosE.FindAsync(id);
                if (evento == null) return false;

                evento.titulo = eventosDTOs.Titulo;
                evento.descripcion = eventosDTOs.Descripcion;
                evento.tipo = eventosDTOs.Tipo;
                evento.imagen = eventosDTOs.Imagen;
                evento.video = eventosDTOs.Video;
                evento.fecha_inicio = eventosDTOs.FechaInicio;
                evento.fecha_fin = eventosDTOs.FechaFin;
                evento.precio = eventosDTOs.Precio;
                evento.capacidad = eventosDTOs.Capacidad;
                evento.localizacion = eventosDTOs.Localizacion;

                _context.EventosE.Update(evento);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateEventos)}: {ex.Message}");
                return false;
            }
        }
    }
}
