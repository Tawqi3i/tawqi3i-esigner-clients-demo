using Microsoft.AspNetCore.Mvc;

namespace ESignerDemo.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Health()
        {
            return this.Ok("I am fine don't worry!");
        }
    }
}
