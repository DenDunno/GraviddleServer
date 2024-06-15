using Microsoft.AspNetCore.Mvc;
namespace GraviddleServer;

[ApiController]
public class Endpoints : ControllerBase
{
    public string Greet()
    {
        return "Hey there, what are doing here?";
    }

    public string PostLevelResult(int input)
    {
        return input.ToString();
    }

    public string GetAllRecords()
    {
        return "All records";
    }
}