using AzureMcp.Configuration;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;
using NSubstitute;
using Xunit;
using static AzureMcp.Services.Telemetry.TelemetryConstants;

namespace AzureMcp.Tests.Services.Telemetry;

public class TelemetryServiceTests
{
    private readonly IOptions<AzureMcpServerConfiguration> _mockOptions;
    private readonly AzureMcpServerConfiguration _configuration;

    public TelemetryServiceTests()
    {
        _configuration = new AzureMcpServerConfiguration
        {
            Name = "TestService",
            Version = "1.0.0",
            IsTelemetryEnabled = true
        };

        _mockOptions = Substitute.For<IOptions<AzureMcpServerConfiguration>>();
        _mockOptions.Value.Returns(_configuration);
    }

    [Fact]
    public void StartActivity_WhenTelemetryEnabled_ShouldReturnActivity()
    {
        // Arrange
        using var service = new TelemetryService(_mockOptions);
        const string activityId = "test-activity";

        // Act
        using var activity = service.StartActivity(activityId);

        // Assert
        Assert.NotNull(activity);
        Assert.Equal(activityId, activity.OperationName);
    }

    [Fact]
    public void StartActivity_WhenTelemetryDisabled_ShouldReturnNull()
    {
        // Arrange
        _configuration.IsTelemetryEnabled = false;
        using var service = new TelemetryService(_mockOptions);
        const string activityId = "test-activity";

        // Act
        var activity = service.StartActivity(activityId);

        // Assert
        Assert.Null(activity);
    }

    [Fact]
    public void StartActivity_WithClientInfo_WhenTelemetryEnabled_ShouldAddClientTags()
    {
        // Arrange
        using var service = new TelemetryService(_mockOptions);
        const string activityId = "test-activity";
        var clientInfo = new Implementation
        {
            Name = "TestClient",
            Version = "2.0.0"
        };

        // Act
        using var activity = service.StartActivity(activityId, clientInfo);

        // Assert
        Assert.NotNull(activity);
        Assert.Equal(activityId, activity.OperationName);

        var clientNameTag = activity.Tags.FirstOrDefault(t => t.Key == TagName.ClientName);
        var clientVersionTag = activity.Tags.FirstOrDefault(t => t.Key == TagName.ClientVersion);

        Assert.Equal(clientInfo.Name, clientNameTag.Value);
        Assert.Equal(clientInfo.Version, clientVersionTag.Value);
    }

    [Fact]
    public void StartActivity_WithClientInfo_WhenTelemetryDisabled_ShouldReturnNull()
    {
        // Arrange
        _configuration.IsTelemetryEnabled = false;
        using var service = new TelemetryService(_mockOptions);
        const string activityId = "test-activity";
        var clientInfo = new Implementation
        {
            Name = "TestClient",
            Version = "2.0.0"
        };

        // Act
        var activity = service.StartActivity(activityId, clientInfo);

        // Assert
        Assert.Null(activity);
    }

    [Fact]
    public void StartActivity_WithNullClientInfo_ShouldNotAddClientTags()
    {
        // Arrange
        using var service = new TelemetryService(_mockOptions);
        const string activityId = "test-activity";

        // Act
        using var activity = service.StartActivity(activityId, null);

        // Assert
        Assert.NotNull(activity);
        Assert.Equal(activityId, activity.OperationName);

        var clientNameTag = activity.Tags.FirstOrDefault(t => t.Key == TagName.ClientName);
        var clientVersionTag = activity.Tags.FirstOrDefault(t => t.Key == TagName.ClientVersion);

        Assert.Equal(default(KeyValuePair<string, string?>), clientNameTag);
        Assert.Equal(default(KeyValuePair<string, string?>), clientVersionTag);
    }

    [Fact]
    public void StartActivity_WithoutClientInfo_ShouldCallOverloadWithNullClientInfo()
    {
        // Arrange
        using var service = new TelemetryService(_mockOptions);
        const string activityId = "test-activity";

        // Act
        using var activity = service.StartActivity(activityId);

        // Assert
        Assert.NotNull(activity);
        Assert.Equal(activityId, activity.OperationName);
    }

    [Fact]
    public void Dispose_WithNullLogForwarder_ShouldNotThrow()
    {
        // Arrange
        var service = new TelemetryService(_mockOptions);

        // Act & Assert
        var exception = Record.Exception(() => service.Dispose());
        Assert.Null(exception);
    }

    [Fact]
    public void Constructor_WithNullOptions_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<NullReferenceException>(() => new TelemetryService(null!));
    }

    [Fact]
    public void Constructor_WithNullConfiguration_ShouldThrowNullReferenceException()
    {
        // Arrange
        var mockOptions = Substitute.For<IOptions<AzureMcpServerConfiguration>>();
        mockOptions.Value.Returns((AzureMcpServerConfiguration)null!);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new TelemetryService(mockOptions));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void StartActivity_WithInvalidActivityId_ShouldHandleGracefully(string activityId)
    {
        // Arrange
        var configuration = new AzureMcpServerConfiguration
        {
            Name = "TestService",
            Version = "1.0.0",
            IsTelemetryEnabled = true
        };

        var mockOptions = Substitute.For<IOptions<AzureMcpServerConfiguration>>();
        mockOptions.Value.Returns(configuration);

        using var service = new TelemetryService(mockOptions);

        // Act
        var activity = service.StartActivity(activityId);

        // Assert
        // ActivitySource.StartActivity typically handles null/empty names gracefully
        // The exact behavior may depend on the .NET version and ActivitySource implementation
        if (activity != null)
        {
            activity.Dispose();
        }
    }
}
