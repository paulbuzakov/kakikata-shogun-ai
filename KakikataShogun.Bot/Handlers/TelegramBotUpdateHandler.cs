using System.Text;
using KakikataShogun.Bot.Interfaces;
using Microsoft.Extensions.Configuration;
using PostHog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KakikataShogun.Bot.Handlers;

internal class TelegramBotUpdateHandler(
    IMessageBuilderFactory messageBuilderFactory,
    IPostHogClient postHogClient,
    IConfiguration configuration
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
            using var httpClient = new HttpClient();

            var apiToken = configuration.GetValue("OpenAI:Token", string.Empty);

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
            var jsonPayload = new
            {
                model = "gpt-4o-mini-tts",
                input = nextMessages.FirstOrDefault(),
                voice = "ash",
            };
            // Serialize the object to a JSON string
            string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonPayload);

            // Create the StringContent with the JSON payload
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(
                "https://api.openai.com/v1/audio/speech",
                content,
                cancellationToken
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception("");

            var responseContent = await response.Content.ReadAsStreamAsync(cancellationToken);

            await client.SendVoice(
                chatId: update.Message.Chat.Id,
                voice: InputFile.FromStream(responseContent),
                caption: nextMessages.FirstOrDefault(),
                cancellationToken: cancellationToken
            );

            await client.SendMessage(
                chatId: update.Message.Chat.Id,
                text: nextMessages.LastOrDefault() ?? string.Empty,
                cancellationToken: cancellationToken
            );
        }
    }
}
