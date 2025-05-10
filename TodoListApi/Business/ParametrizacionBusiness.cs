using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using BackSemillero.Models.Mongo;

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
            ProfesorModel profesorModel = await _parametrizacionData.ConsultarProfesorXCedula(qrModelRequest.CedulaProfesor);
            if(profesorModel != null)
            {
                return await _parametrizacionData.CrearRegistroQr(new QrModelMongo
                {
                    CedulaProfesor = qrModelRequest.CedulaProfesor,
                    IdAsignatura = qrModelRequest.IdAsignatura,
                    IdPrograma =    qrModelRequest.IdPrograma,
                    FechaHoraQr = DateTime.Now
                });
            }
            else
            {
                throw new Exception("Error no existe el profesor");
            }
        }
    }
}
