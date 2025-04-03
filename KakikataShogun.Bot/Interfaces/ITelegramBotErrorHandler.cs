using Telegram.Bot;
using Telegram.Bot.Polling;

namespace KakikataShogun.Bot.Interfaces;

internal interface ITelegramBotErrorHandler
{
    Task HandleErrorAsync(
        ITelegramBotClient client,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken
    );
}
