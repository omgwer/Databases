using System.Text.Json.Serialization;

namespace Lab2.Model.Domain;

public class Restriction
{
    [JsonPropertyName("placement")]
    public IEnumerable<string>? PlacementRestriction { get; set; }
    
    [JsonPropertyName("localityName")]
    public IEnumerable<string>? LocalityName { get; set; }
    
    [JsonPropertyName("busStopName")]
    public IEnumerable<string>? BusStopName { get; set; }
    
    [JsonPropertyName("isHavePavilion")]
    public IEnumerable<string>? IsHavePavilion { get; set; }
}