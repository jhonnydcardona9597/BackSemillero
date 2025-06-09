using BackSemillero.Models;

namespace BackSemillero.Business.Interfaces
{
    public interface IProfesorBusiness
    {
        Task<ProfesorModel> ConsultarProfesor(string CedulaProfesor);
        Task<List<ProfesorModelResponse>> ConsultarDetalleProfesor(string CedulaProfesor);
    }
}
