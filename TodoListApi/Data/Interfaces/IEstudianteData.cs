using System.Threading.Tasks;
using BackSemillero.Models;

namespace BackSemillero.Data.Interfaces
{
    public interface IEstudianteData
    {
        Task<EstudianteModel> ConsultarEstudianteXCedula(string cedula);
    }
}
