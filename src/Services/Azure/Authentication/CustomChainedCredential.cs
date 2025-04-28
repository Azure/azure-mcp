// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Identity;
using Azure.Identity.Broker;
using System.Runtime.InteropServices;
using System.Text;

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
    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        ChainedTokenCredential chainedCredential = CreateChainedCredentialAsync(tenantId).Result;
        return chainedCredential.GetToken(requestContext, cancellationToken);
    }

    public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        ChainedTokenCredential chainedCredential = CreateChainedCredentialAsync(tenantId).Result;
        return ValueTask.FromResult(chainedCredential.GetToken(requestContext, cancellationToken));
    }

    private static async Task<ChainedTokenCredential> CreateChainedCredentialAsync(string? tenantId)
    {
        string? authRecordJson = Environment.GetEnvironmentVariable("AZURE_MCP_AUTH_AUTHENTICATION_RECORD");
        AuthenticationRecord? authRecord = null;
        if (!string.IsNullOrEmpty(authRecordJson))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(authRecordJson);
            using MemoryStream authRecordStream = new MemoryStream(bytes);
            authRecord = await AuthenticationRecord.DeserializeAsync(authRecordStream);
        }

        string? useOnlyBrokerCredential = Environment.GetEnvironmentVariable("AZURE_MCP_USE_ONLY_BROKER_CREDENTIAL");
        if (useOnlyBrokerCredential == "true")
        {
            return new(CreateBrowserCredential(tenantId, authRecord));
        }
        else
        {
            return new(CreateDefaultCredential(tenantId), CreateBrowserCredential(tenantId, authRecord));
        }
    }

    private enum GetAncestorFlags
    {
        GetParent = 1,
        GetRoot = 2,
        /// <summary>
        /// Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.
        /// </summary>
        GetRootOwner = 3
    }

#if WINDOWS
    /// <summary>
    /// Retrieves the handle to the ancestor of the specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window whose ancestor is to be retrieved.
    /// If this parameter is the desktop window, the function returns NULL. </param>
    /// <param name="flags">The ancestor to be retrieved.</param>
    /// <returns>The return value is the handle to the ancestor window.</returns>
    [DllImport("user32.dll", ExactSpelling = true)]
    static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    public static IntPtr GetConsoleOrTerminalWindow()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        IntPtr handle = GetAncestor(consoleHandle, GetAncestorFlags.GetRootOwner);

        return handle;
    }
#else
   public static IntPtr GetConsoleOrTerminalWindow()
    {
        return IntPtr.Zero;
    } 
#endif

    private static string TokenCacheName = "azure-mcp-msal.cache";

    private static InteractiveBrowserCredential CreateBrowserCredential(string? tenantId, AuthenticationRecord? authRecord)
    {
        string? clientId = Environment.GetEnvironmentVariable("AZURE_MCP_AUTH_CLIENT_ID");
        string? useOnlyBrokerCredential = Environment.GetEnvironmentVariable("AZURE_MCP_USE_ONLY_BROKER_CREDENTIAL");

        IntPtr handle = GetConsoleOrTerminalWindow();

        return new(new InteractiveBrowserCredentialBrokerOptions(handle)
        {
            UseDefaultBrokerAccount = useOnlyBrokerCredential != "true" && authRecord is null,
            ClientId = clientId,
            TenantId = tenantId,
            AuthenticationRecord = authRecord,
            TokenCachePersistenceOptions = new TokenCachePersistenceOptions()
            {
                Name = TokenCacheName,
            }
        });
    }

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