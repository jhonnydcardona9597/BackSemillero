using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<SupervisorDetalleModelResponse>> ObtenerDetalleEncuestas(DateTime? fecha)
        {
            var fechaObj = fecha?.Date ?? DateTime.Now.Date;
            var encuestas = await _supervisorData.ObtenerTodasLasEncuestas();

            var encuestasFecha = encuestas
                .Where(e => e.HoraYFechaDeCreacion.Date == fechaObj)
                .ToList();

            if (!encuestasFecha.Any())
            {
                var fechaAnterior = encuestas
                    .Where(e => e.HoraYFechaDeCreacion.Date < fechaObj)
                    .OrderByDescending(e => e.HoraYFechaDeCreacion)
                    .FirstOrDefault();

                if (fechaAnterior != null)
                {
                    encuestasFecha = encuestas
                        .Where(e => e.HoraYFechaDeCreacion.Date == fechaAnterior.HoraYFechaDeCreacion.Date)
                        .ToList();
                }
            }

            var lista = new List<SupervisorDetalleModelResponse>();
            foreach (var encuesta in encuestasFecha)
            {
                foreach (var detalle in encuesta.Detalle_Encuestas)
                {
                    lista.Add(new SupervisorDetalleModelResponse
                    {
                        IdDocente = detalle.IdDocente,
                        IdPrograma = detalle.IdPrograma,
                        IdAsignatura = detalle.IdAsignatura,
                        Jornada = detalle.Jornada,
                        Categoria = detalle.Categoria,
                        FindeSemana = detalle.FindeSemana,
                        Virtual = detalle.Virtual,
                        ObservacionesMejora = detalle.ObservacionesMejora,
                        HoraYFechaCreacion = encuesta.HoraYFechaDeCreacion
                    });
                }
            }

            return lista;
        }

        public async Task<IEnumerable<SupervisorModelResponse>> ObtenerHistorialEncuestas()
        {
            var encuestas = await _supervisorData.ObtenerTodasLasEncuestas();

            return encuestas.Select(e => new SupervisorModelResponse
            {
                Id = e.Id,
                FechaHoraEnvio = e.HoraYFechaDeCreacion,
                CantidadCarrerasNotificadas = e.CantidadCarrerasNotificadas,
                CantidadEncuestasEnviadas = e.CantidadEncuestas,
                EstadoEnvio = e.EstadoEnvio
            });
        }
    }
}
