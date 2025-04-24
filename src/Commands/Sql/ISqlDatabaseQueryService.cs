using System.Threading.Tasks;

namespace AzureMcp.Commands.Sql;

public interface ISqlDatabaseQueryService
{
    Task<object> ExecuteQueryAsync(string subscription, string serverName, string databaseName, string query, IProgress<int>? progress = null);
}