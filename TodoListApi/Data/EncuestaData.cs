// Data/EncuestaData.cs
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSemillero.Data
{
    public class EncuestaData : IEncuestaData
    {
        private readonly IMongoCollection<EncuestaModelResponse> _encuestasCollection;
        private readonly IMongoCollection<ClasificacionModel> _clasificacionesCollection;

        public EncuestaData(IMongoDatabase database)
        {
            _encuestasCollection = database.GetCollection<EncuestaModelResponse>("Encuestas");
            _clasificacionesCollection = database.GetCollection<ClasificacionModel>("Clasificación");
        }

        public async Task<List<EncuestaModelResponse>> ObtenerEncuestas(DateTime fechaBuscada)
        {
            // 1) Normalizar fecha a medianoche
            DateTime inicio = fechaBuscada.Date;
            DateTime fin = inicio.AddDays(1);

            var bldr = Builders<EncuestaModelResponse>.Filter;
            var filtroHoy = bldr.And(
                bldr.Gte(e => e.HoraYFechaDeCreacion, inicio),
                bldr.Lt(e => e.HoraYFechaDeCreacion, fin)
            );

            // 2) Intentar traer encuestas de la fecha solicitada
            var encs = await _encuestasCollection.Find(filtroHoy).ToListAsync();
            if (encs.Any())
                return encs;

            // 3) Si no hay, buscar la fecha anterior más cercana
            var pipeline = _encuestasCollection.Aggregate()
                .Match(bldr.Lt(e => e.HoraYFechaDeCreacion, inicio))
                .Project(e => new { SoloFecha = e.HoraYFechaDeCreacion.Date })
                .Group(x => x.SoloFecha, g => new { Fecha = g.Key })
                .SortByDescending(x => x.Fecha)
                .Limit(1);

            var fechas = await pipeline.ToListAsync();
            if (!fechas.Any())
                return new List<EncuestaModelResponse>();

            // 4) Traer encuestas de esa fecha anterior
            var fechaAnt = fechas[0].Fecha;
            inicio = fechaAnt;
            fin = inicio.AddDays(1);

            var filtroAnt = bldr.And(
                bldr.Gte(e => e.HoraYFechaDeCreacion, inicio),
                bldr.Lt(e => e.HoraYFechaDeCreacion, fin)
            );

             
            var pop = await _encuestasCollection.Find(filtroAnt).ToListAsync();
            return new List<EncuestaModelResponse>();
        }

        public async Task<ClasificacionModel?> ObtenerClasificacion(ObjectId idClasificacion)
        {
            return await _clasificacionesCollection
                         .Find(c => c.Id == idClasificacion)
                         .FirstOrDefaultAsync();
        }
        public async Task<List<EncuestaModelResponse>> ObtenerTodasLasEncuestas()
        {
            return await _encuestasCollection
                .Find(FilterDefinition<EncuestaModelResponse>.Empty)
                .SortByDescending(e => e.HoraYFechaDeCreacion)
                .ToListAsync();
        }
    }
}
