using BackSemillero.Business.Interfaces;
using BackSemillero.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackSemillero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParametrizacionController : ControllerBase
    {
        private readonly IParametrizacionBusiness _parametrizacionBusiness;
        public ParametrizacionController(IParametrizacionBusiness parametrizacionBusiness)
        {
            _parametrizacionBusiness = parametrizacionBusiness;
        }

        // POST: ParametrizacionController/CreateQR
        [HttpPost(Name = "CreateQR")]
        public async Task<ActionResult<QrModelResponse>> CreateQR(QrModelRequest qrModelRequest)
        {
            return Ok(await _parametrizacionBusiness.GenerarQr(qrModelRequest));
        }
        
    }
}
