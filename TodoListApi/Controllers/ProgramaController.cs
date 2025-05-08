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


        [HttpGet(Name = "GetPrograma")]
        public async Task<ActionResult<List<ProgramaModel>>> GetPrograma()
        {
            return Ok(await _programaBusiness.ConsultarPrograma());
        }

    }
}
