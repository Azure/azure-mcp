using AzureMcp.Commands.Sql;
using AzureMcp.Models.Command;
using System.CommandLine.Parsing;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class SqlDatabaseListCommandTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsDatabasesJson()
    {
        var command = new SqlDatabaseListCommand();
        var context = new CommandContext(null!);
        var parser = new System.CommandLine.Command("database list");
        var parseResult = parser.Parse("--subscription test-sub --server-name test-server");

        var result = await command.ExecuteAsync(context, parseResult);

        Assert.Equal(0, result.Status);
        Assert.Contains("db1", result.Results?.ToString());
        Assert.Contains("db2", result.Results?.ToString());
        Assert.Contains("test-sub", result.Results?.ToString());
        Assert.Contains("test-server", result.Results?.ToString());
    }
}
