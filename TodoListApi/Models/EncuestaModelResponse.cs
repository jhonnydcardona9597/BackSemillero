using BackSemillero.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class EncuestaModelResponse
{
    [BsonId] public ObjectId Id { get; set; }

    public int CantidadCarrerasNotificadas { get; set; }
    public int CantidadEncuestas { get; set; }
    public string EstadoEnvio { get; set; } = null!;

    [BsonElement("HoraYFechaDeCreacion")]
    public DateTime HoraYFechaDeCreacion { get; set; }

    [BsonElement("HoraYFechaDeModificacion")]
    public DateTime HoraYFechaDeModificacion { get; set; }

    [BsonElement("Detalle_Encuestas")]
    public List<DetalleEncuestaModel> Detalle_Encuestas { get; set; } = new();
}
