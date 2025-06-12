using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models.Mongo;
using BackSemillero.Models;
using Microsoft.Extensions.Configuration;

namespace BackSemillero.Business
{
    public class ProfesorBusiness : IProfesorBusiness
    {
        private readonly IProfesorData _profesorData;

        public ProfesorBusiness(IProfesorData profesorData, IConfiguration configuration)
        {
            _profesorData = profesorData;
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

        public async Task<List<ProfesorModelResponse>> ConsultarDetalleProfesor(string CedulaProfesor)
        {
            //Hago la consulta por fecha y docente
            string IdClasificacion = "";
            //var consulta = "";
            //foreach (var item in consulta.docentes)
            //{
            //    IdClasificacion = item.IdClasificacion;

            //    if (IdClasificacion != null)
            //    {
            //        var result = await _profesorData.ObtenerClasificacion(IdClasificacion);
            //        //return result;
            //        return new List<ProfesorModelResponse>
            //    {
            //        new ProfesorModelResponse
            //        {
            //            Id = "1",
            //            EncuestaId = "123",
            //            Puntaje = "10",
            //            Puesto = "1",
            //            TipsMejora = "mejorar",
            //            Fortalezas = "fortalecer",
            //            FechaHoraCreacion = "hoy"
            //        },
            //        new ProfesorModelResponse
            //        {
            //            Id = "2",
            //            EncuestaId = "456",
            //            Puntaje = "8",
            //            Puesto = "3",
            //            TipsMejora = "mejorar",
            //            Fortalezas = "fortalecer",
            //            FechaHoraCreacion = "ayer"
            //        }

            //    };
            //    }
            //}
            //fin

            if (IdClasificacion != null)
            {
                var result = await _profesorData.ObtenerClasificacion(IdClasificacion);
                //return result;
                return new List<ProfesorModelResponse>
                {
                    new ProfesorModelResponse
                    {
                        Id = "1",
                        EncuestaId = "123",
                        Puntaje = "10",
                        Puesto = "1",
                        TipsMejora = "mejorar",
                        Fortalezas = "fortalecer",
                        FechaHoraCreacion = "hoy"
                    },
                    new ProfesorModelResponse
                    {
                        Id = "2",
                        EncuestaId = "456",
                        Puntaje = "8",
                        Puesto = "3",
                        TipsMejora = "mejorar",
                        Fortalezas = "fortalecer",
                        FechaHoraCreacion = "ayer"
                    }

                };
            }
            else
            {
                throw new Exception("No existe la encuesta", new Exception("404"));
            }
        }
    }
}
