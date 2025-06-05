using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BackSemillero.Models
{
    public class EncuestaReponseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string IdDocente { get; set; } = null!;
        public string IdPrograma { get; set; } = null!; // "Ingenieria de Sistemas" Debemos mirar con BD Como vamos hacer esto para el Filtro del parametro que nos envia Front
        public string IdAsignatura { get; set; } = null!;
        public string Jornada { get; set; } = null!;
        public string Categoria { get; set; } = null!;  // Entonces el nombre de este modelo sera EncuestaReponseModel porque con este mismo se puede hacer todo
        public bool FindeSemana { get; set; }
        public bool Virtual { get; set; }
        public string? ObservacionMejora { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<RankingModel> rankingModels { get; set; } = new(); // De aqui es donde tengo que hacer el for each(Business) para recorrerlo y luego para retornar una lista de EncuestaModel.
    }
}
