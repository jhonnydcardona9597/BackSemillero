using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BackSemillero.Models
{
    public class ClasificacionModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId Encuesta { get; set; }    
        public Double? Puntaje { get; set; }
        public Int32? Puesto { get; set; }
        public string? Tips_mejora { get; set; }
        public DateTime HoraYFechaDeCreacion { get; set; }
        public DateTime HoraYFechaDeModificacion { get; set; }
        public string? Fortalezas { get; set; }
    }
}
