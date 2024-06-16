namespace GraviddleServer.TelegramBot;

public interface IBotCommand<in T>
{
    Task Handle(T input, CancellationToken token);
}