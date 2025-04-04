using KakikataShogun.Bot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KakikataShogun.Bot.Handlers;

internal class TelegramBotUpdateHandler(IMessageBuilderFactory messageBuilderFactory)
    : ITelegramBotUpdateHandler
{
    public async Task HandleAsync(
        ITelegramBotClient client,
        Update update,
        CancellationToken cancellationToken
    )
    {
        var messageBuilder = messageBuilderFactory.CreateMessageBuilder(
            update.Message?.Text ?? string.Empty
        );

        var messageText = await messageBuilder.BuildMessageAsync(
            update.Message?.Text ?? string.Empty,
            cancellationToken
        );

        if (update.Message?.Chat.Id is not null)
        {
            await client.SendMessage(
                chatId: update.Message.Chat.Id,
                text: messageText,
                cancellationToken: cancellationToken
            );
        }
    }
}
