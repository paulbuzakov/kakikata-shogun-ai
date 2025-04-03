using KakikataShogun.Bot.Interfaces;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace KakikataShogun.Bot;

internal class TelegramBotHostedService(
    ITelegramBotClient telegramBotClient,
    ITelegramBotUpdateHandler updateHandler,
    ITelegramBotErrorHandler errorHandler
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        telegramBotClient.StartReceiving(
            updateHandler.HandleAsync,
            errorHandler.HandleErrorAsync,
            cancellationToken: cancellationToken
        );

        return Task.CompletedTask;
    }
}
