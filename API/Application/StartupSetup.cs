using Persistence.Commands;
using Persistence.Queries;

namespace EducacionContinua.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddStartupSetup(this IServiceCollection service, IConfiguration configuration)
        {
            // Commands Persistance Services
            service.AddTransient<IUsuarioCommand, UsuarioCommand>();

            // Queries Persistance Services
            service.AddTransient<IUsuarioQuerie, UsuarioQuerie>();

            // Utilidades

            // Email

            return service;
        }
    }
}
