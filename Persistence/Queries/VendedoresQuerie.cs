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
    public interface IVendedoresQuerie
    {
        Task<List<VendedoresDTOs>> GetAllVendedores();
        Task<VendedoresDTOs> GetVendedoresById(int id);
        Task<VendedoresDTOs> AddVendedores(VendedoresDTOs vendedoresDTOs);
        Task<bool> UpdateVendedores(int id, VendedoresDTOs vendedoresDTOs);
    }

    public class VendedoresQuerie : IVendedoresQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<VendedoresQuerie> _logger;
        private readonly IConfiguration _configuration;

        public VendedoresQuerie(ILogger<VendedoresQuerie> logger, IConfiguration configuration)
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

        // Get all Vendedores records
        public async Task<List<VendedoresDTOs>> GetAllVendedores()
        {
            try
            {
                var vendedores = await _context.VendedoresE
                    .Select(v => VendedoresDTOs.CreateDTO(v))
                    .ToListAsync();

                return vendedores;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllVendedores)}: {ex.Message}");
                return new List<VendedoresDTOs>();
            }
        }

        // Get a Vendedor by ID
        public async Task<VendedoresDTOs> GetVendedoresById(int id)
        {
            try
            {
                var vendedor = await _context.VendedoresE
                    .Where(v => v.id == id)
                    .Select(v => VendedoresDTOs.CreateDTO(v))
                    .FirstOrDefaultAsync();

                return vendedor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetVendedoresById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Vendedor
        public async Task<VendedoresDTOs> AddVendedores(VendedoresDTOs vendedoresDTOs)
        {
            try
            {
                var vendedor = VendedoresDTOs.CreateE(vendedoresDTOs);

                _context.VendedoresE.Add(vendedor);
                await _context.SaveChangesAsync();

                return VendedoresDTOs.CreateDTO(vendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddVendedores)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Vendedor
        public async Task<bool> UpdateVendedores(int id, VendedoresDTOs vendedoresDTOs)
        {
            try
            {
                var vendedor = await _context.VendedoresE.FindAsync(id);
                if (vendedor == null) return false;

                vendedor.persona_id = vendedoresDTOs.PersonaId;
                vendedor.aprobado = vendedoresDTOs.Aprobado;
                vendedor.porcentaje = vendedoresDTOs.Porcentaje;

                _context.VendedoresE.Update(vendedor);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateVendedores)}: {ex.Message}");
                return false;
            }
        }
    }
}
