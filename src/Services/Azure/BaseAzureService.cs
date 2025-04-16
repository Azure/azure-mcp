// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using AzureMcp.Arguments;
using AzureMcp.Services.Azure.Authentication;
using AzureMcp.Services.Interfaces;
using System.Reflection;
using System.Runtime.Versioning;

namespace AzureMcp.Services.Azure;

public abstract class BaseAzureService(ITenantService? tenantService = null)
{
    private static readonly UserAgentPolicy SharedUserAgentPolicy;
    internal static readonly string DefaultUserAgent;

    private CustomChainedCredential? _credential;
    private string? _lastTenantId;
    private ArmClient? _armClient;
    private string? _lastArmClientTenantId;
    private RetryPolicyArguments? _lastRetryPolicy;
    private readonly ITenantService? _tenantService = tenantService;

    static BaseAzureService()
    {
        var assembly = typeof(BaseAzureService).Assembly;
        var version = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
        var framework = assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
        var platform = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

        DefaultUserAgent = $"azmcp/{version} ({framework}; {platform})";
        SharedUserAgentPolicy = new UserAgentPolicy(DefaultUserAgent);
    }

    protected string UserAgent { get; } = DefaultUserAgent;

    protected async Task<string?> ResolveTenantIdAsync(string? tenant)
    {
        if (tenant == null || _tenantService == null) return tenant;
        return await _tenantService.GetTenantId(tenant);
    }

    protected TokenCredential GetCredential(string? tenant = null)
    {
        var tenantId = string.IsNullOrEmpty(tenant) ? null : Task.Run(() => ResolveTenantIdAsync(tenant)).GetAwaiter().GetResult();

        // Return cached credential if it exists and tenant ID hasn't changed
        if (_credential != null && _lastTenantId == tenantId)
        {
            return _credential;
        }

        try
        {
            _credential = new CustomChainedCredential(tenantId);
            _lastTenantId = tenantId;

            return _credential;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get credential: {ex.Message}", ex);
        }
    }

    protected static T AddDefaultPolicies<T>(T clientOptions) where T : ClientOptions
    {
        clientOptions.AddPolicy(SharedUserAgentPolicy, HttpPipelinePosition.BeforeTransport);

        return clientOptions;
    }

    /// <summary>
    /// Creates an Azure Resource Manager client with optional retry policy
    /// </summary>
    /// <param name="tenant">Optional Azure tenant ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    protected async Task<ArmClient> CreateArmClientAsync(string? tenant = null, RetryPolicyArguments? retryPolicy = null)
    {
        var tenantId = await ResolveTenantIdAsync(tenant);

        // Return cached client if parameters match
        if (_armClient != null &&
            _lastArmClientTenantId == tenantId &&
            RetryPolicyArguments.AreEqual(_lastRetryPolicy, retryPolicy))
        {
            return _armClient;
        }

        try
        {
            var credential = GetCredential(tenantId);
            var options = AddDefaultPolicies(new ArmClientOptions());

            // Configure retry policy if provided
            if (retryPolicy != null)
            {
                options.Retry.MaxRetries = retryPolicy.MaxRetries;
                options.Retry.Mode = retryPolicy.Mode;
                options.Retry.Delay = TimeSpan.FromSeconds(retryPolicy.DelaySeconds);
                options.Retry.MaxDelay = TimeSpan.FromSeconds(retryPolicy.MaxDelaySeconds);
                options.Retry.NetworkTimeout = TimeSpan.FromSeconds(retryPolicy.NetworkTimeoutSeconds);
            }

            _armClient = new ArmClient(credential, default, options);
            _lastArmClientTenantId = tenantId;
            _lastRetryPolicy = retryPolicy;

            return _armClient;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create ARM client: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Validates that the provided parameters are not null or empty
    /// </summary>
    /// <param name="parameters">Array of parameters to validate</param>
    /// <exception cref="ArgumentException">Thrown when any parameter is null or empty</exception>
    protected static void ValidateRequiredParameters(params string?[] parameters)
    {
        foreach (var param in parameters)
        {
            ArgumentException.ThrowIfNullOrEmpty(param);
        }
    }
}