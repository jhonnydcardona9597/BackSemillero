using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BackSemillero.Models
{
    [BsonIgnoreExtraElements]
    public class DetalleEstudianteModel
    {
      
        [BsonElement("Telefono")]
        public string Telefono { get; set; } = null!;

        [BsonElement("Conversacion")]
        public string Conversacion { get; set; } = null!;

        [BsonElement("Conclusion")]
        public string Conclusion { get; set; } = null!;

        [BsonElement("HoraYFechaDeModificacion")]
        public DateTime HoraYFechaDeModificacion { get; set; }

        [BsonElement("Identificacion")]
        public string Identificacion { get; set; } = null!;
    }
}

