using System.Globalization;
using GraviddleServer.Code.Parser;
using TelegramBotNM.Repository.Query;

namespace GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;

public class AnalyticsQueries : Queries<LevelRecord, string>
{
    public override string Insert(LevelRecord element)
    {
        string escapedName = element.Name.Replace("'", "''");
        string escapedLevel = element.Level.Replace("'", "''");

        return
            $@"INSERT INTO Analytics (DeviceId, Name, Stars, LevelsName, Time, DeathCount) VALUES ('{element.DeviceId}', 
                                                                                          '{escapedName}',
                                                                                          {element.Stars},
                                                                                          '{escapedLevel}', 
                                                                                          {element.Time.ToString(CultureInfo.InvariantCulture)}, 
                                                                                          {element.DeathCount});";
    }

    public override string GetAll()
    {
        return $@"SELECT * FROM Analytics;";
    }
}