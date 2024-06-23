using TelegramBotNM.Repository;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class BotStates
{
    public readonly RootState Root = new();
    public readonly StartState Start;
    public readonly StopState StopState = new();

    public BotStates(TelegramUser user, Repository<TelegramUser, long> repository)
    {
        Start = new StartState(repository.Add, user);
    }

    public IList<IState> All => new List<IState>()
    {
        Root,
        Start,
        StopState
    };
}