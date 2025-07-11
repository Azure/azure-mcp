using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Commands.Aks;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using AzureMcp.Services.ProcessExecution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Aks;

public class KubectlCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IExternalProcessService _processService;
    private readonly IToolInstallationService _toolInstallationService;
    private readonly ILogger<KubectlCommand> _logger;
    private readonly KubectlCommand _command;

    public KubectlCommandTests()
    {
        _processService = Substitute.For<IExternalProcessService>();
        _toolInstallationService = Substitute.For<IToolInstallationService>();
        _logger = Substitute.For<ILogger<KubectlCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_processService);
        collection.AddSingleton(_toolInstallationService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("kubectl", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--command \"get pods\" --kubeconfig /path/to/config", true)]
    [InlineData("--command \"get pods\"", false)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var mockResult = new ProcessResult(0, "{\"items\":[]}", "", "get pods");
            _toolInstallationService.FindKubectlAsync().Returns("kubectl");
            _processService.ExecuteAsync(Arg.Any<string>(), Arg.Any<string>(), 300)
                .Returns(mockResult);
            _processService.ParseJsonOutput(mockResult)
                .Returns(JsonDocument.Parse("{}").RootElement);
        }

        var context = new CommandContext(_serviceProvider);
        var parser = new Parser(_command.GetCommand());
        var parseResult = parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (!shouldSucceed)
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesProcessErrors()
    {
        // Arrange
        _toolInstallationService.FindKubectlAsync().Returns("kubectl");
        _processService.ExecuteAsync(Arg.Any<string>(), Arg.Any<string>(), 300)
            .Returns(Task.FromException<ProcessResult>(new Exception("Test error")));

        var context = new CommandContext(_serviceProvider);
        var parser = new Parser(_command.GetCommand());
        var parseResult = parser.Parse("--command \"get pods\" --kubeconfig /path/to/config");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }
}
