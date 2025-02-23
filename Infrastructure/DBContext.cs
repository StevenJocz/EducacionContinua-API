using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DBContext : DbContext
    {
        private readonly string _connection;

        public DBContext(string connection)
        {
            _connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Usuario
        public virtual DbSet<TipoPersonaE> TipoPersonaE { get; set; }
        public virtual DbSet<TitulosProfesorE> TitulosProfesorE { get; set; }
        public virtual DbSet<DependenciasE> DependenciasE { get; set; }
        public virtual DbSet<CategoriasE> CategoriasE { get; set; }
        public virtual DbSet<PersonasE> PersonasE { get; set; }
        public virtual DbSet<CursosE> CursosE { get; set; }
        public virtual DbSet<CursoCategoriaE> CursoCategoriaE { get; set; }
        public virtual DbSet<CursoAprendizajeE> CursoAprendizajeE { get; set; }
        public virtual DbSet<ModulosE> ModulosE { get; set; }
        public virtual DbSet<CuponesE> CuponesE { get; set; }
        public virtual DbSet<GruposE> GruposE { get; set; }
        public virtual DbSet<TemasE> TemasE { get; set; }
        public virtual DbSet<QuizE> QuizE { get; set; }
        public virtual DbSet<PreguntasE> PreguntasE { get; set; }
        public virtual DbSet<RespuestasE> RespuestasE { get; set; }
        public virtual DbSet<QuizPersonaE> QuizPersonaE { get; set; }
        public virtual DbSet<ProfesoresGruposE> ProfesoresGruposE { get; set; }
        public virtual DbSet<ProfesoresDatosE> ProfesoresDatosE { get; set; }
        public virtual DbSet<LoginE> LoginE { get; set; }
        public virtual DbSet<EventosE> EventosE { get; set; }
        public virtual DbSet<ActividadesE> ActividadesE { get; set; }
        public virtual DbSet<EventosImagesE> EventosImagesE { get; set; }
        public virtual DbSet<CertificadosE> CertificadosE { get; set; }
        public virtual DbSet<InscripcionesCursosE> InscripcionesCursosE { get; set; }
        public virtual DbSet<InscripcionesCursosHistoricoE> InscripcionesCursosHistoricoE { get; set; }
        public virtual DbSet<InscripcionesEventosE> InscripcionesEventosE { get; set; }
        public virtual DbSet<InscripcionesEventosHistoricoE> InscripcionesEventosHistoricoE { get; set; }
        public virtual DbSet<ConveniosE> ConveniosE { get; set; }
        public virtual DbSet<ConveniosCursoE> ConveniosCursoE { get; set; }
        public virtual DbSet<ConveniosPersonasE> ConveniosPersonasE { get; set; }
        public virtual DbSet<VendedoresE> VendedoresE { get; set; }
        public virtual DbSet<GruposAnunciosE> GruposAnunciosE { get; set; }
        public virtual DbSet<GrupoComentariosE> GrupoComentariosE { get; set; }
        public virtual DbSet<GrupoEstudianteE> GrupoEstudianteE { get; set; }
        public virtual DbSet<QuizHistoricoE> QuizHistoricoE { get; set; }
        public virtual DbSet<TipoDocumentosE> TipoDocumentosE { get; set; }
        public virtual DbSet<TipoDireccionesE> TipoDireccionesE { get; set; }
        public virtual DbSet<FaqsE> FaqsE { get; set; }

    }
}
