using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncuestaController : ControllerBase
    {
        private readonly IEncuestaBusiness _parametrizacionBusiness;

        public EncuestaController(IEncuestaBusiness parametrizacionBusiness)
        {
            _parametrizacionBusiness = parametrizacionBusiness;
        }

        /// <summary>
        /// GET /Parametrizacion/ConsultarDashboard
        /// Parámetros opcionales en query string: 
        ///   fecha , filtro (string).
        /// </summary>
        [HttpGet]
        [Route("ConsultarDashboard")]
        public async Task<ActionResult> ConsultarDashboardGet([FromQuery] DashboardRequest request)
        {
            try
            {
                var resultado = await _parametrizacionBusiness.ObtenerEncuestasConRanking(request);
                return Ok(new
                {
                    Code = 200,
                    Message = "",
                    Data = resultado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Code = ex.InnerException?.Message ?? "400",
                    Message = ex.Message,
                    Data = (object?)null
                });
            }
        }

        /// <summary>
        /// POST /Parametrizacion/ConsultarDashboard
        /// Body (application/json):
        /// {
        ///   "fecha": "2025-06-05T00:00:00",
        ///   "filtro": "SomeText"
        /// }
        /// </summary>
        [HttpPost]
        [Route("ConsultarDashboard")]
        public async Task<ActionResult> ConsultarDashboardPost(DashboardRequest request)
        {
            try
            {
                var resultado = await _parametrizacionBusiness.ObtenerEncuestasConRanking(request);
                return Ok(new
                {
                    Code = 200,
                    Message = "",
                    Data = resultado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Code = ex.InnerException?.Message ?? "400",
                    Message = ex.Message,
                    Data = (object?)null
                });
            }
        }
    }
}
