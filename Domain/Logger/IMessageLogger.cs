namespace Domain.Logger;

public interface IMessageLogger
{
    Task Log(string text);
}