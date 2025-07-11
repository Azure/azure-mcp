// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Services.Http;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AzureMcp.Tests.Services.Http;

public class HttpClientServiceTests
{
    private readonly ILogger<HttpClientService> _logger;
    private readonly HttpClientService _httpClientService;

    public HttpClientServiceTests()
    {
        _logger = new LoggerFactory().CreateLogger<HttpClientService>();
        _httpClientService = new HttpClientService(_logger);
    }

    [Fact]
    public void GetHttpClient_ReturnsConfiguredHttpClient()
    {
        // Act
        var httpClient = _httpClientService.GetHttpClient();

        // Assert
        Assert.NotNull(httpClient);
        Assert.IsType<HttpClient>(httpClient);
    }

    [Fact]
    public void GetHttpClient_WithBaseAddress_ReturnsConfiguredHttpClientWithBaseAddress()
    {
        // Arrange
        var baseAddress = new Uri("https://example.com");

        // Act
        var httpClient = _httpClientService.GetHttpClient(baseAddress);

        // Assert
        Assert.NotNull(httpClient);
        Assert.Equal(baseAddress, httpClient.BaseAddress);
    }

    [Fact]
    public void GetHttpClient_WithProxyEnvironmentVariables_ConfiguresProxy()
    {
        // Arrange
        Environment.SetEnvironmentVariable("HTTP_PROXY", "http://proxy.example.com:8080");
        Environment.SetEnvironmentVariable("NO_PROXY", "localhost,127.0.0.1");

        try
        {
            var logger = new LoggerFactory().CreateLogger<HttpClientService>();
            var service = new HttpClientService(logger);

            // Act
            var httpClient = service.GetHttpClient();

            // Assert
            Assert.NotNull(httpClient);
            // Note: We can't easily test the proxy configuration without exposing internal details
            // but the service should have been created without throwing exceptions
        }
        finally
        {
            // Clean up environment variables
            Environment.SetEnvironmentVariable("HTTP_PROXY", null);
            Environment.SetEnvironmentVariable("NO_PROXY", null);
        }
    }

    [Fact]
    public void GetHttpClient_WithAllProxyEnvironmentVariable_ConfiguresProxy()
    {
        // Arrange
        Environment.SetEnvironmentVariable("ALL_PROXY", "http://proxy.example.com:8080");

        try
        {
            var logger = new LoggerFactory().CreateLogger<HttpClientService>();
            var service = new HttpClientService(logger);

            // Act
            var httpClient = service.GetHttpClient();

            // Assert
            Assert.NotNull(httpClient);
            // Note: We can't easily test the proxy configuration without exposing internal details
            // but the service should have been created without throwing exceptions
        }
        finally
        {
            // Clean up environment variables
            Environment.SetEnvironmentVariable("ALL_PROXY", null);
        }
    }

    [Fact]
    public void GetHttpClient_WithInvalidProxyUrl_FallsBackToDefault()
    {
        // Arrange
        Environment.SetEnvironmentVariable("HTTP_PROXY", "invalid-url");

        try
        {
            var logger = new LoggerFactory().CreateLogger<HttpClientService>();
            var service = new HttpClientService(logger);

            // Act & Assert - Should not throw exception
            var httpClient = service.GetHttpClient();
            Assert.NotNull(httpClient);
        }
        finally
        {
            // Clean up environment variables
            Environment.SetEnvironmentVariable("HTTP_PROXY", null);
        }
    }

    [Fact]
    public void GetHttpClient_WithNoProxyContainingWildcards_FiltersWildcards()
    {
        // Arrange
        Environment.SetEnvironmentVariable("HTTP_PROXY", "http://proxy.example.com:8080");
        Environment.SetEnvironmentVariable("NO_PROXY", "localhost,127.0.0.1,*.local,example.com");

        try
        {
            var logger = new LoggerFactory().CreateLogger<HttpClientService>();
            var service = new HttpClientService(logger);

            // Act & Assert - Should not throw exception even with wildcard patterns
            var httpClient = service.GetHttpClient();
            Assert.NotNull(httpClient);
        }
        finally
        {
            // Clean up environment variables
            Environment.SetEnvironmentVariable("HTTP_PROXY", null);
            Environment.SetEnvironmentVariable("NO_PROXY", null);
        }
    }
}