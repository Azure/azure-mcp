using System.Text.Json.Serialization;
using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Options;
using AzureMcp.Options.Arc;

namespace AzureMcp.Commands.Arc;

[JsonSerializable(typeof(ArcConnectOptions))]
[JsonSerializable(typeof(CommandResponse))]
[JsonSerializable(typeof(DeploymentResult))]
public sealed partial class ArcJsonContext : JsonSerializerContext
{
}
