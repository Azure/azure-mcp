// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Aks.Commands.Cluster;
using AzureMcp.Aks.Models;
using AzureMcp.Aks.Services;

namespace AzureMcp.Aks.Commands;

[JsonSerializable(typeof(ClusterListCommand.ClusterListCommandResult))]
[JsonSerializable(typeof(ClusterGetCommand.ClusterGetCommandResult))]
[JsonSerializable(typeof(Models.Cluster))]
[JsonSerializable(typeof(AksCluster))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class AksJsonContext : JsonSerializerContext;
