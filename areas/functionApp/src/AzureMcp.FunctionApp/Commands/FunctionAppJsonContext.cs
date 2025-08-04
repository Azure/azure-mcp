// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.FunctionApp.Models;
using AzureMcp.FunctionApp.Commands.FunctionApp;

namespace AzureMcp.FunctionApp.Commands;

[JsonSerializable(typeof(FunctionAppListCommand.FunctionAppListCommandResult))]
[JsonSerializable(typeof(FunctionAppModel))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class FunctionAppJsonContext : JsonSerializerContext;
