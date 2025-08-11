// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Deploy.Commands;
using AzureMcp.Deploy.Commands.Architecture;
using AzureMcp.Deploy.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Deploy.UnitTests.Commands.Architecture;


public class DiagramGenerateCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DiagramGenerateCommand> _logger;

    public DiagramGenerateCommandTests()
    {
        _logger = Substitute.For<ILogger<DiagramGenerateCommand>>();

        var collection = new ServiceCollection();
        _serviceProvider = collection.BuildServiceProvider();
    }


    [Fact]
    public async Task GenerateArchitectureDiagram_ShouldReturnNoServiceDetected()
    {
        var command = new DiagramGenerateCommand(_logger);
        var args = command.GetCommand().Parse(["--raw-mcp-tool-input", "{\"projectName\": \"test\",\"services\": []}"]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Contains("No service detected", response.Message);
    }

    [Fact]
    public async Task GenerateArchitectureDiagram_ShouldReturnEncryptedDiagramUrl()
    {
        var command = new DiagramGenerateCommand(_logger);
        var appTopology = new AppTopology()
        {
            WorkspaceFolder = "testWorkspace",
            ProjectName = "testProject",
            Services = new ServiceConfig[]
            {
                new ServiceConfig
                {
                    Name = "website",
                    AzureComputeHost = "appservice",
                    Language = "dotnet",
                    Port = "80",
                    Dependencies = new DependencyConfig[]
                    {
                        new DependencyConfig { Name = "store", ConnectionType = "system-identity", ServiceType = "azurestorageaccount" }
                    },
                },
                new ServiceConfig
                {
                    Name = "frontend",
                    Path = "testWorkspace/web",
                    AzureComputeHost = "containerapp",
                    Language = "js",
                    Port = "8080",
                    Dependencies = new DependencyConfig[]
                    {
                        new DependencyConfig { Name = "backend", ConnectionType = "http", ServiceType = "containerapp" }
                    }
                },
                new ServiceConfig
                {
                    Name = "backend",
                    Path = "testWorkspace/api",
                    AzureComputeHost = "containerapp",
                    Language = "python",
                    Port = "3000",
                    Dependencies = new DependencyConfig[]
                    {
                        new DependencyConfig { Name = "db", ConnectionType = "secret", ServiceType = "azurecosmosdb" },
                        new DependencyConfig { Name = "secretStore", ConnectionType = "system-identity", ServiceType = "azurekeyvault" }
                    }
                },
                new ServiceConfig
                {
                    Name = "frontendservice",
                    Path = "testWorkspace/web",
                    AzureComputeHost = "aks",
                    Language = "ts",
                    Port = "3001",
                    Dependencies = new DependencyConfig[]
                    {
                        new DependencyConfig { Name = "backendservice", ConnectionType = "user-identity", ServiceType = "aks"}
                    }
                },
                new ServiceConfig
                {
                    Name = "backendservice",
                    Path = "testWorkspace/api",
                    AzureComputeHost = "aks",
                    Language = "python",
                    Port = "3000",
                    Dependencies = new DependencyConfig[]
                    {
                        new DependencyConfig { Name = "database", ConnectionType = "user-identity", ServiceType = "azurecacheforredis" }
                    }
                }
            }
        };

        var args = command.GetCommand().Parse(["--raw-mcp-tool-input", JsonSerializer.Serialize(appTopology)]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        // Extract the URL from the response message
        var urlPattern = "https://mermaid.live/view#pako:";
        var urlStartIndex = response.Message.IndexOf(urlPattern);
        Assert.True(urlStartIndex >= 0, "URL starting with 'https://mermaid.live/view#pako:' should be present in the response");

        // Extract the full URL (assuming it ends at whitespace or end of string)
        var urlStartPosition = urlStartIndex;
        var urlEndPosition = response.Message.IndexOfAny([' ', '\n', '\r', '\t'], urlStartPosition);
        if (urlEndPosition == -1)
            urlEndPosition = response.Message.Length;

        var extractedUrl = response.Message.Substring(urlStartPosition, urlEndPosition - urlStartPosition);
        Assert.StartsWith(urlPattern, extractedUrl);
        var encodedDiagram = extractedUrl.Substring(urlPattern.Length).Replace("_", "/").Replace("-", "+"); // Replace back for decoding
        var decodedDiagram = EncodeMermaid.GetDecodedMermaidChart(encodedDiagram);
        Assert.NotEmpty(decodedDiagram);
        Assert.Contains("website", decodedDiagram);
        Assert.Contains("store", decodedDiagram);
    }
}
