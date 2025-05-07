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

        public DbSet<TodoItem> TodoItems { get; set; } 
        public DbSet<ProfesorModel> Profesores { get; set; }
        public DbSet<AsignaturaModelResponse> Asignaturas { get; set; }

    }
}
