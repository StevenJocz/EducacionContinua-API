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
    public interface IProfesoresDatosQuerie
    {
        Task<List<ProfesoresDatosDTOs>> GetAllProfesoresDatos();
        Task<ProfesoresDatosDTOs> GetProfesoresDatosById(int id);
        Task<ProfesoresDatosDTOs> AddProfesoresDatos(ProfesoresDatosDTOs profesoresDatosDTOs);
        Task<bool> UpdateProfesoresDatos(int id, ProfesoresDatosDTOs profesoresDatosDTOs);
    }

    public class ProfesoresDatosQuerie : IProfesoresDatosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ProfesoresDatosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ProfesoresDatosQuerie(ILogger<ProfesoresDatosQuerie> logger, IConfiguration configuration)
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

        // Get all ProfesoresDatos records
        public async Task<List<ProfesoresDatosDTOs>> GetAllProfesoresDatos()
        {
            try
            {
                var profesoresDatos = await _context.ProfesoresDatosE
                    .Select(p => ProfesoresDatosDTOs.CreateDTO(p))
                    .ToListAsync();

                return profesoresDatos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllProfesoresDatos)}: {ex.Message}");
                return new List<ProfesoresDatosDTOs>();
            }
        }

        // Get a ProfesorDato by ID
        public async Task<ProfesoresDatosDTOs> GetProfesoresDatosById(int id)
        {
            try
            {
                var profesorDato = await _context.ProfesoresDatosE
                    .Where(p => p.persona_id == id)
                    .Select(p => ProfesoresDatosDTOs.CreateDTO(p))
                    .FirstOrDefaultAsync();

                return profesorDato;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetProfesoresDatosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new ProfesorDato
        public async Task<ProfesoresDatosDTOs> AddProfesoresDatos(ProfesoresDatosDTOs profesoresDatosDTOs)
        {
            try
            {
                var profesorDato = ProfesoresDatosDTOs.CreateE(profesoresDatosDTOs);

                _context.ProfesoresDatosE.Add(profesorDato);
                await _context.SaveChangesAsync();

                return ProfesoresDatosDTOs.CreateDTO(profesorDato);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddProfesoresDatos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing ProfesorDato
        public async Task<bool> UpdateProfesoresDatos(int id, ProfesoresDatosDTOs profesoresDatosDTOs)
        {
            try
            {
                var profesorDato = await _context.ProfesoresDatosE.FindAsync(id);
                if (profesorDato == null) return false;

                profesorDato.titulo_id = profesoresDatosDTOs.TituloId;
                profesorDato.foto = profesoresDatosDTOs.Foto;
                profesorDato.descripcion = profesoresDatosDTOs.Descripcion;

                _context.ProfesoresDatosE.Update(profesorDato);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateProfesoresDatos)}: {ex.Message}");
                return false;
            }
        }
    }
}
