using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BackSemillero.Models
{
    [BsonIgnoreExtraElements]
    public class DetalleEncuestaModel
    {
        [BsonElement("_idClasificacion")]
        public ObjectId IdClasificacion { get; set; }

        [BsonElement("Docente")]
        public string IdDocente { get; set; } = null!;

        [BsonElement("Programa")]
        public string IdPrograma { get; set; } = null!;

        [BsonElement("Asignatura")]
        public string IdAsignatura { get; set; } = null!;

        [BsonElement("Jornada")]
        public string Jornada { get; set; } = null!;

        [BsonElement("Categoria")]
        public string Categoria { get; set; } = null!;

        [BsonElement("Findesemana")]
        public bool FindeSemana { get; set; }

        [BsonElement("Virtual")]
        public bool Virtual { get; set; }

        [BsonElement("Observaciones_mejora")]
        public string? ObservacionesMejora { get; set; }

        [BsonElement("HoraYFechaDeModificacion")]
        public DateTime HoraYFechaDeModificacion { get; set; }

        [BsonIgnore]
        public ClasificacionModel? Clasificacion { get; set; }

        [BsonElement("Estudiante")]
        public List<DetalleEstudianteModel> Estudiante { get; set; } = new();

    }
}
