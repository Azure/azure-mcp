using AzureMcp.Commands.Sql;
using AzureMcp.Models.Command;
using Xunit;

namespace AzureMcp.Tests.Commands.Sql;

public class ServerListCommandTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsServerListJson()
    {
        var command = new ServerListCommand();
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