using GraviddleServer.Code.API;
using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.User;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class GenerateStatisticsState : BaseState
{
    private readonly IPNGProvider _imageProvider;
    private readonly TelegramBotBridge _bridge;
    private readonly TelegramUser _user;

    public GenerateStatisticsState(TelegramBotBridge bridge, TelegramUser user, IPNGProvider imageProvider)
    {
        _imageProvider = imageProvider;
        _bridge = bridge;
        _user = user;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        ImageMessageData image = new(_imageProvider.Evaluate());
        await _bridge.SendPNG(_user.Id, image);
    }
}