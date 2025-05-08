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
            return _context.Asignaturas.FromSqlInterpolated($"select * from asignaturas WHERE idprograma = '{IdPrograma}'").ToList();
        }
    }
}
