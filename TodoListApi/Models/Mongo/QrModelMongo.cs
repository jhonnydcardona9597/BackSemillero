using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BackSemillero.Models.Mongo
{
    public class QrModelMongo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? CedulaProfesor { get; set; }
        public int IdPrograma { get; set; }
        public int IdAsignatura { get; set; }
        public DateTime? FechaHoraQr { get; set; }
    }
}
