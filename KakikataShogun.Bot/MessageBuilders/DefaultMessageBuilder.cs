using System.Text;
using KakikataShogun.Bot.Interfaces;
using OpenAI.Chat;

namespace KakikataShogun.Bot.MessageBuilders;

internal class DefaultMessageBuilder(ChatClient openAIClient) : IMessageBuilder
{
    public string CommandPattern => "default";

    public async Task<string> BuildMessageAsync(string message, CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();

        try
        {
            ChatCompletion completion = await openAIClient.CompleteChatAsync(
                @$"
You are an AI language assistant specializing in American English.
Your task is to refine and correct my English phrases to sound natural, fluent, and like a native American speaker.
Ensure the grammar, vocabulary, and tone are appropriate for casual, professional, or formal contexts based on the phrase.
If needed, provide a brief explanation of the changes.
Here is my phrase:
'{message}'.

Please correct it and make it sound natural."
            );

            foreach (var content in completion.Content)
            {
                sb.Append(content.Text);
            }
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);

            sb.Append($"Error: {ex.Message}");
        }

        return sb.ToString();
    }
}
