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
            // 401 - No autorizado (cédula vacía, sin posibilidad de autenticar al profesor)
            if (string.IsNullOrWhiteSpace(qrModelRequest.CedulaProfesor))
                throw new Exception(
                    "Acceso denegado. Autenticación requerida o fallida. Por favor, inicia sesión o proporciona credenciales válidas.",
                    new Exception("401")
                );

            // 402 - Pago requerido (valor simbólico para representar que el programa debe estar diligenciado)
            if (qrModelRequest.IdPrograma <= 0)
                throw new Exception(
                    "Para acceder a este recurso, es necesario completar el proceso de pago. Por favor, realiza el pago pendiente.",
                    new Exception("402")
                );

            // 403 - Prohibido (representa que la asignatura es inválida o no está permitida)
            if (qrModelRequest.IdAsignatura <= 0)
                throw new Exception(
                    "No tienes permiso para acceder a este recurso. Si crees que es un error, contacta al administrador.",
                    new Exception("403")
                );

            // 404 - No encontrado (profesor no existe en la base de datos)
            var profesorModel = await _parametrizacionData.ConsultarProfesorXCedula(qrModelRequest.CedulaProfesor);
            if (profesorModel == null)
                throw new Exception(
                    "El recurso solicitado no está disponible o no existe. Verifica la URL o intenta buscar otra información.",
                    new Exception("404")
                );

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
