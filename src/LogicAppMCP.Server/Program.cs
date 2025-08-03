using LogicAppMCP.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogicAppMCP.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Register MCP Server
                services.AddSingleton<LogicAppMCPServer>();
                
                // Register Azure Services
                services.AddSingleton<IAzureLogicAppService, AzureLogicAppService>();
                services.AddSingleton<IAzureAuthenticationService, AzureAuthenticationService>();
                
                // Register Hosted Service
                services.AddHostedService<MCPServerHostedService>();
                
                // Configure Logging
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                });
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
            });
} 