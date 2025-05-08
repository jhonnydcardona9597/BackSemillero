using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsignaturaController : ControllerBase
    {
        private readonly IAsignaturaBusiness _asignaturaBusiness;
        public AsignaturaController(IAsignaturaBusiness asignaturaBusiness)
        {
            _asignaturaBusiness = asignaturaBusiness;
        }

        // GET: AsignaturaController/ConsultarAsignaturasXPrograma
        [HttpGet(Name = "ConsultarAsignaturasXPrograma")]
        public async Task<ActionResult<AsignaturaModelResponse>> ConsultarAsignaturasXPrograma(int IdPrograma)
        {
            return Ok(await _asignaturaBusiness.ConsultaAsignaturasXPrograma(IdPrograma));
        }
    }
}
