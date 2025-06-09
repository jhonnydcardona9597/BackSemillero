using BackSemillero.Models;
using BackSemillero.Models.Mongo;

namespace BackSemillero.Data.Interfaces
{
    public interface IParametrizacionData
    {
        Task<QrModelResponse> CrearRegistroQr(QrModelMongo qrModelMongo);
        Task<QrModelMongo> ObtenerQrPorId(string idQr);
    }
}
