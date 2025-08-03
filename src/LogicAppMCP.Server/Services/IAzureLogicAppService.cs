using LogicAppMCP.Shared.Models;

namespace LogicAppMCP.Server.Services;

public interface IAzureLogicAppService
{
    Task<List<LogicAppInfo>> ListLogicAppsAsync(string resourceGroupName, string subscriptionId);
    Task<List<LogicAppRun>> GetLogicAppRunsAsync(string logicAppName, string resourceGroupName, string subscriptionId, int top = 10);
    Task<LogicAppRunDetails> GetRunDetailsAsync(string runName, string logicAppName, string resourceGroupName, string subscriptionId);
    Task<LogicAppActionDetails> GetActionDetailsAsync(string actionName, string runName, string logicAppName, string resourceGroupName, string subscriptionId);
    Task<LogicAppTriggerDetails> GetTriggerDetailsAsync(string triggerName, string logicAppName, string resourceGroupName, string subscriptionId);
} 