namespace BackSemillero.Models
{
    public class AsistenciaResponse
    {
        public bool Registrada { get; set; }
        public string Mensaje { get; set; }
        public AsistenciaModelRequest AsistenciaGuardada { get; set; }
    }
}
