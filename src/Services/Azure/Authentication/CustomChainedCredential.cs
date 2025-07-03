// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using Azure.Core;
using Azure.Identity;
using Azure.Identity.Broker;
using AzureMcp.Helpers;

namespace AzureMcp.Services.Azure.Authentication;

/// <summary>
/// A custom token credential that chains DefaultAzureCredential with a broker-enabled instance of
/// InteractiveBrowserCredential to provide a seamless authentication experience.
/// </summary>
/// <remarks>
/// This credential attempts authentication in the following order:
/// 1. DefaultAzureCredential chain (environment variables, managed identity, CLI, etc.)
/// 2. Interactive browser authentication with Identity Broker (supporting Windows Hello, biometrics, etc.)
/// </remarks>
public class CustomChainedCredential(string? tenantId = null) : TokenCredential
{
    private TokenCredential? _credential;

    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        _credential ??= CreateCredential(tenantId);
        return _credential.GetToken(requestContext, cancellationToken);
    }

    public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        _credential ??= CreateCredential(tenantId);
        return _credential.GetTokenAsync(requestContext, cancellationToken);
    }

    private const string AuthenticationRecordEnvVarName = "AZURE_MCP_AUTHENTICATION_RECORD";
    private const string BrowserAuthenticationTimeoutEnvVarName = "AZURE_MCP_BROWSER_AUTH_TIMEOUT_SECONDS";
    private const string OnlyUseBrokerCredentialEnvVarName = "AZURE_MCP_ONLY_USE_BROKER_CREDENTIAL";
    private const string ClientIdEnvVarName = "AZURE_MCP_CLIENT_ID";
    private const string IncludeProductionCredentialEnvVarName = "AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS";

    private static bool ShouldUseOnlyBrokerCredential()
    {
        return EnvironmentHelpers.GetEnvironmentVariableAsBool(OnlyUseBrokerCredentialEnvVarName);
    }

    private static TokenCredential CreateCredential(string? tenantId)
    {
        string? authRecordJson = Environment.GetEnvironmentVariable(AuthenticationRecordEnvVarName);
        AuthenticationRecord? authRecord = null;
        if (!string.IsNullOrEmpty(authRecordJson))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(authRecordJson);
            using MemoryStream authRecordStream = new(bytes);
            authRecord = AuthenticationRecord.Deserialize(authRecordStream);
        }

        if (ShouldUseOnlyBrokerCredential())
        {
            return CreateBrowserCredential(tenantId, authRecord);
        }

        var creds = new List<TokenCredential>();
        var vsCodeCred = CreateVsCodeBrokerCredential(tenantId);
        bool isVsCodeContext = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VSCODE_PID"));

        if (isVsCodeContext && vsCodeCred != null)
        {
            creds.Add(vsCodeCred);
            creds.Add(CreateDefaultCredential(tenantId));
        }
        else
        {
            creds.Add(CreateDefaultCredential(tenantId));
            if (vsCodeCred != null)
            {
                creds.Add(vsCodeCred);
            }
        }
        creds.Add(CreateBrowserCredential(tenantId, authRecord));
        return new ChainedTokenCredential(creds.ToArray());
    }

    private static string TokenCacheName = "azure-mcp-msal.cache";

    private static TokenCredential CreateBrowserCredential(string? tenantId, AuthenticationRecord? authRecord)
    {
        string? clientId = Environment.GetEnvironmentVariable(ClientIdEnvVarName);

        IntPtr handle = WindowHandleProvider.GetWindowHandle();

        InteractiveBrowserCredentialBrokerOptions brokerOptions = new(handle)
        {
            UseDefaultBrokerAccount = !ShouldUseOnlyBrokerCredential() && authRecord is null,
            TenantId = string.IsNullOrEmpty(tenantId) ? null : tenantId,
            AuthenticationRecord = authRecord,
            TokenCachePersistenceOptions = new TokenCachePersistenceOptions()
            {
                Name = TokenCacheName,
            }
        };

        if (clientId is not null)
        {
            brokerOptions.ClientId = clientId;
        }

        var browserCredential = new InteractiveBrowserCredential(brokerOptions);

        // Check for timeout value in the environment variable
        string? timeoutValue = Environment.GetEnvironmentVariable(BrowserAuthenticationTimeoutEnvVarName);
        int timeoutSeconds = 300; // Default to 300 seconds (5 minutes)
        if (!string.IsNullOrEmpty(timeoutValue) && int.TryParse(timeoutValue, out int parsedTimeout) && parsedTimeout > 0)
        {
            timeoutSeconds = parsedTimeout;
        }
        return new TimeoutTokenCredential(browserCredential, TimeSpan.FromSeconds(timeoutSeconds));
    }

    private static DefaultAzureCredential CreateDefaultCredential(string? tenantId)
    {
        var includeProdCreds = EnvironmentHelpers.GetEnvironmentVariableAsBool(IncludeProductionCredentialEnvVarName);

        var defaultCredentialOptions = new DefaultAzureCredentialOptions
        {
            ExcludeWorkloadIdentityCredential = !includeProdCreds,
            ExcludeManagedIdentityCredential = !includeProdCreds
        };

        if (!string.IsNullOrEmpty(tenantId))
        {
            defaultCredentialOptions.TenantId = tenantId;
        }

        return new DefaultAzureCredential(defaultCredentialOptions);
    }

    private static TokenCredential? CreateVsCodeBrokerCredential(string? tenantId)
    {
        const string vsCodeClientId = "aebc6443-996d-45c2-90f0-388ff96faa56";
        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string authRecordPath = Path.Combine(userProfile, ".azure", "ms-azuretools.vscode-azureresourcegroups", "authRecord.json");
        if (!File.Exists(authRecordPath))
        {
            // Try .Azure if .azure is not present
            authRecordPath = Path.Combine(userProfile, ".Azure", "ms-azuretools.vscode-azureresourcegroups", "authRecord.json");
            if (!File.Exists(authRecordPath))
            {
                return null;
            }
        }

        AuthenticationRecord? authRecord;
        try
        {
            using var stream = File.OpenRead(authRecordPath);
            authRecord = AuthenticationRecord.Deserialize(stream);
        }
        catch
        {
            // Deserialization failed
            return null;
        }

        if (authRecord is null)
            return null;

        // Validate client ID
        if (!string.Equals(authRecord.ClientId, vsCodeClientId, StringComparison.OrdinalIgnoreCase))
            return null;

        // Validate tenant ID if present
        if (!string.IsNullOrEmpty(authRecord.TenantId) && !IsValidTenantId(authRecord.TenantId))
            return null;

        // Prefer explicit tenantId, else use from auth record
        string? effectiveTenantId = !string.IsNullOrEmpty(tenantId)
            ? tenantId
            : authRecord.TenantId;

        var options = new InteractiveBrowserCredentialBrokerOptions(0)
        {
            ClientId = vsCodeClientId,
            TenantId = effectiveTenantId,
            AuthenticationRecord = authRecord
        };

        return new InteractiveBrowserCredential(options);
    }

    private static bool IsValidTenantId(string tenantId)
    {
        foreach (char c in tenantId)
        {
            if (!IsValidTenantCharacter(c))
            {
                return false;
            }
        }
        return true;
    }

    // Helper to validate tenant ID: only hex digits and dashes allowed
    private static bool IsValidTenantCharacter(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || (c == '.') || (c == '-');
    }

}
