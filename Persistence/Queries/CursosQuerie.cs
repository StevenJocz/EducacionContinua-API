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
    public interface ICursosQuerie
    {
        Task<List<CursosDTOs>> GetAllCursos();
        Task<CursosDTOs> GetCursosById(int id);
        Task<CursosDTOs> AddCursos(CursosDTOs cursosDTOs);
        Task<bool> UpdateCursos(int id, CursosDTOs cursosDTOs);
    }

    public class CursosQuerie : ICursosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<CursosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public CursosQuerie(ILogger<CursosQuerie> logger, IConfiguration configuration)
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

        // Get all Cursos records
        public async Task<List<CursosDTOs>> GetAllCursos()
        {
            try
            {
                var cursos = await _context.CursosE
                    .Select(c => CursosDTOs.CreateDTO(c))
                    .ToListAsync();

                return cursos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllCursos)}: {ex.Message}");
                return new List<CursosDTOs>();
            }
        }

        // Get a Cursos by ID
        public async Task<CursosDTOs> GetCursosById(int id)
        {
            try
            {
                var curso = await _context.CursosE
                    .Where(c => c.id == id)
                    .Select(c => CursosDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return curso;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCursosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Cursos
        public async Task<CursosDTOs> AddCursos(CursosDTOs cursosDTOs)
        {
            try
            {
                var curso = CursosDTOs.CreateE(cursosDTOs);

                _context.CursosE.Add(curso);
                await _context.SaveChangesAsync();

                return CursosDTOs.CreateDTO(curso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddCursos)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Cursos
        public async Task<bool> UpdateCursos(int id, CursosDTOs cursosDTOs)
        {
            try
            {
                var curso = await _context.CursosE.FindAsync(id);
                if (curso == null) return false;

                curso.nombre = cursosDTOs.Nombre;
                curso.descripcion = cursosDTOs.Descripcion;
                curso.imagen = cursosDTOs.Imagen;
                curso.codigo = cursosDTOs.Codigo;

                _context.CursosE.Update(curso);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateCursos)}: {ex.Message}");
                return false;
            }
        }
    }
}
