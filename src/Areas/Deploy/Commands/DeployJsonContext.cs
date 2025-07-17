// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Areas.Deploy.Services.Util;
using AzureMcp.Areas.Deploy.Commands.Quota;
using AzureMcp.Areas.Deploy.Commands.Region;
using AzureMcp.Areas.Deploy.Models;
using AzureMcp.Areas.Deploy.Options;

namespace AzureMcp.Areas.Deploy.Commands;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
[JsonSerializable(typeof(AppTopology))]
[JsonSerializable(typeof(MermaidData))]
[JsonSerializable(typeof(MermaidConfig))]
[JsonSerializable(typeof(QuotaCheckCommand.QuotaCheckCommandResult))]
[JsonSerializable(typeof(QuotaInfo))]
[JsonSerializable(typeof(Dictionary<string, List<QuotaInfo>>))]
[JsonSerializable(typeof(RegionCheckCommand.RegionCheckCommandResult))]
[JsonSerializable(typeof(List<string>))]
internal sealed partial class DeployJsonContext : JsonSerializerContext
{
}
