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

        [HttpGet]
        [Route("ConsultarAsignaturasXPrograma")]
        public async Task<ActionResult> ConsultarAsignaturasXPrograma(int IdPrograma)
        {
            try
            {
                var result = await _asignaturaBusiness.ConsultaAsignaturasXPrograma(IdPrograma);
                return Ok(new
                {
                    Code = 200,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Code = 400,
                    Message = ex.Message,
                    Data = Empty
                });
            }
        }
    }
}
