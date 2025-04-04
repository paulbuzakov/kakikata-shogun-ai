namespace KakikataShogun.Bot.Interfaces;

internal interface IMessageBuilderFactory
{
    IMessageBuilder CreateMessageBuilder(string message);
}
