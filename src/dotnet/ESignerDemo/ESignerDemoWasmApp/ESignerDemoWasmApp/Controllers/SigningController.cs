using Microsoft.AspNetCore.Mvc;

namespace ESignerDemoWasmApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigningController : ControllerBase
    {
        //[HttpPost("/api/callback")]
        [HttpGet("/api/callback")]
        public async Task<IActionResult> Callback([FromQuery] string pinVerifyUrl)
        {
            return this.Redirect(pinVerifyUrl);
        }
    }
}
