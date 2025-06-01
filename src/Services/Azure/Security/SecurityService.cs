// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Azure;
using Azure.Core;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Services.Azure.Security;

[JsonSerializable(typeof(ResourceGraphRequest))]
internal partial class SecurityJsonContext : JsonSerializerContext
{
}

internal record ResourceGraphRequest(string Query, string[] Subscriptions);

/// <summary>
/// Service for Azure Security operations using Resource Graph API
/// </summary>
public class SecurityService(
    ITenantService tenantService,
    ILogger<SecurityService> logger) : BaseAzureService(tenantService), ISecurityService
{
    private readonly ILogger<SecurityService> _logger = logger;
    private const string ResourceGraphEndpoint = "https://management.azure.com/providers/Microsoft.ResourceGraph/resources";

    /// <inheritdoc />
    public async Task<List<JsonElement>> GetAlertAsync(
        string subscriptionId,
        string systemAlertId,
        string? resourceGroupName = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var credential = await GetCredential();

            // Build the Resource Graph query for security alerts
            var query = BuildSecurityAlertQuery(subscriptionId, systemAlertId, resourceGroupName);
            
            var requestBody = new ResourceGraphRequest(query, [subscriptionId]);

            _logger.LogDebug("Executing Resource Graph query: {Query}", query);

            // Create HTTP client with authentication
            using var httpClient = new HttpClient();
            var accessToken = await credential.GetTokenAsync(
                new TokenRequestContext(new[] { "https://management.azure.com/.default" }),
                cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Token);

            var json = JsonSerializer.Serialize(requestBody, SecurityJsonContext.Default.ResourceGraphRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{ResourceGraphEndpoint}?api-version=2021-03-01", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var jsonDocument = JsonDocument.Parse(responseContent);

            if (!jsonDocument.RootElement.TryGetProperty("data", out var dataElement) || dataElement.GetArrayLength() == 0)
            {
                return new List<JsonElement>();
            }

            // Return all results as JsonElements for maximum flexibility
            var results = new List<JsonElement>();
            foreach (var alertData in dataElement.EnumerateArray())
            {
                results.Add(alertData.Clone());
            }

            return results;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed while getting security alert {AlertId}: {Error}", systemAlertId, ex.Message);
            throw;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            _logger.LogWarning("Security alert {AlertId} not found: {Error}", systemAlertId, ex.Message);
            return new List<JsonElement>();
        }
        catch (RequestFailedException ex)
        {
            _logger.LogError(ex, "Azure request failed while getting security alert {AlertId}: {Error}", systemAlertId, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting security alert {AlertId}", systemAlertId);
            throw;
        }
    }    /// <summary>
    /// Builds a Resource Graph query to find security alerts
    /// </summary>
    private static string BuildSecurityAlertQuery(string subscriptionId, string systemAlertId, string? resourceGroupName)
    {
        var query = """
            securityresources
            | where type =~ 'microsoft.security/locations/alerts'
            """;

        // Add subscription filter
        query += $"""

            | where subscriptionId == "{subscriptionId}"
            """;

        // Add resource group filter if specified
        if (!string.IsNullOrEmpty(resourceGroupName))
        {
            query += $"""

                | where resourceGroup == "{resourceGroupName}"
                """;
        }

        // Add alert ID filter - check both name and system alert ID
        query += $"""

            | where name == "{systemAlertId}" or properties.systemAlertId == "{systemAlertId}"
            | limit 1
            """;

        return query;
    }
}
