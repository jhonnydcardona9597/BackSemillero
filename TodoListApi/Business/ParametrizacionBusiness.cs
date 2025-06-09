using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using BackSemillero.Models.Mongo;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Security.Cryptography.Xml;

namespace BackSemillero.Business
{
    public class ParametrizacionBusiness : IParametrizacionBusiness
    {
        private readonly IParametrizacionData _parametrizacionData;
        private readonly IProfesorData _profesorData;
        private readonly IConfiguration _configuration;
        public ParametrizacionBusiness(IParametrizacionData parametrizacionData, IConfiguration configuration, IProfesorData profesorData)
        {
            _parametrizacionData = parametrizacionData;
            _configuration = configuration;
            _profesorData = profesorData;
        }

        public async Task<QrModelResponse> GenerarQr(QrModelRequest qrModelRequest)
        {
            if (string.IsNullOrWhiteSpace(qrModelRequest.CedulaProfesor) ||
                qrModelRequest.IdPrograma <= 0 ||
                qrModelRequest.IdAsignatura <= 0 ||
                await _parametrizacionData.ConsultarProfesorXCedula(qrModelRequest.CedulaProfesor) is null)
            {
                throw new Exception(
                    "El recurso no existe o fue eliminado.",
                    new Exception("404")
                );
            }

            // Registro del QR
            var result = await _parametrizacionData.CrearRegistroQr(new QrModelMongo
            {
                CedulaProfesor = qrModelRequest.CedulaProfesor,
                IdAsignatura = qrModelRequest.IdAsignatura,
                IdPrograma = qrModelRequest.IdPrograma,
                FechaHoraQr = DateTime.Now
            });

            result.IdQr = _configuration.GetSection("UrlQr:Url").Value?.ToString() + result.IdQr;

            return result;
        }


    }
}
