namespace AzureMcp.Arguments.Monitor.TableType;

public class TableTypeListArguments : BaseMonitorArguments, IWorkspaceArguments
{
    public string? Workspace { get; set; }
}
