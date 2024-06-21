using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class BotStates
{
    public readonly EmptyState Root = new();
    public readonly StartState Start;
    public readonly StopState StopState = new();

    public BotStates(IRecordAdd<TelegramUser> recordAdd)
    {
        Start = new StartState(recordAdd);
    }

    public IList<IState> All => new List<IState>()
    {
        Root,
        Start,
        StopState
    };
}