using Newtonsoft.Json;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration.Data;

public class ChartData
{
    [JsonProperty("labels")] public required string[] Labels { get; init; } 
    [JsonProperty("datasets")] public required GraphData[] GraphData { get; init; } 
}