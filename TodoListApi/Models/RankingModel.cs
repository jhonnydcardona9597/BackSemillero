using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackSemillero.Models
{
    public class RankingModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId IdEncuesta { get; set; }
        public int Puntaje { get; set; }
        public int PuntajeAnterior { get; set; }
        public int Puesto { get; set; }
        public int PuestoAnterior { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}