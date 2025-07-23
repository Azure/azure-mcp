// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Quota.Services.Util;
using AzureMcp.Areas.Quota.Commands;

namespace AzureMcp.Areas.Quota.Commands;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
[JsonSerializable(typeof(UsageCheckCommand.UsageCheckCommandResult))]
[JsonSerializable(typeof(RegionCheckCommand.RegionCheckCommandResult))]
[JsonSerializable(typeof(UsageInfo))]
[JsonSerializable(typeof(Dictionary<string, List<UsageInfo>>))]
internal sealed partial class QuotaJsonContext : JsonSerializerContext
{
}
