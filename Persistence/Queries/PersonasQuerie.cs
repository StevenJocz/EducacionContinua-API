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
    public interface IPersonasQuerie
    {
        Task<List<PersonasDTOs>> GetAllPersonas();
        Task<PersonasDTOs> GetPersonasById(int id);
        Task<PersonasDTOs> AddPersonas(PersonasDTOs personasDTOs);
        Task<bool> UpdatePersonas(int id, PersonasDTOs personasDTOs);
    }

    public class PersonasQuerie : IPersonasQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<PersonasQuerie> _logger;
        private readonly IConfiguration _configuration;

        public PersonasQuerie(ILogger<PersonasQuerie> logger, IConfiguration configuration)
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

        // Get all Personas records
        public async Task<List<PersonasDTOs>> GetAllPersonas()
        {
            try
            {
                var personas = await _context.PersonasE
                    .Select(p => PersonasDTOs.CreateDTO(p))
                    .ToListAsync();

                return personas;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllPersonas)}: {ex.Message}");
                return new List<PersonasDTOs>();
            }
        }

        // Get a Persona by ID
        public async Task<PersonasDTOs> GetPersonasById(int id)
        {
            try
            {
                var persona = await _context.PersonasE
                    .Where(p => p.id == id)
                    .Select(p => PersonasDTOs.CreateDTO(p))
                    .FirstOrDefaultAsync();

                return persona;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPersonasById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Persona
        public async Task<PersonasDTOs> AddPersonas(PersonasDTOs personasDTOs)
        {
            try
            {
                var persona = PersonasDTOs.CreateE(personasDTOs);

                _context.PersonasE.Add(persona);
                await _context.SaveChangesAsync();

                return PersonasDTOs.CreateDTO(persona);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddPersonas)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Persona
        public async Task<bool> UpdatePersonas(int id, PersonasDTOs personasDTOs)
        {
            try
            {
                var persona = await _context.PersonasE.FindAsync(id);
                if (persona == null) return false;

                persona.tipo_persona_id = personasDTOs.TipoPersonaId;
                persona.nombres = personasDTOs.Nombres;
                persona.apellidos = personasDTOs.Apellidos;
                persona.tipo_doc = personasDTOs.TipoDoc;
                persona.correo = personasDTOs.Correo;
                persona.genero = personasDTOs.Genero;
                persona.celular = personasDTOs.Celular;
                persona.pais = personasDTOs.Pais;
                persona.departamento = personasDTOs.Departamento;
                persona.ciudad = personasDTOs.Ciudad;
                persona.direccion = personasDTOs.Direccion;

                _context.PersonasE.Update(persona);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdatePersonas)}: {ex.Message}");
                return false;
            }
        }
    }
}
