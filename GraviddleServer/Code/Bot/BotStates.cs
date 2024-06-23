using TelegramBotNM.Bot;
using TelegramBotNM.Repository;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class BotStates
{
    public readonly IState Root;
    public readonly IState Start;
    public readonly IState Stop;
    public readonly IState Authorization;
    public readonly IState InvalidPassword;
    public readonly IState AdminState;

    public BotStates(TelegramUser user, Repository<TelegramUser, long> repository, TelegramBotBridge bridge)
    {
        Root = new RootState();
        Stop = new StopState();
        Start = new StartState(repository.Add, user);
        Authorization = new AuthorizationState(bridge, user.Id);
        InvalidPassword = new MessageState(bridge, "Invalid password", user.Id);
        AdminState = new AdminState(user, bridge, repository.Update);
    }

    public IList<IState> All => new List<IState>()
    {
        Root,
        Start,
        Stop,
        Authorization,
        InvalidPassword,
        AdminState,
    };
}