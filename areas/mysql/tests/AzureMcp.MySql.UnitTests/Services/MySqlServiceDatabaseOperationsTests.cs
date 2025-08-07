// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.MySql.UnitTests.Services;

public class MySqlServiceConstructorTests
{
    [Fact]
    public void Constructor_WithValidDependencies_InitializesSuccessfully()
    {
        // Arrange
        var resourceGroupService = Substitute.For<IResourceGroupService>();
        var tenantService = Substitute.For<ITenantService>();
        var logger = Substitute.For<ILogger<MySqlService>>();

        // Act
        var service = new MySqlService(resourceGroupService, tenantService, logger);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullResourceGroupService_ThrowsArgumentNullException()
    {
        // Arrange
        var tenantService = Substitute.For<ITenantService>();
        var logger = Substitute.For<ILogger<MySqlService>>();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new MySqlService(null!, tenantService, logger));
        
        Assert.Equal("resourceGroupService", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullTenantService_CreatesInstance()
    {
        // Arrange
        var resourceGroupService = Substitute.For<IResourceGroupService>();
        var logger = Substitute.For<ILogger<MySqlService>>();

        // Act - tenantService can be null as it's passed to BaseAzureService constructor
        var service = new MySqlService(resourceGroupService, null!, logger);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullLogger_CreatesInstance()
    {
        // Arrange
        var resourceGroupService = Substitute.For<IResourceGroupService>();
        var tenantService = Substitute.For<ITenantService>();

        // Act - logger is assigned directly without validation
        var service = new MySqlService(resourceGroupService, tenantService, null!);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithAllNullDependencies_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new MySqlService(null!, null!, null!));
        
        // Should throw for the first null parameter (resourceGroupService)
        Assert.Equal("resourceGroupService", exception.ParamName);
    }

    [Fact]
    public void MySqlService_ImplementsIMySqlService_Interface()
    {
        // Arrange
        var resourceGroupService = Substitute.For<IResourceGroupService>();
        var tenantService = Substitute.For<ITenantService>();
        var logger = Substitute.For<ILogger<MySqlService>>();

        // Act
        var service = new MySqlService(resourceGroupService, tenantService, logger);

        // Assert
        Assert.IsAssignableFrom<IMySqlService>(service);
        
        // Verify service has all required methods
        Assert.True(typeof(IMySqlService).IsAssignableFrom(typeof(MySqlService)));
    }

    [Fact]
    public void MySqlService_InheritsFromBaseAzureService()
    {
        // Arrange
        var resourceGroupService = Substitute.For<IResourceGroupService>();
        var tenantService = Substitute.For<ITenantService>();
        var logger = Substitute.For<ILogger<MySqlService>>();

        // Act
        var service = new MySqlService(resourceGroupService, tenantService, logger);

        // Assert
        Assert.IsAssignableFrom<AzureMcp.Core.Services.Azure.BaseAzureService>(service);
    }
}
