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
    public interface IGrupoEstudianteQuerie
    {
        Task<List<GrupoEstudianteDTOs>> GetAllGrupoEstudiante();
        Task<GrupoEstudianteDTOs> GetGrupoEstudianteById(int personaId, int grupoId);
        Task<GrupoEstudianteDTOs> AddGrupoEstudiante(GrupoEstudianteDTOs grupoEstudianteDTOs);
        Task<bool> UpdateGrupoEstudiante(int personaId, int grupoId, GrupoEstudianteDTOs grupoEstudianteDTOs);
    }

    public class GrupoEstudianteQuerie : IGrupoEstudianteQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<GrupoEstudianteQuerie> _logger;
        private readonly IConfiguration _configuration;

        public GrupoEstudianteQuerie(ILogger<GrupoEstudianteQuerie> logger, IConfiguration configuration)
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

        // Get all GrupoEstudiante records
        public async Task<List<GrupoEstudianteDTOs>> GetAllGrupoEstudiante()
        {
            try
            {
                var grupoEstudiantes = await _context.GrupoEstudianteE
                    .Select(ge => GrupoEstudianteDTOs.CreateDTO(ge))
                    .ToListAsync();

                return grupoEstudiantes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllGrupoEstudiante)}: {ex.Message}");
                return new List<GrupoEstudianteDTOs>();
            }
        }

        // Get a GrupoEstudiante by personaId and grupoId
        public async Task<GrupoEstudianteDTOs> GetGrupoEstudianteById(int personaId, int grupoId)
        {
            try
            {
                var grupoEstudiante = await _context.GrupoEstudianteE
                    .Where(ge => ge.persona_id == personaId && ge.grupo_id == grupoId)
                    .Select(ge => GrupoEstudianteDTOs.CreateDTO(ge))
                    .FirstOrDefaultAsync();

                return grupoEstudiante;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetGrupoEstudianteById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new GrupoEstudiante
        public async Task<GrupoEstudianteDTOs> AddGrupoEstudiante(GrupoEstudianteDTOs grupoEstudianteDTOs)
        {
            try
            {
                var grupoEstudiante = GrupoEstudianteDTOs.CreateE(grupoEstudianteDTOs);

                _context.GrupoEstudianteE.Add(grupoEstudiante);
                await _context.SaveChangesAsync();

                return GrupoEstudianteDTOs.CreateDTO(grupoEstudiante);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddGrupoEstudiante)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing GrupoEstudiante
        public async Task<bool> UpdateGrupoEstudiante(int personaId, int grupoId, GrupoEstudianteDTOs grupoEstudianteDTOs)
        {
            try
            {
                var grupoEstudiante = await _context.GrupoEstudianteE
                    .Where(ge => ge.persona_id == personaId && ge.grupo_id == grupoId)
                    .FirstOrDefaultAsync();
                if (grupoEstudiante == null) return false;

                grupoEstudiante.persona_id = grupoEstudianteDTOs.PersonaId;
                grupoEstudiante.grupo_id = grupoEstudianteDTOs.GrupoId;

                _context.GrupoEstudianteE.Update(grupoEstudiante);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateGrupoEstudiante)}: {ex.Message}");
                return false;
            }
        }
    }
}
