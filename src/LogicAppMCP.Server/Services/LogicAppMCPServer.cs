using LogicAppMCP.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LogicAppMCP.Server.Services;

public class LogicAppMCPServer
{
    private readonly ILogger<LogicAppMCPServer> _logger;
    private readonly IAzureLogicAppService _azureLogicAppService;
    private readonly Dictionary<string, Func<JsonElement, Task<JsonElement>>> _tools;

    public LogicAppMCPServer(
        ILogger<LogicAppMCPServer> logger,
        IAzureLogicAppService azureLogicAppService)
    {
        _logger = logger;
        _azureLogicAppService = azureLogicAppService;
        _tools = InitializeTools();
    }

    private Dictionary<string, Func<JsonElement, Task<JsonElement>>> InitializeTools()
    {
        return new Dictionary<string, Func<JsonElement, Task<JsonElement>>>
        {
            ["list_logic_apps"] = ListLogicAppsAsync,
            ["get_logic_app_runs"] = GetLogicAppRunsAsync,
            ["get_run_details"] = GetRunDetailsAsync,
            ["get_action_details"] = GetActionDetailsAsync,
            ["get_trigger_details"] = GetTriggerDetailsAsync
        };
    }

    public async Task<JsonElement> ExecuteToolAsync(string toolName, JsonElement parameters)
    {
        _logger.LogInformation("Executing tool: {ToolName}", toolName);

        if (!_tools.ContainsKey(toolName))
        {
            throw new ArgumentException($"Unknown tool: {toolName}");
        }

        try
        {
            return await _tools[toolName](parameters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing tool: {ToolName}", toolName);
            throw;
        }
    }

    public JsonElement GetToolsSchema()
    {
        var schema = new
        {
            tools = new object[]
            {
                new
                {
                    name = "list_logic_apps",
                    description = "List all Logic Apps in the specified resource group",
                    inputSchema = new
                    {
                        type = "object",
                        properties = new
                        {
                            resourceGroupName = new
                            {
                                type = "string",
                                description = "Name of the Azure resource group"
                            },
                            subscriptionId = new
                            {
                                type = "string",
                                description = "Azure subscription ID"
                            }
                        },
                        required = new string[] { "resourceGroupName", "subscriptionId" }
                    }
                },
                new
                {
                    name = "get_logic_app_runs",
                    description = "Get recent runs for a specific Logic App",
                    inputSchema = new
                    {
                        type = "object",
                        properties = new
                        {
                            logicAppName = new
                            {
                                type = "string",
                                description = "Name of the Logic App"
                            },
                            resourceGroupName = new
                            {
                                type = "string",
                                description = "Name of the Azure resource group"
                            },
                            subscriptionId = new
                            {
                                type = "string",
                                description = "Azure subscription ID"
                            },
                            top = new
                            {
                                type = "integer",
                                description = "Number of runs to retrieve (default: 10)"
                            }
                        },
                        required = new string[] { "logicAppName", "resourceGroupName", "subscriptionId" }
                    }
                },
                new
                {
                    name = "get_run_details",
                    description = "Get detailed information about a specific Logic App run",
                    inputSchema = new
                    {
                        type = "object",
                        properties = new
                        {
                            runName = new
                            {
                                type = "string",
                                description = "Name of the Logic App run"
                            },
                            logicAppName = new
                            {
                                type = "string",
                                description = "Name of the Logic App"
                            },
                            resourceGroupName = new
                            {
                                type = "string",
                                description = "Name of the Azure resource group"
                            },
                            subscriptionId = new
                            {
                                type = "string",
                                description = "Azure subscription ID"
                            }
                        },
                        required = new string[] { "runName", "logicAppName", "resourceGroupName", "subscriptionId" }
                    }
                }
            }
        };

        return JsonSerializer.SerializeToElement(schema);
    }

    private async Task<JsonElement> ListLogicAppsAsync(JsonElement parameters)
    {
        var resourceGroupName = parameters.GetProperty("resourceGroupName").GetString();
        var subscriptionId = parameters.GetProperty("subscriptionId").GetString();

        var logicApps = await _azureLogicAppService.ListLogicAppsAsync(resourceGroupName!, subscriptionId!);
        return JsonSerializer.SerializeToElement(logicApps);
    }

    private async Task<JsonElement> GetLogicAppRunsAsync(JsonElement parameters)
    {
        var logicAppName = parameters.GetProperty("logicAppName").GetString();
        var resourceGroupName = parameters.GetProperty("resourceGroupName").GetString();
        var subscriptionId = parameters.GetProperty("subscriptionId").GetString();
        var top = parameters.TryGetProperty("top", out var topProp) ? topProp.GetInt32() : 10;

        var runs = await _azureLogicAppService.GetLogicAppRunsAsync(logicAppName!, resourceGroupName!, subscriptionId!, top);
        return JsonSerializer.SerializeToElement(runs);
    }

    private async Task<JsonElement> GetRunDetailsAsync(JsonElement parameters)
    {
        var runName = parameters.GetProperty("runName").GetString();
        var logicAppName = parameters.GetProperty("logicAppName").GetString();
        var resourceGroupName = parameters.GetProperty("resourceGroupName").GetString();
        var subscriptionId = parameters.GetProperty("subscriptionId").GetString();

        var runDetails = await _azureLogicAppService.GetRunDetailsAsync(runName!, logicAppName!, resourceGroupName!, subscriptionId!);
        return JsonSerializer.SerializeToElement(runDetails);
    }

    private Task<JsonElement> GetActionDetailsAsync(JsonElement parameters)
    {
        // Implementation for getting action details
        throw new NotImplementedException();
    }

    private Task<JsonElement> GetTriggerDetailsAsync(JsonElement parameters)
    {
        // Implementation for getting trigger details
        throw new NotImplementedException();
    }
} 