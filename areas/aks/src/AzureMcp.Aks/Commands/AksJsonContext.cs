// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Aks.Commands.Cluster;

namespace AzureMcp.Aks.Commands;

[JsonSerializable(typeof(ClusterListCommand.ClusterListCommandResult))]
[JsonSerializable(typeof(Models.Cluster))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public sealed partial class AksJsonContext : JsonSerializerContext;
