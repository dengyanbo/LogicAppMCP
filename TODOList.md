Planning and Scope
Define project scope and objectives – Clarify the features (e.g. server integration, error diagnostics) and supported AI models.

Responsible: Project Owner

Due: 2025‑08‑07

Priority: High

Research external APIs – Identify Azure App Service (Logic App Standard) endpoints and Graph API calls needed; plan authentication strategy
adobe.com
.

Responsible: Project Owner

Due: 2025‑08‑10

Priority: High

Environment Setup
Install development tools – Install the .NET SDK, Visual Studio Code (with MCP/Copilot extensions), and initialize a Git repository.

Responsible: Project Owner

Due: 2025‑08‑05

Priority: High

Create project structure – Set up folders for server code, client configuration and documentation.

Responsible: Project Owner

Due: 2025‑08‑05

Priority: Medium

Server Implementation
Initialize .NET console project – Create the project and install necessary NuGet packages: ModelContextProtocol, Microsoft.Extensions.Hosting, Azure.Identity, Azure.ResourceManager.AppService.

Responsible: Project Owner

Due: 2025‑08‑07

Priority: High

Dependencies: Environment setup complete

Configure MCP server – Register the MCP server with your preferred transport (stdio/SSE/HTTP) and scaffold tool methods.

Responsible: Project Owner

Due: 2025‑08‑12

Priority: High

Implement diagnostic tools – Write MCP tool methods to list runs, fetch run details and action inputs/outputs using Azure REST calls
adobe.com
.

Responsible: Project Owner

Due: 2025‑08‑17

Priority: High

Dependencies: MCP server configured

Integrate Graph API (optional) – If your design requires Graph data, implement additional tool methods and handle authentication.

Responsible: Project Owner

Due: 2025‑08‑20

Priority: Medium

Client Integration
Create VS Code server configuration – Generate .vscode/mcp.json and register the MCP server for testing with Copilot.

Responsible: Project Owner

Due: 2025‑08‑18

Priority: Medium

Dependencies: Server implementation

Test tool invocation – Verify that your MCP tools can be discovered and executed via Copilot’s agent mode.

Responsible: Project Owner

Due: 2025‑08‑20

Priority: Medium

Testing and Documentation
Develop unit/integration tests – Write tests to validate API calls, data parsing and error-handling logic.

Responsible: Project Owner

Due: 2025‑08‑22

Priority: Medium

Document usage and examples – Write setup instructions, sample prompts and explain tool parameters. Make the documentation easy to access
adobe.com
.

Responsible: Project Owner

Due: 2025‑08‑24

Priority: Medium

Deployment and Maintenance
Plan packaging and deployment – Decide on local or containerized deployment, configure environment variables and secrets handling.

Responsible: Project Owner

Due: 2025‑08‑24

Priority: Low

Set up logging/monitoring – Implement logging for MCP requests and Azure calls; plan how to monitor performance.

Responsible: Project Owner

Due: 2025‑08‑24

Priority: Low

Define backlog and enhancements – Capture future ideas (e.g. new tools, improved diagnostics) and prioritize them.

Responsible: Project Owner

Due: 2025‑08‑24

Priority: Low

Review and Iteration
Regular updates – Review progress, update tasks and adjust deadlines as needed
adobe.com
.

Responsible: Project Owner

Due: Weekly

Priority: Medium