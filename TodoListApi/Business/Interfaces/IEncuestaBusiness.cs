using BackSemillero.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Business.Interfaces
{
    public interface IEncuestaBusiness
    {
        Task<List<EncuestaModelResponse>> ObtenerEncuestas(DashboardRequest filtros);
    }
}
