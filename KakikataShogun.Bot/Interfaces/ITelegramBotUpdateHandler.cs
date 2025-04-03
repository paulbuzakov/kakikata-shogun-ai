using Telegram.Bot;
using Telegram.Bot.Types;

namespace KakikataShogun.Bot.Interfaces;

internal interface ITelegramBotUpdateHandler
{
    Task HandleAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken);
}
