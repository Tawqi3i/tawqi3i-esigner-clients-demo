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
                RedirectUri = settings.RedirectUrl
            };

            var resp = await eSignerService.SanadInit(request);

            if (resp == null)
            {
                this.ViewData["SanadInitResponse"] = "Failed to initialize Sanad";
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

            //Console.WriteLine(query.Code + "," + query.State);

            //// Validate state

            //var tokenResponse = this.tawqi3iService.GetToken(query.Code, CodeVerifier, this.appSettings.ApiKey, this.appSettings.SecretKey, this.appSettings.CallbackUrl);

            //this.SetViewData(tokenResponse);

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
