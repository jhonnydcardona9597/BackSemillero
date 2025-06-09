using BackSemillero.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackSemillero.Data.Interfaces
{
    public interface IEncuestaData
    {
        Task<IEnumerable<EncuestaModelResponse>> ObtenerEncuestasPorFecha(DateTime fechaBuscada);

        Task<IEnumerable<RankingModel>> ObtenerRankingsPorEncuesta(ObjectId idEncuesta);
    }
}