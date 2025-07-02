// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.TerraformSchema.Commands;

[JsonSerializable(typeof(TerraformSchemaGetCommand.TerraformSchemaGetCommandResult))]
internal sealed partial class TerraformSchemaJsonContext : JsonSerializerContext
{
}
