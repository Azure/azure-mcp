// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Net.Http.Headers;
using Azure.Core;
using AzureMcp.Areas.Extension.Models;
using AzureMcp.Services.Azure.Authentication;

namespace AzureMcp.Areas.Extension.Services;

public sealed class ExtensionService(HttpClient httpClient) : IExtensionService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<string> GenerateAzCommandAsync(string intent)
    {
        var requestUri = $"https://azclis-copilot-apim-prod-eus.azure-api.net/azcli/copilot";
        var payload = new GenerateAzCommandPayload()
        {
            Question = intent,
            EnableParameterInjection = true
        };
        var credential = new CustomChainedCredential();
        var token = await credential.GetTokenAsync(
            new TokenRequestContext(["https://management.core.windows.net/.default"]),
            CancellationToken.None
        );
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        httpRequest.Content = new StringContent(
            JsonSerializer.Serialize(payload, JsonSourceGenerationContext.Default.GenerateAzCommandPayload),
            System.Text.Encoding.UTF8, "application/json");
        using var response = await _httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
