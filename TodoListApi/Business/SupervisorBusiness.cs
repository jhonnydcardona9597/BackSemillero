using BackSemillero.Business.Interfaces;
using BackSemillero.Data;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackSemillero.Business
{
    public class SupervisorBusiness : ISupervisorBusiness
    {
        private readonly ISupervisorData _supervisorData;

        public SupervisorBusiness(ISupervisorData supervisorData)
        {
            _supervisorData = supervisorData;
        }

        public async Task<SupervisorModelResponse> ObtenerEstadoEnvio(DateTime fechaBuscada)
        {
            // 1) Normalizar fecha a medianoche
            var inicio = fechaBuscada.Date;
            var fin = inicio.AddDays(1);

            // 2) Intentamos obtener envíos en [inicio, fin)
            var enviosHoy = await _supervisorData.ObtenerEnviosPorRango(inicio, fin);
            var primeroHoy = enviosHoy.FirstOrDefault();
            if (primeroHoy != null)
                return primeroHoy;

            // 3) Si no hay, buscamos la fecha anterior más cercana
            var fechasAnteriores = (await _supervisorData.ObtenerFechasEnvioAnteriores(inicio)).ToList();
            if (!fechasAnteriores.Any())
                throw new Exception("No hay registros de envío en el historial", new Exception("404"));

            var fechaAnterior = fechasAnteriores.First(); // ya están ordenadas desc.
            inicio = fechaAnterior;
            fin = inicio.AddDays(1);

            // 4) Obtenemos el envío de esa fecha anterior
            var enviosAnterior = await _supervisorData.ObtenerEnviosPorRango(inicio, fin);
            var primeroAnt = enviosAnterior.FirstOrDefault();
            if (primeroAnt != null)
                return primeroAnt;

            // Por seguridad, nunca deberíamos llegar aquí (porque sabemos que existía esa fecha)
            throw new Exception("Error inesperado al recuperar estado de envío.");
        }
    }
}
