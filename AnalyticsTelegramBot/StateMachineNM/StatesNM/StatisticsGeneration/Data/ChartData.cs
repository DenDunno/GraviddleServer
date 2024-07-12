using Newtonsoft.Json;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration.Data;

public class ChartData
{
    [JsonProperty("labels")] public required string[] Labels { get; init; } 
    [JsonProperty("datasets")] public required GraphData[] GraphData { get; init; } 
}