namespace BackSemillero.Models
{
    /// <summary>
    /// Modelo de petición para ConsultarDashboard:
    ///   - Fecha: fecha para filtrar las encuestas de ese día. Opcional.
    ///             Si no se envía, se usa DateTime.Now.Date.
    ///   - Filtro: texto libre opcional. 
    ///             Si se envía, se hace “Contains” sobre campos de texto de la encuesta.
    /// </summary>
    public class DashboardRequest
    {
        public DateTime? Fecha { get; set; }
        public string? Filtro { get; set; }
    }
}