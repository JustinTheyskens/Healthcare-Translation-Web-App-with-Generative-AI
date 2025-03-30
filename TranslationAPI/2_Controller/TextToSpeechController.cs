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

        if (string.IsNullOrEmpty(request.DestinationPath)) // || !Directory.Exists(request.DestinationPath))
        {
            return BadRequest(new { message = "Destination Path Required." });
        }

        if (!request.DestinationPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            request.DestinationPath += Path.DirectorySeparatorChar; // Add the separator if missing
        }       

        if (string.IsNullOrEmpty(request.FileName) || Path.GetInvalidFileNameChars().Any(c => request.FileName.Contains(c)))
        {
            return BadRequest(new { message = "Please provide a valid file name." });
        }

        string outputPath = Path.Combine(request.DestinationPath, $"{request.FileName}.mp3");

        var result = await _textToSpeechService.ConvertTextToSpeechAsync(request.Text, outputPath);

        // Log the output path for debugging 
        Console.WriteLine($"Output Path: {outputPath}");

        if (string.IsNullOrEmpty(result))
        {
            return StatusCode(500, new { message = "Failed to convert text to speech"});
        }

        return Ok(new { message = "Conversion successful", filePath = outputPath });
    }
}