using BackSemillero.Business;
using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfesorController: ControllerBase
    {
        private readonly IProfesorBusiness _profesorBusiness;

        public ProfesorController(IProfesorBusiness profesorBusiness)
        {
            _profesorBusiness = profesorBusiness;
        }

        //Get Consulta profesor

        [HttpGet]
        [Route("GetProfesor")]
        public async Task<ActionResult<List<ProgramaModel>>> GetProfesor(string CedulaProfesor)
        {
            try
            {
                var result = await _profesorBusiness.ConsultarProfesor(CedulaProfesor);
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
                    Code = ex.InnerException?.Message ?? "400",
                    Message = ex.Message,
                    Data = Empty
                });
            }
        }

        //Get Consulta detalle profesor

        [HttpGet]
        [Route("GetDetalleProfesor")]
        public async Task<ActionResult<List<ProgramaModel>>> GetDetalleProfesor(string CedulaProfesor)
        {
            try
            {
                var result = await _profesorBusiness.ConsultarDetalleProfesor(CedulaProfesor);
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
                    Code = ex.InnerException?.Message ?? "400",
                    Message = ex.Message,
                    Data = Empty
                });
            }
        }
    }
}
