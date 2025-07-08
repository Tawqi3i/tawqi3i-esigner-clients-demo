using Microsoft.AspNetCore.Mvc;

namespace ESignerDemoWasmApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigningController : ControllerBase
    {
        [HttpPost("api/callback")]
        public async Task<IActionResult> Callback([FromBody] object request)
        {
            return this.Ok();
        }

    }
}
