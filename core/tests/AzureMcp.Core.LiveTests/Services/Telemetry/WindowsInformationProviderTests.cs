// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Core.LiveTests.Services.Telemetry;

[SupportedOSPlatform("windows")]
public class WindowsInformationProviderTests
{
    [Fact]
    [Trait("Category", "Live")]
    public void GetOrCreateDeviceId_WorksCorrectly()
    {
        Assert.SkipUnless(RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
            "Only supported on Windows.");

        // Arrange
        var _logger = Substitute.For<ILogger<WindowsMachineInformationProvider>>();
        var provider = new WindowsMachineInformationProvider(_logger);

        // Act
        var deviceId = provider.GetOrCreateDeviceId();

        // Assert
        Assert.NotNull(deviceId);
        Assert.NotEmpty(deviceId);

        // Verify it's persisted by calling again
        var deviceId2 = provider.GetOrCreateDeviceId();
        Assert.Equal(deviceId, deviceId2);
    }
}
