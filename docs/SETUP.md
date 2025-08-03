# Setup Guide for Logic App MCP Server

This guide will walk you through setting up the Logic App MCP server for development and production use.

## Prerequisites

### Required Software

1. **.NET 8.0 SDK**
   ```bash
   # Download from: https://dotnet.microsoft.com/download/dotnet/8.0
   # Verify installation
   dotnet --version
   ```

2. **Azure CLI** (for authentication)
   ```bash
   # Download from: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
   # Verify installation
   az --version
   ```

3. **Visual Studio Code** (recommended)
   - Install the C# extension
   - Install the MCP extension for testing

### Azure Requirements

1. **Azure Subscription**
   - Active Azure subscription
   - Access to Logic Apps in the subscription

2. **Azure AD Permissions**
   - Reader access to Logic Apps
   - Access to resource groups containing Logic Apps

## Initial Setup

### 1. Clone and Build

```bash
# Clone the repository
git clone <repository-url>
cd LogicAppMCP

# Restore dependencies
dotnet restore

# Build the solution
dotnet build
```

### 2. Azure Authentication Setup

#### Option A: Azure CLI (Recommended for Development)

```bash
# Login to Azure
az login

# Set your subscription (if you have multiple)
az account set --subscription "your-subscription-id"

# Verify access to Logic Apps
az logic workflow list --resource-group "your-resource-group"
```

#### Option B: Service Principal (Recommended for Production)

```bash
# Create service principal
az ad sp create-for-rbac --name "LogicAppMCP" --role "Reader"

# Note the output - you'll need these values:
# - appId (client ID)
# - password (client secret)
# - tenant (tenant ID)
```

### 3. Configuration

#### Update Development Settings

Edit `config/appsettings.Development.json`:

```json
{
  "Azure": {
    "SubscriptionId": "your-subscription-id",
    "ResourceGroupName": "your-resource-group",
    "TenantId": "your-tenant-id"
  }
}
```

#### Environment Variables (Optional)

```bash
# Set environment variables for production
export AZURE_SUBSCRIPTION_ID="your-subscription-id"
export AZURE_RESOURCE_GROUP="your-resource-group"
export AZURE_TENANT_ID="your-tenant-id"
```

### 4. Verify Setup

```bash
# Run tests to verify everything works
dotnet test

# Build the server
dotnet build src/LogicAppMCP.Server

# Run the server (should start without errors)
dotnet run --project src/LogicAppMCP.Server
```

## VS Code Integration

### 1. Install Extensions

- C# Dev Kit
- MCP (Model Context Protocol) extension

### 2. Configure MCP Server

Create `.vscode/mcp.json`:

```json
{
  "mcpServers": {
    "logic-app-mcp": {
      "command": "dotnet",
      "args": ["run", "--project", "src/LogicAppMCP.Server"],
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "AZURE_SUBSCRIPTION_ID": "your-subscription-id",
        "AZURE_RESOURCE_GROUP": "your-resource-group"
      }
    }
  }
}
```

### 3. Test Integration

1. Open VS Code
2. Open Command Palette (Ctrl+Shift+P)
3. Type "MCP: Connect to Server"
4. Select "logic-app-mcp"
5. Verify tools are available

## Development Workflow

### 1. Running the Server

```bash
# Development mode
dotnet run --project src/LogicAppMCP.Server

# Production mode
dotnet run --project src/LogicAppMCP.Server --environment Production
```

### 2. Running Tests

```bash
# All tests
dotnet test

# Specific test project
dotnet test tests/LogicAppMCP.Tests/

# With coverage
dotnet test --collect:"XPlat Code Coverage"
```

### 3. Adding New Tools

1. **Add tool method** in `LogicAppMCPServer.cs`:
   ```csharp
   private async Task<JsonElement> NewToolAsync(JsonElement parameters)
   {
       // Implementation
   }
   ```

2. **Register tool** in `InitializeTools()`:
   ```csharp
   ["new_tool"] = NewToolAsync,
   ```

3. **Add schema** in `GetToolsSchema()`:
   ```csharp
   new
   {
       name = "new_tool",
       description = "Description of the new tool",
       inputSchema = new { /* schema definition */ }
   }
   ```

4. **Implement Azure service** in `AzureLogicAppService.cs`

5. **Add tests** in `LogicAppMCP.Tests/`

## Troubleshooting

### Common Issues

#### 1. Authentication Errors

**Error**: `Azure.Identity.AuthenticationFailedException`

**Solution**:
```bash
# Re-login to Azure CLI
az logout
az login

# Verify subscription
az account show
```

#### 2. Logic App Access Denied

**Error**: `403 Forbidden` when accessing Logic Apps

**Solution**:
```bash
# Check current permissions
az role assignment list --assignee $(az account show --query user.name -o tsv)

# Grant Reader role if needed
az role assignment create --assignee $(az account show --query user.name -o tsv) --role "Reader" --scope "/subscriptions/your-subscription-id"
```

#### 3. Build Errors

**Error**: Missing dependencies

**Solution**:
```bash
# Clean and restore
dotnet clean
dotnet restore
dotnet build
```

#### 4. MCP Connection Issues

**Error**: Cannot connect to MCP server

**Solution**:
1. Check `.vscode/mcp.json` configuration
2. Verify server is running: `dotnet run --project src/LogicAppMCP.Server`
3. Check logs for errors

### Debug Mode

Enable detailed logging:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "LogicAppMCP.Server": "Debug",
      "LogicAppMCP.Client": "Debug"
    }
  }
}
```

## Production Deployment

### 1. Container Deployment

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/LogicAppMCP.Server/LogicAppMCP.Server.csproj", "src/LogicAppMCP.Server/"]
COPY ["src/LogicAppMCP.Shared/LogicAppMCP.Shared.csproj", "src/LogicAppMCP.Shared/"]
RUN dotnet restore "src/LogicAppMCP.Server/LogicAppMCP.Server.csproj"
COPY . .
WORKDIR "/src/src/LogicAppMCP.Server"
RUN dotnet build "LogicAppMCP.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogicAppMCP.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogicAppMCP.Server.dll"]
```

### 2. Azure Container Instances

```bash
# Build and push to Azure Container Registry
az acr build --registry your-registry --image logic-app-mcp:latest .

# Deploy to Container Instances
az container create \
  --resource-group your-rg \
  --name logic-app-mcp \
  --image your-registry.azurecr.io/logic-app-mcp:latest \
  --environment-variables \
    AZURE_SUBSCRIPTION_ID=your-subscription-id \
    AZURE_RESOURCE_GROUP=your-resource-group
```

## Security Considerations

1. **Use Managed Identity** in production
2. **Limit permissions** to minimum required
3. **Secure configuration** storage
4. **Enable logging** for audit trails
5. **Regular updates** of dependencies

## Next Steps

1. Review the TODOList.md for upcoming features
2. Explore the MCP protocol documentation
3. Test with your Logic Apps
4. Contribute improvements 