using BackSemillero.Models;
using System;
using System.Threading.Tasks;

namespace BackSemillero.Business.Interfaces
{
    public interface ISupervisorBusiness
    {
        Task<SupervisorModelResponse> ObtenerEstadoEnvio(DateTime fechaBuscada);
    }
}
