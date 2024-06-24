namespace TelegramBotNM.StateMachineNM;

public class ConversationResult
{
    public readonly int NewStateId;
    public readonly bool NewState;

    public ConversationResult(bool newState, int newStateId)
    {
        NewStateId = newStateId;
        NewState = newState;
    }
}