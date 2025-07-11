using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Commands.Aks;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using AzureMcp.Options.Aks.Cluster;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Aks;

public class ClusterListCommandTests
{
    private readonly IServiceProvider _provider;
    private readonly IAksService _service;
    private readonly ClusterListCommand _command;
    private readonly Parser _parser;
    private readonly CommandContext _context;

    public ClusterListCommandTests()
    {
        _service = Substitute.For<IAksService>();
        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _provider = collection.BuildServiceProvider();
        var logger = Substitute.For<ILogger<ClusterListCommand>>();
        _command = new ClusterListCommand(logger);
        _parser = new Parser(_command.GetCommand());
        _context = new CommandContext(_provider);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsClusters()
    {
        var sub = "sub";
        _service.ListClusters(sub, null, null).Returns(new List<string> { "c1" });
        var args = _parser.Parse(new[] { "--subscription", sub });
        var result = await _command.ExecuteAsync(_context, args);
        Assert.NotNull(result.Results);
    }
}
