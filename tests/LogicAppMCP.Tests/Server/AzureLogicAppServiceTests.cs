using FluentAssertions;
using LogicAppMCP.Server.Services;
using LogicAppMCP.Shared.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LogicAppMCP.Tests.Server;

public class AzureLogicAppServiceTests
{
    private readonly Mock<ILogger<AzureLogicAppService>> _loggerMock;
    private readonly Mock<IAzureAuthenticationService> _authServiceMock;
    private readonly Mock<HttpClient> _httpClientMock;
    private readonly AzureLogicAppService _service;

    public AzureLogicAppServiceTests()
    {
        _loggerMock = new Mock<ILogger<AzureLogicAppService>>();
        _authServiceMock = new Mock<IAzureAuthenticationService>();
        _httpClientMock = new Mock<HttpClient>();
        
        _service = new AzureLogicAppService(_loggerMock.Object, _authServiceMock.Object, _httpClientMock.Object);
    }

    [Fact]
    public async Task ListLogicAppsAsync_ShouldReturnLogicApps_WhenValidParameters()
    {
        // Arrange
        var resourceGroupName = "test-rg";
        var subscriptionId = "test-sub";
        var expectedToken = "test-token";

        _authServiceMock.Setup(x => x.GetAccessTokenAsync())
            .ReturnsAsync(expectedToken);

        // Act
        var result = await _service.ListLogicAppsAsync(resourceGroupName, subscriptionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LogicAppInfo>>();
    }

    [Fact]
    public async Task GetLogicAppRunsAsync_ShouldReturnRuns_WhenValidParameters()
    {
        // Arrange
        var logicAppName = "test-logic-app";
        var resourceGroupName = "test-rg";
        var subscriptionId = "test-sub";
        var top = 5;
        var expectedToken = "test-token";

        _authServiceMock.Setup(x => x.GetAccessTokenAsync())
            .ReturnsAsync(expectedToken);

        // Act
        var result = await _service.GetLogicAppRunsAsync(logicAppName, resourceGroupName, subscriptionId, top);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LogicAppRun>>();
    }

    [Fact]
    public async Task GetRunDetailsAsync_ShouldReturnRunDetails_WhenValidParameters()
    {
        // Arrange
        var runName = "test-run";
        var logicAppName = "test-logic-app";
        var resourceGroupName = "test-rg";
        var subscriptionId = "test-sub";
        var expectedToken = "test-token";

        _authServiceMock.Setup(x => x.GetAccessTokenAsync())
            .ReturnsAsync(expectedToken);

        // Act
        var result = await _service.GetRunDetailsAsync(runName, logicAppName, resourceGroupName, subscriptionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<LogicAppRunDetails>();
    }
} 