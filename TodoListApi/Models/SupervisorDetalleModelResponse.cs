using System;

namespace BackSemillero.Models
{
    public class SupervisorDetalleModelResponse
    {
        public string IdDocente { get; set; } = null!;
        public string IdPrograma { get; set; } = null!;
        public string IdAsignatura { get; set; } = null!;
        public string Jornada { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public bool FindeSemana { get; set; }
        public bool Virtual { get; set; }
        public string? ObservacionesMejora { get; set; }
        public DateTime HoraYFechaCreacion { get; set; } 
    }
}
