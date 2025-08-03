using System.Text.Json.Serialization;

namespace LogicAppMCP.Shared.Models;

public class LogicAppActionDetails
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public LogicAppActionDetailsProperties Properties { get; set; } = new();
}

public class LogicAppActionDetailsProperties
{
    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("error")]
    public LogicAppError? Error { get; set; }

    [JsonPropertyName("inputs")]
    public object? Inputs { get; set; }

    [JsonPropertyName("outputs")]
    public object? Outputs { get; set; }

    [JsonPropertyName("trackedProperties")]
    public object? TrackedProperties { get; set; }

    [JsonPropertyName("retryHistory")]
    public List<LogicAppRetryHistory>? RetryHistory { get; set; }

    [JsonPropertyName("iterationCount")]
    public int? IterationCount { get; set; }

    [JsonPropertyName("repetitionCount")]
    public int? RepetitionCount { get; set; }

    [JsonPropertyName("batchSize")]
    public int? BatchSize { get; set; }

    [JsonPropertyName("minimumIterationCount")]
    public int? MinimumIterationCount { get; set; }

    [JsonPropertyName("maximumIterationCount")]
    public int? MaximumIterationCount { get; set; }
} 