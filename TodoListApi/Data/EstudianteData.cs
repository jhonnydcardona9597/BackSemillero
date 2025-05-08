using System.Threading.Tasks;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Context;

namespace BackSemillero.Data
{
    public class EstudianteData : IEstudianteData
    {
        private readonly AppDBContext _context;
        public EstudianteData(AppDBContext context)
        {
            _context = context;
        }

        public async Task<EstudianteModel> ConsultarEstudianteXCedula(string cedula)
        {
            return _context.Estudiantes.FromSqlInterpolated($"EXEC Consulta_Estudiante @Cedula_Estudiante = {cedula}").FirstOrDefault();          // devolverá null si NO está activo
        }
    }
}
