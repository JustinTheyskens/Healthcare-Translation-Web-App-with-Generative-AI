using System;
using System.IO;
using System.Linq;

public class AudioGenerationExample
{
    public void TestTextToSpeech()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "test_audio.wav");

        byte[] dummyAudioData = new byte[44100 * 2 * 1 * 2]; // Silence, 1 second of 16-bit mono audio

        try
        {
            
            File.WriteAllBytes(filePath, dummyAudioData);
            Console.WriteLine($"Audio file created: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating audio file: {ex.Message}");
        }
    }
}
