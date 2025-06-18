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

        // GET /Supervisor/DetalleEncuestas?fecha=2025-06-06
        [HttpGet]
        [Route("DetalleEncuestas")]
        public async Task<IActionResult> ConsultarDetalle([FromQuery] DateTime? fecha)
        {
            try
            {
                var data = await _supervisorBusiness.ObtenerDetalleEncuestas(fecha);
                return Ok(new
                {
                    Code = 200,
                    Message = string.Empty,
                    Data = data
                });
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

        // GET /Supervisor/HistorialEncuestas
        [HttpGet]
        [Route("HistorialEncuestas")]
        public async Task<IActionResult> ConsultarHistorial()
        {
            try
            {
                var data = await _supervisorBusiness.ObtenerHistorialEncuestas();
                return Ok(new
                {
                    Code = 200,
                    Message = string.Empty,
                    Data = data
                });
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
