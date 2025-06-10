using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BackSemillero.Models
{
    public class EncuestaModelResponse
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string IdDocente { get; set; } = null!;
        public string IdPrograma { get; set; } = null!; 
        public string IdAsignatura { get; set; } = null!;
        public string Jornada { get; set; } = null!;
        public string Categoria { get; set; } = null!; 
        public bool FindeSemana { get; set; }
        public bool Virtual { get; set; }
        public string? ObservacionMejora { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<RankingModel> rankingModels { get; set; } = new(); 
    }
}