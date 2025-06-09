using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BackSemillero.Models.Mongo
{
    public class RankingModelMongo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? EncuestaId { get; set; }
        public string? Puntaje { get; set; }
        public string? Puesto { get; set; }
        public string? TipsMejora { get; set; }
        public string? Fortalezas { get; set; }
        public string? FechaHoraCreacion { get; set; }
    }
}
