using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
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
        private readonly IMongoCollection<EncuestaModelResponse> _encuestasCollection;

        public SupervisorData(IMongoDatabase database)
        {
            _encuestasCollection = database.GetCollection<EncuestaModelResponse>("Encuestas");
        }

        // Ya existente: retorna envíos de supervisor en un rango de fechas
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

        // Ya existente: retorna fechas anteriores disponibles
        public async Task<IEnumerable<DateTime>> ObtenerFechasEnvioAnteriores(DateTime antesDe)
        {
            var builder = Builders<SupervisorModelResponse>.Filter;
            var match = builder.Lt(x => x.FechaHoraEnvio, antesDe);

            var result = await _supervisorCollection.Aggregate()
                .Match(match)
                .Project(x => new { SoloFecha = x.FechaHoraEnvio.Date })
                .Group(x => x.SoloFecha, g => new { Fecha = g.Key })
                .SortByDescending(x => x.Fecha)
                .ToListAsync();

            return result.Select(x => x.Fecha);
        }

        //Nuevo método: retorna TODAS las encuestas completas desde la colección correcta
        public async Task<IEnumerable<EncuestaModelResponse>> ObtenerTodasLasEncuestas()
        {
            return await _encuestasCollection
                .Find(FilterDefinition<EncuestaModelResponse>.Empty)
                .SortByDescending(e => e.HoraYFechaDeCreacion)
                .ToListAsync();
        }
    }
}
