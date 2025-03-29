using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TextController : ControllerBase
{
    private readonly TextToSpeechService _textToSpeechService;

    public TextController(TextToSpeechService textToSpeechService)
    {
        _textToSpeechService = textToSpeechService;
    }

    // POST api/text-to-speech
    [HttpPost("text-to-speech")]
    public async Task<IActionResult> ConvertTextToSpeech([FromBody] TextToSpeechRequest request)
    {
        if (string.IsNullOrEmpty(request.Text))
        {
            return BadRequest(new { message = "Text is required" });
        }

        string outputPath = $"C:/Users/Justi/Desktop/{request.FileName}.mp3";  // Save output as MP3 file on the Desktop
        var result = await _textToSpeechService.ConvertTextToSpeechAsync(request.Text, outputPath);

        return Ok(new { message = result, filePath = outputPath });
    }
}
