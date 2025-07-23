// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using ModelContextProtocol.Client;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.LiveTests;

/// <summary>
/// Live integration tests for Azure MCP Server that validate tool loading behavior across different modes.
/// These tests start actual MCP server instances and verify the correct tools are loaded.
/// </summary>
[Trait("Area", "Server")]
public class ServerCommandTests(ITestOutputHelper output)
{
    protected ITestOutputHelper Output { get; } = output;

    #region Default Mode Tests

    [Fact]
    [Trait("Category", "Live")]
    public async Task DefaultMode_LoadsAllTools()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(listResult);

        // Should have tools from multiple areas
        var toolNames = listResult.Select(t => t.Name).ToList();

        // Should include Azure service tools and extension tools
        Assert.True(toolNames.Count > 60, $"Expected more than 60 tools, got {toolNames.Count}");

        // Should include the microsoft_docs_search tool
        Assert.Contains("microsoft_docs_search", toolNames, StringComparer.OrdinalIgnoreCase);

        // Log for debugging
        Output.WriteLine($"Default mode loaded {toolNames.Count} tools");
        foreach (var name in toolNames.Take(10))
        {
            Output.WriteLine($"  - {name}");
        }
    }

    #endregion

    #region Single Tool Proxy Mode Tests

    [Fact]
    [Trait("Category", "Live")]
    public async Task SingleProxyMode_LoadsSingleAzureTool()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "single");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.Single(listResult);

        var tool = listResult.First();
        Assert.Equal("azure", tool.Name);
        Assert.Contains("azure", tool.Description, StringComparison.OrdinalIgnoreCase);

        Output.WriteLine($"Single proxy mode loaded 1 tool: {tool.Name}");
        Output.WriteLine($"Description: {tool.Description}");
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task SingleProxyMode_WithNamespaceFilter_StillLoadsSingleAzureTool()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "single", "--namespace", "storage");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        // Single proxy mode should still expose single azure tool regardless of namespace filter
        Assert.Single(listResult);
        Assert.Equal("azure", listResult.First().Name);

        Output.WriteLine("Single proxy mode with namespace filter still loaded 1 tool");
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task SingleProxyMode_WithReadOnlyFlag_LoadsSingleAzureTool()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "single", "--read-only");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.Single(listResult);
        Assert.Equal("azure", listResult.First().Name);

        // Verify read-only behavior for single proxy mode
        var tool = listResult.First();
        var readOnlyHint = tool.ProtocolTool?.Annotations?.ReadOnlyHint;

        // The azure tool should have read-only hint when in read-only mode
        Output.WriteLine($"Single proxy read-only tool ReadOnlyHint: {readOnlyHint}");
        Output.WriteLine($"Tool annotations: {tool.ProtocolTool?.Annotations != null}");

        // For single proxy mode, the ReadOnlyHint may be null because it's a proxy tool
        // but we should still have annotations present
        Assert.NotNull(tool.ProtocolTool?.Annotations);

        Output.WriteLine("Single proxy read-only mode loaded 1 tool");
    }

    #endregion

    #region Namespace Proxy Mode Tests

    [Fact]
    [Trait("Category", "Live")]
    public async Task NamespaceProxyMode_LoadsNamespaceTools()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "namespace");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(listResult);

        var toolNames = listResult.Select(t => t.Name).ToList();

        // In namespace mode without specific namespaces, should default to extension tools
        Assert.True(toolNames.Count > 20, "Should have more than 20 tools in namespace mode");

        // Should include the documentation tool
        Assert.Contains("documentation", toolNames, StringComparer.OrdinalIgnoreCase);

        Output.WriteLine($"Namespace proxy mode loaded {toolNames.Count} tools");
        foreach (var name in toolNames.Take(10))
        {
            Output.WriteLine($"  - {name}");
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task NamespaceProxyMode_WithSpecificNamespaces_LoadsNamespaceSpecificTools()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "namespace", "--namespace", "storage", "--namespace", "keyvault");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(listResult);

        var toolNames = listResult.Select(t => t.Name).ToList();

        // Should include namespace-specific tools
        var hasRelevantTools = toolNames.Any(name =>
            name.Contains("storage", StringComparison.OrdinalIgnoreCase) ||
            name.Contains("keyvault", StringComparison.OrdinalIgnoreCase));

        Assert.True(hasRelevantTools, "Should have tools related to specified namespaces");

        // Should not include documentation tool when explicit namespaces are specified
        Assert.DoesNotContain("documentation", toolNames, StringComparer.OrdinalIgnoreCase);

        // Should contain exactly 2 tools for the specified namespaces
        Assert.Equal(2, toolNames.Count);

        // Verify tools are exactly from storage and keyvault namespaces
        Assert.All(toolNames, toolName =>
        {
            var isStorageOrKeyVault = toolName.Contains("storage", StringComparison.OrdinalIgnoreCase) ||
                                    toolName.Contains("keyvault", StringComparison.OrdinalIgnoreCase);
            Assert.True(isStorageOrKeyVault, $"Tool '{toolName}' should be related to storage or keyvault namespaces");
        });

        Output.WriteLine($"Namespace proxy mode with [storage, keyvault] loaded {toolNames.Count} tools");
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task NamespaceProxyMode_WithDocumentationNamespace_LoadsOnlyDocumentationTool()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "namespace", "--namespace", "documentation");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(listResult);

        var toolNames = listResult.Select(t => t.Name).ToList();

        // Should contain only the documentation tool
        Assert.Single(listResult);
        Assert.Contains("documentation", toolNames, StringComparer.OrdinalIgnoreCase);

        // Verify it's exactly the documentation tool
        Assert.Equal("documentation", toolNames.First(), StringComparer.OrdinalIgnoreCase);

        Output.WriteLine($"Namespace proxy mode with [documentation] loaded {toolNames.Count} tools");
        Output.WriteLine($"Tool: {toolNames.First()}");
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task NamespaceProxyMode_IncludesExtensionTools()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "namespace");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(listResult);

        var toolNames = listResult.Select(t => t.Name).ToList();

        // Should include specific extension tools
        var hasExtensionAz = toolNames.Any(name => name.Equals("extension_az", StringComparison.OrdinalIgnoreCase));
        var hasExtensionAzd = toolNames.Any(name => name.Equals("extension_azd", StringComparison.OrdinalIgnoreCase));

        Assert.True(hasExtensionAz, "Should have extension_az tool");
        Assert.True(hasExtensionAzd, "Should have extension_azd tool");

        Output.WriteLine($"Namespace proxy mode with [extension] loaded {toolNames.Count} tools");
        Output.WriteLine($"Found extension_az: {hasExtensionAz}");
        Output.WriteLine($"Found extension_azd: {hasExtensionAzd}");

        // Log all tools for debugging
        foreach (var name in toolNames)
        {
            Output.WriteLine($"  - {name}");
        }
    }

    #endregion

    #region Default Mode with Filters Tests

    [Fact]
    [Trait("Category", "Live")]
    public async Task DefaultMode_WithNamespaceFilter_LoadsFilteredTools()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--namespace", "storage", "--namespace", "keyvault");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(listResult);

        var toolNames = listResult.Select(t => t.Name).ToList();

        // Should only include tools from specified namespaces
        var hasStorageOrKeyVault = toolNames.Any(name =>
            name.Contains("storage", StringComparison.OrdinalIgnoreCase) ||
            name.Contains("keyvault", StringComparison.OrdinalIgnoreCase));

        Assert.True(hasStorageOrKeyVault, "Should have tools related to storage or keyvault namespaces");

        Output.WriteLine($"Default mode with namespaces [storage, keyvault] loaded {toolNames.Count} tools");
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task DefaultMode_WithReadOnlyFlag_LoadsOnlyReadOnlyTools()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--read-only");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(listResult);

        // All tools should be read-only or the count should be reduced
        var toolCount = listResult.Count();
        Assert.True(toolCount > 0, "Should have at least some read-only tools");

        // Verify tools have read-only annotations
        var toolsWithReadOnlyHint = 0;
        var toolsWithAnnotations = 0;

        foreach (var tool in listResult)
        {
            var hasAnnotations = tool.ProtocolTool?.Annotations != null;
            var readOnlyHint = tool.ProtocolTool?.Annotations?.ReadOnlyHint;

            if (hasAnnotations)
            {
                toolsWithAnnotations++;
            }

            if (readOnlyHint.HasValue && readOnlyHint.Value)
            {
                toolsWithReadOnlyHint++;
            }

            Output.WriteLine($"Tool: {tool.Name} - HasAnnotations: {hasAnnotations}, ReadOnlyHint: {readOnlyHint}");
        }

        Output.WriteLine($"Tools with annotations: {toolsWithAnnotations}/{toolCount}");
        Output.WriteLine($"Tools with ReadOnlyHint=true: {toolsWithReadOnlyHint}/{toolCount}");

        // In read-only mode, ALL tools must have annotations and ReadOnlyHint=true
        Assert.Equal(toolCount, toolsWithAnnotations);
        Assert.Equal(toolCount, toolsWithReadOnlyHint);

        // Additional verification message
        Output.WriteLine("✓ All tools have annotations and ReadOnlyHint=true as expected in read-only mode");

        Output.WriteLine($"Default read-only mode loaded {toolCount} tools");
    }

    #endregion

    #region Negative Tests - Invalid Modes and Namespaces

    [Fact]
    [Trait("Category", "Live")]
    public async Task InvalidMode_GracefullyHandlesError()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--mode", "invalid-mode");
        await fixture.InitializeAsync();
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task InvalidNamespace_LoadsGracefully()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start", "--namespace", "invalid-namespace", "--namespace", "another-invalid");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        // Should not crash, but may have fewer or no tools
        Assert.NotNull(listResult);

        // Invalid namespaces should result in 0 tools
        Assert.Empty(listResult);

        Output.WriteLine($"Invalid namespaces loaded {listResult.Count()} tools");
    }

    #endregion

    #region Comparison Tests

    [Fact]
    [Trait("Category", "Live")]
    public async Task VerifyUniqueToolNames_InDefaultMode()
    {
        // Arrange
        await using var fixture = new LiveTestFixture();
        fixture.SetArguments("server", "start");
        await fixture.InitializeAsync();

        // Act
        var listResult = await fixture.Client.ListToolsAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        var toolNames = listResult.Select(t => t.Name).ToList();
        var uniqueNames = toolNames.Distinct().ToList();

        Assert.Equal(toolNames.Count, uniqueNames.Count);

        Output.WriteLine($"Verified {toolNames.Count} unique tool names in default mode");
    }

    #endregion
}
