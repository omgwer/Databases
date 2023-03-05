using System.Text.Json.Serialization;

namespace Lab2.Model.Domain;

public class SearchParameters
{
    [JsonPropertyName("placement")] public string? PlacementRestriction { get; set; }

    [JsonPropertyName("localityName")] public string? LocalityName { get; set; }

    [JsonPropertyName("busStopName")] public string? BusStopName { get; set; }

    [JsonPropertyName("isHavePavilion")] public string? IsHavePavilion { get; set; }

    [JsonPropertyName("minRange")] public double? MinRange { get; set; }
    [JsonPropertyName("maxRange")] public double? MaxRange { get; set; }
}