using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;

namespace BackSemillero.Business
{
    public class ParametrizacionBusiness : IParametrizacionBusiness
    {
        private readonly IParametrizacionData _parametrizacionData;
        public ParametrizacionBusiness(IParametrizacionData parametrizacionData)
        {
            _parametrizacionData = parametrizacionData;
        }

        public async Task<QrModelResponse> GenerarQr(QrModelRequest qrModelRequest)
        {
            ProfesorModel profesorModel = await _parametrizacionData.ConsultarProfesorXCedula(qrModelRequest.Cedula);
            if(profesorModel != null)
            {
                return await _parametrizacionData.CrearRegistroQr(qrModelRequest);
            }
            else
            {
                throw new Exception("Error no existe el profesor");
            }
        }
    }
}
