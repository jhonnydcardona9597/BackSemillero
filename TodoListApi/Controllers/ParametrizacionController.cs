using BackSemillero.Business;
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
        [HttpPost]
        [Route("CreateQR")]
        public async Task<ActionResult> CreateQR(QrModelRequest qrModelRequest)
        {
            try
            {
                var result = await _parametrizacionBusiness.GenerarQr(qrModelRequest);
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
