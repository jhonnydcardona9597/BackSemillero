using BackSemillero.Models;

namespace BackSemillero.Data.Interfaces
{
    public interface IParametrizacionData
    {
        Task<ProfesorModel> ConsultarProfesorXCedula(string Cedula);
        Task<QrModelResponse> CrearRegistroQr(QrModelRequest qrModelRequest);
    }
}
