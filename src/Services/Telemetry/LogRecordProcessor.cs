// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using OpenTelemetry;
using OpenTelemetry.Logs;

namespace AzureMcp.Services.Telemetry;

/// <summary>
/// Removes Attributes, Body, and FormattedMessage from <see cref="LogRecord"/>.
/// </summary>
internal class LogRecordProcessor : BaseProcessor<LogRecord>
{
    private static readonly IReadOnlyList<KeyValuePair<string, object?>> EmptyAttributes = new List<KeyValuePair<string, object?>>().AsReadOnly();

    public override void OnEnd(LogRecord data)
    {
        data.Attributes = EmptyAttributes;
        data.Body = string.Empty;
        data.FormattedMessage = string.Empty;
    }
}
