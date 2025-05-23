using BackSemillero.Business;
using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Models;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramaController : ControllerBase
    {
        private readonly IProgramaBusiness _programaBusiness;

        public ProgramaController(IProgramaBusiness programaBusiness)
        {
            _programaBusiness = programaBusiness;
        }

        //Get Consulta programa

        [HttpGet]
        [Route("GetPrograma")]
        public async Task<ActionResult<List<ProgramaModel>>> GetPrograma()
        {
            try
            {
                var result = await _programaBusiness.ConsultarPrograma();
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
