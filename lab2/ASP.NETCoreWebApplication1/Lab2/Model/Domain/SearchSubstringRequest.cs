using System.Text.Json.Serialization;

namespace Lab2.Model.Domain;

public class SearchSubstringRequest
{
    [JsonPropertyName("substring")]
    public string Substring;
    [JsonPropertyName("offset")]
    public int Offset;
    [JsonPropertyName("limit")]
    public int Limit;
}