// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Authorization.Commands;

namespace AzureMcp.Authorization.Commands;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(RoleAssignmentListCommand.RoleAssignmentListCommandResult))]
internal partial class AuthorizationJsonContext : JsonSerializerContext;
