using BackSemillero.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Data.Interfaces
{
    public interface ISupervisorData
    {
        /// <summary>
        /// Devuelve todos los documentos de 'EncuestasSup' cuya FechaHoraEnvio
        /// esté en [inicio, fin).
        /// </summary>
        Task<IEnumerable<SupervisorModelResponse>> ObtenerEnviosPorRango(DateTime inicio, DateTime fin);

        /// <summary>
        /// Devuelve la lista de fechas (solo parte Date) para las cuales existe
        /// al menos un documento con FechaHoraEnvio &lt; antesDe.
        /// </summary>
        Task<IEnumerable<DateTime>> ObtenerFechasEnvioAnteriores(DateTime antesDe);
    }
}
