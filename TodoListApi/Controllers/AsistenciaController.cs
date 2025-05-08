using System.Threading.Tasks;
using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaBusiness _business;
        public AsistenciaController(IAsistenciaBusiness business)
        {
            _business = business;
        }

        // POST: AsistenciaController/RegistrarAsistencia
        [HttpPost(Name = "RegistrarAsistencia")]
        public async Task<ActionResult<AsistenciaResponse>> RegistrarAsistencia(AsistenciaModelRequest asistenciaModelRequest)
        {
            var result = await _business.RegistrarAsistencia(asistenciaModelRequest);
            if (!result.Registrada) return BadRequest(result.Mensaje);
            return Ok(result);
        }
    }
}
