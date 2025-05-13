using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Monitor
{
    public abstract class WorkspaceArguments : BaseMonitorArguments, IWorkspaceArguments
    {
        [JsonPropertyName(ArgumentDefinitions.Monitor.WorkspaceIdOrName)]
        public string? Workspace { get; set; }
    }
}
