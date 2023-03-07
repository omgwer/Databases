using System.Text.Json.Serialization;

namespace Lab2.Model.Domain;

public class SearchParameters
{
    [JsonPropertyName("startPoint")] public string? StartPoint { get; set; }
    [JsonPropertyName("finishPoint")] public string? FinishPoint { get; set; }
    [JsonPropertyName("minRange")] public double? MinRange { get; set; }
    [JsonPropertyName("maxRange")] public double? MaxRange { get; set; }
    [JsonPropertyName("busStopName")] public string? BusStopName { get; set; }
    [JsonPropertyName("placement")] public string? Placement { get; set; }
    [JsonPropertyName("isHavePavilion")] public string? IsHavePavilion { get; set; }
    [JsonPropertyName("offset")] public int? Offset { get; set; }
    [JsonPropertyName("order")] public string? Order { get; set; }
    [JsonPropertyName("direction")] public string? Direction { get; set; }
}