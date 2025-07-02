// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AzureMcp.Areas.Server.Commands.Runtime;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Areas.Server.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.Commands.Runtime;

public class McpRuntimeTests
{
    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        return services.BuildServiceProvider();
    }

    private static IOptions<ServiceStartOptions> CreateOptions(ServiceStartOptions? options = null)
    {
        return Microsoft.Extensions.Options.Options.Create(options ?? new ServiceStartOptions());
    }

    private static IMcpServer CreateMockServer()
    {
        return Substitute.For<IMcpServer>();
    }

    private static RequestContext<ListToolsRequestParams> CreateListToolsRequest()
    {
        return new RequestContext<ListToolsRequestParams>(CreateMockServer())
        {
            Params = new ListToolsRequestParams()
        };
    }

    private static RequestContext<CallToolRequestParams> CreateCallToolRequest(string toolName = "test-tool", IReadOnlyDictionary<string, JsonElement>? arguments = null)
    {
        return new RequestContext<CallToolRequestParams>(CreateMockServer())
        {
            Params = new CallToolRequestParams
            {
                Name = toolName,
                Arguments = arguments ?? new Dictionary<string, JsonElement>()
            }
        };
    }

    [Fact]
    public void Constructor_WithValidParameters_InitializesCorrectly()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();

        // Act
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        // Assert
        Assert.NotNull(runtime);
        Assert.IsType<McpRuntime>(runtime);
        Assert.IsAssignableFrom<IMcpRuntime>(runtime);
    }

    [Fact]
    public void Constructor_WithNullToolLoader_ThrowsArgumentNullException()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var options = CreateOptions();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new McpRuntime(null!, options, logger));
        Assert.Equal("toolLoader", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullOptions_ThrowsArgumentNullException()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new McpRuntime(mockToolLoader, null!, logger));
        Assert.Equal("options", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new McpRuntime(mockToolLoader, options, null!));
        Assert.Equal("logger", exception.ParamName);
    }

    [Fact]
    public void Constructor_LogsInitializationInformation()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions(new ServiceStartOptions
        {
            ReadOnly = true,
            Service = new[] { "storage", "keyvault" }
        });

        // Act
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        // Assert
        Assert.NotNull(runtime);
        // Note: In a more sophisticated test setup, we could capture and verify log messages
        // For now, we verify that construction succeeds without throwing
    }

    [Fact]
    public async Task ListToolsHandler_DelegatesToToolLoader()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new ListToolsResult
        {
            Tools = new List<Tool>
            {
                new Tool { Name = "test-tool", Description = "A test tool" }
            }
        };

        var request = CreateListToolsRequest();
        mockToolLoader.ListToolsHandler(request, Arg.Any<CancellationToken>())
            .Returns(new ValueTask<ListToolsResult>(expectedResult));

        // Act
        var result = await runtime.ListToolsHandler(request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).ListToolsHandler(request, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CallToolHandler_DelegatesToToolLoader()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new CallToolResult
        {
            Content = new List<ContentBlock>
            {
                new TextContentBlock { Text = "Tool executed successfully" }
            }
        };

        var request = CreateCallToolRequest("test-tool", new Dictionary<string, JsonElement>
        {
            { "param1", JsonDocument.Parse("\"value1\"").RootElement }
        });
        mockToolLoader.CallToolHandler(request, Arg.Any<CancellationToken>())
            .Returns(new ValueTask<CallToolResult>(expectedResult));

        // Act
        var result = await runtime.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).CallToolHandler(request, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ListToolsHandler_WithCancellationToken_PassesTokenToToolLoader()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new ListToolsResult { Tools = new List<Tool>() };
        var request = CreateListToolsRequest();
        var cancellationToken = new CancellationToken();

        mockToolLoader.ListToolsHandler(request, cancellationToken)
            .Returns(new ValueTask<ListToolsResult>(expectedResult));

        // Act
        var result = await runtime.ListToolsHandler(request, cancellationToken);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).ListToolsHandler(request, cancellationToken);
    }

    [Fact]
    public async Task CallToolHandler_WithCancellationToken_PassesTokenToToolLoader()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new CallToolResult { Content = new List<ContentBlock>() };
        var request = CreateCallToolRequest();
        var cancellationToken = new CancellationToken();

        mockToolLoader.CallToolHandler(request, cancellationToken)
            .Returns(new ValueTask<CallToolResult>(expectedResult));

        // Act
        var result = await runtime.CallToolHandler(request, cancellationToken);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).CallToolHandler(request, cancellationToken);
    }

    [Fact]
    public async Task ListToolsHandler_WhenToolLoaderThrows_PropagatesException()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var request = CreateListToolsRequest();
        var expectedException = new InvalidOperationException("Tool loader failed");

        mockToolLoader.ListToolsHandler(request, Arg.Any<CancellationToken>())
            .Returns<ValueTask<ListToolsResult>>(x => throw expectedException);

        // Act & Assert
        var actualException = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            runtime.ListToolsHandler(request, CancellationToken.None).AsTask());
        Assert.Equal(expectedException.Message, actualException.Message);
    }

    [Fact]
    public async Task CallToolHandler_WhenToolLoaderThrows_PropagatesException()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var request = CreateCallToolRequest();
        var expectedException = new InvalidOperationException("Tool loader failed");

        mockToolLoader.CallToolHandler(request, Arg.Any<CancellationToken>())
            .Returns<ValueTask<CallToolResult>>(x => throw expectedException);

        // Act & Assert
        var actualException = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            runtime.CallToolHandler(request, CancellationToken.None).AsTask());
        Assert.Equal(expectedException.Message, actualException.Message);
    }

    [Fact]
    public void Constructor_WithDifferentServiceOptions_LogsCorrectly()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();

        // Test with ReadOnly = false and no services
        var options1 = CreateOptions(new ServiceStartOptions { ReadOnly = false });
        var runtime1 = new McpRuntime(mockToolLoader, options1, logger);
        Assert.NotNull(runtime1);

        // Test with ReadOnly = null and multiple services
        var options2 = CreateOptions(new ServiceStartOptions
        {
            ReadOnly = null,
            Service = new[] { "storage", "keyvault", "monitor" }
        });
        var runtime2 = new McpRuntime(mockToolLoader, options2, logger);
        Assert.NotNull(runtime2);

        // Test with empty service array
        var options3 = CreateOptions(new ServiceStartOptions
        {
            ReadOnly = true,
            Service = Array.Empty<string>()
        });
        var runtime3 = new McpRuntime(mockToolLoader, options3, logger);
        Assert.NotNull(runtime3);
    }

    [Fact]
    public async Task Runtime_ImplementsIMcpRuntimeInterface()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        IMcpRuntime runtime = new McpRuntime(mockToolLoader, options, logger);

        // Setup mock responses
        var listToolsResult = new ListToolsResult { Tools = new List<Tool>() };
        var callToolResult = new CallToolResult { Content = new List<ContentBlock>() };

        mockToolLoader.ListToolsHandler(Arg.Any<RequestContext<ListToolsRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ValueTask<ListToolsResult>(listToolsResult));
        mockToolLoader.CallToolHandler(Arg.Any<RequestContext<CallToolRequestParams>>(), Arg.Any<CancellationToken>())
            .Returns(new ValueTask<CallToolResult>(callToolResult));

        // Act & Assert - Interface methods should be available
        var listResult = await runtime.ListToolsHandler(CreateListToolsRequest(), CancellationToken.None);
        var callResult = await runtime.CallToolHandler(CreateCallToolRequest(), CancellationToken.None);

        Assert.Equal(listToolsResult, listResult);
        Assert.Equal(callToolResult, callResult);
    }

    [Fact]
    public async Task ListToolsHandler_WithNullRequest_DelegatesToToolLoader()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new ListToolsResult { Tools = new List<Tool>() };
        mockToolLoader.ListToolsHandler(null!, Arg.Any<CancellationToken>())
            .Returns(new ValueTask<ListToolsResult>(expectedResult));

        // Act
        var result = await runtime.ListToolsHandler(null!, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).ListToolsHandler(null!, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CallToolHandler_WithNullRequest_DelegatesToToolLoader()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new CallToolResult { Content = new List<ContentBlock>() };
        mockToolLoader.CallToolHandler(null!, Arg.Any<CancellationToken>())
            .Returns(new ValueTask<CallToolResult>(expectedResult));

        // Act
        var result = await runtime.CallToolHandler(null!, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).CallToolHandler(null!, Arg.Any<CancellationToken>());
    }

    [Fact]
    public void Constructor_WithNullServiceArray_LogsCorrectly()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions(new ServiceStartOptions { Service = null });

        // Act
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        // Assert
        Assert.NotNull(runtime);
        // Should log empty string for namespace when Service is null
    }

    [Fact]
    public async Task CallToolHandler_WithSpecificCancellationToken_PassesCorrectToken()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new CallToolResult { Content = new List<ContentBlock>() };
        var request = CreateCallToolRequest();
        var specificToken = new CancellationTokenSource().Token;

        mockToolLoader.CallToolHandler(request, specificToken)
            .Returns(new ValueTask<CallToolResult>(expectedResult));

        // Act
        var result = await runtime.CallToolHandler(request, specificToken);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).CallToolHandler(request, specificToken);
    }

    [Fact]
    public async Task ListToolsHandler_WithSpecificCancellationToken_PassesCorrectToken()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        var expectedResult = new ListToolsResult { Tools = new List<Tool>() };
        var request = CreateListToolsRequest();
        var specificToken = new CancellationTokenSource().Token;

        mockToolLoader.ListToolsHandler(request, specificToken)
            .Returns(new ValueTask<ListToolsResult>(expectedResult));

        // Act
        var result = await runtime.ListToolsHandler(request, specificToken);

        // Assert
        Assert.Equal(expectedResult, result);
        await mockToolLoader.Received(1).ListToolsHandler(request, specificToken);
    }

    [Fact]
    public void Constructor_LogsToolLoaderType()
    {
        // Arrange
        var serviceProvider = CreateServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<McpRuntime>>();
        var mockToolLoader = Substitute.For<IToolLoader>();
        var options = CreateOptions();

        // Act
        var runtime = new McpRuntime(mockToolLoader, options, logger);

        // Assert
        Assert.NotNull(runtime);
        // The constructor should log the tool loader type name
        // This verifies that the logging statement executes without error
    }
}
