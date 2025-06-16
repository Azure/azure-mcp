// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.RegularExpressions;

namespace AzureMcp.Helpers;

/// <summary>
/// Utility class for parsing SQL statements and extracting information from SQL advisor recommendations.
/// </summary>
public static class SqlStatementParser
{
    /// <summary>
    /// Extracts table name from advisor recommendation details text.
    /// </summary>
    /// <param name="details">The recommendation details text from Azure SQL advisor.</param>
    /// <returns>The extracted table name, or empty string if not found.</returns>
    public static string ExtractTableNameFromDetails(string details)
    {
        if (string.IsNullOrEmpty(details))
            return string.Empty;

        // Look for common patterns like "ON [tableName]" or "table: tableName"
        var tablePatterns = new[]
        {
            @"ON \[([^\]]+)\]",
            @"table:\s*([^\s,]+)",
            @"Table:\s*([^\s,]+)"
        };

        foreach (var pattern in tablePatterns)
        {
            var match = Regex.Match(details, pattern, RegexOptions.IgnoreCase);
            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// Extracts table name from CREATE INDEX SQL statements.
    /// </summary>
    /// <param name="sql">The SQL statement to parse.</param>
    /// <returns>The extracted table name, or empty string if not found.</returns>
    public static string ExtractTableNameFromSql(string sql)
    {
        if (string.IsNullOrEmpty(sql))
            return string.Empty;

        // Extract table name from CREATE INDEX statements
        // Pattern: CREATE [NONCLUSTERED] INDEX ... ON [schema].[table] or ON [table]
        var patterns = new[]
        {
            @"ON\s+\[?([^\]\s\[]+)\]?\.\[?([^\]\s\[]+)\]?",  // ON [schema].[table] or ON schema.table
            @"ON\s+\[?([^\]\s\[]+)\]?(?!\s*\.)",             // ON [table] (without schema)
        };

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(sql, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                // If we have schema.table format, return the table part (group 2)
                if (match.Groups.Count > 2 && !string.IsNullOrEmpty(match.Groups[2].Value))
                {
                    return match.Groups[2].Value;
                }
                // Otherwise return the first capture (table name)
                return match.Groups[1].Value;
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// Extracts index name from CREATE INDEX SQL statements.
    /// </summary>
    /// <param name="sql">The SQL statement to parse.</param>
    /// <returns>The extracted index name, or empty string if not found.</returns>
    public static string ExtractIndexNameFromCreateSql(string sql)
    {
        if (string.IsNullOrEmpty(sql))
            return string.Empty;

        // Extract index name from CREATE INDEX statements
        // Pattern: CREATE [NONCLUSTERED] INDEX [indexName] ON ...
        var pattern = @"CREATE\s+(?:NONCLUSTERED\s+)?INDEX\s+\[?([^\]\s\[]+)\]?\s+ON";
        var match = Regex.Match(sql, pattern, RegexOptions.IgnoreCase);

        if (match.Success && match.Groups.Count > 1)
        {
            return match.Groups[1].Value;
        }

        return string.Empty;
    }

    /// <summary>
    /// Generates a DROP INDEX statement for a given index and table.
    /// </summary>
    /// <param name="indexName">The name of the index to drop.</param>
    /// <param name="tableName">The name of the table containing the index.</param>
    /// <returns>A DROP INDEX SQL statement, or empty string if parameters are invalid.</returns>
    public static string GenerateDropIndexSql(string indexName, string tableName)
    {
        if (string.IsNullOrEmpty(indexName) || string.IsNullOrEmpty(tableName))
            return string.Empty;

        return $"DROP INDEX IF EXISTS [{indexName}] ON [{tableName}];";
    }
}
