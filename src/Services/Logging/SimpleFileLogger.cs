// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace AzureMcp.Services.Logging;

/// <summary>
/// Simple file logger that writes logs to a file without external dependencies
/// </summary>
public class SimpleFileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;
    private readonly LogLevel _minimumLevel;
    private readonly ConcurrentDictionary<string, SimpleFileLogger> _loggers = new();
    private readonly object _lock = new();

    public SimpleFileLoggerProvider(string filePath, LogLevel minimumLevel = LogLevel.Information)
    {
        _filePath = filePath;
        _minimumLevel = minimumLevel;
        
        // Ensure directory exists
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new SimpleFileLogger(name, _filePath, _minimumLevel, _lock));
    }

    public void Dispose()
    {
        _loggers.Clear();
    }
}

public class SimpleFileLogger : ILogger
{
    private readonly string _categoryName;
    private readonly string _filePath;
    private readonly LogLevel _minimumLevel;
    private readonly object _lock;

    public SimpleFileLogger(string categoryName, string filePath, LogLevel minimumLevel, object lockObject)
    {
        _categoryName = categoryName;
        _filePath = filePath;
        _minimumLevel = minimumLevel;
        _lock = lockObject;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minimumLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} [{logLevel:G}] [{_categoryName}] {message}";
        
        if (exception != null)
        {
            logEntry += Environment.NewLine + exception.ToString();
        }

        logEntry += Environment.NewLine;

        lock (_lock)
        {
            try
            {
                // Ensure directory exists
                var directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                File.AppendAllText(_filePath, logEntry);
            }
            catch
            {
                // Silently ignore file writing errors to prevent logging from breaking the application
            }
        }
    }
}

/// <summary>
/// Extension methods for adding simple file logging
/// </summary>
public static class SimpleFileLoggerExtensions
{
    public static ILoggingBuilder AddSimpleFile(this ILoggingBuilder builder, string filePath, LogLevel minimumLevel = LogLevel.Information)
    {
        builder.Services.AddSingleton<ILoggerProvider>(sp => new SimpleFileLoggerProvider(filePath, minimumLevel));
        return builder;
    }
}
