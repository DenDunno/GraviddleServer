using System.Globalization;
using Application.Records;

namespace Application.Repository;

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

    public string GetUsers()
    {
        return $"""
                WITH RankedAnalytics AS (
                    SELECT *,
                           ROW_NUMBER() OVER (PARTITION BY DeviceId ORDER BY (SELECT NULL)) AS rn
                    FROM Analytics
                )
                SELECT * 
                FROM RankedAnalytics
                WHERE rn = 1;
                """;
    }

    public string Contains(string userId)
    {
        return $@"SELECT COUNT(*) FROM Analytics WHERE DeviceId='{userId}';";
    }
    
    public string Fetch(string userId)
    {
        return $@"SELECT * FROM Analytics WHERE DeviceId='{userId}';";
    }
}