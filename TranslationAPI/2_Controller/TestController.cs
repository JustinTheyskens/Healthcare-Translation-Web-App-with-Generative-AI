using Microsoft.AspNetCore.Mvc;

namespace TranslationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }
    }
}