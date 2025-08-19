// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.Core.Configuration;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;
using static AzureMcp.Core.Services.Telemetry.TelemetryConstants;

namespace AzureMcp.Core.Services.Telemetry;

/// <summary>
/// Provides access to services.
/// </summary>
internal class TelemetryService : ITelemetryService
{
    private readonly bool _isEnabled;
    private readonly List<KeyValuePair<string, object?>> _tagsList;
    private readonly IMachineInformationProvider _informationProvider;

    internal ActivitySource Parent { get; }

    public TelemetryService(IMachineInformationProvider informationProvider, IOptions<AzureMcpServerConfiguration> options)
    {
        _isEnabled = options.Value.IsTelemetryEnabled;
        _tagsList = new List<KeyValuePair<string, object?>>()
        {
            new(TagName.AzureMcpVersion, options.Value.Version),
        };

        Parent = new ActivitySource(options.Value.Name, options.Value.Version, _tagsList);
        _informationProvider = informationProvider;

        InitializeTagList();
    }

    public Activity? StartActivity(string activityId) => StartActivity(activityId, null);

    public Activity? StartActivity(string activityId, Implementation? clientInfo)
    {
        if (!_isEnabled)
        {
            return null;
        }

        var activity = Parent.StartActivity(activityId);

        if (activity == null)
        {
            return activity;
        }

        if (clientInfo != null)
        {
            activity.AddTag(TagName.ClientName, clientInfo.Name)
                .AddTag(TagName.ClientVersion, clientInfo.Version);
        }

        activity.AddTag(TagName.EventId, Guid.NewGuid().ToString());

        _tagsList.ForEach(kvp => activity.AddTag(kvp.Key, kvp.Value));

        return activity;
    }

    public void Dispose()
    {
    }

    private void InitializeTagList()
    {
        var macAddressHash = _informationProvider.GetMacAddressHash();
        var deviceId = _informationProvider.GetOrCreateDeviceId();

        _tagsList.Add(new(TagName.MacAddressHash, macAddressHash));
        _tagsList.Add(new(TagName.DevDeviceId, deviceId));
    }
}
