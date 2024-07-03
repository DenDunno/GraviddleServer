using TelegramBotNM.Notification;

namespace GraviddleServer.Code.API;

public class DeathRecordNotification : INotification<DeathRecord>
{
    private readonly INotification<ImageMessageData> _imageNotification;
    private readonly IFormatter<DeathRecord> _formatter;

    public DeathRecordNotification(INotification<ImageMessageData> imageNotification, IFormatter<DeathRecord> formatter)
    {
        _imageNotification = imageNotification;
        _formatter = formatter;
    }

    public async Task Notify(DeathRecord record)
    {
        string message = _formatter.Execute(record);
        byte[] imageBytes = Convert.FromBase64String(record.ScreenShot);

        await _imageNotification.Notify(new ImageMessageData(imageBytes, message));
    }
}