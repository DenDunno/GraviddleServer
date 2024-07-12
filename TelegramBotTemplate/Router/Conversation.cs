using Domain.Repository.Commands.Contract;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.StateMachineNM;
using TelegramBotTemplate.User;

namespace TelegramBotTemplate.Router;

public class Conversation : RouterBranch<Message>
{
    private readonly IRecordUpdate<TelegramUser> _conversationUpdate;
    private readonly IStateMachineFactory _stateMachineFactory;
    private readonly ITelegramUserProvider _userProvider;

    public Conversation(IRecordUpdate<TelegramUser> conversationUpdate, IStateMachineFactory stateMachineFactory, ITelegramUserProvider userProvider)
    {
        _stateMachineFactory = stateMachineFactory;
        _conversationUpdate = conversationUpdate;
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
            _conversationUpdate.Execute(user with { ConversationState = conversationResult.NewStateId });
        }
    }
}