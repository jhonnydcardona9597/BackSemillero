using BackSemillero.Business;
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
        private readonly ISupervisorBusiness _supervisorBusiness;

        public SupervisorController(ISupervisorBusiness supervisorBusiness)
        {
            _supervisorBusiness = supervisorBusiness;
        }

        // GET /Supervisor/EstadoEnvio?fechaBuscada=2025-06-05
        [HttpGet]
        [Route("EstadoEnvio")]
        public async Task<IActionResult> EstadoEnvio([FromQuery] DateTime fechaBuscada)
        {
            try
            {
                var resultado = await _supervisorBusiness.ObtenerEstadoEnvio(fechaBuscada);
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