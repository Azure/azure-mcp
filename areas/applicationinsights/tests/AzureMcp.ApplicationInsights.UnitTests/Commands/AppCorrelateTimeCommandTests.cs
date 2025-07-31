// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.ApplicationInsights.Commands;
using AzureMcp.ApplicationInsights.Services;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.ApplicationInsights.UnitTests.Commands;

[Trait("Area", "ApplicationInsights")]
public class AppCorrelateTimeCommandTests
{
    private readonly CommandContext _context;
    private readonly IAppDiagnoseService _appDiagoseService = Substitute.For<IAppDiagnoseService>();
    private readonly ILogger<AppCorrelateTimeCommand> _logger = Substitute.For<ILogger<AppCorrelateTimeCommand>>();

    private readonly AppCorrelateTimeCommand _sut;

    public AppCorrelateTimeCommandTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IAppDiagnoseService>(_appDiagoseService);
        var sp = serviceCollection.BuildServiceProvider();

        _context = new CommandContext(sp);

        _sut = new AppCorrelateTimeCommand(_logger);
    }

    [Fact]
    public async Task Validates_NoOptionsPassed_ReturnsInvalid()
    {
        var args = _sut.GetCommand().ParseFromDictionary(new Dictionary<string, JsonElement>());

        var response = await _sut.ExecuteAsync(_context, args);

        Assert.Equal(400, response.Status);
        Assert.Equal("Missing Required options: --data-sets", response.Message);
    }
}
