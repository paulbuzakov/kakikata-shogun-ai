using KakikataShogun.Bot.Interfaces;
using OpenAI.Chat;

namespace KakikataShogun.Bot.MessageBuilders;

internal class TranslateEngMessageBuilder(ChatClient openAiClient) : IMessageBuilder
{
    public string CommandPattern => "default";

    public async Task<string[]> BuildMessageAsync(
        string message,
        CancellationToken cancellationToken
    )
    {
        try
        {
            ChatCompletion completion = await openAiClient.CompleteChatAsync(
                @$"
You are an AI language assistant specializing in American English.
Your task is to refine and correct my English phrases to sound natural, fluent, and like a native American speaker.
Ensure the grammar, vocabulary, and tone are appropriate for casual, professional, or formal contexts based on the phrase.
If needed, provide a brief explanation of the changes.
Here is my phrase:
'{message}'.
Please correct it and make it sound natural.
First message of your answer should be you version of my phrase.
Second message of your answer should be explanation.
Insert between messages '---' string."
            );

            var answer = completion.Content.Select(i => i.Text).FirstOrDefault() ?? String.Empty;

            var messages = answer.Split("---");

            return [messages[0], messages[1]];
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);

            return [];
        }
    }
}
