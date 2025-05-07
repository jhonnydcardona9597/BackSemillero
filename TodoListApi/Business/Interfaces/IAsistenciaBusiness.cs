using System.Threading.Tasks;
using BackSemillero.Models;

namespace BackSemillero.Business.Interfaces
{
    public interface IAsistenciaBusiness
    {
        Task<AsistenciaResponse> RegistrarAsistencia(AsistenciaModelRequest asistenciaModelRequest);
    }
}
