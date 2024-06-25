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
    private readonly TelegramUsersRepository _telegramUsersRepository;
    private readonly IUserProvider _userProvider;
    
    public Conversation(TelegramUsersRepository telegramUsersRepository, IStateMachineFactory stateMachineFactory, IUserProvider userProvider)
    {
        _stateMachineFactory = stateMachineFactory;
        _telegramUsersRepository = telegramUsersRepository;
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
            _telegramUsersRepository.UpdateConversation.Execute(user with { ConversationState = conversationResult.NewStateId });
        }
    }
}