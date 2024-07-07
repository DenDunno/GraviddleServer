namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public interface IPNGProvider
{
    Task<byte[]> Evaluate();
}