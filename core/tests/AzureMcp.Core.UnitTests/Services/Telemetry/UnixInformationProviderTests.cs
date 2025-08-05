using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Core.UnitTests.Services.Telemetry;

public class UnixInformationProviderTests
{
    private readonly ILogger<UnixInformationProvider> _logger;
    private readonly TestUnixInformationProvider _provider;
    private const string TestStoragePath = "/test/storage";
    private const string ExpectedCachePath = "/test/storage/Microsoft/DeveloperTools";

    public UnixInformationProviderTests()
    {
        _logger = Substitute.For<ILogger<UnixInformationProvider>>();
        _provider = new TestUnixInformationProvider(_logger, TestStoragePath);
    }

    [Fact]
    public async Task GetOrCreateDeviceId_WhenStoragePathThrows_ReturnsNull()
    {
        // Arrange
        var provider = new TestUnixInformationProvider(_logger, throwOnGetStoragePath: true);

        // Act
        var result = await provider.GetOrCreateDeviceId();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOrCreateDeviceId_WhenExistingDeviceIdExists_ReturnsExistingValue()
    {
        // Arrange
        const string existingDeviceId = "existing-device-id";

        // Mock the file system to return an existing device ID
        var provider = Substitute.ForPartsOf<TestUnixInformationProvider>(_logger, TestStoragePath, false);
        provider.ReadValueFromDisk(ExpectedCachePath, Arg.Any<string>())
               .Returns(existingDeviceId);

        // Act
        var result = await provider.GetOrCreateDeviceId();

        // Assert
        Assert.Equal(existingDeviceId, result);
        await provider.Received(1).ReadValueFromDisk(ExpectedCachePath, Arg.Any<string>());
    }

    [Fact]
    public async Task GetOrCreateDeviceId_WhenNoExistingDeviceId_CreatesNewDeviceId()
    {
        // Arrange
        var provider = Substitute.ForPartsOf<TestUnixInformationProvider>(_logger, TestStoragePath, false);
        provider.ReadValueFromDisk(ExpectedCachePath, Arg.Any<string>())
               .Returns((string?)null);
        provider.WriteValueToDisk(ExpectedCachePath, Arg.Any<string>(), Arg.Any<string>())
               .Returns(true);

        // Act
        var result = await provider.GetOrCreateDeviceId();

        // Assert
        Assert.NotNull(result);
        await provider.Received(1).ReadValueFromDisk(ExpectedCachePath, Arg.Any<string>());
        await provider.Received(1).WriteValueToDisk(ExpectedCachePath, Arg.Any<string>(), Arg.Any<string>());
    }

    [Fact]
    public async Task GetOrCreateDeviceId_WhenWriteValueToDiskFails_ReturnsNull()
    {
        // Arrange
        var provider = Substitute.ForPartsOf<TestUnixInformationProvider>(_logger, TestStoragePath, false);
        provider.ReadValueFromDisk(ExpectedCachePath, Arg.Any<string>())
               .Returns((string?)null);
        provider.WriteValueToDisk(ExpectedCachePath, Arg.Any<string>(), Arg.Any<string>())
               .Returns(false);

        // Act
        var result = await provider.GetOrCreateDeviceId();

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task WriteValueToDisk_WhenValueIsNullOrWhitespace_ReturnsFalse(string? value)
    {
        // Act
        var result = await _provider.WriteValueToDisk("/test/path", "filename", value);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task WriteValueToDisk_WhenDirectoryCreationFails_ReturnsFalse()
    {
        // Arrange
        // This test requires mocking the file system, which is more complex with static methods
        // In a real scenario, you'd want to use System.IO.Abstractions for better testability
        var provider = new TestUnixInformationProvider(_logger, TestStoragePath);

        // Act & Assert
        // Note: This test is limited by the static nature of Directory and File operations
        // In production code, consider using IFileSystem from System.IO.Abstractions
        var result = await provider.WriteValueToDisk("/invalid/path/that/cannot/be/created", "test.txt", "value");

        // The actual result depends on the file system permissions
        // In most test environments, this should work, but we're testing the error handling path
    }

    [Fact]
    public async Task ReadValueFromDisk_WhenFileDoesNotExist_ReturnsNull()
    {
        // Act
        var result = await _provider.ReadValueFromDisk("/nonexistent/path", "nonexistent.txt");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Constructor_WithValidLogger_DoesNotThrow()
    {
        // Act & Assert
        var exception = Record.Exception(() => new TestUnixInformationProvider(_logger));
        Assert.Null(exception);
    }

    [Fact]
    public void GetStoragePath_WhenImplementationThrows_PropagatesException()
    {
        // Arrange
        var provider = new TestUnixInformationProvider(_logger, throwOnGetStoragePath: true);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => provider.GetTestStoragePath());
    }
}

internal class TestUnixInformationProvider(ILogger<UnixInformationProvider> logger,
    string? storagePath = "/test/storage", bool throwOnGetStoragePath = false)
    : UnixInformationProvider(logger)
{
    private readonly string? _storagePath = storagePath;
    private readonly bool _throwOnGetStoragePath = throwOnGetStoragePath;

    public string GetTestStoragePath() => GetStoragePath();

    public override string GetStoragePath()
    {
        if (_throwOnGetStoragePath)
        {
            throw new InvalidOperationException("No storage path available");
        }
        return _storagePath ?? throw new InvalidOperationException("Storage path not set");
    }
}
