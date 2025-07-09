using ESignerDemoWasmApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESignerDemoWasmApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ESignerController(ESignerService eSignerService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            if (await eSignerService.Login())
            {
                return this.Ok();
            }

            return this.BadRequest("Login failed. Please try again.");
        }

        [HttpGet("/api/callback")]
        public async Task<IActionResult> Callback([FromQuery] string pinVerifyUrl)
        {
            return this.Redirect(pinVerifyUrl);
        }
    }
}
