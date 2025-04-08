using KakikataShogun.Bot.Interfaces;
using PostHog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KakikataShogun.Bot.Handlers;

internal class TelegramBotUpdateHandler(
    IMessageBuilderFactory messageBuilderFactory,
    IPostHogClient postHogClient
) : ITelegramBotUpdateHandler
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

        postHogClient.Capture(
            Consts.AppName,
            messageBuilder.CommandPattern,
            properties: new()
            {
                ["chatId"] = update?.Message?.Chat.Id.ToString() ?? "UNKNOWN_CHAT_ID",
                ["name"] = update?.Message?.From?.ToString() ?? "UNKNOWN_USER",
                ["messageText"] = update?.Message?.Text ?? string.Empty,
            }
        );

        var nextMessages = await messageBuilder.BuildMessageAsync(
            update?.Message?.Text ?? string.Empty,
            cancellationToken
        );

        if (update?.Message?.Chat.Id is not null)
        {
            foreach (var message in nextMessages)
            {
                await client.SendMessage(
                    chatId: update.Message.Chat.Id,
                    text: message,
                    cancellationToken: cancellationToken
                );
            }
        }
    }
}
