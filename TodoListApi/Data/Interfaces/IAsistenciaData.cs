using System.Threading.Tasks;
using BackSemillero.Models;

namespace BackSemillero.Data.Interfaces
{
    public interface IAsistenciaData
    {
        Task<AsistenciaResponse> CrearRegistroAsistencia(AsistenciaModelRequest asistenciaModelRequest);
    }
}
