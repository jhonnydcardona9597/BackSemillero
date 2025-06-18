using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;

namespace BackSemillero.Business
{
    public class SupervisorBusiness : ISupervisorBusiness
    {
        private readonly IEncuestaData _encuestaData;

        public SupervisorBusiness(IEncuestaData encuestaData)
        {
            _encuestaData = encuestaData;
        }

        public async Task<List<SupervisorDetalleModelResponse>> ObtenerDetalleEncuestas(DateTime? fecha)
        {
            var Encuesta = await _encuestaData.ObtenerEncuestas(fecha?.Date ?? DateTime.Now.Date);

            if (Encuesta.Count() != 0)
            {
                var lista = new List<SupervisorDetalleModelResponse>();
                foreach (var encuesta in Encuesta)
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
            else
            {
                throw new Exception("No existen encuestas", new Exception("404"));
            }
            //var fechaObj = fecha?.Date ?? DateTime.Now.Date;
            //var encuestas = await _supervisorData.ObtenerTodasLasEncuestas();

            //var encuestasFecha = encuestas
            //    .Where(e => e.HoraYFechaDeCreacion.Date == fechaObj)
            //    .ToList();

            //if (!encuestasFecha.Any())
            //{
            //    var fechaAnterior = encuestas
            //        .Where(e => e.HoraYFechaDeCreacion.Date < fechaObj)
            //        .OrderByDescending(e => e.HoraYFechaDeCreacion)
            //        .FirstOrDefault();

            //    if (fechaAnterior != null)
            //    {
            //        encuestasFecha = encuestas
            //            .Where(e => e.HoraYFechaDeCreacion.Date == fechaAnterior.HoraYFechaDeCreacion.Date)
            //            .ToList();
            //    }
            //}

            //var lista = new List<SupervisorDetalleModelResponse>();
            //foreach (var encuesta in encuestasFecha)
            //{
            //    foreach (var detalle in encuesta.Detalle_Encuestas)
            //    {
            //        lista.Add(new SupervisorDetalleModelResponse
            //        {
            //            IdDocente = detalle.IdDocente,
            //            IdPrograma = detalle.IdPrograma,
            //            IdAsignatura = detalle.IdAsignatura,
            //            Jornada = detalle.Jornada,
            //            Categoria = detalle.Categoria,
            //            FindeSemana = detalle.FindeSemana,
            //            Virtual = detalle.Virtual,
            //            ObservacionesMejora = detalle.ObservacionesMejora,
            //            HoraYFechaCreacion = encuesta.HoraYFechaDeCreacion
            //        });
            //    }
            //}

            //return lista;
        }

        public async Task<List<SupervisorModelResponse>> ObtenerHistorialEncuestas()
        {
            var Encuestas = await _encuestaData.ObtenerTodasLasEncuestas();
            var lista = new List<SupervisorModelResponse>();

            if (Encuestas.Count() == 0)
                throw new Exception("No existen encuestas", new Exception("404"));

            foreach (var encuesta in Encuestas)
            {
                lista.Add(new SupervisorModelResponse
                {
                    FechaHoraEnvio = encuesta.HoraYFechaDeCreacion,
                    CantidadCarrerasNotificadas = encuesta.CantidadCarrerasNotificadas,
                    CantidadEncuestasEnviadas = encuesta.CantidadEncuestas,
                    EstadoEnvio = encuesta.EstadoEnvio
                });
            }
            return lista;
            //return Encuestas.Select(e => new SupervisorModelResponse
            //{
            //    FechaHoraEnvio = e.HoraYFechaDeCreacion,
            //    CantidadCarrerasNotificadas = e.CantidadCarrerasNotificadas,
            //    CantidadEncuestasEnviadas = e.CantidadEncuestas,
            //    EstadoEnvio = e.EstadoEnvio
            //});
        }
    }
}
