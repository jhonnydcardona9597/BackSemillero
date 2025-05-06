using BackSemillero.Models;

namespace BackSemillero.Business.Interfaces
{
    public interface IProgramaBusiness
    {
        Task<List<ProgramaModel>> ConsultarPrograma();
    }
}
