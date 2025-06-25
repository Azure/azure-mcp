using System.Text.Json.Serialization;
using AzureMcp.Areas.AiFoundry.Models;

namespace AzureMcp.Areas.AiFoundry.Commands;

[JsonSerializable(typeof(ProjectListCommand.ProjectListResult))]
[JsonSerializable(typeof(ProjectDescribeCommand.ProjectDescribeResult))]
[JsonSerializable(typeof(ProjectInfo))]
[JsonSerializable(typeof(ModelListCommand.ModelListResult))]
[JsonSerializable(typeof(ModelInfo))]
[JsonSerializable(typeof(DeploymentListCommand.DeploymentListResult))]
[JsonSerializable(typeof(DeploymentInfo))]
[JsonSerializable(typeof(ConnectionListCommand.ConnectionListResult))]
[JsonSerializable(typeof(ConnectionInfo))]
[JsonSerializable(typeof(AgentListCommand.AgentListResult))]
[JsonSerializable(typeof(AgentInfo))]
[JsonSerializable(typeof(DatasetListCommand.DatasetListResult))]
[JsonSerializable(typeof(DatasetInfo))]
[JsonSerializable(typeof(VectorStoreListCommand.VectorStoreListResult))]
[JsonSerializable(typeof(VectorStoreInfo))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class AiFoundryJsonContext : JsonSerializerContext
{
} 