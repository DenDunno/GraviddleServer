namespace TelegramBotNM.StateMachineNM;

public class ConversationResult
{
    public readonly int NewStateId;
    public readonly bool Success;

    public ConversationResult(bool success, int newStateId)
    {
        NewStateId = newStateId;
        Success = success;
    }
}