using Telegram.Bot.Types.Enums;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class UserRecordsDumpMessage : ITelegramMessage
{
    private readonly IRecordsDump<TelegramUser> _userRecordsDump;

    public UserRecordsDumpMessage(IRecordsDump<TelegramUser> userRecordsDump)
    {
        _userRecordsDump = userRecordsDump;
    }

    public ParseMode ParseMode => ParseMode.MarkdownV2;
    
    public string GetText()
    {
        throw new NotImplementedException();
    }
}