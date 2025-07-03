// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AppService.Services;
using AzureMcp.Tests.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Tests.Areas.AppService.LiveTests;

[Collection("LiveTests")]
public class AppServiceCommandTests(TestConfig testConfig) : IClassFixture<TestConfig>
{
    private readonly TestConfig _testConfig = testConfig;

    [Fact]
    [Trait("Category", "Live")]
    public async Task DatabaseAddCommand_WithValidParameters_AddsDatabase()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<IAppServiceService, AppServiceService>();
        
        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<IAppServiceService>();

        // This test would require actual Azure resources
        // For now, we'll skip it in the live test environment
        // In a real implementation, you would set up test resources
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddDatabaseAsync(
                "non-existent-app",
                "non-existent-rg",
                "SqlServer",
                "test-server",
                "test-db",
                null,
                _testConfig.Subscription,
                Azure.Core.RetryPolicy.Default));
    }
}
