// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Quota.Commands;
using AzureMcp.Quota.Commands.Region;
using AzureMcp.Quota.Commands.Usage;
using AzureMcp.Quota.Services.Util;

namespace AzureMcp.Quota.Commands;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
[JsonSerializable(typeof(CheckCommand.UsageCheckCommandResult))]
[JsonSerializable(typeof(AvailabilityListCommand.RegionCheckCommandResult))]
[JsonSerializable(typeof(UsageInfo))]
[JsonSerializable(typeof(Dictionary<string, List<UsageInfo>>))]
internal sealed partial class QuotaJsonContext : JsonSerializerContext
{
}
