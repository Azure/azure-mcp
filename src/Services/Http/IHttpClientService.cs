// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Services.Http;

/// <summary>
/// Service for providing configured HttpClient instances with centralized HTTP options and proxy support.
/// </summary>
public interface IHttpClientService
{
    /// <summary>
    /// Gets a configured HttpClient instance.
    /// </summary>
    /// <returns>A configured HttpClient instance.</returns>
    HttpClient GetHttpClient();

    /// <summary>
    /// Gets a configured HttpClient instance with a specific base address.
    /// </summary>
    /// <param name="baseAddress">The base address for the HttpClient.</param>
    /// <returns>A configured HttpClient instance with the specified base address.</returns>
    HttpClient GetHttpClient(Uri baseAddress);
}