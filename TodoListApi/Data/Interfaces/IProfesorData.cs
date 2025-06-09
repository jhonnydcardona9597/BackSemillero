using BackSemillero.Models;
using BackSemillero.Models.Mongo;

namespace BackSemillero.Data.Interfaces
{
    public interface IProfesorData
    {
        Task<ProfesorModel> ConsultarProfesorXCedula(string Cedula);
        Task<RankingModelMongo> ObtenerDetalleProfesor(string IdEncuesta);
    }
}
