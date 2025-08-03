using LogicAppMCP.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LogicAppMCP.Client.Services;

public class LogicAppMCPClient
{
    private readonly ILogger<LogicAppMCPClient> _logger;
    private readonly HttpClient _httpClient;

    public LogicAppMCPClient(
        ILogger<LogicAppMCPClient> logger,
        HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<List<LogicAppInfo>> ListLogicAppsAsync(string resourceGroupName, string subscriptionId)
    {
        try
        {
            _logger.LogInformation("Requesting Logic Apps for resource group: {ResourceGroup}", resourceGroupName);

            var parameters = new
            {
                resourceGroupName,
                subscriptionId
            };

            var response = await ExecuteToolAsync("list_logic_apps", parameters);
            return JsonSerializer.Deserialize<List<LogicAppInfo>>(response) ?? new List<LogicAppInfo>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing Logic Apps");
            throw;
        }
    }

    public async Task<List<LogicAppRun>> GetLogicAppRunsAsync(string logicAppName, string resourceGroupName, string subscriptionId, int top = 10)
    {
        try
        {
            _logger.LogInformation("Requesting runs for Logic App: {LogicAppName}", logicAppName);

            var parameters = new
            {
                logicAppName,
                resourceGroupName,
                subscriptionId,
                top
            };

            var response = await ExecuteToolAsync("get_logic_app_runs", parameters);
            return JsonSerializer.Deserialize<List<LogicAppRun>>(response) ?? new List<LogicAppRun>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Logic App runs");
            throw;
        }
    }

    public async Task<LogicAppRunDetails> GetRunDetailsAsync(string runName, string logicAppName, string resourceGroupName, string subscriptionId)
    {
        try
        {
            _logger.LogInformation("Requesting run details for run: {RunName}", runName);

            var parameters = new
            {
                runName,
                logicAppName,
                resourceGroupName,
                subscriptionId
            };

            var response = await ExecuteToolAsync("get_run_details", parameters);
            return JsonSerializer.Deserialize<LogicAppRunDetails>(response) ?? new LogicAppRunDetails();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting run details");
            throw;
        }
    }

    private async Task<string> ExecuteToolAsync(string toolName, object parameters)
    {
        // This is a placeholder for the actual MCP tool execution
        // In a real implementation, this would communicate with the MCP server
        _logger.LogInformation("Executing tool: {ToolName}", toolName);

        // Simulate tool execution
        await Task.Delay(100); // Simulate network delay

        // Return mock data for now
        return JsonSerializer.Serialize(new { message = $"Tool {toolName} executed successfully" });
    }
} 