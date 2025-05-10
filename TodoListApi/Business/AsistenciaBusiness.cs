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
            // 1. Validar estudiante activo (ahora obtenemos lista)
            var estudiantes = await _estudianteData.ConsultarEstudianteXCedula(asistenciaModelRequest.CedulaEstudiante!);
            if (estudiantes == null)
                throw new Exception("Estudiante no activo o no existe.");

            //_parametrizacionData.ObtenerQrPorId(asistenciaModelRequest.IdQr);

            var qrGenerado = await _parametrizacionData.ObtenerQrPorId(asistenciaModelRequest.IdQr);
            if (qrGenerado == null || qrGenerado.FechaHoraQr == null)
                throw new Exception("QR no encontrado o no tiene fecha válida.");

            // Convertimos a UTC por seguridad en la comparación
            var fechaQrUtc = qrGenerado.FechaHoraQr.Value.ToUniversalTime();
            var fechaActualUtc = DateTime.UtcNow;

            // Calculamos la diferencia en horas
            var diferenciaHoras = (fechaActualUtc - fechaQrUtc).TotalHours;

            if (diferenciaHoras > 2)
                throw new Exception("El QR ha expirado. Solo es válido durante las 2 horas posteriores a su generación.");

            // 3. Crear registro de asistencia
            var respuesta = await _asistenciaData.CrearRegistroAsistencia(new AsistenciaModelMongo
            {
                 CedulaEstudiante = asistenciaModelRequest.CedulaEstudiante,
                 IdQr = asistenciaModelRequest.IdQr,
                 Fecha = DateTime.Now
            });
            if (!respuesta.Exito)
                throw new Exception("No se genero registro de asistencia");

            return respuesta;


        }
    }
}
