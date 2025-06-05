using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSemillero.Data
{
    public class ParametrizacionData : IParametrizacionData
    {
        private readonly IMongoCollection<EncuestaReponseModel> _encuestasCollection;
        private readonly IMongoCollection<RankingModel> _rankingCollection;

        public ParametrizacionData(IOptions<MongoDBSettings> settings, IMongoDatabase database)
        {
            _encuestasCollection = database.GetCollection<EncuestaReponseModel>("Encuestas");
            _rankingCollection = database.GetCollection<RankingModel>("Ranking");
        }

        /// <summary>
        /// 1) Intenta traer encuestas en el día 'fechaBuscada'.
        /// 2) Si no hay, usa Aggregate para encontrar la fecha anterior más cercana con encuestas.
        /// 3) Retorna las encuestas de esa fecha (o lista vacía si no hay historial).
        /// </summary>
        public async Task<IEnumerable<EncuestaReponseModel>> ObtenerEncuestasPorFecha(DateTime fechaBuscada)
        {
            // Normalizar fecha a medianoche
            DateTime inicio = fechaBuscada.Date;
            DateTime fin = inicio.AddDays(1);

            // Filtrar por [inicio, fin)
            var filtroDia = Builders<EncuestaReponseModel>.Filter.And(
                Builders<EncuestaReponseModel>.Filter.Gte(e => e.FechaCreacion, inicio),
                Builders<EncuestaReponseModel>.Filter.Lt(e => e.FechaCreacion, fin)
            );

            var encuestasDelDia = await _encuestasCollection.Find(filtroDia).ToListAsync();
            if (encuestasDelDia.Count > 0)
                return encuestasDelDia;

            // Si no hay encuestas ese día, buscar fecha previa más cercana con encuestas
            var pipeline = _encuestasCollection.Aggregate()
                .Match(Builders<EncuestaReponseModel>.Filter.Lt(e => e.FechaCreacion, inicio))
                .Project(e => new { SoloFecha = e.FechaCreacion.Date })
                .Group(x => x.SoloFecha, g => new { Fecha = g.Key })
                .SortByDescending(x => x.Fecha)
                .Limit(1);

            var resultFecha = await pipeline.ToListAsync();
            if (resultFecha.Count == 0)
                return Enumerable.Empty<EncuestaReponseModel>();

            DateTime fechaMasCercana = resultFecha[0].Fecha;
            DateTime inicioCercano = fechaMasCercana;
            DateTime finCercano = inicioCercano.AddDays(1);

            var filtroCercano = Builders<EncuestaReponseModel>.Filter.And(
                Builders<EncuestaReponseModel>.Filter.Gte(e => e.FechaCreacion, inicioCercano),
                Builders<EncuestaReponseModel>.Filter.Lt(e => e.FechaCreacion, finCercano)
            );

            return await _encuestasCollection.Find(filtroCercano).ToListAsync();
        }

        /// <summary>
        /// Retorna TODOS los RankingModel cuyo campo IdEncuesta sea igual al ObjectId dado.
        /// </summary>
        public async Task<IEnumerable<RankingModel>> ObtenerRankingsPorEncuesta(ObjectId idEncuesta)
        {
            var filtro = Builders<RankingModel>.Filter.Eq(r => r.IdEncuesta, idEncuesta);
            return await _rankingCollection.Find(filtro).ToListAsync();
        }
    }
}
