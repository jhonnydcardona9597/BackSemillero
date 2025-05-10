using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoListApi.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options) { }

        public DbSet<ProfesorModel> Profesores { get; set; }
        public DbSet<AsignaturaModelResponse> Asignaturas { get; set; }
        public DbSet<EstudianteModel> EstudianteModel { get; set; }
        public DbSet<ProgramaModel> Programas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsignaturaModelResponse>().HasNoKey();
            modelBuilder.Entity<EstudianteModel>().HasNoKey();
            modelBuilder.Entity<ProfesorModel>().HasNoKey();
            modelBuilder.Entity<ProgramaModel>().HasNoKey();
        }
    }
}
