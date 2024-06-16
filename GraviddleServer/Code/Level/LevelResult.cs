namespace GraviddleServer.Level;

[Serializable]
public class LevelResult
{
    public string Name { get; set; } = null!;
    public int Stars { get; set; }
    public string Level { get; set; } = null!;
    public float Time { get; set; }
    public int DeathCount { get; set; }

    public override string ToString()
    {
        return $"Name = {Name}\n" +
               $"Level = {Level}\n" +
               $"Stars = {Stars}\n" +
               $"Time in sec = {Time}\n" +
               $"Number of deaths = {DeathCount}\n";
    }
}