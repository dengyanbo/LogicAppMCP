# LogicAppMCP

A .NET-based Model Context Protocol (MCP) server and client for Azure Logic Apps diagnostics and management.

## Overview

LogicAppMCP provides tools for:
- **MCP Server**: Azure Logic Apps diagnostics and management via MCP protocol
- **MCP Client**: Client application for interacting with the MCP server
- **Azure Integration**: Direct integration with Azure Logic Apps REST API
- **VS Code Integration**: Ready for VS Code MCP server configuration

## Quick Start

### Prerequisites
- .NET 8.0 SDK
- Azure subscription with Logic Apps
- Azure CLI (for authentication)

### Build and Run

```bash
# Build the solution
dotnet build

# Run the MCP server
dotnet run --project src/LogicAppMCP.Server

# Run the MCP client
dotnet run --project src/LogicAppMCP.Client

# Run tests
dotnet test
```

## Project Structure

```
LogicAppMCP/
├── src/
│   ├── LogicAppMCP.Server/     # MCP server implementation
│   ├── LogicAppMCP.Client/     # MCP client implementation
│   └── LogicAppMCP.Shared/     # Shared models and interfaces
├── tests/
│   └── LogicAppMCP.Tests/      # Unit and integration tests
├── docs/                       # Detailed documentation
├── config/                     # Configuration files
└── TODOList.md                 # Project roadmap
```

## Features

### MCP Server Tools
- `list_logic_apps`: List all Logic Apps in a resource group
- `get_logic_app_runs`: Get runs for a specific Logic App
- `get_run_details`: Get detailed information about a specific run
- `get_action_details`: Get action details from a run
- `get_trigger_details`: Get trigger details from a run

### Azure Integration
- Azure Identity authentication
- Logic Apps REST API integration
- Resource management and diagnostics

## Documentation

- **[Setup Guide](docs/SETUP.md)**: Detailed setup and configuration instructions
- **[Project Documentation](docs/README.md)**: Comprehensive project documentation

## Development

This project uses:
- **.NET 8.0** for the core application
- **Model Context Protocol** for server-client communication
- **Azure SDK** for Azure resource management
- **xUnit** and **Moq** for testing
- **Dependency Injection** for service management

## License

This project is licensed under the MIT License.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## Support

For issues and questions, please check the documentation in the `docs/` folder or create an issue in the repository. 