using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using BackSemillero.Models.Mongo;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BackSemillero.Business
{
    public class ParametrizacionBusiness : IParametrizacionBusiness
    {
        private readonly IParametrizacionData _parametrizacionData;
        private readonly IConfiguration _configuration;
        public ParametrizacionBusiness(IParametrizacionData parametrizacionData, IConfiguration configuration)
        {
            _parametrizacionData = parametrizacionData;
            _configuration = configuration;
        }

        public async Task<QrModelResponse> GenerarQr(QrModelRequest qrModelRequest)
        {
            ProfesorModel profesorModel = await _parametrizacionData.ConsultarProfesorXCedula(qrModelRequest.CedulaProfesor);
            if(profesorModel != null)
            {
                var result = await _parametrizacionData.CrearRegistroQr(new QrModelMongo
                {
                    CedulaProfesor = qrModelRequest.CedulaProfesor,
                    IdAsignatura = qrModelRequest.IdAsignatura,
                    IdPrograma =    qrModelRequest.IdPrograma,
                    FechaHoraQr = DateTime.Now
                });
                result.IdQr = _configuration.GetSection("UrlQr:Url").Value?.ToString()+result.IdQr;
                return result;
            }
            else
            {
                throw new Exception("No existe el profesor", new Exception ("404"));
            }
        }
    }
}
