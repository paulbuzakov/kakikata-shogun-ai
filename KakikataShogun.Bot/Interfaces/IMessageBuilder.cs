namespace KakikataShogun.Bot.Interfaces;

internal interface IMessageBuilder
{
    string CommandPattern { get; }
    Task<string> BuildMessageAsync(string message, CancellationToken cancellationToken);
}
