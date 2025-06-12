using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models.Mongo;
using BackSemillero.Models;
using Microsoft.Extensions.Configuration;
using BackSemillero.Data;

namespace BackSemillero.Business
{
    public class ProfesorBusiness : IProfesorBusiness
    {
        private readonly IProfesorData _profesorData;
        private readonly IEncuestaData _encuestaData;

        public ProfesorBusiness(IProfesorData profesorData, IEncuestaData encuestaData)
        {
            _profesorData = profesorData;
            _encuestaData = encuestaData;
        }

        public async Task<ProfesorModel> ConsultarProfesor(string CedulaProfesor)
        {
            ProfesorModel profesorModel = await _profesorData.ConsultarProfesorXCedula(CedulaProfesor);
            if (profesorModel != null)
            {
                return profesorModel;
            }
            else
            {
                throw new Exception("No existe el profesor", new Exception("404"));
            }
        }

        public async Task<List<ClasificacionProfesorModel>> ConsultarDetalleProfesor(string CedulaProfesor)
        {
            var Encuesta = await _encuestaData.ObtenerEncuestas(DateTime.Now.Date);

            var EncuestaFiltrada = Encuesta.Where(e =>
                    e.Detalle_Encuestas.Any(d =>
                        (d.IdDocente?.ToLowerInvariant().Contains(CedulaProfesor.ToLowerInvariant().Trim()) ?? false)
                    )
                ).ToList();


            if (EncuestaFiltrada.Count() != 0)
            {
                var lista = new List<ClasificacionProfesorModel>();
                foreach (var encuesta in EncuestaFiltrada)
                {
                    foreach (var detalle in encuesta.Detalle_Encuestas)
                    {
                        detalle.Clasificacion =
                        await _encuestaData.ObtenerClasificacion(detalle.IdClasificacion);

                        lista.Add(new ClasificacionProfesorModel
                        {
                            Asignatura = detalle.IdAsignatura,
                            Fortalezas = detalle.Clasificacion?.Fortalezas,
                            TipsMejora = detalle.Clasificacion?.Tips_mejora,
                            Puntaje = detalle.Clasificacion?.Puntaje,
                            Puesto = detalle.Clasificacion?.Puesto,
                            FechaCreacion = encuesta.HoraYFechaDeCreacion
                        });
                    }
                }

                return lista;
            }
            else
            {
                throw new Exception("No existen clasificaciones para esa cedula", new Exception("404"));
            }
        }
    }
}
