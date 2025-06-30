using KakikataShogun.Bot;
using KakikataShogun.Bot.Handlers;
using KakikataShogun.Bot.Interfaces;
using KakikataShogun.Bot.MessageBuilders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Chat;
using PostHog;
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

            SentrySdk.Init(options =>
            {
                options.Dsn = configuration.GetValue("Sentry:Dsn", string.Empty);
                options.Debug = true;
            });
            services.AddSingleton<IPostHogClient>(
                new PostHogClient(
                    new PostHogOptions
                    {
                        ProjectApiKey = configuration.GetValue(
                            "PostHog:ProjectApiKey",
                            string.Empty
                        ),
                        HostUrl = new Uri(configuration.GetValue("PostHog:Host", string.Empty)),
                        PersonalApiKey = configuration.GetValue(
                            "PostHog:PersonalApiKey",
                            string.Empty
                        ),
                    }
                )
            );

            services.AddHostedService<TelegramBotHostedService>();

            services.AddTransient<ITelegramBotUpdateHandler, TelegramBotUpdateHandler>();
            services.AddTransient<ITelegramBotErrorHandler, TelegramBotErrorHandler>();
            services.AddSingleton<ITelegramBotClient>(
                new TelegramBotClient(configuration.GetValue("TelegramBot:Token", string.Empty))
            );

            services.AddSingleton(
                new ChatClient(
                    configuration.GetValue("OpenAI:Model", string.Empty),
                    configuration.GetValue("OpenAI:Token", string.Empty)
                )
            );

            services.AddTransient<IMessageBuilderFactory, MessageBuilderFactory>();
            services.AddTransient<IMessageBuilder, TranslateEngMessageBuilder>();
            services.AddTransient<IMessageBuilder, TranslateRusMessageBuilder>();
            services.AddTransient<IMessageBuilder, WelcomeMessageBuilder>();
        }
    )
    .Build();

await host.RunAsync(default);
