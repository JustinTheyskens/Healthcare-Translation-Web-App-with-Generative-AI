using Google.Cloud.Translation.V2;
using Microsoft.AspNetCore.Mvc;

public class AudioFileRequest
{
    public string AudioFilePath { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class SpeechController : ControllerBase
{
    private readonly SpeechToTextService _speechToTextService;

    public SpeechController()
    {
        _speechToTextService = new SpeechToTextService();
    }
    

    [HttpPost("speech-to-text")]
    public async Task<IActionResult> SpeechToText([FromBody] AudioFileRequest request)
    {
        if (string.IsNullOrEmpty(request.AudioFilePath))
        {
            return BadRequest(new { message = "Audio file path is required" });
        }

        try
        {
            var result = await _speechToTextService.ConvertSpeechToTextAsync(request.AudioFilePath);
            return Ok(new { transcript = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Speech-to-text failed", error = ex.Message });
        }
    }
}
