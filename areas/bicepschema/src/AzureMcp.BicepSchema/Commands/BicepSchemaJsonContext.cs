// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.BicepSchema.Commands;

namespace AzureMcp.BicepSchema.Commands;

[JsonSerializable(typeof(BicepSchemaGetCommand.BicepSchemaGetCommandResult))]
internal sealed partial class BicepSchemaJsonContext : JsonSerializerContext
{
}
