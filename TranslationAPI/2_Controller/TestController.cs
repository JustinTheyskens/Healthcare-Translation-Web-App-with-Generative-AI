using Microsoft.AspNetCore.Mvc;

namespace TranslationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// A simple endpoint to check if the API is running.
        /// </summary>
        /// <returns>A success message.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }
    }
}