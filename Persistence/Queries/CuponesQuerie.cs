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
    public interface ICuponesQuerie
    {
        Task<List<CuponesDTOs>> GetAllCupones();
        Task<CuponesDTOs> GetCuponesById(int id);
        Task<CuponesDTOs> AddCupones(CuponesDTOs cuponesDTOs);
        Task<bool> UpdateCupones(int id, CuponesDTOs cuponesDTOs);
    }

    public class CuponesQuerie : ICuponesQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<CuponesQuerie> _logger;
        private readonly IConfiguration _configuration;

        public CuponesQuerie(ILogger<CuponesQuerie> logger, IConfiguration configuration)
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

        // Get all Cupones records
        public async Task<List<CuponesDTOs>> GetAllCupones()
        {
            try
            {
                var cupones = await _context.CuponesE
                    .Select(c => CuponesDTOs.CreateDTO(c))
                    .ToListAsync();

                return cupones;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllCupones)}: {ex.Message}");
                return new List<CuponesDTOs>();
            }
        }

        // Get a Cupones by ID
        public async Task<CuponesDTOs> GetCuponesById(int id)
        {
            try
            {
                var cupones = await _context.CuponesE
                    .Where(c => c.id == id)
                    .Select(c => CuponesDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return cupones;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCuponesById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Cupones
        public async Task<CuponesDTOs> AddCupones(CuponesDTOs cuponesDTOs)
        {
            try
            {
                var cupones = CuponesDTOs.CreateE(cuponesDTOs);

                _context.CuponesE.Add(cupones);
                await _context.SaveChangesAsync();

                return CuponesDTOs.CreateDTO(cupones);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddCupones)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Cupones
        public async Task<bool> UpdateCupones(int id, CuponesDTOs cuponesDTOs)
        {
            try
            {
                var cupones = await _context.CuponesE.FindAsync(id);
                if (cupones == null) return false;

                cupones.descuento = cuponesDTOs.Descuento;
                cupones.codigo = cuponesDTOs.Codigo;
                cupones.fecha_inicio = cuponesDTOs.FechaInicio;
                cupones.fecha_fin = cuponesDTOs.FechaFin;

                _context.CuponesE.Update(cupones);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateCupones)}: {ex.Message}");
                return false;
            }
        }
    }
}
