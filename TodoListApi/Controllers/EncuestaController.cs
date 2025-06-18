using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> ConsultarDashboard([FromQuery] DashboardRequest request)
        {
            try
            {
                var data = await _encuestaBusiness.ObtenerEncuestas(request);
                return Ok(new { Code = 200, Message = "", Data = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Code = ex.InnerException?.Message ?? "400",
                    Message = ex.Message,
                    Data = Empty
                });
            }
        }
    }
}
