// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.FunctionApp.Commands.FunctionApp;
using AzureMcp.FunctionApp.Models;

namespace AzureMcp.FunctionApp.Commands;

[JsonSerializable(typeof(FunctionAppListCommand.FunctionAppListCommandResult))]
[JsonSerializable(typeof(FunctionAppInfo))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class FunctionAppJsonContext : JsonSerializerContext;
