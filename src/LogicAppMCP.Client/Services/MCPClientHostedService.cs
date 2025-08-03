using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogicAppMCP.Client.Services;

public class MCPClientHostedService : BackgroundService
{
    private readonly ILogger<MCPClientHostedService> _logger;
    private readonly LogicAppMCPClient _mcpClient;

    public MCPClientHostedService(
        ILogger<MCPClientHostedService> logger,
        LogicAppMCPClient mcpClient)
    {
        _logger = logger;
        _mcpClient = mcpClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting Logic App MCP Client...");

        try
        {
            // Initialize MCP client
            _logger.LogInformation("MCP Client initialized successfully");

            // Keep the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MCP Client hosted service");
            throw;
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Logic App MCP Client...");
        await base.StopAsync(cancellationToken);
    }
} 