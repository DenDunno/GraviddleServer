namespace TelegramBotTemplate.StateMachineNM.State.AlbumState;

public interface IAlbumGeneration
{
    Task<byte[][]> Generate();
}