using Google.Cloud.Translation.V2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TranslationAPI.Models;
using TranslationAPI.Services;

namespace TranslationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly TranslationClient _translationClient;

        public TranslationController()
        {
            _translationClient = TranslationClient.Create();
        }

        [HttpPost("translate")]
        public async Task<IActionResult> TranslateText([FromBody] TranslationRequest request)
        {
            try
            {
                var response = await _translationClient.TranslateTextAsync(
                    request.Text, 
                    targetLanguage: request.TargetLanguage); 
                
                return Ok(new { translatedText = response.TranslatedText });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Translation failed", error = ex.Message });
            }
        }
    }
}

// namespace TranslationAPI.Controllers  // Ensure this matches your project namespace
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class TranslationController : ControllerBase
//     {
//         private readonly OpenAIService _openAiService;

//         public TranslationController(OpenAIService openAiService)
//         {
//             _openAiService = openAiService;
//         }

//         [HttpPost("translate")]
//         public async Task<IActionResult> TranslateText([FromBody] TranslationRequest request)
//         {
//             try
//             {
//                 var translationResult = await _openAiService.ImproveTranslationAsync(request.Text);
//                 return Ok(new { translatedText = translationResult });
//             }
//             catch (Exception ex)
//             {
//                 return BadRequest(new { message = "Translation failed", error = ex.Message });
//             }
//         }
//     }
// }
