using AzureMcp.Commands.Sql;
using AzureMcp.Models.Command;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class SqlDatabaseQueryCommandTests
{
    private class MockSqlDatabaseQueryService : ISqlDatabaseQueryService
    {
        public Task<object> ExecuteQueryAsync(string subscription, string serverName, string databaseName, string query, IProgress<int>? progress = null)
        {
            return Task.FromResult<object>(new[] { new { TestColumn = 1 } });
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsQueryResultsJson()
    {
        var command = new SqlDatabaseQueryCommand(new MockSqlDatabaseQueryService());
        var context = new CommandContext(null!);
        var parser = new Parser();
        var parseResult = parser.Parse("query --subscription test-sub --server-name test-server --database-name test-db --query 'SELECT 1 AS TestColumn'");

        var result = await command.ExecuteAsync(context, parseResult);

        Assert.Equal(0, result.Status);
        Assert.Contains("TestColumn", result.Results?.ToString());
    }
}
