using System.Globalization;
using GraviddleServer.Code.Parser;

namespace GraviddleServer.Code.Queries;

public class AnalyticsQueries : IQueries<LevelRecord>
{
    public string Insert(LevelRecord element)
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


    public string Remove(LevelRecord element)
    {
        throw new NotImplementedException();
    }

    public string Contains(LevelRecord element)
    {
        throw new NotImplementedException();
    }

    public string GetAll()
    {
        return $@"SELECT * FROM Analytics;";
    }
}