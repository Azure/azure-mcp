// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Core.LiveTests.Services.Telemetry;

public class DefaultMachineInformationProviderTests
{
    [Fact]
    public async Task ReturnsNullDeviceId()
    {
        var logger = Substitute.For<ILogger<DefaultMachineInformationProvider>>();
        var provider = new DefaultMachineInformationProvider(logger);

        var result = await provider.GetOrCreateDeviceId();

        Assert.Null(result);
    }
}
