using ESignerDemo.Common;
using ESignerDemo.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ESignerDemo.Backend.Controllers
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

        [HttpPost("sanad/init")]
        public async Task<IActionResult> SanadInit([FromBody] SanadInitRequest request)
        {
            var resp = await eSignerService.SanadInit(request);
            if (resp != null)
            {
                return this.Ok(resp);
            }

            return this.BadRequest();
        }

        [HttpPost("sign/advanced")]
        public async Task<IActionResult> AdvancedSign([FromBody] EnvelopRequest request)
        {
            var resp = await eSignerService.AdvancedSign(request);
            if (resp != null)
            {
                return this.Ok(resp);
            }

            return this.BadRequest();
        }

        [HttpGet("/esigner/callback")]
        public async Task<IActionResult> Callback([FromQuery] CallbackQuery query)
        {
            if (!string.IsNullOrEmpty(query.Error))
            {
                return this.BadRequest(query.Error);
            }

            if (!string.IsNullOrEmpty(query.PinVerifyUrl))
            {
                return this.Redirect(query.PinVerifyUrl);
            }

            if (query.CanSign.Value == true)
            {
                return this.Redirect($"http://localhost:5016/signing/{query.SessionId}");
            }

            return this.Unauthorized();
        }
    }
}
