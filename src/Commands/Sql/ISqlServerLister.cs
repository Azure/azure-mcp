namespace AzureMcp.Commands.Sql;

public interface ISqlServerLister
{
    Task<IReadOnlyList<object>> ListSqlServersAsync();
}