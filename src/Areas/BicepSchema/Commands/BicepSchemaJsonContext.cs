// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.BicepSchema.Commands;

namespace AzureMcp.Commands.BicepSchema;

[JsonSerializable(typeof(BicepSchemaGetCommand.BicepSchemaGetCommandResult))]
internal sealed partial class BicepSchemaJsonContext : JsonSerializerContext
{
}
