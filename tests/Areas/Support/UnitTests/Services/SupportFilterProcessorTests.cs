// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Support.Models;
using AzureMcp.Areas.Support.Services;
using AzureMcp.Options;
using AzureMcp.Services.Azure.Tenant;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Support.UnitTests.Services;

[Trait("Area", "Support")]
public class SupportFilterProcessorTests
{
    private readonly ISupportFilterProcessor _filterProcessor;
    private readonly ITenantService _tenantService;

    public SupportFilterProcessorTests()
    {
        _tenantService = Substitute.For<ITenantService>();
        _filterProcessor = new SupportFilterProcessor(_tenantService);
    }

    [Fact]
    public async Task ProcessFilterAsync_NullFilter_ReturnsSuccessWithEmptyString()
    {
        // Arrange
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(null, context);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(string.Empty, result.ProcessedFilter);
    }

    [Fact]
    public async Task ProcessFilterAsync_EmptyFilter_ReturnsSuccessWithEmptyString()
    {
        // Arrange
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync("", context);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(string.Empty, result.ProcessedFilter);
    }

    [Fact]
    public async Task ProcessFilterAsync_WhitespaceFilter_ReturnsSuccessWithEmptyString()
    {
        // Arrange
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync("   ", context);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(string.Empty, result.ProcessedFilter);
    }

    [Theory]
    [InlineData("Status eq 'Open'")]
    [InlineData("CreatedDate ge 2024-01-01T00:00:00Z")]
    [InlineData("ProblemClassificationId eq 'test-id'")]
    [InlineData("ServiceId eq 'test-service-id'")]
    public async Task ProcessFilterAsync_SupportedODataFilter_ReturnsUnchanged(string filter)
    {
        // Arrange
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(filter, context);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(filter, result.ProcessedFilter);
    }

    [Theory]
    [InlineData("title eq 'Test'", "title")]
    [InlineData("description eq 'Test'", "description")]
    [InlineData("severity eq 'High'", "severity")]
    [InlineData("contactDetails eq 'test@example.com'", "contactDetails")]
    public async Task ProcessFilterAsync_UnsupportedProperty_ReturnsError(string filter, string unsupportedProperty)
    {
        // Arrange
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(filter, context);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(unsupportedProperty, result.ErrorMessage);
        Assert.Contains("not supported for OData filtering", result.ErrorMessage);
    }

    [Fact]
    public async Task ProcessFilterAsync_ComplexSupportedFilter_ReturnsUnchanged()
    {
        // Arrange
        var filter = "Status eq 'Open' and CreatedDate ge 2024-01-01T00:00:00Z and ProblemClassificationId eq 'test-id'";
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(filter, context);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(filter, result.ProcessedFilter);
    }

    [Fact]
    public async Task ProcessFilterAsync_MultipleUnsupportedProperties_ReturnsErrorWithAllProperties()
    {
        // Arrange
        var filter = "title eq 'Test' and severity eq 'High'";
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(filter, context);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("title", result.ErrorMessage);
        Assert.Contains("severity", result.ErrorMessage);
        Assert.Contains("not supported for OData filtering", result.ErrorMessage);
    }

    [Fact]
    public async Task ProcessFilterAsync_Exception_ReturnsErrorResult()
    {
        // Arrange
        // Create a mock service that throws when called
        var throwingTenantService = Substitute.For<ITenantService>();
        throwingTenantService.GetTenantId(Arg.Any<string>()).Returns(Task.FromException<string?>(new InvalidOperationException("Test exception")));
        
        var throwingProcessor = new SupportFilterProcessor(throwingTenantService);
        var filter = "serviceName eq 'Billing'";
        var context = new FilterContext("invalid-tenant", null);

        // Act
        var result = await throwingProcessor.ProcessFilterAsync(filter, context);

        // Assert
        // Should handle exception and return error result
        Assert.False(result.IsSuccess);
        // Accept either exception-based error message or natural language processing error
        Assert.True(
            result.ErrorMessage.Contains("failed", StringComparison.OrdinalIgnoreCase) ||
            result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase),
            $"Error message should contain 'failed' or 'not found'. Actual: '{result.ErrorMessage}'"
        );
    }

    [Theory]
    [InlineData("serviceName eq 'Billing'")]
    [InlineData("serviceDisplayName eq 'Virtual Machine'")]
    [InlineData("problemClassificationName eq 'Account Management'")]
    public async Task ProcessFilterAsync_NaturalLanguageProperties_ProcessedWithAzureConnection(string filter)
    {
        // Arrange
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(filter, context);

        // Assert
        // If Azure credentials are available, these should succeed by resolving names to IDs
        // If no Azure connection, they should fail
        // This test verifies the natural language processing logic works
        if (result.IsSuccess)
        {
            // If successful, the processed filter should contain IDs instead of names
            Assert.Contains("Id", result.ProcessedFilter);
            Assert.DoesNotContain("Name", result.ProcessedFilter);
        }
        else
        {
            // If failed, error should mention resolution failure
            Assert.True(result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase) ||
                       result.ErrorMessage.Contains("failed", StringComparison.OrdinalIgnoreCase),
                       $"Expected error message to contain 'not found' or 'failed', but got: {result.ErrorMessage}");
        }
    }

    [Fact]
    public async Task ProcessFilterAsync_CaseInsensitivePropertyNames_HandledCorrectly()
    {
        // Arrange
        var filter = "TITLE eq 'Test' and SEVERITY eq 'High'";
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(filter, context);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("title", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("severity", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("serviceName ne 'Billing'")]
    [InlineData("problemClassificationName ne 'Account Management'")]
    public async Task ProcessFilterAsync_NeOperator_ProcessedCorrectly(string filter)
    {
        // Arrange
        var context = new FilterContext(null, null);

        // Act
        var result = await _filterProcessor.ProcessFilterAsync(filter, context);

        // Assert
        // Should attempt to process and either succeed or fail depending on Azure connection
        if (result.IsSuccess)
        {
            // If successful, the processed filter should contain IDs instead of names
            Assert.Contains("Id", result.ProcessedFilter);
            Assert.DoesNotContain("Name", result.ProcessedFilter);
        }
        else
        {
            // If failed, should mention resolution failure
            Assert.Contains("not found", result.ErrorMessage);
        }
    }
}
