using Domain.Utilidades;
using Persistence.Commands;
using Persistence.Queries;

namespace EducacionContinua.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddStartupSetup(this IServiceCollection service, IConfiguration configuration)
        {
            // Queries Persistance Services
            service.AddTransient<IUsuarioQuerie, UsuarioQuerie>();
            service.AddTransient<ITipoPersonaQuerie, TipoPersonaQuerie>();
            service.AddTransient<ITitulosProfesorQuerie, TitulosProfesorQuerie>();
            service.AddTransient<IDependenciasQuerie, DependenciasQuerie>();
            service.AddTransient<ICategoriasQuerie, CategoriasQuerie>();
            service.AddTransient<IPersonasQuerie, PersonasQuerie>();
            service.AddTransient<ICursosQuerie, CursosQuerie>();
            service.AddTransient<IModulosQuerie, ModulosQuerie>();
            service.AddTransient<ICuponesQuerie, CuponesQuerie>();
            service.AddTransient<IGruposQuerie, GruposQuerie>();
            service.AddTransient<ITemasQuerie, TemasQuerie>();
            service.AddTransient<IQuizQuerie, QuizQuerie>();
            service.AddTransient<IPreguntasQuerie, PreguntasQuerie>();
            service.AddTransient<IRespuestasQuerie, RespuestasQuerie>();
            service.AddTransient<IQuizPersonaQuerie, QuizPersonaQuerie>();
            service.AddTransient<IProfesoresGruposQuerie, ProfesoresGruposQuerie>();
            service.AddTransient<IProfesoresDatosQuerie, ProfesoresDatosQuerie>();
            service.AddTransient<ILoginQuerie, LoginQuerie>();
            service.AddTransient<IEventosQuerie, EventosQuerie>();
            service.AddTransient<IActividadesQuerie, ActividadesQuerie>();
            service.AddTransient<IEventosImagesQuerie, EventosImagesQuerie>();
            service.AddTransient<ICertificadosQuerie, CertificadosQuerie>();
            service.AddTransient<IInscripcionesCursosQuerie, InscripcionesCursosQuerie>();
            service.AddTransient<IInscripcionesCursosHistoricoQuerie, InscripcionesCursosHistoricoQuerie>();
            service.AddTransient<IInscripcionesEventosQuerie, InscripcionesEventosQuerie>();
            service.AddTransient<IInscripcionesEventosHistoricoQuerie, InscripcionesEventosHistoricoQuerie>();
            service.AddTransient<IConveniosQuerie, ConveniosQuerie>();
            service.AddTransient<IConveniosCursoQuerie, ConveniosCursoQuerie>();
            service.AddTransient<IConveniosPersonasQuerie, ConveniosPersonasQuerie>();
            service.AddTransient<IVendedoresQuerie, VendedoresQuerie>();
            service.AddTransient<IGruposAnunciosQuerie, GruposAnunciosQuerie>();
            service.AddTransient<IGrupoComentariosQuerie, GrupoComentariosQuerie>();
            service.AddTransient<IGrupoEstudianteQuerie, GrupoEstudianteQuerie>();
            service.AddTransient<IQuizHistoricoQuerie, QuizHistoricoQuerie>();
            service.AddTransient<ITipoDocumentosQuerie, TipoDocumentosQuerie>();
            service.AddTransient<ITipoDireccionesQuerie, TipoDireccionesQuerie>();
            service.AddTransient<IFaqsQuerie, FaqsQuerie>();

            // Utilidades
            service.AddScoped<IImagenes, Imagenes>();

            // Email

            return service;
        }
    }
}
