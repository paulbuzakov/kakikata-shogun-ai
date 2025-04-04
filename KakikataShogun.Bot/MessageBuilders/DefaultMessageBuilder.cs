using KakikataShogun.Bot.Interfaces;

namespace KakikataShogun.Bot.MessageBuilders;

internal class DefaultMessageBuilder : IMessageBuilder
{
    public string CommandPattern => "default";

    public Task<string> BuildMessageAsync(string message, CancellationToken cancellationToken)
    {
        return Task.FromResult("I'm sorry, but I don't understand that command.");
    }
}
