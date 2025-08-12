// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Net.Http.Headers;
using AzureMcp.Core.Services.Azure.Authentication;
using AzureMcp.Extension.Models;

namespace AzureMcp.Extension.Services;

public interface IAzCommandCopilotService
{
    /// <summary>
    /// Sends a request to the REST API to generate an Az CLI command and returns the response body as a string.
    /// </summary>
    /// <param name="intent">The user intent for the Azure CLI command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response body from the API.</returns>
    Task<string> GenerateAzCliCommandAsync(string intent, CancellationToken cancellationToken = default);
}

public sealed class AzCommandCopilotService : IAzCommandCopilotService
{
    private readonly HttpClient _httpClient;

    public AzCommandCopilotService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GenerateAzCliCommandAsync(string intent, CancellationToken cancellationToken = default)
    {
        var requestUri = $"https://azclis-copilot-apim-prod-eus.azure-api.net/azcli/copilot";
        var payload = new GenerateAzCommandPayload()
        {
            Question = intent,
            EnableParameterInjection = true
        };
        var credential = new CustomChainedCredential();
        var token = await credential.GetTokenAsync(
            new Azure.Core.TokenRequestContext(["todo_define_scope"]),
            cancellationToken
        );
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        httpRequest.Content = new StringContent(
            JsonSerializer.Serialize(payload, ExtensionJsonContext.Default.GenerateAzCommandPayload),
            System.Text.Encoding.UTF8,
            "application/json"
        );
        using var httpResponse = await _httpClient.SendAsync(httpRequest);
        var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Request failed with status code {(int)httpResponse.StatusCode} ({httpResponse.StatusCode}): {responseContent}");
        }
        return responseContent;
    }
}
