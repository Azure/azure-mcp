// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Deploy.Commands;
using AzureMcp.Areas.Deploy.Options;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Deploy.UnitTests;

[Trait("Area", "Deploy")]
public class ArchitectureDiagramTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<GenerateArchitectureDiagramCommand> _logger;

    public ArchitectureDiagramTests()
    {
        _logger = Substitute.For<ILogger<GenerateArchitectureDiagramCommand>>();

        var collection = new ServiceCollection();
        _serviceProvider = collection.BuildServiceProvider();
    }


    [Fact]
    public async Task GenerateArchitectureDiagram_ShouldReturnNoServiceDetected()
    {
        var command = new GenerateArchitectureDiagramCommand(_logger);
        var args = command.GetCommand().Parse(["--rawMcpToolInput", "{\"projectName\": \"test\",\"services\": []}"]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Contains("No service detected", response.Message);
    }

    [Fact]
    public async Task GenerateArchitectureDiagram_ShouldReturnEncryptedDiagramUrl()
    {
        var command = new GenerateArchitectureDiagramCommand(_logger);
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
                }
            }
        };

        var args = command.GetCommand().Parse(["--rawMcpToolInput", JsonSerializer.Serialize(appTopology)]);
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
        var encodedDiagram = extractedUrl.Substring(urlPattern.Length);
        var decodedDiagram = EncodeMermaid.GetDecodedMermaidChart(encodedDiagram);
        Assert.NotEmpty(decodedDiagram);
        Assert.Contains("website", decodedDiagram);
        Assert.Contains("store", decodedDiagram);
    }
}
