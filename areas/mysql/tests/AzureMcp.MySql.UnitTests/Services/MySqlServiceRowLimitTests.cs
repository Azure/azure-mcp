// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using System.Reflection;

namespace AzureMcp.MySql.UnitTests.Services;

public class MySqlServiceRowLimitTests
{
    private readonly IResourceGroupService _resourceGroupService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<MySqlService> _logger;
    private readonly MySqlService _mysqlService;

    public MySqlServiceRowLimitTests()
    {
        _resourceGroupService = Substitute.For<IResourceGroupService>();
        _tenantService = Substitute.For<ITenantService>();
        _logger = Substitute.For<ILogger<MySqlService>>();
        
        _mysqlService = new MySqlService(_resourceGroupService, _tenantService, _logger);
    }

    [Fact]
    public void ValidateQuerySafety_WithValidQuery_ShouldPassValidation()
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();
        var query = "SELECT * FROM users";
        
        // Act & Assert - Should not throw any exception
        validateMethod.Invoke(null, new object[] { query });
    }

    [Theory]
    [InlineData("SELECT * FROM users LIMIT 100")]
    [InlineData("SELECT * FROM users LIMIT 10000")]
    public void ValidateQuerySafety_WithLimitClause_ShouldPassValidation(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();
        
        // Act & Assert - Should not throw any exception
        validateMethod.Invoke(null, new object[] { query });
    }

    private static MethodInfo GetValidateQuerySafetyMethod()
    {
        var method = typeof(MySqlService).GetMethod("ValidateQuerySafety", 
            BindingFlags.NonPublic | BindingFlags.Static);
        
        Assert.NotNull(method);
        return method;
    }
}
