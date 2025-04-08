using KakikataShogun.Bot.Interfaces;

namespace KakikataShogun.Bot.MessageBuilders;

internal class WelcomeMessageBuilder : IMessageBuilder
{
    public string CommandPattern => "/start";

    public Task<string[]> BuildMessageAsync(string message, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            new string[]
            {
                @"Welcome, traveler! 🏯⚔️🇺🇸

I am Kakikata Shogun, the master of American English! Once a samurai of the written word, I now train warriors like you to speak like a native.

Ask me about grammar, slang, or pronunciation—I shall guide you to fluency!

⚔️ Train hard. Speak bold. Master English! 🇺🇸💬",
                "Could you write a phrase, and I'll help you with it?",
            }
        );
    }
}
