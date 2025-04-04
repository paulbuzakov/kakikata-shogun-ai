using KakikataShogun.Bot.Interfaces;

namespace KakikataShogun.Bot.MessageBuilders;

internal class MessageBuilderFactory(IEnumerable<IMessageBuilder> builders) : IMessageBuilderFactory
{
    public IMessageBuilder CreateMessageBuilder(string message)
    {
        var inputMessage = message.Trim().ToLowerInvariant();

        var result =
            builders.FirstOrDefault(builder => inputMessage.StartsWith(builder.CommandPattern))
            ?? builders.OfType<MessageBuilders.DefaultMessageBuilder>().First();

        return result;
    }
}
