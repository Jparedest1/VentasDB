using Microsoft.AspNetCore.Mvc;

namespace VentasDB.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API funcionando");
        }
    }
}