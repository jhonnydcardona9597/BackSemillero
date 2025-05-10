using BackSemillero.Models;

namespace BackSemillero.Business.Interfaces
{
    public interface IParametrizacionBusiness
    {
        Task<QrModelResponse> GenerarQr(QrModelRequest qrModelRequest);
        
    }
}
