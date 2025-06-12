using BackSemillero.Models;
using BackSemillero.Models.Mongo;

namespace BackSemillero.Data.Interfaces
{
    public interface IProfesorData
    {
        Task<ProfesorModel> ConsultarProfesorXCedula(string Cedula);
        Task<ClasificacionModelMongo> ObtenerClasificacion(string IdClasificacion);
    }
}
