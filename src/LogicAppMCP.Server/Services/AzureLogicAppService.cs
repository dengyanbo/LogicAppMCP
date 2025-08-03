using Azure.Core;
using Azure.Identity;
using LogicAppMCP.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LogicAppMCP.Server.Services;

public class AzureLogicAppService : IAzureLogicAppService
{
    private readonly ILogger<AzureLogicAppService> _logger;
    private readonly IAzureAuthenticationService _authService;
    private readonly HttpClient _httpClient;

    public AzureLogicAppService(
        ILogger<AzureLogicAppService> logger,
        IAzureAuthenticationService authService,
        HttpClient httpClient)
    {
        _logger = logger;
        _authService = authService;
        _httpClient = httpClient;
    }

    public async Task<List<LogicAppInfo>> ListLogicAppsAsync(string resourceGroupName, string subscriptionId)
    {
        try
        {
            var token = await _authService.GetAccessTokenAsync();
            var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Logic/workflows?api-version=2019-05-01";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AzureResourceListResponse<LogicAppInfo>>(content);

            return result?.Value ?? new List<LogicAppInfo>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing Logic Apps for resource group: {ResourceGroup}", resourceGroupName);
            throw;
        }
    }

    public async Task<List<LogicAppRun>> GetLogicAppRunsAsync(string logicAppName, string resourceGroupName, string subscriptionId, int top = 10)
    {
        try
        {
            var token = await _authService.GetAccessTokenAsync();
            var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Logic/workflows/{logicAppName}/runs?api-version=2019-05-01&$top={top}";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AzureResourceListResponse<LogicAppRun>>(content);

            return result?.Value ?? new List<LogicAppRun>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting runs for Logic App: {LogicAppName}", logicAppName);
            throw;
        }
    }

    public async Task<LogicAppRunDetails> GetRunDetailsAsync(string runName, string logicAppName, string resourceGroupName, string subscriptionId)
    {
        try
        {
            var token = await _authService.GetAccessTokenAsync();
            var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Logic/workflows/{logicAppName}/runs/{runName}?api-version=2019-05-01";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LogicAppRunDetails>(content);

            return result ?? new LogicAppRunDetails();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting run details for run: {RunName}", runName);
            throw;
        }
    }

    public async Task<LogicAppActionDetails> GetActionDetailsAsync(string actionName, string runName, string logicAppName, string resourceGroupName, string subscriptionId)
    {
        try
        {
            var token = await _authService.GetAccessTokenAsync();
            var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Logic/workflows/{logicAppName}/runs/{runName}/actions/{actionName}?api-version=2019-05-01";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LogicAppActionDetails>(content);

            return result ?? new LogicAppActionDetails();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting action details for action: {ActionName}", actionName);
            throw;
        }
    }

    public async Task<LogicAppTriggerDetails> GetTriggerDetailsAsync(string triggerName, string logicAppName, string resourceGroupName, string subscriptionId)
    {
        try
        {
            var token = await _authService.GetAccessTokenAsync();
            var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Logic/workflows/{logicAppName}/triggers/{triggerName}?api-version=2019-05-01";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LogicAppTriggerDetails>(content);

            return result ?? new LogicAppTriggerDetails();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting trigger details for trigger: {TriggerName}", triggerName);
            throw;
        }
    }
}

public class AzureResourceListResponse<T>
{
    public List<T> Value { get; set; } = new();
    public string? NextLink { get; set; }
} 