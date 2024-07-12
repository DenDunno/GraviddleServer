namespace Domain.Notification;

public class ImageMessageData
{
    public readonly string? Message;
    public readonly byte[] Image;

    public ImageMessageData(byte[] image, string? message = null)
    {
        Message = message;
        Image = image;
    }
}