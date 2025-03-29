using OpenAI;
using OpenAI.Chat;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TranslationAPI.Models;

namespace TranslationAPI.Services
{
    public class OpenAIService
    {
        private readonly OpenAIClient _openAiClient;

        public OpenAIService(IOptions<OpenAiOptions> openAiOptions)
        {
            _openAiClient = new OpenAIClient(openAiOptions.Value.ApiKey);
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
