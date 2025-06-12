using BackSemillero.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Business.Interfaces
{
    public interface ISupervisorBusiness
    {
        Task<IEnumerable<SupervisorDetalleModelResponse>> ObtenerDetalleEncuestas(DateTime? fecha);
        Task<IEnumerable<SupervisorModelResponse>> ObtenerHistorialEncuestas();
    }
}
