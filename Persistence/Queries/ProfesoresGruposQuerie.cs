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
    public interface IProfesoresGruposQuerie
    {
        Task<List<ProfesoresGruposDTOs>> GetAllProfesoresGrupos();
        Task<ProfesoresGruposDTOs> GetProfesoresGruposById(int id);
        Task<ProfesoresGruposDTOs> AddProfesoresGrupos(ProfesoresGruposDTOs profesoresGruposDTOs);
        Task<bool> UpdateProfesoresGrupos(int id, ProfesoresGruposDTOs profesoresGruposDTOs);
    }

    public class ProfesoresGruposQuerie : IProfesoresGruposQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ProfesoresGruposQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ProfesoresGruposQuerie(ILogger<ProfesoresGruposQuerie> logger, IConfiguration configuration)
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

        // Get all ProfesoresGrupos records
        public async Task<List<ProfesoresGruposDTOs>> GetAllProfesoresGrupos()
        {
            try
            {
                var profesoresGrupos = await _context.ProfesoresGruposE
                    .Select(p => ProfesoresGruposDTOs.CreateDTO(p))
                    .ToListAsync();

                return profesoresGrupos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllProfesoresGrupos)}: {ex.Message}");
                return new List<ProfesoresGruposDTOs>();
            }
        }

        // Get a ProfesorGrupo by ID
        public async Task<ProfesoresGruposDTOs> GetProfesoresGruposById(int id)
        {
            try
            {
                var profesorGrupo = await _context.ProfesoresGruposE
                    .Where(p => p.grupo_id == id)
                    .Select(p => ProfesoresGruposDTOs.CreateDTO(p))
                    .FirstOrDefaultAsync();

                return profesorGrupo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetProfesoresGruposById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new ProfesorGrupo
        public async Task<ProfesoresGruposDTOs> AddProfesoresGrupos(ProfesoresGruposDTOs profesoresGruposDTOs)
        {
            try
            {
                var profesorGrupo = ProfesoresGruposDTOs.CreateE(profesoresGruposDTOs);

                _context.ProfesoresGruposE.Add(profesorGrupo);
                await _context.SaveChangesAsync();

                return ProfesoresGruposDTOs.CreateDTO(profesorGrupo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddProfesoresGrupos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing ProfesorGrupo
        public async Task<bool> UpdateProfesoresGrupos(int id, ProfesoresGruposDTOs profesoresGruposDTOs)
        {
            try
            {
                var profesorGrupo = await _context.ProfesoresGruposE.FindAsync(id);
                if (profesorGrupo == null) return false;

                profesorGrupo.persona_id = profesoresGruposDTOs.PersonaId;
                profesorGrupo.activo = profesoresGruposDTOs.Activo;

                _context.ProfesoresGruposE.Update(profesorGrupo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateProfesoresGrupos)}: {ex.Message}");
                return false;
            }
        }
    }
}
