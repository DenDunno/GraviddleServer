namespace GraviddleServer.Code.Parser;

[Serializable]
public class LevelRecord
{
    public string DeviceId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Stars { get; set; }
    public string Level { get; set; } = null!;
    public double Time { get; set; }
    public int DeathCount { get; set; }

    public override string ToString()
    {
        return $"Name = {Name}\n" +
               $"Level = {Level}\n" +
               $"Stars = {Stars}\n" +
               $"Time in sec = {Math.Round(Time, 1)}\n" +
               $"Number of deaths = {DeathCount}\n";
    }
}