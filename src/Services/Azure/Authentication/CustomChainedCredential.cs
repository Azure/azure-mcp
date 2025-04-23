// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Identity;
using Azure.Identity.Broker;

namespace AzureMcp.Services.Azure.Authentication;

/// <summary>
/// A custom token credential that chains the Identity Broker-enabled InteractiveBrowserCredential 
/// with DefaultAzureCredential to provide a seamless authentication experience.
/// </summary>
/// <remarks>
/// This credential attempts authentication in the following order:
/// 1. Interactive browser authentication with Identity Broker (supporting Windows Hello, biometrics, etc.)
/// 2. DefaultAzureCredential chain (environment variables, managed identity, CLI, etc.)
/// </remarks>
public class CustomChainedCredential(string? tenantId = null) : TokenCredential
{
    private readonly ChainedTokenCredential _chainedCredential = CreateChainedCredential(tenantId);

    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken) =>
        _chainedCredential.GetToken(requestContext, cancellationToken);

    public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken) =>
        _chainedCredential.GetTokenAsync(requestContext, cancellationToken);

    private static ChainedTokenCredential CreateChainedCredential(string? tenantId) =>
        new(CreateDefaultCredential(tenantId), CreateBrowserCredential());

    private static InteractiveBrowserCredential CreateBrowserCredential() =>
        new(new InteractiveBrowserCredentialBrokerOptions(IntPtr.Zero)
        {
            UseDefaultBrokerAccount = true
        });

    private static DefaultAzureCredential CreateDefaultCredential(string? tenantId)
    {
        var includeProdCreds =
            Environment.GetEnvironmentVariable("AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS")?.Equals("true", StringComparison.OrdinalIgnoreCase) == true;

        return new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            TenantId = string.IsNullOrEmpty(tenantId) ? null : tenantId,
            ExcludeWorkloadIdentityCredential = !includeProdCreds,
            ExcludeManagedIdentityCredential = !includeProdCreds
        });
    }
}