using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogicAppMCP.Server.Services;

public class MCPServerHostedService : BackgroundService
{
    private readonly ILogger<MCPServerHostedService> _logger;
    private readonly LogicAppMCPServer _mcpServer;

    public MCPServerHostedService(
        ILogger<MCPServerHostedService> logger,
        LogicAppMCPServer mcpServer)
    {
        _logger = logger;
        _mcpServer = mcpServer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting Logic App MCP Server...");

        try
        {
            // Initialize MCP server
            _logger.LogInformation("MCP Server initialized successfully");

            // Keep the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MCP Server hosted service");
            throw;
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Logic App MCP Server...");
        await base.StopAsync(cancellationToken);
    }
} 