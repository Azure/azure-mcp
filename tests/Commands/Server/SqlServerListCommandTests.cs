using AzureMcp.Commands.Server;
using AzureMcp.Models.Command;
using System.CommandLine.Parsing;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class SqlServerListCommandTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsServerListJson()
    {
        var command = new SqlServerListCommand();
        var context = new CommandContext(null!);
        var parser = new System.CommandLine.Parsing.Parser(new System.CommandLine.RootCommand());
        var parseResult = parser.Parse("");
        var response = await command.ExecuteAsync(context, parseResult);

        Assert.Equal(0, response.Status);
        Assert.Contains("sqlserver1", response.Results?.ToString());
        Assert.Contains("sqlserver2", response.Results?.ToString());
        Assert.Equal("SQL Servers listed successfully.", response.Message);
    }
}