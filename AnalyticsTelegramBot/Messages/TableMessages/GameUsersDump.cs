using Application.Records;
using Domain.Repository.Commands.Contract;

namespace AnalyticsTelegramBot.Messages.TableMessages;

public class GameUsersDump : TableMessage
{
    private readonly IRecordsDump<LevelRecord> _gameUsersDump;

    public GameUsersDump(IRecordsDump<LevelRecord> gameUsersDump)
    {
        _gameUsersDump = gameUsersDump;
    }

    protected override string[] Columns => new[] { "Nickname", "Game id" };

    protected override Task WriteRaws(List<object[]> raws)
    {
        List<LevelRecord> gameUsers = _gameUsersDump.Execute();

        foreach (LevelRecord userRecord in gameUsers)
        {
            raws.Add(new object[] { userRecord.Name, userRecord.Id });
        }

        return Task.CompletedTask;
    }
}