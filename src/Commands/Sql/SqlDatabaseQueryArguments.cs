namespace AzureMcp.Commands.Sql;

public sealed class SqlDatabaseQueryArguments(string? Subscription, string? ServerName, string? DatabaseName, string? Query)
{
    public string? Subscription { get; } = Subscription;
    public string? ServerName { get; } = ServerName;
    public string? DatabaseName { get; } = DatabaseName;
    public string? Query { get; } = Query;
}
