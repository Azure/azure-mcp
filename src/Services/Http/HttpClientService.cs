// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;

namespace AzureMcp.Services.Http;

/// <summary>
/// Service for providing configured HttpClient instances with centralized HTTP options and proxy support.
/// Supports proxy configuration through environment variables: ALL_PROXY, HTTP_PROXY, HTTPS_PROXY, and NO_PROXY.
/// </summary>
public class HttpClientService : IHttpClientService
{
    private readonly ILogger<HttpClientService> _logger;
    private readonly HttpClientHandler _handler;

    public HttpClientService(ILogger<HttpClientService> logger)
    {
        _logger = logger;
        _handler = CreateConfiguredHandler();
    }

    /// <summary>
    /// Gets a configured HttpClient instance.
    /// </summary>
    /// <returns>A configured HttpClient instance.</returns>
    public HttpClient GetHttpClient()
    {
        return new HttpClient(_handler, disposeHandler: false);
    }

    /// <summary>
    /// Gets a configured HttpClient instance with a specific base address.
    /// </summary>
    /// <param name="baseAddress">The base address for the HttpClient.</param>
    /// <returns>A configured HttpClient instance with the specified base address.</returns>
    public HttpClient GetHttpClient(Uri baseAddress)
    {
        var client = GetHttpClient();
        client.BaseAddress = baseAddress;
        return client;
    }

    private HttpClientHandler CreateConfiguredHandler()
    {
        var handler = new HttpClientHandler();
        
        ConfigureProxy(handler);
        
        return handler;
    }

    private void ConfigureProxy(HttpClientHandler handler)
    {
        var allProxy = Environment.GetEnvironmentVariable("ALL_PROXY");
        var httpProxy = Environment.GetEnvironmentVariable("HTTP_PROXY");
        var httpsProxy = Environment.GetEnvironmentVariable("HTTPS_PROXY");
        var noProxy = Environment.GetEnvironmentVariable("NO_PROXY");

        // Check if any proxy environment variables are set
        if (string.IsNullOrEmpty(allProxy) && string.IsNullOrEmpty(httpProxy) && string.IsNullOrEmpty(httpsProxy))
        {
            // No proxy configuration found, use system default
            _logger.LogDebug("No proxy environment variables found, using system default proxy settings");
            return;
        }

        try
        {
            var proxy = new WebProxy();
            
            // Configure proxy addresses
            if (!string.IsNullOrEmpty(allProxy))
            {
                proxy.Address = new Uri(allProxy);
                _logger.LogDebug("Configured proxy from ALL_PROXY: {ProxyAddress}", allProxy);
            }
            else
            {
                // Use HTTP_PROXY for HTTP and HTTPS_PROXY for HTTPS if available
                var proxyUri = !string.IsNullOrEmpty(httpsProxy) ? httpsProxy : httpProxy;
                if (!string.IsNullOrEmpty(proxyUri))
                {
                    proxy.Address = new Uri(proxyUri);
                    _logger.LogDebug("Configured proxy: {ProxyAddress}", proxyUri);
                }
            }

            // Configure no-proxy list
            if (!string.IsNullOrEmpty(noProxy))
            {
                var bypasses = noProxy.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                     .Select(s => s.Trim())
                                     .Where(s => !string.IsNullOrEmpty(s))
                                     .Where(s => !s.Contains('*')) // Filter out wildcard patterns which aren't supported
                                     .ToArray();
                if (bypasses.Length > 0)
                {
                    proxy.BypassList = bypasses;
                    _logger.LogDebug("Configured proxy bypass list: {BypassList}", string.Join(", ", bypasses));
                }
            }

            handler.Proxy = proxy;
            handler.UseProxy = true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to configure proxy settings, using system default");
        }
    }
}