using System.Text.Json.Serialization;

namespace LogicAppMCP.Shared.Models;

public class LogicAppRunDetails
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public LogicAppRunDetailsProperties Properties { get; set; } = new();
}

public class LogicAppRunDetailsProperties
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

    [JsonPropertyName("actions")]
    public Dictionary<string, LogicAppAction>? Actions { get; set; }
}

public class LogicAppAction
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public LogicAppActionProperties Properties { get; set; } = new();
}

public class LogicAppActionProperties
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
}

public class LogicAppRetryHistory
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
} 