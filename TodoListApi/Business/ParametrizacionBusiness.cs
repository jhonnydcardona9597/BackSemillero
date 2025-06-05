using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSemillero.Business
{
    public class ParametrizacionBusiness : IParametrizacionBusiness
    {
        private readonly IParametrizacionData _parametrizacionData;

        public ParametrizacionBusiness(IParametrizacionData parametrizacionData)
        {
            _parametrizacionData = parametrizacionData;
        }

        public async Task<IEnumerable<EncuestaReponseModel>> ObtenerEncuestasConRanking(DashboardRequest filtros)
        {
            // 1) Fecha objetivo: si viene filtros.Fecha, la usamos; si no, tomamos “hoy”
            DateTime fechaObjetivo = filtros.Fecha?.Date ?? DateTime.Now.Date;

            // 2) Traer encuestas de la fecha (o fecha anterior con datos)
            var encuestasDelDia = await _parametrizacionData.ObtenerEncuestasPorFecha(fechaObjetivo);

            // 3) Si el usuario envió un filtro de texto, lo aplicamos (“Contains”):
            IEnumerable<EncuestaReponseModel> encuestasFiltradas = encuestasDelDia;

            if (!string.IsNullOrWhiteSpace(filtros.Filtro))
            {
                string textoFiltro = filtros.Filtro.Trim().ToLowerInvariant();

                encuestasFiltradas = encuestasDelDia.Where(e =>
                    (e.IdDocente?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                    || (e.IdPrograma?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                    || (e.IdAsignatura?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                    || (e.Jornada?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                    || (e.Categoria?.ToLowerInvariant().Contains(textoFiltro) ?? false)
                );
            }

            // 4) Para cada encuesta filtrada, cargamos sus rankings completos (sin filtrar más)
            var listaFinal = new List<EncuestaReponseModel>();
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
