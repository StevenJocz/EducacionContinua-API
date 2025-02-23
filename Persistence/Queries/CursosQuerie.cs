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
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using Domain.Utilidades;
using static System.Net.Mime.MediaTypeNames;

namespace Persistence.Queries
{
    public interface ICursosQuerie
    {
        Task<List<CursoAdminDTOs>> GetAllCursosAdmin();
        Task<CursoIdAdminDTOs> GetCursosByIdAdmin(int id);
        Task<bool> AddCursos(AddCursoDTOs addCursoDTOs);
        Task<bool> UpdateCursos(int id, CursosDTOs cursosDTOs);
    }

    public class CursosQuerie : ICursosQuerie, IDisposable
    {
        private readonly DBContext _context = null;
        private readonly ILogger<CursosQuerie> _logger;
        private readonly IConfiguration _configuration; 
        private readonly IImagenes _imagen;
        private readonly IDependenciasQuerie _dependenciasQueri;
        private readonly ICategoriasQuerie _categoriasQuerie;

        public CursosQuerie(ILogger<CursosQuerie> logger, IConfiguration configuration, IImagenes imagen, IDependenciasQuerie dependenciasQueri, ICategoriasQuerie categoriasQuerie)
        {
            _logger = logger;
            _configuration = configuration;
            _imagen = imagen;
            _dependenciasQueri = dependenciasQueri;
            _categoriasQuerie = categoriasQuerie;
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
        public async Task<List<CursoAdminDTOs>> GetAllCursosAdmin()
        {
            try
            {
                var cursosE = await _context.CursosE
                    .Select(c => CursosDTOs.CreateDTO(c))
                    .ToListAsync();

                var listaCursos =  new List<CursoAdminDTOs>();

                if (cursosE.Count > 0)
                {
                    foreach (var curso in cursosE)
                    {
                        var dependencia = await _dependenciasQueri.GetDependenciasById(curso.DependenciaId);
                        var CursoCategoria = await _context.CursoCategoriaE.Where(x => x.curso_id == curso.Id).ToListAsync();

                        var listaCategorias = new List<CategoriasDTOs>();

                        foreach (var itemCategoria in CursoCategoria)
                        {
                            var categoria = await _categoriasQuerie.GetCategoriasById(itemCategoria.categoria_id);

                            var listaC = new CategoriasDTOs
                            {
                                Id = categoria.Id,
                                Nombre = categoria.Nombre
                            };

                            listaCategorias.Add(listaC);
                        }

                        var lista = new CursoAdminDTOs
                        {
                            Id = curso.Id,
                            Codigo = curso.Codigo,
                            Titulo = curso.Nombre,
                            Imagen = curso.Imagen,
                            Estado = "No activo",
                            CursoCategorias = listaCategorias,
                            Dependencia = dependencia.Nombre,
                            Temario = false,
                            Recursos = false,
                            Grupos = false,
                            Cupones = false,
                        };

                        listaCursos.Add(lista);
                    }
                }

                return listaCursos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllCursosAdmin)}: {ex.Message}");
                return new List<CursoAdminDTOs>();
            }
        }

        // Get a Cursos by ID
        public async Task<CursoIdAdminDTOs> GetCursosByIdAdmin(int id)
        {
            try
            {
                var cursoId = await _context.CursosE
                    .Where(c => c.id == id)
                    .Select(c => CursosDTOs.CreateDTO(c))
                    .FirstOrDefaultAsync();

                var curso = new CursoIdAdminDTOs();
                if (cursoId != null)
                {
                  
                    var CursoCategoria = await _context.CursoCategoriaE.Where(x => x.curso_id == cursoId.Id).ToListAsync();

                    var listaCategorias = new List<CategoriasDTOs>();

                    foreach (var itemCategoria in CursoCategoria)
                    {
                        var categoria = await _categoriasQuerie.GetCategoriasById(itemCategoria.categoria_id);

                        var listaC = new CategoriasDTOs
                        {
                            Id = categoria.Id,
                            Nombre = categoria.Nombre
                        };

                        listaCategorias.Add(listaC);
                    }

                    var aprendera = await _context.CursoAprendizajeE.Where(x => x.curso_id == cursoId.Id).ToListAsync();

                    var listaAprendera = new List<CursoAprendizajeDTOs>();

                    foreach (var itemAprendera in aprendera)
                    {
                        var listaA = CursoAprendizajeDTOs.CreateDTO(itemAprendera);

                        listaAprendera.Add(listaA);
                    }

                    curso = new CursoIdAdminDTOs
                    {
                        Id = cursoId.Id,
                        Titulo = cursoId.Nombre,
                        Descripcion = cursoId.Descripcion,
                        Imagen = cursoId.Imagen,
                        Dirigido = cursoId.ParaQuien,
                        Dependencia = cursoId.DependenciaId,
                        Categorias = listaCategorias,
                        Aprendera = listaAprendera.Select(x => x.Descripcion).ToList(),
                    };
                }

                return curso;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCursosByIdAdmin)}: {ex.Message}");
                return null;
            }
        }

        // Add a new Cursos
        public async Task<bool> AddCursos(AddCursoDTOs addCursoDTOs)
        {
            try
            {
                var rutaPhoto = "";
                if (addCursoDTOs.Imagen != null)
                {
                    string rutaImagen = "wwwroot/Imagenes/ImagenesCursos";
                    rutaPhoto = await _imagen.GuardarImagen(addCursoDTOs.Imagen, rutaImagen);
                }

                var random = new Random();
                var curso = new CursosDTOs
                {
                    Id = addCursoDTOs.Id,
                    DependenciaId = addCursoDTOs.Dependencia,
                    Nombre = addCursoDTOs.Titulo,
                    Descripcion = addCursoDTOs.Descripcion,
                    Imagen = rutaPhoto,
                    Codigo = random.Next(1000, 100000).ToString(),
                    ParaQuien = addCursoDTOs.Dirigido
                };

                var cursoE = CursosDTOs.CreateE(curso);
                _context.CursosE.Add(cursoE);
                await _context.SaveChangesAsync();

                if (cursoE.id > 0)
                {
                    foreach (var categoria in addCursoDTOs.Categoria)
                    {
                        var cursoCategoria = new CursoCategoriaDTOs
                        {
                            CursoId = cursoE.id,
                            CategoriaId = categoria.Id
                        };

                        var cursoCategoriaE = CursoCategoriaDTOs.CreateE(cursoCategoria);
                        await _context.CursoCategoriaE.AddAsync(cursoCategoriaE);
                        await _context.SaveChangesAsync();
                    }

                    foreach (var aprendizaje in addCursoDTOs.Aprendera)
                    {
                        var aprendera = new CursoAprendizajeDTOs
                        {
                            CursoId = cursoE.id,
                            Descripcion = aprendizaje
                        };

                        var aprenderaE = CursoAprendizajeDTOs.CreateE(aprendera);
                        await _context.CursoAprendizajeE.AddAsync(aprenderaE);
                        await _context.SaveChangesAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddCursos)}: {ex.Message}");
                return false;
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
