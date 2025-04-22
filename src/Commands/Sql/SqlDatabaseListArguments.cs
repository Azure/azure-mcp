namespace AzureMcp.Commands.Sql;

public sealed class SqlDatabaseListArguments(string? Subscription, string? ServerName)
{
    public string? Subscription { get; } = Subscription;
    public string? ServerName { get; } = ServerName;
}
