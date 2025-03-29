using Google.Cloud.Speech.V1;
using System;
using System.IO;
using System.Threading.Tasks;

public class SpeechToTextService
{
    private readonly SpeechClient _speechClient;

    public SpeechToTextService()
    {
        _speechClient = SpeechClient.Create();
    }

    public async Task<string> ConvertSpeechToTextAsync(string audioFilePath)
    {
        //debugging confirm path
        // Console.WriteLine("Audio File Path: " + audioFilePath);


        var audio = new RecognitionAudio
        {
            Content = Google.Protobuf.ByteString.CopyFrom(File.ReadAllBytes(audioFilePath))
        };

        // Set the recognition config (language and encoding type)
        var config = new RecognitionConfig
        {
            Encoding = RecognitionConfig.Types.AudioEncoding.Linear16, //AudioEncoding.Linear16,  
            SampleRateHertz = 16000,  
            LanguageCode = "en-US"  // English
        };

        var response = await _speechClient.RecognizeAsync(config, audio);

        //debugging response
        // Console.WriteLine("Response: " + response);

        if (response.Results.Count > 0)
        {
            var transcript = response.Results[0].Alternatives[0].Transcript;
            return transcript;
        }
        else
        {
            return "No speech detected.";
        }
    }
}
