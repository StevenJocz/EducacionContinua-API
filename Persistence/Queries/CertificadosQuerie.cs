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
    public interface ICertificadosQuerie
    {
        Task<List<CertificadosDTOs>> GetAllCertificados();
        Task<CertificadosDTOs> GetCertificadosById(int id);
        Task<CertificadosDTOs> AddCertificados(CertificadosDTOs certificadosDTOs);
        Task<bool> UpdateCertificados(int id, CertificadosDTOs certificadosDTOs);
    }

    public class CertificadosQuerie : ICertificadosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<CertificadosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public CertificadosQuerie(ILogger<CertificadosQuerie> logger, IConfiguration configuration)
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

        // Get all Certificados records
        public async Task<List<CertificadosDTOs>> GetAllCertificados()
        {
            try
            {
                var certificados = await _context.CertificadosE
                    .Select(c => CertificadosDTOs.CreateDTO(c))
                    .ToListAsync();

                return certificados;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllCertificados)}: {ex.Message}");
                return new List<CertificadosDTOs>();
            }
        }

        // Get a Certificado by ID
        public async Task<CertificadosDTOs> GetCertificadosById(int id)
        {
            try
            {
                var certificado = await _context.CertificadosE
                    .Where(c => c.id == id)
                    .Select(c => CertificadosDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return certificado;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCertificadosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Certificado
        public async Task<CertificadosDTOs> AddCertificados(CertificadosDTOs certificadosDTOs)
        {
            try
            {
                var certificado = CertificadosDTOs.CreateE(certificadosDTOs);

                _context.CertificadosE.Add(certificado);
                await _context.SaveChangesAsync();

                return CertificadosDTOs.CreateDTO(certificado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddCertificados)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Certificado
        public async Task<bool> UpdateCertificados(int id, CertificadosDTOs certificadosDTOs)
        {
            try
            {
                var certificado = await _context.CertificadosE.FindAsync(id);
                if (certificado == null) return false;

                certificado.precio = certificadosDTOs.Precio;
                certificado.imagen = certificadosDTOs.Imagen;

                _context.CertificadosE.Update(certificado);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateCertificados)}: {ex.Message}");
                return false;
            }
        }
    }
}
