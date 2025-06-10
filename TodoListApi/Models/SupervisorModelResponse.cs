using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BackSemillero.Models
{
    public class SupervisorModelResponse
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime FechaHoraEnvio { get; set; }
        public int CantidadCarrerasNotificadas { get; set; }
        public int CantidadEncuestasEnviadas { get; set; }
        public string EstadoEnvio { get; set; } = null!;
    }
}
