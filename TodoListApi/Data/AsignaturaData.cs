using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Context;

namespace BackSemillero.Data
{
    public class AsignaturaData : IAsignaturaData
    {
        private readonly AppDBContext _context;
        public AsignaturaData(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<AsignaturaModelResponse>> ConsultaAsignaturasXPrograma(int IdPrograma)
        {
            return await _context.Asignaturas.FromSqlInterpolated($"exec Consulta_Asignatura @id_Programa = {IdPrograma}").ToListAsync();
        }
    }
}
