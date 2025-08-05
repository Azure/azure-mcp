// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.Extension.Commands;
using AzureMcp.Extension.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Extension.UnitTests;

public sealed class AzCommandTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsExpectedResponse_FromApiClient()
    {
        // Arrange
        var logger = Substitute.For<ILogger<AzCommand>>();
        var copilotService = Substitute.For<IAzCommandCopilotService>();
        var expectedOutput = "my hardcoded az cli response";
        copilotService.GenerateAzCliCommandAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(expectedOutput);

        var services = new ServiceCollection();
        services.AddSingleton(copilotService);
        var serviceProvider = services.BuildServiceProvider();

        var command = new AzCommand(logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse("--intent \"List all resource groups\"");
        var context = new CommandContext(serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
    }
}
