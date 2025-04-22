using AzureMcp.Commands.Sql;
using AzureMcp.Models.Command;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AzureMcp.Tests.Commands.Sql;

public class ServerListCommandTests
{
    private class MockSqlServerLister : ISqlServerLister
    {
        public Task<IReadOnlyList<object>> ListSqlServersAsync() =>
            Task.FromResult<IReadOnlyList<object>>(new List<object>
            {
                new { Name = "sqlserver1", Location = "eastus", ResourceGroup = "rg1", FullyQualifiedDomainName = "sqlserver1.database.windows.net" },
                new { Name = "sqlserver2", Location = "westus", ResourceGroup = "rg2", FullyQualifiedDomainName = "sqlserver2.database.windows.net" }
            });
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsServerListJson()
    {
        var command = new SqlServerListCommand(new MockSqlServerLister());
        var context = new CommandContext(null!);
        var parser = new System.CommandLine.Parsing.Parser(new System.CommandLine.RootCommand());
        var parseResult = parser.Parse(new string[] { });
        var response = await command.ExecuteAsync(context, parseResult);

        Assert.Equal(0, response.Status);
        Assert.Contains("sqlserver1", response.Results?.ToString());
        Assert.Contains("sqlserver2", response.Results?.ToString());
        Assert.Equal("SQL Servers listed successfully.", response.Message);
    }
}