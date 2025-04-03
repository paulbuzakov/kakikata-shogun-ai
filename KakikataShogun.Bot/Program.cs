using KakikataShogun.Bot;
using KakikataShogun.Bot.Handlers;
using KakikataShogun.Bot.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(
        (context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile("appsettings.user.json", optional: true, reloadOnChange: true);
        }
    )
    .ConfigureServices(
        (context, services) =>
        {
            var configuration = context.Configuration;

            services.AddHostedService<TelegramBotHostedService>();

            services.AddTransient<ITelegramBotUpdateHandler, TelegramBotUpdateHandler>();
            services.AddTransient<ITelegramBotErrorHandler, TelegramBotErrorHandler>();
            services.AddSingleton<ITelegramBotClient>(
                new TelegramBotClient(configuration.GetValue("TelegramBot:Token", string.Empty))
            );
        }
    )
    .Build();

await host.RunAsync(default);
