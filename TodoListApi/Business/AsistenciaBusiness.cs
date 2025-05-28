using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using BackSemillero.Models.Mongo;
using System;
using System.Threading.Tasks;

namespace BackSemillero.Business
{
    public class AsistenciaBusiness : IAsistenciaBusiness
    {
        private readonly IEstudianteData _estudianteData;
        private readonly IAsistenciaData _asistenciaData;
        private readonly IParametrizacionData _parametrizacionData;

        public AsistenciaBusiness(
            IEstudianteData estudianteData,
            IParametrizacionData parametrizacionData,
            IAsistenciaData asistenciaData)
        {
            _estudianteData = estudianteData;
            _asistenciaData = asistenciaData;
            _parametrizacionData = parametrizacionData;
        }

        public async Task<AsistenciaModelResponse> RegistrarAsistencia(AsistenciaModelRequest asistenciaModelRequest)
        {
            // 404 - No encontrado: estudiante no está en base de datos
            var estudiantes = await _estudianteData.ConsultarEstudianteXCedula(asistenciaModelRequest.CedulaEstudiante!);
            if (estudiantes == null)
                throw new Exception(
                    "El recurso solicitado no está disponible o no existe. Verifica la URL o intenta buscar otra información.",
                    new Exception("404")
                );

            // 404 - QR no encontrado
            var qrGenerado = await _parametrizacionData.ObtenerQrPorId(asistenciaModelRequest.IdQr);
            if (qrGenerado == null || qrGenerado.FechaHoraQr == null)
                throw new Exception(
                    "El recurso solicitado no está disponible o no existe. Verifica la URL o intenta buscar otra información.",
                    new Exception("404")
                );

            // 403 - Prohibido: QR vencido
            var fechaQrUtc = qrGenerado.FechaHoraQr.Value.ToUniversalTime();
            var fechaActualUtc = DateTime.UtcNow;
            var diferenciaHoras = (fechaActualUtc - fechaQrUtc).TotalHours;

            if (diferenciaHoras > 2)
                throw new Exception(
                    "No tienes permiso para acceder a este recurso. Si crees que es un error, contacta al administrador.",
                    new Exception("403")
                );

            // Crear registro de asistencia
            var respuesta = await _asistenciaData.CrearRegistroAsistencia(new AsistenciaModelMongo
            {
                CedulaEstudiante = asistenciaModelRequest.CedulaEstudiante,
                IdQr = asistenciaModelRequest.IdQr,
                Fecha = DateTime.Now
            });

            // 400 - Solicitud incorrecta: error técnico al guardar
            if (!respuesta.Exito)
                throw new Exception(
                    "La solicitud no pudo ser procesada debido a un error en los datos enviados. Por favor, verifica la información e intenta nuevamente.",
                    new Exception("400")
                );

            return respuesta;
        }

    }
}
