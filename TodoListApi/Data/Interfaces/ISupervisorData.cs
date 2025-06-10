using BackSemillero.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Data.Interfaces
{
    public interface ISupervisorData
    {
        Task<IEnumerable<SupervisorModelResponse>> ObtenerEnviosPorRango(DateTime inicio, DateTime fin);
        Task<IEnumerable<DateTime>> ObtenerFechasEnvioAnteriores(DateTime antesDe);
    }
}
