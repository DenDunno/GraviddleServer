using Newtonsoft.Json;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class ChartConfig
{
    [JsonProperty("type")] public required string Type { get; init; }
    [JsonProperty("data")] public required ChartData Data { get; init; }
}