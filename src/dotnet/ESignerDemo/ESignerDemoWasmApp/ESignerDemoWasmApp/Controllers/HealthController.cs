using Microsoft.AspNetCore.Mvc;

namespace ESignerDemoWasmApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Health()
        {
            return this.Ok("Don't worry i am healthy!");
        }
    }
}
