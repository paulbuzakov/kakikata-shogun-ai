using KakikataShogun.Bot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KakikataShogun.Bot.Handlers;

internal class TelegramBotUpdateHandler : ITelegramBotUpdateHandler
{
    public async Task HandleAsync(
        ITelegramBotClient client,
        Update update,
        CancellationToken cancellationToken
    )
    {
        if (update.Message?.Text != null)
        {
            await client.SendMessage(
                chatId: update.Message.Chat.Id,
                text: $"You said: {update.Message.Text}",
                cancellationToken: cancellationToken
            );
        }
    }
}
