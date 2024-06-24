using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Repository;
using TelegramBotNM.Router;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.UserProvider;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class Conversation : RouterBranch<Message>
{
    private readonly IStateMachineFactory _stateMachineFactory;
    private readonly UserRepository _userRepository;
    private readonly IUserProvider _userProvider;
    
    public Conversation(UserRepository userRepository, IStateMachineFactory stateMachineFactory, IUserProvider userProvider)
    {
        _stateMachineFactory = stateMachineFactory;
        _userRepository = userRepository;
        _userProvider = userProvider;
    }

    protected override UpdateType UpdateType => UpdateType.Message;
    protected override Message GetKey(Update update) => update.Message!;

    protected override async Task OnHandle(Message message, CancellationToken cancellationToken)
    {
        TelegramUser user = _userProvider.Create(message.Chat.Id);
        StateMachine stateMachine = _stateMachineFactory.Create(message.Text!, user);
        ConversationResult conversationResult = await stateMachine.UpdateConversation(cancellationToken);

        if (conversationResult.NewState)
        {
            _userRepository.UpdateConversation.Execute(user with { ConversationState = conversationResult.NewStateId });
        }
    }
}