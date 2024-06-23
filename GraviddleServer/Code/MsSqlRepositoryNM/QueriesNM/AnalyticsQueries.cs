using System.Globalization;
using GraviddleServer.Code.Parser;
using TelegramBotNM.Repository.Query;

namespace GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;

public class AnalyticsQueries 
{
    public string Insert(LevelRecord element)
    {
        string escapedName = element.Name.Replace("'", "''");
        string escapedLevel = element.Level.Replace("'", "''");

        return
            $@"INSERT INTO Analytics (DeviceId, Name, Stars, LevelsName, Time, DeathCount) VALUES ('{element.Id}', 
                                                                                          '{escapedName}',
                                                                                          {element.Stars},
                                                                                          '{escapedLevel}', 
                                                                                          {element.Time.ToString(CultureInfo.InvariantCulture)}, 
                                                                                          {element.DeathCount});";
    }

    public string GetAll()
    {
        return $@"SELECT * FROM Analytics;";
    }
}