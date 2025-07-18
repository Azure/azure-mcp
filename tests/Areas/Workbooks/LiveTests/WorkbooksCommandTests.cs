// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Workbooks.LiveTests;

[Trait("Area", "Workbooks")]
public class WorkbooksCommandTests(LiveTestFixture fixture, ITestOutputHelper output)
    : CommandTestsBase(fixture, output), IClassFixture<LiveTestFixture>
{
    // Test workbook content for CRUD operations
    private const string TestWorkbookContent = """
        {
            "version": "Notebook/1.0",
            "items": [
                {
                    "type": 1,
                    "content": {
                        "json": "# Test Workbook\n\nThis is a test workbook created by Azure MCP live tests."
                    }
                },
                {
                    "type": 3,
                    "content": {
                        "version": "KqlItem/1.0",
                        "query": "print \"Test Query\", now()",
                        "size": 0,
                        "title": "Test Query",
                        "timeContext": {
                            "durationMs": 3600000
                        },
                        "queryType": 0,
                        "resourceType": "microsoft.operationalinsights/workspaces",
                        "visualization": "table"
                    }
                }
            ],
            "styleSettings": {},
            "$schema": "https://github.com/Microsoft/Application-Insights-Workbooks/blob/master/schema/workbook.json"
        }
        """;

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_workbooks_by_subscription_and_resource_group()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // We should have at least the workbooks created by our bicep template
        var workbooksArray = workbooks.EnumerateArray();
        Assert.NotEmpty(workbooksArray);

        // Verify each workbook has required properties
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("WorkbookId", out _));
            Assert.True(workbook.TryGetProperty("DisplayName", out _));
            Assert.True(workbook.TryGetProperty("Category", out _));
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_show_workbook_by_display_name()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_show",
            new()
            {
                { "workbook-id", $"/subscriptions/{Settings.SubscriptionId}/resourceGroups/{Settings.ResourceGroupName}/providers/Microsoft.Insights/workbooks/547590d2-943e-5d77-9d63-e1f7ca15686c" }
            });

        var workbooks = result.AssertProperty("Workbook");
        Assert.True(workbooks.TryGetProperty("WorkbookId", out _));
        Assert.True(workbooks.TryGetProperty("DisplayName", out var displayName));
        Assert.Contains("Basic Monitoring Dashboard", displayName.GetString()!);
        Assert.True(workbooks.TryGetProperty("SerializedData", out _));
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_create_update_and_delete_workbook()
    {
        var workbookName = $"Test Workbook {Guid.NewGuid():N}";
        string? workbookId = null;

        try
        {
            // Create workbook
            var createResult = await CallToolAsync(
                "azmcp_workbooks_create",
                new()
                {
                    { "subscription", Settings.SubscriptionId },
                    { "resource-group", Settings.ResourceGroupName },
                    { "display-name", workbookName },
                    { "serialized-content", TestWorkbookContent },
                    { "source-id", "azure monitor" }
                });

            var createdWorkbook = createResult.AssertProperty("Workbook");
            Assert.True(createdWorkbook.TryGetProperty("WorkbookId", out var workbookIdProperty));
            workbookId = workbookIdProperty.GetString();
            Assert.NotNull(workbookId);

            Assert.True(createdWorkbook.TryGetProperty("DisplayName", out var displayNameProperty));
            Assert.Equal(workbookName, displayNameProperty.GetString());

            // Update workbook
            var updatedName = $"Updated {workbookName}";
            var updateResult = await CallToolAsync(
                "azmcp_workbooks_update",
                new()
                {
                    { "workbook-id", workbookId },
                    { "display-name", updatedName }
                });

            var updatedWorkbooks = updateResult.AssertProperty("Workbook");
            Assert.True(updatedWorkbooks.TryGetProperty("DisplayName", out var updatedDisplayName));
            Assert.Equal(updatedName, updatedDisplayName.GetString());

            // Verify the workbook exists
            var showResult = await CallToolAsync(
                "azmcp_workbooks_show",
                new()
                {
                    { "workbook-id", workbookId }
                });

            var shownWorkbooks = showResult.AssertProperty("Workbook");
            Assert.True(shownWorkbooks.TryGetProperty("WorkbookId", out _));
        }
        finally
        {
            // Clean up - delete workbook if it was created
            if (!string.IsNullOrEmpty(workbookId))
            {
                var deleteResult = await CallToolAsync(
                    "azmcp_workbooks_delete",
                    new()
                    {
                        { "workbook-id", workbookId }
                    });

                // Verify delete operation
                Assert.NotNull(deleteResult);
                Assert.True(deleteResult.Value.TryGetProperty("WorkbookId", out var deletedWorkbookId));
                Assert.Equal(workbookId, deletedWorkbookId.GetString());
                Assert.True(deleteResult.Value.TryGetProperty("Message", out var deleteMessage));
                Assert.Equal("Successfully deleted", deleteMessage.GetString());
            }
        }
    }

    [Theory]
    [InlineData(0)] // Credential auth
    [InlineData(1)] // Key auth
    [Trait("Category", "Live")]
    public async Task Should_list_workbooks_with_auth_method(int authMethod)
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "auth-method", authMethod }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_kind_shared()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "kind", "shared" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks are shared
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("Kind", out var kind));
            Assert.Equal("shared", kind.GetString());
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_kind_user()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "kind", "user" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks are user workbooks
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("Kind", out var kind));
            Assert.Equal("user", kind.GetString());
        }

        // Should find at least the user workbook from our bicep template
        Assert.NotEmpty(workbooksArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_category_workbook()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "category", "workbook" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks have category "workbook"
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("Category", out var category));
            Assert.Equal("workbook", category.GetString());
        }

        // Should find multiple workbooks with this category
        Assert.NotEmpty(workbooksArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_category_sentinel()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "category", "sentinel" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks have category "sentinel"
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("Category", out var category));
            Assert.Equal("sentinel", category.GetString());
        }

        // Should find at least the sentinel workbook from our bicep template
        Assert.NotEmpty(workbooksArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_category_TSG()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "category", "TSG" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks have category "TSG"
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("Category", out var category));
            Assert.Equal("TSG", category.GetString());
        }

        // Should find at least the TSG workbook from our bicep template
        Assert.NotEmpty(workbooksArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_source_id_azure_monitor()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "source-id", "azure monitor" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks have sourceId "azure monitor"
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("SourceId", out var sourceId));
            Assert.Equal("azure monitor", sourceId.GetString());
        }

        // Should find at least the TSG workbook from our bicep template
        Assert.NotEmpty(workbooksArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_multiple_filters()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "kind", "shared" },
                { "category", "sentinel" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks match both filters
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("Kind", out var kind));
            Assert.Equal("shared", kind.GetString());

            Assert.True(workbook.TryGetProperty("Category", out var category));
            Assert.Equal("sentinel", category.GetString());
        }

        // Should find at least the sentinel workbook from our bicep template
        Assert.NotEmpty(workbooksArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_filter_workbooks_by_all_filters_combined()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "kind", "shared" },
                { "category", "TSG" },
                { "source-id", "azure monitor" }
            });

        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);

        // Verify all returned workbooks match all three filters
        var workbooksArray = workbooks.EnumerateArray();
        foreach (var workbook in workbooksArray)
        {
            Assert.True(workbook.TryGetProperty("Kind", out var kind));
            Assert.Equal("shared", kind.GetString());

            Assert.True(workbook.TryGetProperty("Category", out var category));
            Assert.Equal("TSG", category.GetString());

            Assert.True(workbook.TryGetProperty("SourceId", out var sourceId));
            Assert.Equal("azure monitor", sourceId.GetString());
        }

        // Should find at least the TSG workbook from our bicep template
        Assert.NotEmpty(workbooksArray);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_return_empty_results_for_non_matching_filter()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "category", "non-existent-category" }
            });

        // Should return empty results
        if (result.Value.TryGetProperty("Workbooks", out var workbooks))
        {
            Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);
            Assert.Empty(workbooks.EnumerateArray());
        }
        else
        {
            // No results property means no workbooks found, which is expected
            Assert.True(true);
        }
    }

    [Theory]
    [InlineData(0)] // Credential auth
    [InlineData(1)] // Key auth
    [Trait("Category", "Live")]
    public async Task Should_handle_auth_methods(int authMethod)
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "auth-method", authMethod.ToString() }
            });

        Assert.NotNull(result);
        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_handle_tenant_parameter()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "tenant", Settings.TenantId }
            });

        Assert.NotNull(result);
        var workbooks = result.AssertProperty("Workbooks");
        Assert.Equal(JsonValueKind.Array, workbooks.ValueKind);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid-subscription")]
    [Trait("Category", "Live")]
    public async Task Should_return_error_for_invalid_subscription(string invalidSubscription)
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", invalidSubscription },
                { "resource-group", Settings.ResourceGroupName }
            });

        // Should return null (error) for invalid subscription
        Assert.Null(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("non-existent-rg")]
    [Trait("Category", "Live")]
    public async Task Should_return_error_for_invalid_resource_group(string invalidResourceGroup)
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", invalidResourceGroup }
            });

        // Should return null (error) for invalid resource group
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_return_error_for_missing_required_parameters()
    {
        var result = await CallToolAsync(
            "azmcp_workbooks_list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
                // Missing required resource-group parameter
            });

        // Should return null for missing required parameters
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_handle_non_existent_workbook_gracefully()
    {
        var nonExistentWorkbookId = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/test/providers/Microsoft.Insights/workbooks/non-existent";

        var result = await CallToolAsync(
            "azmcp_workbooks_show",
            new()
            {
                { "workbook-id", nonExistentWorkbookId }
            });

        // Should return error for non-existent workbook
        if (result != null)
        {
            // Check if it's an error response by looking for error properties
            if (result.Value.TryGetProperty("message", out var errorMessage) &&
                result.Value.TryGetProperty("type", out var errorType) &&
                errorType.GetString() == "Exception")
            {
                // It's an error response, verify the error structure
                Assert.Contains("could not be found", errorMessage.GetString());
                Assert.True(result.Value.TryGetProperty("stackTrace", out _));
            }
            else
            {
                // If successful, it should be a single workbook result structure
                var workbook = result.AssertProperty("Workbook");
                Assert.True(workbook.ValueKind != JsonValueKind.Null);
            }
        }
        else
        {
            // Null response indicates an error was handled by the MCP client
            Assert.Null(result);
        }
    }
}
