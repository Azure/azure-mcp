// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Marketplace.Commands.Product;
using AzureMcp.Areas.Marketplace.Models;
using AzureMcp.Areas.Marketplace.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Marketplace.UnitTests.Product;

public class ProductGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMarketplaceService _marketplaceService;
    private readonly ILogger<ProductGetCommand> _logger;
    private readonly ProductGetCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public ProductGetCommandTests()
    {
        _marketplaceService = Substitute.For<IMarketplaceService>();
        _logger = Substitute.For<ILogger<ProductGetCommand>>();

        var collection = new ServiceCollection().AddSingleton(_marketplaceService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("get", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("marketplace product", command.Description.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_WithValidParameters_ReturnsSuccess()
    {
        // Arrange
        var subscriptionId = "test-sub";
        var productId = "test-product";
        var expectedProduct = new ProductDetails
        {
            Id = "test-product",
            DisplayName = "Test Product"
        };

        _marketplaceService.GetProduct(
            Arg.Is(productId),
            Arg.Is(subscriptionId),
            Arg.Any<bool?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<bool?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<bool?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedProduct);

        var args = _parser.Parse(["--subscription", subscriptionId, "--product-id", productId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_WithMissingSubscription_ReturnsValidationError()
    {
        // Arrange
        var args = _parser.Parse(["--product-id", "test-product"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceException()
    {
        // Arrange
        var expectedError = "Test error";
        var subscriptionId = "test-sub";
        var productId = "test-product";

        _marketplaceService.GetProduct(
            Arg.Is(productId),
            Arg.Is(subscriptionId),
            Arg.Any<bool?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<bool?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<bool?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse(["--subscription", subscriptionId, "--product-id", productId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }
}
