
using System.Text.Json.Serialization;
using AzureMcp.Commands.LoadTesting.LoadTest;

namespace AzureMcp.Commands.LoadTesting;

[JsonSerializable(typeof(LoadTestListCommand.LoadTestListCommandResult))]
internal sealed partial class LoadTestJsonContext : JsonSerializerContext
{
}
