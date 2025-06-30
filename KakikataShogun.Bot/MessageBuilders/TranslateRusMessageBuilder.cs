using KakikataShogun.Bot.Interfaces;
using OpenAI.Chat;

namespace KakikataShogun.Bot.MessageBuilders;

internal class TranslateRusMessageBuilder(ChatClient openAiClient) : IMessageBuilder
{
    public string CommandPattern => "/ru";

    public async Task<string[]> BuildMessageAsync(
        string message,
        CancellationToken cancellationToken
    )
    {
        try
        {
            ChatCompletion completion = await openAiClient.CompleteChatAsync(
                @$"
Ты — ИИ-помощник, специализирующийся на русском языке.
Твоя задача — переводить мои английские фразы на естественный, грамотный и звучащий по-русски язык.
Убедись, что грамматика, лексика и тональность соответствуют разговорному, профессиональному или формальному стилю — в зависимости от исходной фразы.
Если нужно, дай краткое объяснение перевода.
Вот моя фраза:
'{message.Replace("/ru", "")}'.
Пожалуйста, переведи её на русский и сделай, чтобы она звучала естественно.
Первым сообщением должен быть твой перевод моей фразы.
Вторым — объяснение перевода.
Между сообщениями вставь строку '---'."
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
