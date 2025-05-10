using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;

namespace TodoListApi.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions <AppDBContext> options) : base(options)
        {

        }

        public DbSet<ProfesorModel> Profesores { get; set; }
        public DbSet<AsignaturaModelResponse> Asignaturas { get; set; }
        public DbSet<EstudianteModel> Estudiantes { get; set; }
        public DbSet<ProgramaModel> Programas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura 'idAsignatura' como la clave primaria para la entidad AsignaturaModelResponse
            modelBuilder.Entity<AsignaturaModelResponse>()
                .HasNoKey();

            modelBuilder.Entity<EstudianteModel>()
                .HasNoKey();

            modelBuilder.Entity<ProfesorModel>()
                .HasNoKey();

            modelBuilder.Entity<ProgramaModel>()
                .HasNoKey();

            // Puedes agregar aquí otras configuraciones de entidades si las tienes
        }

    }
}
