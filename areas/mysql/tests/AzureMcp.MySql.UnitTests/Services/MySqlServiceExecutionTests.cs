// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using System.Reflection;

namespace AzureMcp.MySql.UnitTests.Services;

public class MySqlServiceExecutionTests
{
    private readonly IResourceGroupService _resourceGroupService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<MySqlService> _logger;
    private readonly MySqlService _mysqlService;

    public MySqlServiceExecutionTests()
    {
        _resourceGroupService = Substitute.For<IResourceGroupService>();
        _tenantService = Substitute.For<ITenantService>();
        _logger = Substitute.For<ILogger<MySqlService>>();
        
        _mysqlService = new MySqlService(_resourceGroupService, _tenantService, _logger);
    }

    [Fact]
    public void ValidateQuerySafety_WithNullQuery_ShouldThrowArgumentException()
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { null! }));
        
        Assert.IsType<ArgumentException>(exception.InnerException);
        Assert.Contains("Query cannot be null or empty", exception.InnerException!.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n\r")]
    public void ValidateQuerySafety_WithEmptyOrWhitespaceQuery_ShouldThrowArgumentException(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { query }));
        
        Assert.IsType<ArgumentException>(exception.InnerException);
        Assert.True(
            exception.InnerException!.Message.Contains("Query cannot be null or empty") ||
            exception.InnerException.Message.Contains("empty after removing comments"),
            $"Expected null/empty or comments validation error, but got: {exception.InnerException.Message}");
    }

    [Fact]
    public void ValidateQuerySafety_WithQueryLengthExceeding10000Characters_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();
        var longQuery = "SELECT * FROM users WHERE " + new string('X', 10000);
        
        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { longQuery }));
        
        Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.Contains("Query length exceeds the maximum allowed limit of 10,000 characters", exception.InnerException!.Message);
        Assert.Contains("prevent potential DoS attacks", exception.InnerException.Message);
    }

    [Theory]
    [InlineData("-- Only comment")]
    [InlineData("/* Only block comment */")]
    [InlineData("   \n\t   ")]
    [InlineData("-- comment\n/* block */\n   ")]
    public void ValidateQuerySafety_WithOnlyCommentsOrWhitespace_ShouldThrowArgumentException(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { query }));
        
        Assert.IsType<ArgumentException>(exception.InnerException);
        Assert.True(
            exception.InnerException!.Message.Contains("empty after removing comments") ||
            exception.InnerException.Message.Contains("Query cannot be null or empty"),
            $"Expected comments or null/empty validation error, but got: {exception.InnerException.Message}");
    }

    [Theory]
    [InlineData("SELECT * FROM users -- This is a comment")]
    [InlineData("SELECT * FROM users # This is a hash comment")]
    [InlineData("SELECT * FROM users /* This is a block comment */")]
    [InlineData("   SELECT   *   FROM   users   ")]
    public void ValidateQuerySafety_WithCommentsAndWhitespace_ShouldCleanAndPass(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert - Should not throw any exception
        validateMethod.Invoke(null, new object[] { query });
    }

    [Theory]
    [InlineData("SELECT * FROM users; DROP TABLE users")]
    [InlineData("SELECT * FROM users;")]
    [InlineData("; SELECT * FROM users")]
    public void ValidateQuerySafety_WithMultipleStatements_ShouldThrowInvalidOperationException(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { query }));
        
        Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.Contains("Multiple SQL statements are not allowed", exception.InnerException!.Message);
    }

    [Theory]
    [InlineData("SELECT * FROM users UNION SELECT password FROM admin")]
    [InlineData("SELECT * FROM users WHERE id = 1 OR 1=1")]
    [InlineData("DROP TABLE users")]
    [InlineData("INSERT INTO users VALUES")]
    public void ValidateQuerySafety_WithDangerousPatterns_ShouldThrowInvalidOperationException(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { query }));
        
        Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.True(
            exception.InnerException!.Message.Contains("dangerous patterns") || 
            exception.InnerException.Message.Contains("dangerous keyword") ||
            exception.InnerException.Message.Contains("Multiple SQL statements") ||
            exception.InnerException.Message.Contains("Only SELECT statements"),
            $"Expected validation error, but got: {exception.InnerException.Message}");
    }

    [Theory]
    [InlineData("CHAR(")]
    [InlineData("ASCII(")]
    [InlineData("HEX(")]
    [InlineData("CONVERT(")]
    [InlineData("CAST(")]
    [InlineData("FROM_BASE64(")]
    [InlineData("AES_ENCRYPT(")]
    [InlineData("PASSWORD(")]
    public void ValidateQuerySafety_WithObfuscationFunctions_ShouldThrowInvalidOperationException(string function)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();
        var query = $"SELECT {function}'test') FROM users";

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { query }));
        
        Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.True(
            exception.InnerException!.Message.Contains("Character conversion and obfuscation functions") ||
            exception.InnerException.Message.Contains("dangerous keyword"),
            $"Expected obfuscation or keyword validation error, but got: {exception.InnerException.Message}");
    }

    [Theory]
    [InlineData("DROP")]
    [InlineData("DELETE")]
    [InlineData("TRUNCATE")]
    [InlineData("INSERT")]
    [InlineData("ALTER")]
    [InlineData("GRANT")]
    [InlineData("REVOKE")]
    [InlineData("KILL")]
    [InlineData("SHUTDOWN")]
    [InlineData("LOAD DATA")]
    [InlineData("OUTFILE")]
    public void ValidateQuerySafety_WithDangerousKeywords_ShouldThrowInvalidOperationException(string keyword)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();
        var query = $"{keyword} something";

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { query }));
        
        Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.True(
            exception.InnerException!.Message.Contains("dangerous keyword") ||
            exception.InnerException.Message.Contains("Only SELECT statements") ||
            exception.InnerException.Message.Contains("dangerous patterns"),
            $"Expected keyword, statement, or pattern validation error, but got: {exception.InnerException.Message}");
    }

    [Theory]
    [InlineData("SHOW DATABASES")]
    [InlineData("DESCRIBE users")]
    [InlineData("EXPLAIN SELECT")]
    [InlineData("CALL procedure")]
    [InlineData("WITH cte AS")]
    public void ValidateQuerySafety_WithNonSelectStatements_ShouldThrowInvalidOperationException(string statement)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { statement }));
        
        Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.Contains("Only SELECT statements are allowed", exception.InnerException!.Message);
    }

    [Theory]
    [InlineData("SELECT * FROM users")]
    [InlineData("select count(*) from products")]
    [InlineData("SELECT u.id, u.name FROM users u WHERE u.active = 1")]
    public void ValidateQuerySafety_WithValidSelectQueries_ShouldNotThrow(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert - Should not throw any exception
        validateMethod.Invoke(null, new object[] { query });
    }

    [Theory]
    [InlineData("SELECT * FROM users -- comment with ; semicolon")]
    [InlineData("SELECT * FROM users /* comment with ; semicolon */")]
    [InlineData("SELECT * FROM users # comment with ; semicolon")]
    public void ValidateQuerySafety_WithSemicolonInComments_ShouldPassAfterCleaning(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert - Should not throw any exception because semicolons in comments are removed
        validateMethod.Invoke(null, new object[] { query });
    }

    [Fact]
    public void ValidateQuerySafety_WithQueryLengthExactly10000Characters_ShouldNotThrowLengthException()
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();
        var baseQuery = "SELECT * FROM users WHERE id = '";
        var padding = new string('X', 10000 - baseQuery.Length - 1); // -1 for closing quote
        var exactLengthQuery = baseQuery + padding + "'";
        
        Assert.Equal(10000, exactLengthQuery.Length);

        // Act & Assert - Should not throw length exception
        validateMethod.Invoke(null, new object[] { exactLengthQuery });
    }

    [Theory]
    [InlineData("SELECT * FROM users\n-- comment\nWHERE id > 0")]
    [InlineData("SELECT * FROM users /* inline comment */ WHERE active = 1")]
    [InlineData("SELECT *\n/* multi-line\nblock comment */\nFROM users")]
    [InlineData("SELECT * FROM users # hash comment\nWHERE status = 'active'")]
    public void ValidateQuerySafety_WithComplexCommentsAndQueries_ShouldCleanAndValidate(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();

        // Act & Assert - Should not throw any exception after cleaning
        validateMethod.Invoke(null, new object[] { query });
    }

    [Theory]
    [InlineData("CHR(65) FROM users")]
    [InlineData("ORD('A') FROM users")]
    [InlineData("BINARY('test') FROM users")]
    [InlineData("COMPRESS('data') FROM users")]
    [InlineData("DES_ENCRYPT('data', 'key') FROM users")]
    [InlineData("OLD_PASSWORD('secret') FROM users")]
    public void ValidateQuerySafety_WithAdditionalObfuscationFunctions_ShouldThrowInvalidOperationException(string query)
    {
        // Arrange
        var validateMethod = GetValidateQuerySafetyMethod();
        var fullQuery = $"SELECT {query}";

        // Act & Assert
        var exception = Assert.Throws<TargetInvocationException>(() => 
            validateMethod.Invoke(null, new object[] { fullQuery }));
        
        Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.True(
            exception.InnerException!.Message.Contains("Character conversion and obfuscation functions") ||
            exception.InnerException.Message.Contains("dangerous keyword"),
            $"Expected obfuscation or keyword validation error, but got: {exception.InnerException.Message}");
    }

    private static MethodInfo GetValidateQuerySafetyMethod()
    {
        var method = typeof(MySqlService).GetMethod("ValidateQuerySafety", 
            BindingFlags.NonPublic | BindingFlags.Static);
        
        Assert.NotNull(method);
        return method;
    }
}
