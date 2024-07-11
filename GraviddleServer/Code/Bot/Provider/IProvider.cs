namespace GraviddleServer.Code.Bot;

public interface IProvider<out T>
{
    T Provide();
}