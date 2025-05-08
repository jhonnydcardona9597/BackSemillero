using BackSemillero.Models;

namespace BackSemillero.Data.Interfaces
{
    public interface IProgramaData
    {
        Task<List<ProgramaModel>> ConsultarPrograma();
    }
}
