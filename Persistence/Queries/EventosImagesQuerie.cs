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
    public interface IEventosImagesQuerie
    {
        Task<List<EventosImagesDTOs>> GetAllEventosImages();
        Task<EventosImagesDTOs> GetEventosImagesById(int eventoId);
        Task<EventosImagesDTOs> AddEventosImages(EventosImagesDTOs eventosImagesDTOs);
        Task<bool> UpdateEventosImages(int eventoId, EventosImagesDTOs eventosImagesDTOs);
    }

    public class EventosImagesQuerie : IEventosImagesQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<EventosImagesQuerie> _logger;
        private readonly IConfiguration _configuration;

        public EventosImagesQuerie(ILogger<EventosImagesQuerie> logger, IConfiguration configuration)
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

        // Get all EventosImages records
        public async Task<List<EventosImagesDTOs>> GetAllEventosImages()
        {
            try
            {
                var eventosImages = await _context.EventosImagesE
                    .Select(ei => EventosImagesDTOs.CreateDTO(ei))
                    .ToListAsync();

                return eventosImages;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllEventosImages)}: {ex.Message}");
                return new List<EventosImagesDTOs>();
            }
        }

        // Get a EventosImages by Evento ID
        public async Task<EventosImagesDTOs> GetEventosImagesById(int eventoId)
        {
            try
            {
                var eventoImage = await _context.EventosImagesE
                    .Where(ei => ei.evento_id == eventoId)
                    .Select(ei => EventosImagesDTOs.CreateDTO(ei))
                    .FirstOrDefaultAsync();

                return eventoImage;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEventosImagesById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new EventosImage
        public async Task<EventosImagesDTOs> AddEventosImages(EventosImagesDTOs eventosImagesDTOs)
        {
            try
            {
                var eventoImage = EventosImagesDTOs.CreateE(eventosImagesDTOs);

                _context.EventosImagesE.Add(eventoImage);
                await _context.SaveChangesAsync();

                return EventosImagesDTOs.CreateDTO(eventoImage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddEventosImages)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing EventosImage
        public async Task<bool> UpdateEventosImages(int eventoId, EventosImagesDTOs eventosImagesDTOs)
        {
            try
            {
                var eventoImage = await _context.EventosImagesE
                    .FirstOrDefaultAsync(ei => ei.evento_id == eventoId);

                if (eventoImage == null) return false;

                eventoImage.imagen = eventosImagesDTOs.Imagen;

                _context.EventosImagesE.Update(eventoImage);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateEventosImages)}: {ex.Message}");
                return false;
            }
        }
    }
}
