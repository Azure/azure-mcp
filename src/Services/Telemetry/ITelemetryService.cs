// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;

namespace AzureMcp.Services.Telemetry;

public interface ITelemetryService : IDisposable
{
    Activity? StartActivity(string activityName);
}
