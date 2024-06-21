using TelegramBotNM.StateMachineNM;

namespace GraviddleServer.Code.Bot;

public class BotStates
{
    public readonly EmptyState Root = new();
    public readonly StartState Start = new();
    public readonly StopState StopState = new();

    public IList<IState> All => new List<IState>()
    {
        Root,
        Start,
        StopState
    };
}