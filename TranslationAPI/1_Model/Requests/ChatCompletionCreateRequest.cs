using OpenAI.Chat;

namespace TranslationAPI.Model;

public class ChatCompletionCreateRequest() 
{
    public List<Message> Messages { get; set; }
}