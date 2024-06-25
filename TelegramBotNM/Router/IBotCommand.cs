namespace TelegramBotNM.Commands;

public interface IBotCommand<in TInput>
{
    Task Handle(TInput input, CancellationToken token);
}