using BackSemillero.Data;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSemillero.Data
{
    public class SupervisorData : ISupervisorData
    {
        private readonly IMongoCollection<SupervisorModelResponse> _supervisorCollection;

        public SupervisorData(IMongoDatabase database)
        {
            _supervisorCollection = database.GetCollection<SupervisorModelResponse>("EncuestasSup");
        }

        public async Task<IEnumerable<SupervisorModelResponse>> ObtenerEnviosPorRango(DateTime inicio, DateTime fin)
        {
            var filter = Builders<SupervisorModelResponse>.Filter.And(
                Builders<SupervisorModelResponse>.Filter.Gte(x => x.FechaHoraEnvio, inicio),
                Builders<SupervisorModelResponse>.Filter.Lt(x => x.FechaHoraEnvio, fin)
            );

            return await _supervisorCollection
                          .Find(filter)
                          .SortByDescending(x => x.FechaHoraEnvio)
                          .ToListAsync();
        }

        public async Task<IEnumerable<DateTime>> ObtenerFechasEnvioAnteriores(DateTime antesDe)
        {
            var builder = Builders<SupervisorModelResponse>.Filter;
            var match = builder.Lt(x => x.FechaHoraEnvio, antesDe);

            // Agrupamos por la parte Date de FechaHoraEnvio
            var result = await _supervisorCollection.Aggregate()
                .Match(match)
                .Project(x => new { SoloFecha = x.FechaHoraEnvio.Date })
                .Group(x => x.SoloFecha, g => new { Fecha = g.Key })
                .SortByDescending(x => x.Fecha)
                .ToListAsync();

            return result.Select(x => x.Fecha);
        }
    }
}
