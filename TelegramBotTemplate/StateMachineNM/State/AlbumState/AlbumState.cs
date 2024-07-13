using TelegramBotTemplate.Bot;

namespace TelegramBotTemplate.StateMachineNM.State.AlbumState;

public class AlbumState : BaseState
{
    private readonly IAlbumGeneration _albumGeneration;
    private readonly TelegramBotBridge _bridge;
    private readonly long _chatId;

    public AlbumState(TelegramBotBridge bridge, long chatId, IAlbumGeneration albumGeneration)
    {
        _albumGeneration = albumGeneration;
        _bridge = bridge;
        _chatId = chatId;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        byte[][] images = await _albumGeneration.Generate();
        await _bridge.SendPNGAlbum(_chatId, images);
    }
}