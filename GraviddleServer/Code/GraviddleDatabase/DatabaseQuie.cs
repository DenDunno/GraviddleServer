using System.Globalization;
using GraviddleServer.Code.Level;

namespace GraviddleServer.Code.GraviddleDatabase;

public class DatabaseQueries
{
    public string InsertCommand(LevelResult dataForAnalytics)
    {
        return $@"INSERT INTO Analytics VALUES ('{dataForAnalytics.DeviceId}', 
                                                '{dataForAnalytics.Name}',
                                                {dataForAnalytics.Stars},
                                                '{dataForAnalytics.Level}', 
                                                {dataForAnalytics.Time.ToString(CultureInfo.InvariantCulture)},                                    , 
                                                {dataForAnalytics.DeathCount});";
    }
}