using Coravel.Invocable;
using Domain.Logger;

namespace GraviddleServer.Code.Refresh;

public class ServerRefresh : IInvocable
{
    private readonly IMessageLogger _messageLogger;
    private readonly string _serverName;

    public ServerRefresh(IMessageLogger messageLogger, string serverName)
    {
        _messageLogger = messageLogger;
        _serverName = serverName;
    }

    public async Task Invoke()
    {
        string response = await new HttpClient().GetStringAsync(_serverName);
        await _messageLogger.Log($"Server was refreshed. Response: {response}");
    }
}