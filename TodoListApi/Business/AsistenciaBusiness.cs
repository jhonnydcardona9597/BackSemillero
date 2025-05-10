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

        public AsistenciaBusiness(
            IEstudianteData estudianteData,
            IAsistenciaData asistenciaData)
        {
            _estudianteData = estudianteData;
            _asistenciaData = asistenciaData;
        }

        public async Task<AsistenciaModelResponse> RegistrarAsistencia(AsistenciaModelRequest asistenciaModelRequest)
        {
            // 1. Validar estudiante activo (ahora obtenemos lista)
            var estudiantes = await _estudianteData.ConsultarEstudianteXCedula(asistenciaModelRequest.CedulaEstudiante!);
            if (estudiantes == null)
                throw new Exception("Estudiante no activo o no existe.");

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
