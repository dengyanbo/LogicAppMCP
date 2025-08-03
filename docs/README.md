# Logic App MCP (Model Context Protocol) Server

This project provides an MCP server for Azure Logic App diagnostics and management, enabling AI assistants to interact with Logic Apps through a standardized protocol.

## Project Structure

```
LogicAppMCP/
├── src/
│   ├── LogicAppMCP.Server/          # MCP Server implementation
│   ├── LogicAppMCP.Client/          # MCP Client implementation
│   └── LogicAppMCP.Shared/          # Shared models and interfaces
├── tests/
│   └── LogicAppMCP.Tests/           # Unit and integration tests
├── config/                          # Configuration files
├── docs/                            # Documentation
└── TODOList.md                      # Project planning and tasks
```

## Features

### MCP Server Tools

1. **list_logic_apps** - List all Logic Apps in a resource group
2. **get_logic_app_runs** - Get recent runs for a specific Logic App
3. **get_run_details** - Get detailed information about a specific run
4. **get_action_details** - Get detailed information about a specific action
5. **get_trigger_details** - Get detailed information about a specific trigger

### Azure Integration

- Azure Resource Manager API integration
- Support for multiple authentication methods (Managed Identity, Service Principal, Azure CLI)
- Comprehensive error handling and logging
- JSON-based data models for Logic App resources

## Prerequisites

- .NET 8.0 SDK
- Azure subscription with Logic Apps
- Azure CLI or Service Principal credentials

## Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd LogicAppMCP
   ```

2. **Configure Azure credentials**
   - Option 1: Use Azure CLI
     ```bash
     az login
     ```
   - Option 2: Use Service Principal
     ```bash
     az ad sp create-for-rbac --name LogicAppMCP
     ```

3. **Update configuration**
   - Edit `config/appsettings.Development.json`
   - Set your Azure subscription ID, resource group, and tenant ID

4. **Build the solution**
   ```bash
   dotnet build
   ```

5. **Run tests**
   ```bash
   dotnet test
   ```

## Usage

### Running the MCP Server

```bash
cd src/LogicAppMCP.Server
dotnet run
```

### Running the MCP Client

```bash
cd src/LogicAppMCP.Client
dotnet run
```

### VS Code Integration

Create `.vscode/mcp.json`:

```json
{
  "mcpServers": {
    "logic-app-mcp": {
      "command": "dotnet",
      "args": ["run", "--project", "src/LogicAppMCP.Server"],
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Development

### Adding New Tools

1. Add tool method to `LogicAppMCPServer.cs`
2. Update tool schema in `GetToolsSchema()`
3. Implement Azure service method in `AzureLogicAppService.cs`
4. Add corresponding model in `LogicAppMCP.Shared/Models/`
5. Write unit tests

### Testing

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/LogicAppMCP.Tests/

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## Configuration

### Azure Settings

- `SubscriptionId`: Your Azure subscription ID
- `ResourceGroupName`: Default resource group for Logic Apps
- `TenantId`: Azure AD tenant ID

### MCP Settings

- `Transport`: Communication protocol (stdio, HTTP, SSE)
- `Tools`: List of available MCP tools
- `Timeout`: Request timeout duration

## Troubleshooting

### Common Issues

1. **Authentication Errors**
   - Ensure Azure CLI is logged in: `az login`
   - Check service principal permissions
   - Verify tenant and subscription IDs

2. **Logic App Access**
   - Ensure the authenticated user has Reader access to Logic Apps
   - Check resource group permissions

3. **MCP Protocol Issues**
   - Verify tool schema format
   - Check JSON serialization of parameters

### Logging

The application uses structured logging with different levels:
- `Debug`: Detailed diagnostic information
- `Information`: General application flow
- `Warning`: Potential issues
- `Error`: Errors that need attention

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## License

[Add your license information here]

## Support

For issues and questions:
- Create an issue in the repository
- Check the troubleshooting section
- Review the TODOList.md for known issues 