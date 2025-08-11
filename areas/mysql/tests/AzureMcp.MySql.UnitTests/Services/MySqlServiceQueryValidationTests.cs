// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.MySql.UnitTests.Services;

public class MySqlServiceQueryValidationTests
{
    private readonly IResourceGroupService _resourceGroupService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<MySqlService> _logger;
    private readonly MySqlService _mysqlService;

    public MySqlServiceQueryValidationTests()
    {
        _resourceGroupService = Substitute.For<IResourceGroupService>();
        _tenantService = Substitute.For<ITenantService>();
        _logger = Substitute.For<ILogger<MySqlService>>();
        
        _mysqlService = new MySqlService(_resourceGroupService, _tenantService, _logger);
    }

    [Theory]
    [InlineData("SELECT * FROM users LIMIT 100")]
    [InlineData("select * from users limit 50")]
    [InlineData("SELECT COUNT(*) FROM products LIMIT 1")]
    public async Task ExecuteQueryAsync_WithSafeQueries_ShouldNotThrow(string query)
    {
        // This test will fail during connection since we're not mocking the connection,
        // but it should pass the validation step
        var exception = await Record.ExceptionAsync(async () =>
            await _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Should not throw InvalidOperationException for dangerous keywords
        Assert.IsNotType<InvalidOperationException>(exception);
        
        // If it throws, it should be due to connection issues, not validation
        if (exception != null)
        {
            Assert.DoesNotContain("dangerous keyword", exception.Message);
            Assert.DoesNotContain("not allowed for security reasons", exception.Message);
        }
    }

    [Theory]
    [InlineData("DROP TABLE users")]
    [InlineData("DELETE FROM users")]
    [InlineData("TRUNCATE TABLE users")]
    [InlineData("ALTER TABLE users")]
    [InlineData("CREATE TABLE test")]
    [InlineData("INSERT INTO users")]
    [InlineData("UPDATE users SET")]
    [InlineData("GRANT ALL PRIVILEGES")]
    [InlineData("REVOKE SELECT")]
    [InlineData("SET GLOBAL")]
    [InlineData("KILL 123")]
    [InlineData("SHUTDOWN")]
    [InlineData("LOAD DATA INFILE")]
    [InlineData("SELECT * INTO OUTFILE")]
    [InlineData("CREATE USER")]
    [InlineData("DROP USER")]
    [InlineData("CREATE DATABASE")]
    [InlineData("DROP DATABASE")]
    [InlineData("CREATE PROCEDURE")]
    [InlineData("DROP PROCEDURE")]
    [InlineData("CREATE FUNCTION")]
    [InlineData("DROP FUNCTION")]
    [InlineData("CREATE TRIGGER")]
    [InlineData("DROP TRIGGER")]
    [InlineData("LOCK TABLES")]
    [InlineData("START TRANSACTION")]
    [InlineData("BEGIN")]
    public async Task ExecuteQueryAsync_WithDangerousQueries_ShouldThrowInvalidOperationException(string query)
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Check for either dangerous keyword message or dangerous pattern message
        Assert.True(
            exception.Message.Contains("dangerous keyword") || 
            exception.Message.Contains("dangerous patterns"),
            $"Expected error message to contain either 'dangerous keyword' or 'dangerous patterns', but got: {exception.Message}");
    }

    [Theory]
    [InlineData("MERGE INTO users")]
    [InlineData("CALL some_procedure()")]
    [InlineData("EXECUTE immediate")]
    [InlineData("LOAD XML")]
    [InlineData("WITH cte AS (SELECT * FROM users) SELECT * FROM cte")]
    [InlineData("SHOW DATABASES")]
    [InlineData("SHOW TABLES")]
    [InlineData("SHOW COLUMNS FROM users")]
    [InlineData("DESCRIBE users")]
    [InlineData("DESC users")]
    [InlineData("EXPLAIN SELECT * FROM users")]
    [InlineData("SHOW STATUS")]
    [InlineData("SHOW VARIABLES")]
    public async Task ExecuteQueryAsync_WithDisallowedStatements_ShouldThrowInvalidOperationException(string query)
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        Assert.Contains("Only SELECT statements are allowed", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ExecuteQueryAsync_WithEmptyQuery_ShouldThrowArgumentException(string query)
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        Assert.Contains("Query cannot be null or empty", exception.Message);
    }

    [Fact]
    public async Task ExecuteQueryAsync_WithNullQuery_ShouldThrowArgumentException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", null!));
        
        Assert.Contains("Query cannot be null or empty", exception.Message);
    }

    [Fact]
    public async Task ExecuteQueryAsync_WithParameterizedQuery_ShouldPassValidation()
    {
        var query = "SELECT * FROM users WHERE name = @name AND age > @age";

        // This test will fail during connection since we're not mocking the connection,
        // but it should pass the validation step
        var exception = await Record.ExceptionAsync(async () =>
            await _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Should not throw InvalidOperationException for dangerous keywords
        Assert.IsNotType<InvalidOperationException>(exception);
        
        // If it throws, it should be due to connection issues, not validation
        if (exception != null)
        {
            Assert.DoesNotContain("dangerous keyword", exception.Message);
            Assert.DoesNotContain("not allowed for security reasons", exception.Message);
        }
    }

    [Fact]
    public async Task ExecuteQueryAsync_WithParameterPlaceholders_ShouldPassValidation()
    {
        var query = "SELECT * FROM users WHERE class = @className";

        // This test will fail during connection since we're not mocking the connection,
        // but it should pass the validation step
        var exception = await Record.ExceptionAsync(async () =>
            await _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Should not throw InvalidOperationException for dangerous keywords
        Assert.IsNotType<InvalidOperationException>(exception);
        
        // If it throws, it should be due to connection issues, not validation
        if (exception != null)
        {
            Assert.DoesNotContain("dangerous keyword", exception.Message);
            Assert.DoesNotContain("not allowed for security reasons", exception.Message);
            Assert.DoesNotContain("JsonElement", exception.Message);
        }
    }

    [Theory]
    [InlineData("SELECT * FROM users; DROP TABLE users")]  // SQL injection with semicolon
    [InlineData("SELECT * FROM users UNION SELECT password FROM admin")]  // UNION SELECT injection
    [InlineData("SELECT * FROM users WHERE id = 1 OR 1=1")]  // Boolean-based injection
    public async Task ExecuteQueryAsync_WithSqlInjectionPatterns_ShouldThrowInvalidOperationException(string query)
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Check for multiple possible error messages based on validation order
        Assert.True(
            exception.Message.Contains("dangerous patterns") || 
            exception.Message.Contains("Multiple SQL statements") ||
            exception.Message.Contains("dangerous keyword"),
            $"Expected error message to contain validation error, but got: {exception.Message}");
    }

    [Theory]
    [InlineData("SELECT CHAR(65,66,67) FROM users")]  // CHAR function for obfuscation
    [InlineData("SELECT ASCII('A') FROM users")]  // ASCII function
    [InlineData("SELECT HEX('abc') FROM users")]  // HEX function
    [InlineData("SELECT UNHEX('616263') FROM users")]  // UNHEX function
    [InlineData("SELECT CONV('a',16,2) FROM users")]  // CONV function
    [InlineData("SELECT CONVERT('test' USING utf8) FROM users")]  // CONVERT function
    [InlineData("SELECT CAST('123' AS UNSIGNED) FROM users")]  // CAST function
    [InlineData("SELECT FROM_BASE64('dGVzdA==') FROM users")]  // FROM_BASE64 function
    [InlineData("SELECT TO_BASE64('test') FROM users")]  // TO_BASE64 function
    [InlineData("SELECT PASSWORD('secret') FROM users")]  // PASSWORD function
    [InlineData("SELECT AES_ENCRYPT('data', 'key') FROM users")]  // AES_ENCRYPT function
    public async Task ExecuteQueryAsync_WithObfuscationFunctions_ShouldThrowInvalidOperationException(string query)
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Check for either obfuscation function error or dangerous keyword error (validation order may vary)
        Assert.True(
            exception.Message.Contains("Character conversion and obfuscation functions") || 
            exception.Message.Contains("dangerous keyword"),
            $"Expected error message to contain obfuscation or keyword validation error, but got: {exception.Message}");
    }

    [Theory]
    [InlineData("SELECT * FROM users -- This is a comment")]
    [InlineData("SELECT * FROM users # This is a hash comment")]
    [InlineData("SELECT * FROM users /* This is a block comment */")]
    [InlineData("SELECT * FROM users /* multi-line\nblock comment */")]
    [InlineData("   SELECT   *   FROM   users   ")]  // Extra whitespace
    [InlineData("SELECT * FROM users\n\t-- comment\n  LIMIT 10")]  // Mixed whitespace and comments
    [InlineData("SELECT * FROM users /* comment */ WHERE id > 0")]  // Block comment in middle
    public async Task ExecuteQueryAsync_WithCommentsAndWhitespace_ShouldCleanAndValidate(string query)
    {
        // This test will fail during connection since we're not mocking the connection,
        // but it should pass the validation step after cleaning
        var exception = await Record.ExceptionAsync(async () =>
            await _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Should not throw InvalidOperationException for dangerous keywords after cleaning
        Assert.IsNotType<InvalidOperationException>(exception);
        
        // If it throws, it should be due to connection issues, not validation
        if (exception != null)
        {
            Assert.DoesNotContain("dangerous keyword", exception.Message);
            Assert.DoesNotContain("not allowed for security reasons", exception.Message);
            Assert.DoesNotContain("dangerous patterns", exception.Message);
        }
    }

    [Theory]
    [InlineData("-- Only comment")]
    [InlineData("/* Only block comment */")]
    [InlineData("   \n\t   ")]  // Only whitespace
    [InlineData("-- comment\n/* block */\n   ")]  // Comments and whitespace only
    public async Task ExecuteQueryAsync_WithOnlyCommentsOrWhitespace_ShouldThrowArgumentException(string query)
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        Assert.True(
            exception.Message.Contains("empty after removing comments") || 
            exception.Message.Contains("Query cannot be null or empty"),
            $"Expected error message to contain emptiness validation, but got: {exception.Message}");
    }

    [Theory]
    [InlineData("SELECT * FROM users -- comment with ; semicolon")]
    [InlineData("SELECT * FROM users /* comment with ; semicolon */")]
    [InlineData("SELECT * FROM users # comment with ; semicolon")]
    public async Task ExecuteQueryAsync_WithSemicolonInComments_ShouldPassValidation(string query)
    {
        // This test will fail during connection since we're not mocking the connection,
        // but it should pass the validation step after cleaning (semicolons in comments should be removed)
        var exception = await Record.ExceptionAsync(async () =>
            await _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Should not throw InvalidOperationException for multiple statements after cleaning comments
        Assert.IsNotType<InvalidOperationException>(exception);
        
        // If it throws, it should be due to connection issues, not validation
        if (exception != null)
        {
            Assert.DoesNotContain("Multiple SQL statements", exception.Message);
            Assert.DoesNotContain("dangerous patterns", exception.Message);
        }
    }

    [Fact]
    public async Task ExecuteQueryAsync_WithQueryLengthExceeding10000Characters_ShouldThrowInvalidOperationException()
    {
        // Create a query that exceeds 10,000 characters
        var baseQuery = "SELECT * FROM users WHERE ";
        var longCondition = string.Join(" AND ", Enumerable.Range(1, 400).Select(i => $"field{i} = 'value{i}'"));
        var longQuery = baseQuery + longCondition; // This should exceed 10,000 characters
        
        // Ensure the query is actually longer than 10,000 characters
        Assert.True(longQuery.Length > 10000, $"Test query length {longQuery.Length} should exceed 10,000 characters");

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", longQuery));
        
        Assert.Contains("Query length exceeds the maximum allowed limit of 10,000 characters", exception.Message);
        Assert.Contains("prevent potential DoS attacks", exception.Message);
    }

    [Fact]
    public async Task ExecuteQueryAsync_WithQueryLengthExactly10000Characters_ShouldNotThrowLengthException()
    {
        // Create a query that is exactly 10,000 characters
        var baseQuery = "SELECT * FROM users WHERE id = '";
        var closingPart = "'";
        var padding = new string('X', 10000 - baseQuery.Length - closingPart.Length);
        var exactLengthQuery = baseQuery + padding + closingPart;
        
        // Ensure the query is exactly 10,000 characters
        Assert.Equal(10000, exactLengthQuery.Length);

        // This test will fail during connection since we're not mocking the connection,
        // but it should pass the length validation step
        var exception = await Record.ExceptionAsync(async () =>
            await _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", exactLengthQuery));
        
        // Should not throw InvalidOperationException for query length
        if (exception is InvalidOperationException invalidOpEx)
        {
            Assert.DoesNotContain("Query length exceeds the maximum allowed limit", invalidOpEx.Message);
            Assert.DoesNotContain("prevent potential DoS attacks", invalidOpEx.Message);
        }
    }

    [Theory]
    [InlineData("SELECT CHR(65) FROM users")]  // CHR function variant
    [InlineData("SELECT ORD('A') FROM users")]  // ORD function
    [InlineData("SELECT BINARY('test') FROM users")]  // BINARY function
    [InlineData("SELECT CONCAT_WS(',', 'a', 'b') FROM users")]  // CONCAT_WS function
    [InlineData("SELECT ELT(1, 'a', 'b') FROM users")]  // ELT function
    [InlineData("SELECT FIELD('b', 'a', 'b') FROM users")]  // FIELD function
    [InlineData("SELECT COMPRESS('data') FROM users")]  // COMPRESS function
    [InlineData("SELECT UNCOMPRESS(data) FROM users")]  // UNCOMPRESS function
    [InlineData("SELECT DES_ENCRYPT('data', 'key') FROM users")]  // DES_ENCRYPT function
    [InlineData("SELECT DES_DECRYPT(data, 'key') FROM users")]  // DES_DECRYPT function
    [InlineData("SELECT AES_DECRYPT(data, 'key') FROM users")]  // AES_DECRYPT function
    [InlineData("SELECT ENCODE('data', 'key') FROM users")]  // ENCODE function
    [InlineData("SELECT DECODE(data, 'key') FROM users")]  // DECODE function
    public async Task ExecuteQueryAsync_WithAdditionalObfuscationFunctions_ShouldThrowInvalidOperationException(string query)
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Check for either obfuscation function error or dangerous keyword error (validation order may vary)
        Assert.True(
            exception.Message.Contains("Character conversion and obfuscation functions") || 
            exception.Message.Contains("dangerous keyword"),
            $"Expected error message to contain obfuscation or keyword validation error, but got: {exception.Message}");
    }

    [Theory]
    [InlineData("SHOW MASTER STATUS")]
    [InlineData("SHOW SLAVE STATUS")]
    [InlineData("SHOW BINARY LOGS")]
    [InlineData("SHOW BINLOG EVENTS")]
    [InlineData("RESET MASTER")]
    [InlineData("RESET SLAVE")]
    [InlineData("KILL CONNECTION 123")]
    [InlineData("KILL QUERY 123")]
    [InlineData("RESTART")]
    [InlineData("LOAD DATA LOCAL INFILE")]
    [InlineData("SELECT * INTO DUMPFILE")]
    [InlineData("ALTER USER 'user'@'host'")]
    [InlineData("RENAME USER 'old'@'host' TO 'new'@'host'")]
    [InlineData("CREATE SCHEMA test")]
    [InlineData("DROP SCHEMA test")]
    [InlineData("CREATE EVENT test_event")]
    [InlineData("DROP EVENT test_event")]
    [InlineData("CREATE VIEW test_view AS SELECT")]
    [InlineData("DROP VIEW test_view")]
    [InlineData("CREATE INDEX idx ON table")]
    [InlineData("DROP INDEX idx")]
    [InlineData("RENAME TABLE old TO new")]
    [InlineData("UNLOCK TABLES")]
    [InlineData("COMMIT")]
    [InlineData("ROLLBACK")]
    [InlineData("SET SESSION sql_mode")]
    [InlineData("SET SQL_MODE = 'STRICT'")]
    public async Task ExecuteQueryAsync_WithAdditionalDangerousKeywords_ShouldThrowInvalidOperationException(string query)
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        Assert.True(
            exception.Message.Contains("dangerous keyword") || 
            exception.Message.Contains("Only SELECT statements are allowed"),
            $"Expected error message to contain keyword validation error, but got: {exception.Message}");
    }

    [Theory]
    [InlineData("SELECT * FROM users; ")]  // Semicolon at end
    [InlineData("SELECT * FROM users ;")]  // Semicolon with space
    [InlineData("; SELECT * FROM users")]  // Semicolon at beginning
    [InlineData("SELECT * FROM users\n;\n")]  // Semicolon with newlines
    public async Task ExecuteQueryAsync_WithSemicolonVariations_ShouldThrowInvalidOperationException(string query)
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        Assert.True(
            exception.Message.Contains("Multiple SQL statements") || 
            exception.Message.Contains("dangerous patterns"),
            $"Expected error message to contain semicolon validation error, but got: {exception.Message}");
    }

    [Theory]
    [InlineData("SELECT * FROM users WHERE name LIKE '%test%'")]
    [InlineData("SELECT COUNT(*) AS total FROM products")]
    [InlineData("SELECT u.id, u.name FROM users u")]
    [InlineData("SELECT DISTINCT category FROM products")]
    [InlineData("SELECT * FROM users WHERE age BETWEEN 18 AND 65")]
    [InlineData("SELECT * FROM users WHERE status IN ('active', 'pending')")]
    [InlineData("SELECT u.*, p.title FROM users u LEFT JOIN posts p ON u.id = p.user_id")]
    [InlineData("SELECT AVG(price) FROM products GROUP BY category")]
    public async Task ExecuteQueryAsync_WithComplexButSafeQueries_ShouldPassValidation(string query)
    {
        // This test will fail during connection since we're not mocking the connection,
        // but it should pass the validation step
        var exception = await Record.ExceptionAsync(async () =>
            await _mysqlService.ExecuteQueryAsync("sub", "rg", "user", "server", "db", query));
        
        // Should not throw InvalidOperationException for dangerous keywords or patterns
        Assert.IsNotType<InvalidOperationException>(exception);
        
        // If it throws, it should be due to connection issues, not validation
        if (exception != null)
        {
            Assert.DoesNotContain("dangerous keyword", exception.Message);
            Assert.DoesNotContain("not allowed for security reasons", exception.Message);
            Assert.DoesNotContain("dangerous patterns", exception.Message);
            Assert.DoesNotContain("Multiple SQL statements", exception.Message);
        }
    }
}
