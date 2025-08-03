namespace LogicAppMCP.Server.Services;

public interface IAzureAuthenticationService
{
    Task<string> GetAccessTokenAsync();
    Task<bool> ValidateCredentialsAsync();
} 