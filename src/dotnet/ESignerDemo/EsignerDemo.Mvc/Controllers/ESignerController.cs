using ESignerDemo.Common;
using ESignerDemo.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EsignerDemo.Mvc.Controllers
{
    public class ESignerController(ESignerService eSignerService, Settings settings) : Controller
    {
        [HttpPost]
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

            // At this point, we assume the user has successfully authenticated and can sign.
            // Redirect user to your signing page to review document and consent before signing.

            if (query.CanSign.Value)
            {
                Console.WriteLine("RedirectUrl:" + settings.RedirectUrl);
                Console.WriteLine("SignPageUrl:" + settings.SignPageUrl);

                return this.Redirect($"{settings.SignPageUrl}/{query.SessionId}");
            }

            return this.Unauthorized();
        }
    }
}
