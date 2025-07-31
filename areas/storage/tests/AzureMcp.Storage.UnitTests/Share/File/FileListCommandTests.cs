// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Storage.Commands.Share.File;
using AzureMcp.Storage.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Storage.UnitTests.Share.File;

public class FileListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStorageService _storageService;
    private readonly ILogger<FileListCommand> _logger;
    private readonly FileListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public FileListCommandTests()
    {
        _storageService = Substitute.For<IStorageService>();
        _logger = Substitute.For<ILogger<FileListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_storageService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Name_ReturnsExpectedValue()
    {
        // Act & Assert
        Assert.Equal("list", _command.Name);
    }

    [Fact]
    public void Description_ContainsRequiredParameters()
    {
        // Act
        var description = _command.Description;

        // Assert
        Assert.Contains("account-name", description);
        Assert.Contains("share-name", description);
        Assert.Contains("directory-path", description);
        Assert.Contains("prefix", description);
    }

    [Fact]
    public void Title_ReturnsExpectedValue()
    {
        // Act & Assert
        Assert.Equal("List Storage Share Files and Directories", _command.Title);
    }

    [Fact]
    public void Metadata_IsReadOnly()
    {
        // Act
        var metadata = _command.Metadata;

        // Assert
        Assert.False(metadata.Destructive);
        Assert.True(metadata.ReadOnly);
    }
}
