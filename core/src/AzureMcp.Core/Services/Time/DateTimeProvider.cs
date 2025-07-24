// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Core.Services.Time;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
