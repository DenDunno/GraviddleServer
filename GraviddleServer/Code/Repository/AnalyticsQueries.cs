using System.Globalization;
using GraviddleServer.Code.API;

namespace GraviddleServer.Code.Repository;

public class AnalyticsQueries 
{
    public string Insert(LevelRecord element)
    {
        string escapedName = element.Name.Replace("'", "''");
        string escapedLevel = element.Level.Replace("'", "''");

        return
            $@"INSERT INTO Analytics (DeviceId, Name, Stars, Level, LevelIndex, Time, DeathCount) VALUES ('{element.Id}', 
                                                                                          '{escapedName}',
                                                                                          {element.Stars},
                                                                                          '{escapedLevel}', 
                                                                                          {element.LevelIndex}, 
                                                                                          {element.Time.ToString(CultureInfo.InvariantCulture)}, 
                                                                                          {element.DeathCount});";
    }

    public string GetAll()
    {
        return $@"SELECT * FROM Analytics;";
    }
}