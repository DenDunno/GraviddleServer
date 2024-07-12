namespace TelegramBotTemplate.Router.Commands;

public interface IBotCommand<in TInput>
{
    Task Handle(TInput input, CancellationToken token);
}