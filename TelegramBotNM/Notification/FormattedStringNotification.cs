namespace TelegramBotNM.Notification;

public class FormattedStringNotification<TInput> : INotification<TInput>
{
    private readonly INotification<string> _stringNotification;
    private readonly IFormatter<TInput> _formatter;
    
    public FormattedStringNotification(INotification<string> stringNotification, IFormatter<TInput> formatter)
    {
        _stringNotification = stringNotification;
        _formatter = formatter;
    }
    
    public async Task Notify(TInput record)
    {
        string stringRecord = _formatter.Execute(record);
        await _stringNotification.Notify(stringRecord);
    }
}