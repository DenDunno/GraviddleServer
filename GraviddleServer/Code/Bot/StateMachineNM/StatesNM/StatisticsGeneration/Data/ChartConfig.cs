using Newtonsoft.Json;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration.Data;

public class ChartConfig
{
    [JsonProperty("type")] public required string Type { get; init; }
    [JsonProperty("data")] public required ChartData Data { get; init; }
}