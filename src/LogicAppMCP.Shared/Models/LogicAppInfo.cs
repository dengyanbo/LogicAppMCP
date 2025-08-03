using System.Text.Json.Serialization;

namespace LogicAppMCP.Shared.Models;

public class LogicAppInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public LogicAppProperties Properties { get; set; } = new();

    [JsonPropertyName("tags")]
    public Dictionary<string, string> Tags { get; set; } = new();
}

public class LogicAppProperties
{
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("createdTime")]
    public DateTime? CreatedTime { get; set; }

    [JsonPropertyName("changedTime")]
    public DateTime? ChangedTime { get; set; }

    [JsonPropertyName("definition")]
    public object? Definition { get; set; }
} 