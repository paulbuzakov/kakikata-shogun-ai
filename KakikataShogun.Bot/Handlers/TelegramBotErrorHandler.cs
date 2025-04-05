using KakikataShogun.Bot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace KakikataShogun.Bot.Handlers;

internal class TelegramBotErrorHandler : ITelegramBotErrorHandler
{
    public Task HandleErrorAsync(
        ITelegramBotClient client,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken
    )
    {
        SentrySdk.CaptureException(exception);

        return Task.CompletedTask;
    }
}
