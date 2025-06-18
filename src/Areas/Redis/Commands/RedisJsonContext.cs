// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.Redis.CacheForRedis;
using AzureMcp.Commands.Redis.ManagedRedis;

namespace AzureMcp.Commands.Redis;

[JsonSerializable(typeof(CacheListCommand.CacheListCommandResult))]
[JsonSerializable(typeof(AccessPolicyListCommand.AccessPolicyListCommandResult))]
[JsonSerializable(typeof(ClusterListCommand.ClusterListCommandResult))]
[JsonSerializable(typeof(DatabaseListCommand.DatabaseListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class RedisJsonContext : JsonSerializerContext;
