using BackSemillero.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Data.Interfaces
{
    public interface IEstudianteData
    {
        // Ahora devuelve lista en lugar de único modelo
        Task<EstudianteModel> ConsultarEstudianteXCedula(string cedula);
    }
}
