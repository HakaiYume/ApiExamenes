using Microsoft.AspNetCore.Mvc;

namespace ApiExamne.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Redirect("/index.html");
        }
    }
}
