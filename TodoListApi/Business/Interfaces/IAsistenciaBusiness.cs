using BackSemillero.Models;
using System.Threading.Tasks;

namespace BackSemillero.Business.Interfaces
{
    public interface IAsistenciaBusiness
    {
        Task<AsistenciaModelResponse> RegistrarAsistencia(AsistenciaModelRequest asistenciaModelRequest);
    }
}
