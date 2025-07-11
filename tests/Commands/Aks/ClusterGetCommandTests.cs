using System.CommandLine.Parsing;
using Azure.ResourceManager.ContainerService;
using Azure.ResourceManager.ContainerService.Models;
using AzureMcp.Commands.Aks;
using AzureMcp.Models.Command;
using AzureMcp.Options.Aks.Cluster;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Aks;

public class ClusterGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAksService _service;
    private readonly ILogger<ClusterGetCommand> _logger;
    private readonly ClusterGetCommand _command;

    public ClusterGetCommandTests()
    {
        _service = Substitute.For<IAksService>();
        _logger = Substitute.For<ILogger<ClusterGetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("get", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--subscription sub --resource-group rg --cluster test-cluster", true)]
    [InlineData("--subscription sub", false)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            // Create a mock cluster data with required location parameter
            var mockClusterData = new ContainerServiceManagedClusterData(Azure.Core.AzureLocation.EastUS);
            _service.GetCluster(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<AzureMcp.Options.RetryPolicyOptions>())
                .Returns(mockClusterData);
        }

        var context = new CommandContext(_serviceProvider);
        var parser = new Parser(_command.GetCommand());
        var parseResult = parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
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
        _service.GetCluster(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<AzureMcp.Options.RetryPolicyOptions>())
            .Returns(Task.FromException<ContainerServiceManagedClusterData>(new Exception("Test error")));

        var context = new CommandContext(_serviceProvider);
        var parser = new Parser(_command.GetCommand());
        var parseResult = parser.Parse("--subscription sub --resource-group rg --cluster test-cluster");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }
}
