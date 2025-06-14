// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Helpers;
using Xunit;

namespace AzureMcp.Tests.Helpers;

public class SqlStatementParserTests
{
    [Theory]
    [InlineData("CREATE INDEX IX_Users_Email ON [Users]", "Users")]
    [InlineData("CREATE NONCLUSTERED INDEX IX_Orders_Date ON [dbo].[Orders]", "Orders")]
    [InlineData("CREATE INDEX IX_Products_Name ON Products", "Products")]
    [InlineData("CREATE NONCLUSTERED INDEX IX_Test ON [schema].[table_name]", "table_name")]
    [InlineData("", "")]
    public void ExtractTableNameFromSql_ShouldReturnCorrectTableName(string sql, string expectedTableName)
    {
        // Act
        var result = SqlStatementParser.ExtractTableNameFromSql(sql);

        // Assert
        Assert.Equal(expectedTableName, result);
    }

    [Fact]
    public void ExtractTableNameFromSql_WithNullInput_ShouldReturnEmptyString()
    {
        // Act
        var result = SqlStatementParser.ExtractTableNameFromSql(null!);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("CREATE INDEX IX_Users_Email ON [Users]", "IX_Users_Email")]
    [InlineData("CREATE NONCLUSTERED INDEX IX_Orders_Date ON [Orders]", "IX_Orders_Date")]
    [InlineData("CREATE INDEX [IX_Products_Name] ON Products", "IX_Products_Name")]
    [InlineData("", "")]
    public void ExtractIndexNameFromCreateSql_ShouldReturnCorrectIndexName(string sql, string expectedIndexName)
    {
        // Act
        var result = SqlStatementParser.ExtractIndexNameFromCreateSql(sql);

        // Assert
        Assert.Equal(expectedIndexName, result);
    }

    [Fact]
    public void ExtractIndexNameFromCreateSql_WithNullInput_ShouldReturnEmptyString()
    {
        // Act
        var result = SqlStatementParser.ExtractIndexNameFromCreateSql(null!);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("Recommendation for table: Users", "Users")]
    [InlineData("Index recommendation ON [Orders]", "Orders")]
    [InlineData("Table: Products needs optimization", "Products")]
    [InlineData("", "")]
    public void ExtractTableNameFromDetails_ShouldReturnCorrectTableName(string details, string expectedTableName)
    {
        // Act
        var result = SqlStatementParser.ExtractTableNameFromDetails(details);

        // Assert
        Assert.Equal(expectedTableName, result);
    }

    [Fact]
    public void ExtractTableNameFromDetails_WithNullInput_ShouldReturnEmptyString()
    {
        // Act
        var result = SqlStatementParser.ExtractTableNameFromDetails(null!);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("IX_Users_Email", "Users", "DROP INDEX IF EXISTS [IX_Users_Email] ON [Users];")]
    [InlineData("IX_Orders_Date", "Orders", "DROP INDEX IF EXISTS [IX_Orders_Date] ON [Orders];")]
    [InlineData("", "Users", "")]
    [InlineData("IX_Test", "", "")]
    [InlineData("", "", "")]
    public void GenerateDropIndexSql_ShouldReturnCorrectDropStatement(string indexName, string tableName, string expectedSql)
    {
        // Act
        var result = SqlStatementParser.GenerateDropIndexSql(indexName, tableName);

        // Assert
        Assert.Equal(expectedSql, result);
    }

    [Fact]
    public void GenerateDropIndexSql_WithNullIndexName_ShouldReturnEmptyString()
    {
        // Act
        var result = SqlStatementParser.GenerateDropIndexSql(null!, "Users");

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GenerateDropIndexSql_WithNullTableName_ShouldReturnEmptyString()
    {
        // Act
        var result = SqlStatementParser.GenerateDropIndexSql("IX_Test", null!);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ExtractTableNameFromSql_ComplexScenario_ShouldReturnCorrectTableName()
    {
        // Arrange
        var sql = @"
            CREATE NONCLUSTERED INDEX [IX_Users_Email_LastLogin] 
            ON [dbo].[Users] ([Email] ASC, [LastLoginDate] DESC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF)";

        // Act
        var result = SqlStatementParser.ExtractTableNameFromSql(sql);

        // Assert
        Assert.Equal("Users", result);
    }

    [Fact]
    public void ExtractIndexNameFromCreateSql_ComplexScenario_ShouldReturnCorrectIndexName()
    {
        // Arrange
        var sql = @"
            CREATE NONCLUSTERED INDEX [IX_Users_Email_LastLogin] 
            ON [dbo].[Users] ([Email] ASC, [LastLoginDate] DESC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF)";

        // Act
        var result = SqlStatementParser.ExtractIndexNameFromCreateSql(sql);        // Assert
        Assert.Equal("IX_Users_Email_LastLogin", result);
    }
}
