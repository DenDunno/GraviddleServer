using Coravel.Invocable;

namespace GraviddleServer.Code.Refresh;

public class ServerRefresh : IInvocable
{
    private readonly string _serverName;

    public ServerRefresh(string serverName)
    {
        _serverName = serverName;
    }

    public async Task Invoke()
    {
        await new HttpClient().GetStringAsync(_serverName);
    }
}