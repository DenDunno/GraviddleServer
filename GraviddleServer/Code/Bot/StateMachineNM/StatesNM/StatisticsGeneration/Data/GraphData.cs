using Newtonsoft.Json;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration.Data;

public class GraphData
{
    [JsonProperty("label")] public required string Label { get; init; }
    [JsonProperty("data")] public required double[] Data { get; init; }
    [JsonProperty("fill")] public bool? Fill { get; set; }
    [JsonProperty("borderColor")] public string? BorderColor { get; set; }
    [JsonProperty("backgroundColor")] public string? BackgroundColor { get; set; }
}