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

        public async Task<List<ObservacionesMejoraModel>> ObservacionMejoraProfesor()
        {
            var EncuestaGeneral = await _encuestaData.ObtenerEncuestas(new DateTime(2025, 6, 6));

            if (EncuestaGeneral.Count() != 0)
            {
                var lista = new List<ObservacionesMejoraModel>();

                foreach (var encuesta in EncuestaGeneral) 
                {
                    foreach (var detalle_profesor in encuesta.Detalle_Encuestas) 
                    {
                        foreach (var detalle_Estudiante in detalle_profesor.Estudiante)
                        {
                            lista.Add(new ObservacionesMejoraModel
                            {
                                NombreProfesor = detalle_profesor.IdDocente,
                                ApellidoProfesor = detalle_profesor.IdDocente,
                                Carrera = detalle_profesor.IdPrograma,
                                Materia = detalle_profesor.IdAsignatura,
                                CedulaEstudiante = detalle_Estudiante.Identificacion,
                                NombreEstudiante = detalle_Estudiante.Identificacion,
                                Observacion_Mejora = detalle_Estudiante.Conclusion,
                            });
                        }
                    }
                }

                return lista;
            }
            else
            {
                throw new Exception("No existe informacion de observaciones de mejora", new Exception("404"));
            }
        }

    }
}
