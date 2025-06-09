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
        private readonly IEncuestaData _parametrizacionData;

        public EncuestaBusiness(IEncuestaData parametrizacionData)
        {
            _parametrizacionData = parametrizacionData;
        }

        public async Task<IEnumerable<EncuestaModelResponse>> ObtenerEncuestasConRanking(DashboardRequest filtros)
        {
            // 1) Fecha objetivo: si viene filtros.Fecha, la usamos; si no, tomamos “hoy”
            DateTime fechaObjetivo = filtros.Fecha?.Date ?? DateTime.Now.Date;

            // 2) Traer encuestas de la fecha (o fecha anterior con datos)
            var encuestasDelDia = await _parametrizacionData.ObtenerEncuestasPorFecha(fechaObjetivo);

            // 3) Si el usuario envió un filtro de texto, lo aplicamos (“Contains”):
            IEnumerable<EncuestaModelResponse> encuestasFiltradas = encuestasDelDia;

            if (!string.IsNullOrWhiteSpace(filtros.Filtro))
            {
                string textoFiltro = filtros.Filtro.Trim().ToLowerInvariant();

                encuestasFiltradas = encuestasDelDia.Where(e =>
                        // Campos string de Encuesta
                        (e.IdDocente?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                     || (e.IdPrograma?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                     || (e.IdAsignatura?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                     || (e.Jornada?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                     || (e.Categoria?.ToLowerInvariant().Contains(textoFiltro) ?? false)

                     // Campos booleanos de Encuesta
                     || e.FindeSemana.ToString().ToLowerInvariant().Contains(textoFiltro)
                     || e.Virtual.ToString().ToLowerInvariant().Contains(textoFiltro)

                     // Campos numéricos en cada ranking
                     || e.rankingModels.Any(r => r.Puntaje.ToString().Contains(textoFiltro))
                     || e.rankingModels.Any(r => r.Puesto.ToString().Contains(textoFiltro))
                     || e.rankingModels.Any(r => r.PuntajeAnterior.ToString().Contains(textoFiltro))
                     || e.rankingModels.Any(r => r.PuestoAnterior.ToString().Contains(textoFiltro))
                    );
            }


            // 4) Para cada encuesta filtrada, cargamos sus rankings completos (sin filtrar más)
            var listaFinal = new List<EncuestaModelResponse>();
            foreach (var encuesta in encuestasFiltradas)
            {
                var todosLosRankings = await _parametrizacionData.ObtenerRankingsPorEncuesta(encuesta.Id);
                encuesta.rankingModels = todosLosRankings.ToList();
                listaFinal.Add(encuesta);
            }

            return listaFinal;
        }
    }
}