using ESignerDemo.Common;
using ESignerDemo.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ESignerDemoWasmApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ESignerController(ESignerService eSignerService, Settings settings) : ControllerBase
    {
        private static IDictionary<string, SanadInitRequest> cache = new Dictionary<string, SanadInitRequest>();

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
                cache.Add(resp.SessionId, request);
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

            // User completed authentication with SANAD SSO, next step to verify PIN for signing.
            if (!string.IsNullOrEmpty(query.PinVerifyUrl))
            {
                return this.Redirect(query.PinVerifyUrl);
            }

            if (query.ReadyToSign.Value)
            {
                if (cache.TryGetValue(query.SessionId, out var request) && !string.IsNullOrWhiteSpace(request.SigningPage))
                {
                    return this.Redirect($"{request.SigningPage}/{query.SessionId}");
                }

                return this.Redirect($"{settings.SignPageUrl}/{query.SessionId}");
            }

            return this.Unauthorized();
        }
    }
}
