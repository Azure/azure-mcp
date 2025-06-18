// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Authorization.Commands;

namespace AzureMcp.Commands.Authorization;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(RoleAssignmentListCommand.RoleAssignmentListCommandResult))]
internal partial class AuthorizationJsonContext : JsonSerializerContext;
