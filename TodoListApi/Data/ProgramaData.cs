using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Context;

namespace BackSemillero.Data
{
    public class ProgramaData : IProgramaData
    {
        private readonly AppDBContext _context;
        public ProgramaData(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramaModel>> ConsultarPrograma()
        {
            return await _context.Programas.FromSqlInterpolated($"select * from programa").ToListAsync();
        }
    }
}