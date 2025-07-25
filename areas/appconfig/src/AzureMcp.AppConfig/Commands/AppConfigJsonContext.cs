// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.AppConfig.Commands.Account;
using AzureMcp.AppConfig.Commands.KeyValue;

namespace AzureMcp.AppConfig.Commands;

[JsonSerializable(typeof(KeyValueUnlockCommand.KeyValueUnlockResult))]
[JsonSerializable(typeof(KeyValueShowCommand.KeyValueShowResult))]
[JsonSerializable(typeof(KeyValueSetCommand.KeyValueSetCommandResult))]
[JsonSerializable(typeof(KeyValueLockCommand.KeyValueLockCommandResult))]
[JsonSerializable(typeof(KeyValueListCommand.KeyValueListCommandResult))]
[JsonSerializable(typeof(KeyValueDeleteCommand.KeyValueDeleteCommandResult))]
[JsonSerializable(typeof(AccountListCommand.AccountListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class AppConfigJsonContext : JsonSerializerContext
{
}
