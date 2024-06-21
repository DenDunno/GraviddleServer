using TelegramBotNM.Bot;

namespace TelegramBotNM.Commands.MessageCommands;

public class AuthorizeCommand : OutputMessageCommand
{
    public AuthorizeCommand(TelegramBotBridge bridge) : base(bridge)
    {
    }

    protected override Task<string> EvaluateOutput()
    {
        throw new NotImplementedException();
    }
}