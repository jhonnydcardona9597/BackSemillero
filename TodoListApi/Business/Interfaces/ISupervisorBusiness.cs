using BackSemillero.Models;
using System;
using System.Threading.Tasks;

namespace BackSemillero.Business.Interfaces
{
    public interface ISupervisorBusiness
    {
        /// <summary>
        /// Obtiene el estado de envío del día indicado (o retrocede al anterior más cercano).
        /// </summary>
        Task<SupervisorModelResponse> ObtenerEstadoEnvio(DateTime fechaBuscada);
    }
}
