using TelegramBotNM.Logger;

namespace GraviddleServer.Code.API;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMessageLogger _logger;

    public ExceptionMiddleware(RequestDelegate next, IMessageLogger logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await _logger.Log(ex.ToString());
        }
    }
}