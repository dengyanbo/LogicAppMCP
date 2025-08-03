using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Logging;

namespace LogicAppMCP.Server.Services;

public class AzureAuthenticationService : IAzureAuthenticationService
{
    private readonly ILogger<AzureAuthenticationService> _logger;
    private readonly TokenCredential _credential;

    public AzureAuthenticationService(ILogger<AzureAuthenticationService> logger)
    {
        _logger = logger;
        
        // Use DefaultAzureCredential which supports multiple authentication methods
        // including Managed Identity, Service Principal, and Azure CLI
        _credential = new DefaultAzureCredential();
    }

    public async Task<string> GetAccessTokenAsync()
    {
        try
        {
            var token = await _credential.GetTokenAsync(
                new TokenRequestContext(new[] { "https://management.azure.com/.default" }),
                CancellationToken.None);

            return token.Token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting access token from Azure");
            throw;
        }
    }

    public async Task<bool> ValidateCredentialsAsync()
    {
        try
        {
            await GetAccessTokenAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate Azure credentials");
            return false;
        }
    }
} 