using Newtonsoft.Json;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class ChartData
{
    [JsonProperty("labels")] public required string[] Labels { get; init; } 
    [JsonProperty("datasets")] public required GraphData[] GraphData { get; init; } 
}