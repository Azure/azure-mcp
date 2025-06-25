// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.Configuration;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;
using static AzureMcp.Services.Telemetry.TelemetryConstants;

namespace AzureMcp.Services.Telemetry;

/// <summary>
/// Provides access to services.
/// </summary>
public class TelemetryService : ITelemetryService
{
    private readonly AzureEventSourceLogForwarder? _logForwarder;
    private readonly bool _isEnabled;

    public ActivitySource Parent { get; }

    public TelemetryService(IOptions<AzureMcpServerConfiguration> options, AzureEventSourceLogForwarder? logForwarder = null)
    {
        _isEnabled = options.Value.IsTelemetryEnabled;

        if (_isEnabled && logForwarder != null)
        {
            _logForwarder = logForwarder;
            _logForwarder?.Start();
        }

        var tagsList = new List<KeyValuePair<string, object?>>()
        {
            new(TagName.AzureMcpVersion, options.Value.Version),
        };

        Parent = new ActivitySource(options.Value.Name, options.Value.Version, tagsList);
    }

    public Activity? StartActivity(string activityId) => StartActivity(activityId, null);

    public Activity? StartActivity(string activityId, Implementation? clientInfo)
    {
        if (!_isEnabled)
        {
            return null;
        }

        var activity = Parent.StartActivity(activityId);

        if (activity != null && clientInfo != null)
        {
            activity.AddTag(TagName.ClientName, clientInfo.Name)
                .AddTag(TagName.ClientVersion, clientInfo.Version);
        }

        return activity;
    }

    public void Dispose()
    {
        _logForwarder?.Dispose();
    }
}
