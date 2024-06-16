namespace GraviddleServer.Level;

public class LevelResult
{
    public int Value { get; init; }

    public override string ToString()
    {
        return $"Value = {Value}";
    }
}