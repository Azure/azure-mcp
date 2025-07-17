// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.Extension.Commands;
using AzureMcp.Areas.Extension.Services;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Extension.UnitTests;

[Trait("Area", "Extension")]
public sealed class GenerateAzCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IExtensionService _extensionService;
    private readonly ILogger<AzCommand> _logger;

    public GenerateAzCommandTests()
    {
        _extensionService = Substitute.For<IExtensionService>();
        _logger = Substitute.For<ILogger<AzCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_extensionService);
        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = new AzCommand(_logger).GetCommand();
        Assert.Equal("az", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--intent 'List all resource groups'", true)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _extensionService.GenerateAzCommandAsync(Arg.Any<string>())
                .Returns("az group list");
        }

        var command = new AzCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var parseResult = parser.Parse(args);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _extensionService.GenerateAzCommandAsync(Arg.Any<string>())
            .Returns(Task.FromException<string>(new Exception("Test error")));

        var command = new AzCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var parseResult = parser.Parse("--intent 'List all resource groups'");
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
    }
}
