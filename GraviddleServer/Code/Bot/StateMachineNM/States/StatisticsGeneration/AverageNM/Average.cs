namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class Average
{
    private double _sum;
    private int _count;

    public void Add(double value)
    {
        _sum += value;
        _count++;
    }

    public double Value => _sum / _count;
}