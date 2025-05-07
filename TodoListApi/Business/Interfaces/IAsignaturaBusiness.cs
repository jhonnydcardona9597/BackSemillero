using BackSemillero.Models;

namespace BackSemillero.Business.Interfaces
{
    public interface IAsignaturaBusiness
    {
        Task<List<AsignaturaModelResponse>> ConsultaAsignaturasXPrograma(int IdPrograma);
    }
}
