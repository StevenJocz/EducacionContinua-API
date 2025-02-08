using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Commands
{
    public interface IUsuarioCommand
    {

    }

    public class UsuarioCommand: IUsuarioCommand, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<UsuarioCommand> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioCommand(ILogger<UsuarioCommand> logger, IConfiguration configuration)
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
    }
}
