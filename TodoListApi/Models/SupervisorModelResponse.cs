using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace BackSemillero.Models
{
    public class SupervisorModelResponse
    {
        //[BsonId]
        //[JsonIgnore]
        //public ObjectId Id { get; set; }

        //[BsonIgnore]
        //public string IdString => Id.ToString();

        public DateTime FechaHoraEnvio { get; set; }
        public int CantidadCarrerasNotificadas { get; set; }
        public int CantidadEncuestasEnviadas { get; set; }
        public string EstadoEnvio { get; set; } = null!;
    }
}
