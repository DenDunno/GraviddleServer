namespace TelegramBotNM.Commands;

public interface IBotCommand<in T>
{
    Task Handle(T input, CancellationToken token);
}