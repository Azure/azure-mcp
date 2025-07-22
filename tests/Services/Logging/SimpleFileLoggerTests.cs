// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Services.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AzureMcp.Tests.Services.Logging;

public class SimpleFileLoggerTests
{
    [Fact]
    public void SimpleFileLogger_WritesToFile_Successfully()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();

        Console.WriteLine($"Using temporary file for logging: {tempFile}");

        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddSimpleFile(tempFile, LogLevel.Information));
        
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<SimpleFileLoggerTests>>();

        try
        {
            // Act
            logger.LogInformation("Test log message");
            logger.LogWarning("Test warning message");
            logger.LogError("Test error message");

            // Assert
            var logContent = File.ReadAllText(tempFile);
            Assert.Contains("Test log message", logContent);
            Assert.Contains("Test warning message", logContent);
            Assert.Contains("Test error message", logContent);
            Assert.Contains("[Information]", logContent);
            Assert.Contains("[Warning]", logContent);
            Assert.Contains("[Error]", logContent);
        }
        finally
        {
            // Cleanup
            serviceProvider.Dispose();
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void SimpleFileLogger_RespectsMinimumLevel()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddSimpleFile(tempFile, LogLevel.Warning));
        
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<SimpleFileLoggerTests>>();

        try
        {
            // Act
            logger.LogDebug("Debug message - should not appear");
            logger.LogInformation("Info message - should not appear");
            logger.LogWarning("Warning message - should appear");
            logger.LogError("Error message - should appear");

            // Assert
            var logContent = File.ReadAllText(tempFile);
            Assert.DoesNotContain("Debug message", logContent);
            Assert.DoesNotContain("Info message", logContent);
            Assert.Contains("Warning message", logContent);
            Assert.Contains("Error message", logContent);
        }
        finally
        {
            // Cleanup
            serviceProvider.Dispose();
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
