// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.BicepSchema.Commands;

[JsonSerializable(typeof(BicepSchemaGetCommand.BicepSchemaGetCommandResult))]
internal sealed partial class BicepSchemaJsonContext : JsonSerializerContext
{
}
