using ESignerDemo.Common;
using ESignerDemo.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EsignerDemo.Mvc.Controllers
{
    public class Demo1Controller(ESignerService eSignerService, Settings settings) : Controller
    {
        public IActionResult Index()
        {
            this.ViewData["ESignerLoggedin"] = null;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ESignerLogin()
        {
            if (await eSignerService.Login())
            {
                this.ViewData["ESignerLoggedin"] = true;

                return this.View("Index");
            }

            this.ViewData["ESignerLoggedin"] = false;

            return this.View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SanadInit()
        {
            var request = new SanadInitRequest
            {
                NationalId = "9831001234",
                RedirectUrl = settings.RedirectUrl
            };

            this.ViewData["SanadInitResponse"] = null;

            var resp = await eSignerService.SanadInit(request);

            if (resp == null)
            {
                return this.View("Index");
            }

            this.ViewData["SanadInitResponse"] = resp;

            return this.Redirect(resp.AuthUrl);
        }

        [HttpGet]
        [Route("callback")]
        public IActionResult Callback([FromQuery] CallbackQuery query)
        {
            if (!string.IsNullOrEmpty(query.Error))
            {
                return this.BadRequest(query.Error);
            }

            if (!string.IsNullOrEmpty(query.PinVerifyUrl))
            {
                return this.Redirect(query.PinVerifyUrl);
            }

            if (!query.CanSign.Value)
            {
                return this.View("Error");
            }

            this.ViewData["CallbackQuery"] = query;

            return this.View("Sign1");
        }

        [HttpPost]
        public async Task<IActionResult> AdvancedSign(string sessionId)
        {
            var request = new EnvelopRequest
            {
                Data = Helper.PdfBase64,
                SessionId = sessionId
            };

            var response = await eSignerService.AdvancedSign(request);

            this.ViewData["EnvelopeId"] = response.EnvelopeId;

            return this.View("Sign1");
        }

        //private void SetViewData(TokenResponse response)
        //{
        //    this.ViewData["access_token"] = response.AccessToken;
        //    this.ViewData["refresh_token"] = response.RefreshToken;
        //    this.ViewData["expires_in"] = response.ExpiresIn;
        //}
    }
}
