// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using Microsoft.Extensions.Azure;

namespace AzureMcp.Services.Telemetry;

/// <summary>
/// Provides access to services.
/// </summary>
public class TelemetryService : ITelemetryService
{
    private readonly AzureEventSourceLogForwarder _logForwarder;

    public ActivitySource Parent { get; }

    public TelemetryService(AzureEventSourceLogForwarder logForwarder, string name, string version)
    {
        _logForwarder = logForwarder;
        _logForwarder.Start();

        var tagsList = new List<KeyValuePair<string, object?>>()
        {
            new(TelemetryConstants.AzureMcpVersion, version)
        };

        Parent = new ActivitySource(name, version, tagsList);
    }

    public Activity? StartActivity(string activityId)
    {
        return Parent.StartActivity(activityId);
    }

    public void Dispose()
    {
        _logForwarder.Dispose();
    }
}
