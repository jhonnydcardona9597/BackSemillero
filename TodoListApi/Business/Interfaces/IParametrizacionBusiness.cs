using BackSemillero.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Business.Interfaces
{
    public interface IParametrizacionBusiness
    {
        Task<IEnumerable<EncuestaReponseModel>> ObtenerEncuestasConRanking(DashboardRequest filtros);
    }
}
