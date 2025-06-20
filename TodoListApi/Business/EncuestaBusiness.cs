using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSemillero.Business
{
    public class EncuestaBusiness : IEncuestaBusiness
    {
        private readonly IEncuestaData _encuestaData;

        public EncuestaBusiness(IEncuestaData encuestaData)
        {
            _encuestaData = encuestaData;
        }

        public async Task<List<EncuestaModelResponse>> ObtenerEncuestas(DashboardRequest filtros)
        {
            // 1) Fecha objetivo: si viene filtros.Fecha, la usamos; si no, tomamos “hoy”
            DateTime fechaObjetivo = filtros.Fecha?.Date ?? DateTime.Now.Date;

            // 2) Traer encuestas de la fecha (o de la anterior más cercana)
            var encuestasDelDia = await _encuestaData.ObtenerEncuestas(fechaObjetivo);

            // 3) Filtro de texto sobre detalle y clasificación
            var encuestasFiltradas = encuestasDelDia;
            if (!string.IsNullOrWhiteSpace(filtros.Filtro))
            {
                string tf = filtros.Filtro.Trim().ToLowerInvariant();
                encuestasFiltradas = encuestasDelDia.Where(e =>
                    e.Detalle_Encuestas.Any(d =>
                        (d.IdDocente?.ToLowerInvariant().Contains(tf) ?? false) ||
                        (d.IdPrograma?.ToLowerInvariant().Contains(tf) ?? false) ||
                        (d.IdAsignatura?.ToLowerInvariant().Contains(tf) ?? false) ||
                        (d.Jornada?.ToLowerInvariant().Contains(tf) ?? false) ||
                        (d.Categoria?.ToLowerInvariant().Contains(tf) ?? false) ||
                        d.FindeSemana.ToString().ToLowerInvariant().Contains(tf) ||
                        d.Virtual.ToString().ToLowerInvariant().Contains(tf) ||
                        (d.Clasificacion?.Puntaje.ToString().Contains(tf) ?? false) ||
                        //(d.Clasificacion?.PuntajeAnterior.ToString().Contains(tf) ?? false) ||
                        (d.Clasificacion?.Puesto.ToString().Contains(tf) ?? false) ||
                        //(d.Clasificacion?.PuestoAnterior.ToString().Contains(tf) ?? false) ||
                        e.HoraYFechaDeCreacion.ToString("O").Contains(tf)
                        
                    )
                ).ToList();
            }

            // 4) Cargar la clasificación en cada detalle
            foreach (var encuesta in encuestasFiltradas)
            {
                foreach (var detalle in encuesta.Detalle_Encuestas)
                {
                    detalle.Clasificacion =
                        await _encuestaData.ObtenerClasificacion(detalle.IdClasificacion);
                }
            }

            return encuestasFiltradas;
        }
    }
}
