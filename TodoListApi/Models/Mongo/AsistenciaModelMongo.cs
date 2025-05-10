using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BackSemillero.Models.Mongo
{
    public class AsistenciaModelMongo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string? CedulaEstudiante { get; set; } = null!;

        public string IdQr { get; set; } = null!;

        public DateTime? Fecha { get; set; }
    }
}
