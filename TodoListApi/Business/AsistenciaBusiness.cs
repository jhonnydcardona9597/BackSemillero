using System.Threading.Tasks;
using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;

namespace BackSemillero.Business
{
    public class AsistenciaBusiness : IAsistenciaBusiness
    {
        private readonly IEstudianteData _estudianteData;
        private readonly IAsistenciaData _asistenciaData;

        public AsistenciaBusiness(IEstudianteData estudianteData,IAsistenciaData asistenciaData)
        {
            _estudianteData = estudianteData;
            _asistenciaData = asistenciaData;
        }

        public async Task<AsistenciaResponse> RegistrarAsistencia(AsistenciaModelRequest asistenciaModelRequest)
        {
            var estudiante = await _estudianteData.ConsultarEstudianteXCedula(asistenciaModelRequest.Cedula);
            if (estudiante is null || !estudiante.Activo)
                return new AsistenciaResponse { Registrada = false, Mensaje = "Estudiante inactivo o no existe." };

            return await _asistenciaData.CrearRegistroAsistencia(asistenciaModelRequest);
        }
    }
}
