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
    public interface ICategoriasQuerie
    {
        Task<List<CategoriasDTOs>> GetAllCategorias();
        Task<CategoriasDTOs> GetCategoriasById(int id);
        Task<CategoriasDTOs> AddCategorias(CategoriasDTOs categoriasDTOs);
        Task<bool> UpdateCategorias(CategoriasDTOs categoriasDTOs);
    }

    public class CategoriasQuerie : ICategoriasQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<CategoriasQuerie> _logger;
        private readonly IConfiguration _configuration;

        public CategoriasQuerie(ILogger<CategoriasQuerie> logger, IConfiguration configuration)
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

        // Get all Categorias records
        public async Task<List<CategoriasDTOs>> GetAllCategorias()
        {
            try
            {
                var categorias = await _context.CategoriasE
                    .Select(c => CategoriasDTOs.CreateDTO(c))
                    .ToListAsync();

                return categorias;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllCategorias)}: {ex.Message}");
                return new List<CategoriasDTOs>();
            }
        }

        // Get a Categoria by ID
        public async Task<CategoriasDTOs> GetCategoriasById(int id)
        {
            try
            {
                var categoria = await _context.CategoriasE
                    .Where(c => c.id == id)
                    .Select(c => CategoriasDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return categoria;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCategoriasById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Categoria
        public async Task<CategoriasDTOs> AddCategorias(CategoriasDTOs categoriasDTOs)
        {
            try
            {
                var categoria = CategoriasDTOs.CreateE(categoriasDTOs);

                _context.CategoriasE.Add(categoria);
                await _context.SaveChangesAsync();

                return CategoriasDTOs.CreateDTO(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddCategorias)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Categoria
        public async Task<bool> UpdateCategorias(CategoriasDTOs categoriasDTOs)
        {
            try
            {
                var categoria = await _context.CategoriasE.FindAsync(categoriasDTOs.Id);
                if (categoria == null) return false;

                categoria.nombre = categoriasDTOs.Nombre;

                _context.CategoriasE.Update(categoria);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateCategorias)}: {ex.Message}");
                return false;
            }
        }
    }
}
