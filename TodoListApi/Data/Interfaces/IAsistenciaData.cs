using BackSemillero.Models;
using BackSemillero.Models.Mongo;
using System.Threading.Tasks;

namespace BackSemillero.Data.Interfaces
{
    public interface IAsistenciaData
    {
        Task<AsistenciaModelResponse> CrearRegistroAsistencia(AsistenciaModelMongo asistenciaModelMongo);
    }
}
