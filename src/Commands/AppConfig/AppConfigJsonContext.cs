
using System.Text.Json.Serialization;
using AzureMcp.Commands.AppConfig.KeyValue;

namespace AzureMcp.Commands.AppConfig;

[JsonSerializable(typeof(KeyValueUnlockCommand.KeyValueUnlockResult))]
[JsonSerializable(typeof(KeyValueShowCommand.KeyValueShowResult))]
[JsonSerializable(typeof(KeyValueSetCommand.KeyValueSetCommandResult))]
[JsonSerializable(typeof(KeyValueLockCommand.KeyValueLockCommandResult))]
[JsonSerializable(typeof(KeyValueListCommand.KeyValueListCommandResult))]
[JsonSerializable(typeof(KeyValueDeleteCommand.KeyValueDeleteCommandResult))]
[JsonSerializable(typeof(Account.AccountListCommand.AccountListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class AppConfigJsonContext : JsonSerializerContext
{
}
