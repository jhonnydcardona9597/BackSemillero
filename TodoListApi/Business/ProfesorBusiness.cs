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
            //ProfesorModel profesorModel = await _profesorData.ConsultarProfesorXCedula(CedulaProfesor);
            ProfesorModel profesorModel = new ProfesorModel();
            if (profesorModel != null)
            {
                //return profesorModel;
                return new ProfesorModel
                {
                    CedulaProfesor = "1",
                    NombreProfesor = "qwe",
                    ApellidoProfesor = "asd",
                    TelefonoProfesor = "123",
                    CorreoProfesor = "123@gml.com",
                    EstadoProfesor = "1",
                    Habilidades = "Habilitar"
                };
            }
            else
            {
                throw new Exception("No existe el profesor", new Exception("404"));
            }
        }

        public async Task<List<ProfesorModelResponse>> ConsultarDetalleProfesor(string CedulaProfesor)
        {
            string IdEncuesta = "";
            if (IdEncuesta != null)
            {
                var result = await _profesorData.ObtenerDetalleProfesor(IdEncuesta);
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
