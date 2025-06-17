namespace AzureMcp.Commands.Server;

public class ToolFailedException : Exception
{
    public ToolFailedException(string toolName, string message) : base(message)
    {
        ToolName = toolName;
    }

    public string ToolName { get; }
}
