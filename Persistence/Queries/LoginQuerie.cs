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
    public interface ILoginQuerie
    {
        Task<List<LoginDTOs>> GetAllLogin();
        Task<LoginDTOs> GetLoginById(int id);
        Task<LoginDTOs> AddLogin(LoginDTOs loginDTOs);
        Task<bool> UpdateLogin(int id, LoginDTOs loginDTOs);
    }

    public class LoginQuerie : ILoginQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<LoginQuerie> _logger;
        private readonly IConfiguration _configuration;

        public LoginQuerie(ILogger<LoginQuerie> logger, IConfiguration configuration)
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

        // Get all Login records
        public async Task<List<LoginDTOs>> GetAllLogin()
        {
            try
            {
                var login = await _context.LoginE
                    .Select(l => LoginDTOs.CreateDTO(l))
                    .ToListAsync();

                return login;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllLogin)}: {ex.Message}");
                return new List<LoginDTOs>();
            }
        }

        // Get a Login by ID
        public async Task<LoginDTOs> GetLoginById(int id)
        {
            try
            {
                var login = await _context.LoginE
                    .Where(l => l.id == id)
                    .Select(l => LoginDTOs.CreateDTO(l))
                    .FirstOrDefaultAsync();

                return login;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetLoginById)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Login
        public async Task<LoginDTOs> AddLogin(LoginDTOs loginDTOs)
        {
            try
            {
                var login = LoginDTOs.CreateE(loginDTOs);

                _context.LoginE.Add(login);
                await _context.SaveChangesAsync();

                return LoginDTOs.CreateDTO(login);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddLogin)}: {ex.Message}");
                return null;
            }
        }

        // Update an existing Login
        public async Task<bool> UpdateLogin(int id, LoginDTOs loginDTOs)
        {
            try
            {
                var login = await _context.LoginE.FindAsync(id);
                if (login == null) return false;

                login.contrasena = loginDTOs.Contrasena;

                _context.LoginE.Update(login);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateLogin)}: {ex.Message}");
                return false;
            }
        }
    }
}
