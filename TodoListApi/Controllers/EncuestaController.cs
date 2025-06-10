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
        private readonly IEncuestaBusiness _encuestaBusiness;

        public EncuestaController(IEncuestaBusiness encuestaBusiness)
        {
            _encuestaBusiness = encuestaBusiness;
        }

        [HttpGet]
        [Route("ConsultarDashboard")]
        public async Task<ActionResult> ConsultarDashboardGet([FromQuery] DashboardRequest request)
        {
            try
            {
                var resultado = await _encuestaBusiness.ObtenerEncuestasConRanking(request);
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
