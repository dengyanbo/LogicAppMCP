using System.Text.Json.Serialization;

namespace LogicAppMCP.Shared.Models;

public class LogicAppRun
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public LogicAppRunProperties Properties { get; set; } = new();
}

public class LogicAppRunProperties
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

    [JsonPropertyName("correlation")]
    public LogicAppCorrelation? Correlation { get; set; }

    [JsonPropertyName("workflow")]
    public LogicAppWorkflowReference? Workflow { get; set; }

    [JsonPropertyName("trigger")]
    public LogicAppTriggerReference? Trigger { get; set; }

    [JsonPropertyName("outputs")]
    public Dictionary<string, object>? Outputs { get; set; }
}

public class LogicAppError
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public class LogicAppCorrelation
{
    [JsonPropertyName("clientTrackingId")]
    public string? ClientTrackingId { get; set; }

    [JsonPropertyName("clientKeywords")]
    public string? ClientKeywords { get; set; }
}

public class LogicAppWorkflowReference
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}

public class LogicAppTriggerReference
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
} 