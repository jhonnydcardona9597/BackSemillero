namespace BackSemillero.Models
{
    /// <summary>Documento que se guardará en Mongo.</summary>
    public class AsistenciaModelRequest
    {
        public string Id { get; set; }   // _id de Mongo
        public string Cedula { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
