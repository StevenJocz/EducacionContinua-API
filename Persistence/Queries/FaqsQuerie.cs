using Domain.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Queries
{
    public interface IFaqsQuerie
    {
        Task<List<FaqsDTOs>> GetAllFaqs();
        Task<FaqsDTOs> GetFaqById(int id);
        Task<FaqsDTOs> AddFaq(FaqsDTOs faqsDTO);
        Task<bool> UpdateFaq(FaqsDTOs faqsDTO);
    }

    public class FaqsQuerie : IFaqsQuerie, IDisposable
    {
        private readonly DBContext _context;
        private readonly ILogger<FaqsQuerie> _logger;
        private readonly IConfiguration _configuration;
        private bool disposed = false;

        public FaqsQuerie(ILogger<FaqsQuerie> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new DBContext(connectionString);
        }

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

        public async Task<List<FaqsDTOs>> GetAllFaqs()
        {
            try
            {
                var faqs = await _context.FaqsE
                    .Select(f => FaqsDTOs.CreateDTO(f))
                    .ToListAsync();

                return faqs;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetAllFaqs)}: {ex.Message}");
                return new List<FaqsDTOs>();
            }
        }

        public async Task<FaqsDTOs> GetFaqById(int id)
        {
            try
            {
                var faq = await _context.FaqsE
                    .Where(f => f.id == id)
                    .Select(f => FaqsDTOs.CreateDTO(f))
                    .FirstOrDefaultAsync();

                return faq ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetFaqById)}: {ex.Message}");
                return null;
            }
        }

        public async Task<FaqsDTOs> AddFaq(FaqsDTOs faqsDTO)
        {
            try
            {
                var faq = FaqsDTOs.CreateEntity(faqsDTO);

                _context.FaqsE.Add(faq);
                await _context.SaveChangesAsync();

                return FaqsDTOs.CreateDTO(faq);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddFaq)}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateFaq(FaqsDTOs faqsDTO)
        {
            try
            {
                var faq = await _context.FaqsE.FindAsync(faqsDTO.Id);
                if (faq == null) return false;

                faq.pregunta = faqsDTO.Pregunta;
                faq.respuesta = faqsDTO.Respuesta;

                _context.FaqsE.Update(faq);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateFaq)}: {ex.Message}");
                return false;
            }
        }
    }
}
