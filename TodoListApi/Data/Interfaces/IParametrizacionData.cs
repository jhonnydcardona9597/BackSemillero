using BackSemillero.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Data.Interfaces
{
    public interface IParametrizacionData
    {
        Task<IEnumerable<EncuestaReponseModel>> ObtenerEncuestasPorFecha(DateTime fechaBuscada);

        Task<IEnumerable<RankingModel>> ObtenerRankingsPorEncuesta(ObjectId idEncuesta);
    }
}
