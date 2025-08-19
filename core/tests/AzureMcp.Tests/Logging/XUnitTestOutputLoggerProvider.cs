// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AzureMcp.Tests.Logging;

/// <summary>
/// AOT-safe ILoggerProvider that writes logs to xUnit's ITestOutputHelper.
/// </summary>
public sealed class XUnitTestOutputLoggerProvider : ILoggerProvider
{
    private readonly ITestOutputHelper _output;
    private readonly ConcurrentDictionary<string, XUnitTestOutputLogger> _loggers = new();

    public XUnitTestOutputLoggerProvider(ITestOutputHelper output)
    {
        _output = output;
        // Add a test message to verify the logger provider is being used
        _output.WriteLine("[TEST] XUnitTestOutputLoggerProvider created");
    }

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new XUnitTestOutputLogger(_output, name));

    public void Dispose() { }

    private sealed class XUnitTestOutputLogger(ITestOutputHelper output, string categoryName) : ILogger
    {
        private static readonly object _lock = new();

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel) || formatter is null) return;

            var message = formatter(state, exception);
            lock (_lock)
            {
                // Single line to keep test output tidy - with MCP prefix to identify MCP server logs
                output.WriteLine($"[MCP-LOG-{logLevel}] {categoryName}: {message}{(exception is null ? "" : $" | {exception.Message}")}");
            }
        }

        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();
            public void Dispose() { }
        }
    }
}
