using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("api/asistencia")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaBusiness _asistenciaBusiness;

        public AsistenciaController(IAsistenciaBusiness asistenciaBusiness)
            => _asistenciaBusiness = asistenciaBusiness;

        [HttpPost]
        [Route("CreateAsistencia")]
        public async Task<ActionResult> CreateAsistencia(AsistenciaModelRequest asistenciaModelRequest)
        {
            try
            {
                var result = await _asistenciaBusiness.RegistrarAsistencia(asistenciaModelRequest);
                return Ok(new { Code = 200, Message = "", Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Code = 400, Message = ex.Message, Data = string.Empty });
            }
        }
    }
}
