using System.Text.Json.Serialization;

namespace LogicAppMCP.Shared.Models;

public class LogicAppTriggerDetails
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public LogicAppTriggerDetailsProperties Properties { get; set; } = new();
}

public class LogicAppTriggerDetailsProperties
{
    [JsonPropertyName("provisioningState")]
    public string ProvisioningState { get; set; } = string.Empty;

    [JsonPropertyName("createdTime")]
    public DateTime? CreatedTime { get; set; }

    [JsonPropertyName("changedTime")]
    public DateTime? ChangedTime { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public LogicAppTriggerStatus? Status { get; set; }

    [JsonPropertyName("recurrence")]
    public LogicAppTriggerRecurrence? Recurrence { get; set; }

    [JsonPropertyName("workflow")]
    public LogicAppWorkflowReference? Workflow { get; set; }
}

public class LogicAppTriggerStatus
{
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class LogicAppTriggerRecurrence
{
    [JsonPropertyName("frequency")]
    public string Frequency { get; set; } = string.Empty;

    [JsonPropertyName("interval")]
    public int? Interval { get; set; }

    [JsonPropertyName("schedule")]
    public LogicAppTriggerSchedule? Schedule { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; }

    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; set; }
}

public class LogicAppTriggerSchedule
{
    [JsonPropertyName("minutes")]
    public List<int>? Minutes { get; set; }

    [JsonPropertyName("hours")]
    public List<int>? Hours { get; set; }

    [JsonPropertyName("weekDays")]
    public List<string>? WeekDays { get; set; }

    [JsonPropertyName("monthDays")]
    public List<int>? MonthDays { get; set; }

    [JsonPropertyName("monthlyOccurrences")]
    public List<LogicAppMonthlyOccurrence>? MonthlyOccurrences { get; set; }
}

public class LogicAppMonthlyOccurrence
{
    [JsonPropertyName("day")]
    public string? Day { get; set; }

    [JsonPropertyName("occurrence")]
    public int? Occurrence { get; set; }
} 