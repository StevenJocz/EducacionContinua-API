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
using Domain.Entities;

namespace Persistence.Queries
{
    public interface IConveniosQuerie
    {
        Task<List<listConveniosDTOs>> GetAllConvenios();
        Task<ConveniosDTOs> GetConveniosById(int id);
        Task<bool> AddConvenios(ConveniosDTOs conveniosDTOs);
        Task<bool> UpdateConvenios(int id, ConveniosDTOs conveniosDTOs);
    }

    public class ConveniosQuerie : IConveniosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<ConveniosQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ConveniosQuerie(ILogger<ConveniosQuerie> logger, IConfiguration configuration)
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

        // Get all Convenios records
        public async Task<List<listConveniosDTOs>> GetAllConvenios()
        {
            try
            {
                var convenios = await _context.ConveniosE
                    .Select(c => ConveniosDTOs.CreateDTO(c))
                    .ToListAsync();

                var listaConvenios = new List<listConveniosDTOs>();
                
                foreach (var registro in convenios)
                {
                    var cursoNombre = await _context.CursosE
                                            .Where(c => c.id == registro.IdCurso) 
                                            .Select(c => c.nombre)  
                                            .FirstOrDefaultAsync();

                    var lista = new listConveniosDTOs
                    {
                        Id = registro.Id,
                        Nombre = registro.Nombre,
                        Nit = registro.Nit,
                        FechaInicio = registro.FechaInicio.ToString(),
                        FechaFin = registro.FechaFin.ToString(),
                        Curso = cursoNombre,
                        Estado = DateTime.Now > registro.FechaFin ? false : true,
                    };

                    listaConvenios.Add(lista);
                }


                return listaConvenios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllConvenios)}: {ex.Message}");
                return new List<listConveniosDTOs>();
            }
        }

        // Get a Convenios by ID
        public async Task<ConveniosDTOs> GetConveniosById(int id)
        {
            try
            {
                var convenios = await _context.ConveniosE
                    .Where(c => c.id == id)
                    .Select(c => ConveniosDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                return convenios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetConveniosById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Convenios
        public async Task<bool> AddConvenios(ConveniosDTOs conveniosDTOs)
        {
            try
            {
                var convenios = ConveniosDTOs.CreateE(conveniosDTOs);
                _context.ConveniosE.Add(convenios);
                await _context.SaveChangesAsync();

                if (convenios.id > 0 )
                {
                    foreach (var registro in conveniosDTOs.registros)
                    {
                        var newRegistro = new ConveniosPersonasDTOs
                        {
                            ConvenioId = convenios.id,
                            Documento = registro.Documento,
                            TipoDocumento = registro.TipoDocumento,
                            Nombre = registro.Nombre
                        };

                        var convenioE = ConveniosPersonasDTOs.CreateE(newRegistro);
                        await _context.ConveniosPersonasE.AddAsync(convenioE);
                        await _context.SaveChangesAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddConvenios)}: {ex.Message}");
                return false;
            }
        }

        // Update an existing Convenios
        public async Task<bool> UpdateConvenios(int id, ConveniosDTOs conveniosDTOs)
        {
            try
            {
                var convenios = await _context.ConveniosE.FindAsync(id);
                if (convenios == null) return false;

                convenios.nombre = conveniosDTOs.Nombre;

                _context.ConveniosE.Update(convenios);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateConvenios)}: {ex.Message}");
                return false;
            }
        }
    }
}
