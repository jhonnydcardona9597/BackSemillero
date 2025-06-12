using MongoDB.Bson;

namespace BackSemillero.Models
{
    public class ClasificacionProfesorModel
    {
        public string? Asignatura { get; set; }
        public string? Fortalezas { get; set; }
        public string? TipsMejora { get; set; }
        public Double? Puntaje { get; set; }
        public Int32? Puesto { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
