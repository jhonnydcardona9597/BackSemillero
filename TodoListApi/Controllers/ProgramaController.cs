using Microsoft.AspNetCore.Mvc;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramaController : ControllerBase
    {
        [HttpGet(Name = "GetPrograma")]
        public void GetPrograma()
        {
            
        }
    }
}
