using BackSemillero.Models;

namespace BackSemillero.Data.Interfaces
{
    public interface IAsignaturaData
    {
        Task<List<AsignaturaModelResponse>> ConsultaAsignaturasXPrograma(int IdPrograma);
    }
}
