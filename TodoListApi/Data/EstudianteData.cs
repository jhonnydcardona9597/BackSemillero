using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListApi.Context;

namespace BackSemillero.Data
{
    public class EstudianteData : IEstudianteData
    {
        private readonly AppDBContext _context;
        public EstudianteData(AppDBContext context) => _context = context;

        public async Task<EstudianteModel> ConsultarEstudianteXCedula(string cedula)
        {
            var result = await _context.EstudianteModel.FromSqlInterpolated($"EXEC Consulta_Estudiante @Cedula_Estudiante = {cedula}").ToListAsync();
            return result.FirstOrDefault();
        }
    }
}
