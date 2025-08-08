// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Acr.Commands.Registry;
using AzureMcp.Acr.Services;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Acr.UnitTests.Registry;

public class RegistryListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAcrService _service;
    private readonly ILogger<RegistryListCommand> _logger;
    private readonly RegistryListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public RegistryListCommandTests()
    {
        _service = Substitute.For<IAcrService>();
        _logger = Substitute.For<ILogger<RegistryListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--subscription sub", true)]
    [InlineData("--subscription sub --resource-group rg", true)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _service.ListRegistries(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
                .Returns(new List<string> { "registry1", "registry2" });
        }

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

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
        _service.ListRegistries(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<List<string>>(new Exception("Test error")));

        var parseResult = _parser.Parse(["--subscription", "sub"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_FiltersById_ReturnsFilteredRegistries()
    {
        // Arrange
        var expectedRegistries = new List<string> { "registry1" };
        _service.ListRegistries("sub", "rg", Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedRegistries);

        var parseResult = _parser.Parse(["--subscription", "sub", "--resource-group", "rg"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        await _service.Received(1).ListRegistries("sub", "rg", Arg.Any<string>(), Arg.Any<RetryPolicyOptions>());
    }
}
