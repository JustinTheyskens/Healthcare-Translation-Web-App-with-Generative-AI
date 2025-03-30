using OpenAI;
using OpenAI.Chat;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TranslationAPI.Models;
using Google.Cloud.TextToSpeech.V1;
using System.IO;

namespace TranslationAPI.Services
{
    public class OpenAIService
    {
        private readonly OpenAIClient _openAiClient;

        public OpenAIService(IOptions<OpenAiOptions> openAiOptions)
        {
            _openAiClient = new OpenAIClient(openAiOptions.Value.ApiKey);
        }

            public void GenerateAudio()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "test_audio_google.mp3");

        // Create a client for the Google Cloud Text-to-Speech API
        var client = TextToSpeechClient.Create();

        // Set the input text
        var input = new SynthesisInput
        {
            Text = "Hello, this is a test of Google Cloud text-to-speech."
        };

        // Set the voice parameters
        var voice = new VoiceSelectionParams
        {
            LanguageCode = "en-US",
            SsmlGender = SsmlVoiceGender.Neutral
        };

        // Set the audio configuration (MP3 format)
        var audioConfig = new Google.Cloud.TextToSpeech.V1.AudioConfig
        {
            AudioEncoding = AudioEncoding.Mp3
        };

        // Call the API to synthesize the speech
        var response = client.SynthesizeSpeech(input, voice, audioConfig);

        // Write the audio content to a file
        using (var output = File.Create(filePath))
        {
            response.AudioContent.WriteTo(output);
        }

        Console.WriteLine($"Audio file created: {filePath}");
    }

        public async Task<string> ImproveTranslationAsync(string text)
        {
            var chatRequest = new ChatRequest(
                new List<Message>
                {
                    new Message(Role.System, "Improve the translation of the following text:"),
                    new Message(Role.User, text)
                },
                //Model.GPT4_Turbo
                "gpt-4"
            );

            var response = await _openAiClient.ChatEndpoint.GetCompletionAsync(chatRequest);

            // if (response?.Choices != null && response.Choices.Count > 0)
            // {
            //     return response.Choices[0].Message.Content.Trim();
            // }

            if (response?.Choices != null && response.Choices.Count > 0)
            {
                // Convert JsonElement to string first before calling Trim
                return response.Choices[0].Message.Content.ToString().Trim();
            }

            // if (response.Successful && response.Choices.Count > 0)
            // {
            //     return response.Choices[0].Message.Content.Trim();
            // }

            return "Translation enhancement failed.";
        }
    }
}
