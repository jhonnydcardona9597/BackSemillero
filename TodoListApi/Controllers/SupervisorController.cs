using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorBusiness _business;

        public SupervisorController(ISupervisorBusiness business)
        {
            _business = business;
        }

        // GET /Supervisor/EstadoEnvio?fechaBuscada=2025-06-05
        [HttpGet]
        [Route("EstadoEnvio")]
        public async Task<IActionResult> EstadoEnvio([FromQuery] DateTime fechaBuscada)
        {
            try
            {
                var dto = await _business.ObtenerEstadoEnvio(fechaBuscada);
                return Ok(new { Code = 200, Message = "", Data = dto });
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
