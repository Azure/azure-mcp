// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;

namespace Microsoft.Mcp.Core.Services.Telemetry;

/// <summary>
/// Base class for machine information providers.
/// </summary>
public abstract class MachineInformationProviderBase(ILogger<MachineInformationProviderBase> logger) : IMachineInformationProvider
{
    protected readonly ILogger<MachineInformationProviderBase> Logger = logger;

    /// <summary>
    /// Gets or creates a unique device identifier.
    /// </summary>
    /// <returns>A unique device identifier, or null if not available.</returns>
    public abstract Task<string?> GetOrCreateDeviceId();

    /// <summary>
    /// Gets a hash of the machine's MAC address.
    /// </summary>
    /// <returns>A hash of the machine's MAC address.</returns>
    public abstract Task<string> GetMacAddressHash();
}