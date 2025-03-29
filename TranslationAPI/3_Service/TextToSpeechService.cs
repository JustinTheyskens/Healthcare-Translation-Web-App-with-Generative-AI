using Google.Cloud.TextToSpeech.V1;
using Google.Protobuf;
using System.IO;
using System.Threading.Tasks;

public class TextToSpeechService
{
    private readonly TextToSpeechClient _textToSpeechClient;

    public TextToSpeechService()
    {
        // Initialize the Google Cloud Text-to-Speech client
        _textToSpeechClient = TextToSpeechClient.Create();
    }

    public async Task<string> ConvertTextToSpeechAsync(string text, string outputPath)
    {
        // Set up the Synthesis Input
        var input = new SynthesisInput
        {
            Text = text
        };

        // Set up the Voice parameters (e.g., English, Male, etc.)
        var voiceSelectionParams = new VoiceSelectionParams
        {
            LanguageCode = "en-US",  // Choose your language
            SsmlGender = SsmlVoiceGender.Female  // You can use Male or Female
        };

        // Set up the AudioConfig (output audio format)
        var audioConfig = new AudioConfig
        {
            AudioEncoding = AudioEncoding.Mp3  // Output audio format (MP3)
        };

        // Perform the text-to-speech conversion
        var response = await _textToSpeechClient.SynthesizeSpeechAsync(input, voiceSelectionParams, audioConfig);

        // Convert the AudioContent (ByteString) to a MemoryStream
        using (var memoryStream = new MemoryStream(response.AudioContent.ToByteArray()))
        {
            // Use FileStream to save the audio content to a file
            using (var outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                await memoryStream.CopyToAsync(outputFile);
            }
        }

        return $"Audio content written to file {outputPath}";
    }
}
